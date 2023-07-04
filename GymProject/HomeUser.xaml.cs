using FireSharp.Interfaces;
using FireSharp.Config;
using FireSharp.Response;
using Firebase;
using FireSharp;

namespace GymProject;

public partial class HomeUser : ContentPage
{
    IFirebaseClient client=null;
    Utente utente= null;
    private List<SchedaAllenamento> schede=null;

    public HomeUser(IFirebaseClient clientp, Utente utentep)
	{
        client = clientp;
        utente = utentep;
        String nome = utente.Nome;  
        InitializeComponent();
        titolo.Text = "Benvenuto " + nome;
    }
    private async void PrenotaAttrezzatureButton_Clicked(object sender, EventArgs e)
    {
        // Naviga alla pagina di prenotazione attrezzature
        schede = await GetSchedeByEmail(utente.Email);
        if (schede != null && schede.Count > 0)
        {
            await Navigation.PushAsync(new PrenotaAttrezzature(client, utente, schede));
        }
        else
        {
            await DisplayAlert("Errore", "L'utente non ha delle schede da prenotare", "OK");
        }


    }

    public async Task<List<SchedaAllenamento>> GetSchedeByEmail(string email)
    {
        try
        {
            // Crea un riferimento al percorso delle schedeAllenamento
            string path = "schedeAllenamento";

            // Ottieni la risposta dal percorso specificato
            FirebaseResponse response = await client.GetAsync(path);

            if (response != null && response.Body != "null")
            {
                // Deserializza i dati della risposta in un dizionario
                Dictionary<string, SchedaAllenamento> schedeDict = response.ResultAs<Dictionary<string, SchedaAllenamento>>();

                // Filtra le schede per email
                List<SchedaAllenamento> schede = schedeDict.Values
                    .Where(scheda => scheda.Nome.StartsWith("scheda/" + email))
                    .ToList();

                return schede;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Si è verificato un errore durante il recupero delle schede allenamento dal database: " + ex.Message);
        }

        return null; // Nessuna scheda allenamento trovata o errore durante il recupero
    }

    private void RichiediSchedaButton_Clicked(object sender, EventArgs e)
    {
        // Naviga alla pagina di richiesta scheda
        Navigation.PushAsync(new RichiediSchedaPage(client,utente));
    }
}
