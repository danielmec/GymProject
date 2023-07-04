using FireSharp.Interfaces;
using FireSharp.Config;
using FireSharp.Response;
using Firebase;
using FireSharp;

namespace GymProject;

public partial class PrenotaAttrezzature : ContentPage
{
    IFirebaseClient client = null;
    Utente utente = null;
    public List<SchedaAllenamento> Schede { get; set; }

    public PrenotaAttrezzature(IFirebaseClient clientp, Utente utentep, List<SchedaAllenamento> schedep)
	{
		InitializeComponent();
        client = clientp;
        utente = utentep;
        Schede= schedep;
        CreateSchedeContainer(Schede);
    }

    private void CreateSchedeContainer(List<SchedaAllenamento> schede)
    {
        foreach (var scheda in schede)
        {
            // Creazione del contenitore della scheda
            var schedaContainer = new StackLayout
            {
                BackgroundColor = Color.FromHex("#000000"),
                Padding = new Thickness(10),
                Margin = new Thickness(0, 0, 0, 10)
            };

            // Aggiunta del nome della scheda
            var nomeLabel = new Label
            {
                Text = scheda.Nome,
                FontSize = 20,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.FromHex("#FFA500")
            };
            schedaContainer.Children.Add(nomeLabel);

            // Aggiunta degli esercizi della scheda
            foreach (var esercizio in scheda.Esercizi)
            {
                // Creazione del contenitore dell'esercizio
                var esercizioContainer = new StackLayout
                {
                    BackgroundColor = Color.FromHex("#000000"),
                    Padding = new Thickness(10),
                    Margin = new Thickness(0, 10, 0, 0)
                };
                Label esercizioLabel = new Label
                {
                    Text = "Esercizio:",
                    TextColor = Color.FromHex("#FFA500"), // Arancione
                    FontSize = 18
                };
                esercizioContainer.Children.Add(esercizioLabel);

                // Aggiunta dei dettagli dell'esercizio
                var dettagliLabel = new Label
                {
                    Text = $"Numero ripetizioni: {esercizio.NumeroRipetizioni}\n" +
                           $"Numero serie: {esercizio.NumeroSerie}\n" +
                           $"Tempo serie: {esercizio.TempoSerie}\n" +
                           $"Durata recupero: {esercizio.DurataRecupero}\n" +
                           $"Priorità: {esercizio.Priorita}\n" +
                           $"Tipo esercizio: {esercizio.TipoEsercizio.Nome}\n" +
                           $"Descrizione tipo: {esercizio.TipoEsercizio.Descrizione}",
                    FontSize = 16,
                    TextColor = Color.FromHex("#FFA500")
                };
                esercizioContainer.Children.Add(dettagliLabel);

                // Aggiunta del contenitore dell'esercizio al contenitore della scheda
                schedaContainer.Children.Add(esercizioContainer);
            }

            // Aggiunta del pulsante "Prenota"
            var prenotaButton = new Button
            {
                Text = "Prenota",
                BackgroundColor = Color.FromHex("#FFA500"),
                TextColor = Color.FromHex("#000000")
            };
            schedaContainer.Children.Add(prenotaButton);

            // Aggiunta del contenitore della scheda al contenitore principale
            stackLayout.Children.Add(schedaContainer);
        }
    }


}
