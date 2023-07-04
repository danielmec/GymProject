using FireSharp.Interfaces;
using FireSharp.Config;
using FireSharp.Response;
using Firebase;
using FireSharp;

namespace GymProject;

public partial class RegistrazioneM : ContentPage
{
    IFirebaseClient client;
    public RegistrazioneM(String tipo, IFirebaseClient clientp)
	{
		InitializeComponent();
        DisplayAlert("Successo", "L'utente è di tipo: " + tipo, "OK");
        client = clientp;
    }

   async private void OnRegistraMembroClicked(object sender, EventArgs e)
    {
        string nome = NomeEntry.Text;
        string cognome = CognomeEntry.Text;
        string dataNascita = DataNascitaEntry.Text;
        string email = EmailEntry.Text;
        string password = PasswordEntry.Text;
        string tipoMembro = TipoMembroPicker.SelectedItem.ToString();

        // Effettua le validazioni dei dati inseriti, ad esempio controlla se i campi sono vuoti o se la password coincide con la conferma password.

        // Se i dati sono validi, puoi salvare l'utente nel tuo sistema o eseguire altre azioni necessarie.

        try
        {
            Membro membro = new Membro(nome, cognome, dataNascita, email, password,tipoMembro);

            // Inserisci i dati nel percorso "users" con una chiave generata automaticamente
            PushResponse response = await client.PushAsync("membri", membro);
            await DisplayAlert("Successo", "Il membro è stato inserito", "OK");

        }
        catch (Exception ex)
        {
            await this.DisplayAlert("Errore", "Si è verificato un errore: " + ex.Message, "OK");
        }      

        // Esegui la navigazione a una pagina successiva, ad esempio la pagina di conferma di registrazione.
        
    }

 
}
