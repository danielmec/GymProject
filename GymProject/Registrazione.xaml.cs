using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Firebase;
using FireSharp;


namespace GymProject;

public partial class Registrazione : ContentPage
{
   
    public Registrazione()
	{
		InitializeComponent();


    }

    IFirebaseConfig config = new FirebaseConfig
    {
        AuthSecret = "8nuzPvlSodfC9nbeg5qXCV2qOKsjYhPSawwBPg0N",
        BasePath = "https://fitflow-e460b-default-rtdb.europe-west1.firebasedatabase.app/"
    };

    // Crea un'istanza di FirebaseClient
    IFirebaseClient client;

  async  private void OnRegistraClicked(object sender, EventArgs e)
    {
        string nome = NomeEntry.Text;
        string cognome = CognomeEntry.Text;
        string dataNascita = DataNascitaEntry.Text;
        string email = EmailEntry.Text;
        string password = PasswordEntry.Text;
        string confermaPassword = ConfermaPasswordEntry.Text;

        if(password.Equals(confermaPassword))
        {
            try
            {
                client = new FirebaseClient(config);


                Utente utente = new Utente(nome, cognome, dataNascita, email, password);

                // Inserisci i dati nel percorso "users" con una chiave generata automaticamente
                PushResponse response = await client.PushAsync("users", utente);
                await DisplayAlert("Successo", "L'utente è stato inserito", "OK");

            }
            catch (Exception ex)
            {
                await this.DisplayAlert("Errore", "Si è verificato un errore: " + ex.Message, "OK");
            }
        }
        else
        {
            await DisplayAlert("Errore", "Le password non combaciano", "OK");
        }

        
    }


}
