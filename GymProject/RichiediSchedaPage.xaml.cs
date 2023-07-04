using FireSharp.Interfaces;
using FireSharp.Response;
using FireSharp.Config;
using Firebase;
using FireSharp;


namespace GymProject;

public partial class RichiediSchedaPage : ContentPage
{
	Utente utente;
    IFirebaseClient client = null;
    public RichiediSchedaPage(IFirebaseClient clientp, Utente u)
	{
		utente = u;
        client = clientp;
		InitializeComponent();
		EmailEntry.Text = utente.Email;

	}


    async private void OnInviaClicked(object sender, EventArgs e)
    {

        int eta = int.Parse(EtaEntry.Text);
        int peso = int.Parse(PesoEntry.Text);
        int altezza = int.Parse(AltezzaEntry.Text);
        // Effettua le validazioni dei dati inseriti, ad esempio controlla se i campi sono vuoti o se la password coincide con la conferma password.

        // Se i dati sono validi, puoi salvare l'utente nel tuo sistema o eseguire altre azioni necessarie.

        try
        {
            Richiesta richiesta = new Richiesta(TipoEntry.Text, DescrizioneEntry.Text, eta, utente.Email, peso, altezza);


            // Inserisci i dati nel percorso "users" con una chiave generata automaticamente
            PushResponse response = await client.PushAsync("richieste", richiesta);
            await DisplayAlert("Successo", "La richiesta è stata inoltrata", "OK");

        }
        catch (Exception ex)
        {
            await this.DisplayAlert("Errore", "Si è verificato un errore: " + ex.Message, "OK");
        }

        // Esegui la navigazione a una pagina successiva, ad esempio la pagina di conferma di registrazione.

    }
}
