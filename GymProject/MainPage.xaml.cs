
using Microsoft.Maui.Controls;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Firebase;
using FireSharp;


namespace GymProject;



public partial class MainPage : ContentPage
    {

    public MainPage()
        {
            InitializeComponent();
         
            LoginButton.Clicked += OnLoginButtonClicked;
        }

        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "8nuzPvlSodfC9nbeg5qXCV2qOKsjYhPSawwBPg0N",
            BasePath = "https://fitflow-e460b-default-rtdb.europe-west1.firebasedatabase.app/"
        };

        // Crea un'istanza di FirebaseClient
        IFirebaseClient client;

   


    async private void OnLoginButtonClicked(object sender, EventArgs e)
        {
             string email = EntryEmail.Text;
             string pass = EntryPassword.Text;

             String tipoUtente = null;//U-user; Membro-M; Admin-A

            try
            {
                client = new FirebaseClient(config);
                Utente userExists = null;
                userExists = await VerifyUser(email, pass);

                if(userExists!=null)
                {
                    tipoUtente = "U";
                    await Navigation.PushAsync(new HomeUser(client,userExists));
                }   
                else
                {
                    bool Admin = await VerifyAdmin(email,pass);

                    if(Admin==true)
                    {
                        tipoUtente = "A";
                        await Navigation.PushAsync(new RegistrazioneM(tipoUtente, client));
                    }
                    else
                    {
                        tipoUtente = "M";
                        (bool membroExists, String tipoMembro) = await VerifyMembro(email, pass);
                        if(membroExists==true)
                        {
                            if(tipoMembro=="Personal Trainer")
                            {
                                List<Richiesta> richieste = await GetRichiesteFromDatabase();
                                await Navigation.PushAsync(new RichiesteSchede(richieste,client));
                             }else
                            {
                                await DisplayAlert("Info", "In arrivo le pagine per il Receptionist", "OK");
                            }
                            
                        }
                        else
                        {
                            await DisplayAlert("Error", "Nessun utente con queste credenziali", "OK");
                        }
                    }
                }
                
            }
            catch (Exception ex)
            {
                await this.DisplayAlert("Errore", "Si è verificato un errore durante il login: " + ex.Message, "OK");
            }




         }

    public async Task<List<Richiesta>> GetRichiesteFromDatabase()
    {
        List<Richiesta> richieste = new List<Richiesta>();

        try
        {
            FirebaseResponse response = await client.GetAsync("richieste");

            if (response != null && response.Body != "null")
            {
                Dictionary<string, Richiesta> richiesteDict = response.ResultAs<Dictionary<string, Richiesta>>();

                foreach (var richiesta in richiesteDict.Values)
                {
                    richieste.Add(richiesta);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Si è verificato un errore durante il recupero delle richieste dal database: " + ex.Message);
        }

        return richieste;
    }

    async private Task<bool> VerifyAdmin (String email,String password)
    {

        // Crea un riferimento al percorso del nodo "users"
        FirebaseResponse response = await client.GetAsync("admin");

        // Controlla se la risposta è valida e contiene dati
        if (response != null && response.Body != "null")
        {
            // Deserializza i dati della risposta in un dizionario
            Dictionary<string, Utente> users = response.ResultAs<Dictionary<string, Utente>>();

            // Verifica se esiste un admin con la combinazione email/password fornita
            foreach (var user in users)
            {
                if (user.Value.Email == email && user.Value.Password == password)
                {
                    return true; // Utente trovato
                }
            }
        }

        return false; // Utente non trovato
    }

    private void OnRegistrationButtonClicked(object sender, EventArgs e)
        {

            Navigation.PushAsync(new Registrazione());
        }


    public async Task<Utente> VerifyUser(string email, string password)
    {
        // Crea un riferimento al percorso del nodo "users"
        FirebaseResponse response = await client.GetAsync("users");

        // Controlla se la risposta è valida e contiene dati
        if (response != null && response.Body != "null")
        {
            // Deserializza i dati della risposta in un dizionario
            Dictionary<string, Utente> users = response.ResultAs<Dictionary<string, Utente>>();

            // Verifica se esiste un utente con la combinazione email/password fornita
            foreach (var user in users)
            {
                if (user.Value.Email == email && user.Value.Password == password)
                {
                    return user.Value; // Utente trovato
                }
            }
        }

        return null; // Utente non trovato
    }

    public async Task<(bool,String)> VerifyMembro(string email, string password)
    {
        // Crea un riferimento al percorso del nodo "users"
        FirebaseResponse response = await client.GetAsync("membri");
        String tipo = null;
        // Controlla se la risposta è valida e contiene dati
        if (response != null && response.Body != "null")
        {
            // Deserializza i dati della risposta in un dizionario
            Dictionary<string, Membro> users = response.ResultAs<Dictionary<string, Membro>>();

            // Verifica se esiste un utente con la combinazione email/password fornita
            foreach (var user in users)
            {
                if (user.Value.Email == email && user.Value.Password == password)
                {
                    tipo = user.Value.TipoMembro;
                    return (true,tipo); // Utente trovato
                }
            }
        }

        return (false,null); // Utente non trovato
    }

}



