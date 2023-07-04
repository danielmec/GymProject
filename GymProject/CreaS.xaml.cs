using FireSharp.Interfaces;
using FireSharp.Config;
using FireSharp.Response;
using Firebase;
using FireSharp;

namespace GymProject;

public partial class CreaS : ContentPage
{
    private List<Esercizio> esercizi;
    private SchedaAllenamento scheda;
    private String Email;
    IFirebaseClient client;
    public CreaS(IFirebaseClient clientp, String email)
	{
		InitializeComponent();
        esercizi = new List<Esercizio>();
        client = clientp;
        Email = email;
    }

  async  private void AggiungiEsercizio_Clicked(object sender, EventArgs e)
    {
        // Ottenere i valori
        string tipoEsercizioSelezionato = TipoEsercizioPicker.SelectedItem as string;
        int numeroRipetizioni = Convert.ToInt32(RipetizioniLabel.Text);
        int durataSerie = Convert.ToInt32(DurataSerieLabel.Text);
        int tempoS = Convert.ToInt32(TempoSerie.Text);
        int durataR = Convert.ToInt32(DurataRecupero.Text);
        string priorita = Priorita.SelectedItem as string;

        // Creare un'istanza del tipo di esercizio selezionato
        TipoEsercizio tipoEsercizio = new TipoEsercizio(tipoEsercizioSelezionato, "");

        // Creare un'istanza dell'esercizio utilizzando il tipo di esercizio selezionato
        Esercizio esercizio = new Esercizio(tipoEsercizio,numeroRipetizioni, durataSerie, tempoS, durataR,priorita);
        esercizi.Add(esercizio);
        await DisplayAlert("Informazione", "L'esercizio è stato inserito", "OK");

    }

    private async void VediEsercizi_Clicked(object sender, EventArgs e)
    {
        
        // Verifica se la lista degli esercizi è valida
        if (esercizi != null && esercizi.Count > 0)
        {
            // Naviga alla pagina VisualizzaEsercizi passando la lista degli esercizi come parametro
            await Navigation.PushAsync(new VisualizzaEsercizi(esercizi));
        }
        else
        {
            await DisplayAlert("Errore", "Nessun esercizio disponibile", "OK");
        }
    }


    async private void CreaScheda_Clicked(object sender, EventArgs e)
    {
        try
        {

            string schedaId = "scheda/"+Email;

            scheda = new SchedaAllenamento(schedaId, esercizi);
       
            // Inserisci i dati 
            PushResponse response = await client.PushAsync("schedeAllenamento", scheda);
            await DisplayAlert("Successo", "La scheda è stata inserita", "OK");

        }
        catch (Exception ex)
        {
            await this.DisplayAlert("Errore", "Si è verificato un errore: " + ex.Message, "OK");
        }

        //elimina richiesta
        bool eliminazioneRiuscita = await DeleteRichiestaByEmail(Email);

        if (eliminazioneRiuscita)
        {
            // La richiesta è stata eliminata con successo
            Console.WriteLine("La richiesta con l'email " + Email + " è stata eliminata.");
        }
        else
        {
            // La richiesta non è stata trovata o l'eliminazione è fallita
            Console.WriteLine("Impossibile eliminare la richiesta con l'email " + Email + ".");
        }
    }

    //per eliminare la richiesta in seguito alla creazione della scheda
    public async Task<bool> DeleteRichiestaByEmail(string email)
    {
        try
        {
            // Ottieni tutte le richieste dal nodo "richieste"
            FirebaseResponse response = await client.GetAsync("richieste");

            if (response != null && response.Body != "null")
            {
                // Deserializza i dati della risposta in un dizionario
                Dictionary<string, Richiesta> richieste = response.ResultAs<Dictionary<string, Richiesta>>();

                // Cerca la richiesta con l'email specificata
                foreach (var richiesta in richieste)
                {
                    if (richiesta.Value.Email == email)
                    {
                        // Elimina la richiesta dal database utilizzando l'ID come chiave
                        await client.DeleteAsync("richieste/" + richiesta.Key);

                        return true; // Eliminazione riuscita
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Si è verificato un errore durante l'eliminazione della richiesta dal database: " + ex.Message);
        }

        return false; // Richiesta non trovata o eliminazione fallita
    }



    private void DecrementButton_Clicked(object sender, EventArgs e)
    {
        int ripetizioni = Convert.ToInt32(RipetizioniLabel.Text);
        if (ripetizioni > 1)
            RipetizioniLabel.Text = (ripetizioni - 1).ToString();
    }

    private void IncrementButton_Clicked(object sender, EventArgs e)
    {
        int ripetizioni = Convert.ToInt32(RipetizioniLabel.Text);
        RipetizioniLabel.Text = (ripetizioni + 1).ToString();
    }
    private void DecrementButton2_Clicked(object sender, EventArgs e)
    {
        int ripetizioni = Convert.ToInt32(DurataSerieLabel.Text);
        if (ripetizioni > 1)
            DurataSerieLabel.Text = (ripetizioni - 1).ToString();
    }

    private void IncrementButton2_Clicked(object sender, EventArgs e)
    {
        int ripetizioni = Convert.ToInt32(DurataSerieLabel.Text);
        DurataSerieLabel.Text = (ripetizioni + 1).ToString();
    }
    private void DecrementButton3_Clicked(object sender, EventArgs e)
    {
        int ripetizioni = Convert.ToInt32(TempoSerie.Text);
        if (ripetizioni > 1)
            TempoSerie.Text = (ripetizioni - 1).ToString();
    }

    private void IncrementButton3_Clicked(object sender, EventArgs e)
    {
        int ripetizioni = Convert.ToInt32(TempoSerie.Text);
        TempoSerie.Text = (ripetizioni + 1).ToString();
    }
    private void DecrementButton4_Clicked(object sender, EventArgs e)
    {
        int ripetizioni = Convert.ToInt32(DurataRecupero.Text);
        if (ripetizioni > 1)
            DurataRecupero.Text = (ripetizioni - 1).ToString();
    }

    private void IncrementButton4_Clicked(object sender, EventArgs e)
    {
        int ripetizioni = Convert.ToInt32(DurataRecupero.Text);
        DurataRecupero.Text = (ripetizioni + 1).ToString();
    }

}
