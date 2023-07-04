namespace GymProject;

public partial class VisualizzaEsercizi : ContentPage
{
	List<Esercizio> esercizi=null;

    public VisualizzaEsercizi(List<Esercizio> esercizip)
	{
		InitializeComponent();

		esercizi = esercizip;
        CreateSchedeContainer(esercizi);

	}

    private void CreateSchedeContainer(List<Esercizio> esercizi)
    {
        foreach (var es in esercizi)
        {
            // Creazione del contenitore della scheda
            var schedaContainer = new StackLayout
            {
                BackgroundColor = Color.FromHex("#000000"),
                Padding = new Thickness(10),
                Margin = new Thickness(0, 0, 0, 10)
            };


           
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
                    Text = $"Numero ripetizioni: {es.NumeroRipetizioni}\n" +
                           $"Numero serie: {es.NumeroSerie}\n" +
                           $"Tempo serie: {es.TempoSerie}\n" +
                           $"Durata recupero: {es.DurataRecupero}\n" +
                           $"Priorità: {es.Priorita}\n" +
                           $"Tipo esercizio: {es.TipoEsercizio.Nome}\n" +
                           $"Descrizione tipo: {es.TipoEsercizio.Descrizione}",
                    FontSize = 16,
                    TextColor = Color.FromHex("#FFA500")
                };
                esercizioContainer.Children.Add(dettagliLabel);

                // Aggiunta del contenitore dell'esercizio al contenitore della scheda
                schedaContainer.Children.Add(esercizioContainer);
            

            // Aggiunta del pulsante "Prenota"
            var eliminaButton = new Button
            {
                Text = "Elimina",
                BackgroundColor = Color.FromHex("#FFA500"),
                TextColor = Color.FromHex("#000000")

            };
            // Aggiungi l'evento Clicked al pulsante
            eliminaButton.Clicked += (sender, e) =>
            {
                // Esegui il metodo per eliminare l'esercizio 'es' dalla lista degli esercizi 'esercizi'
                esercizi.Remove(es);
                DisplayAlert("Informazione", "L'esercizio è stato eliminato", "OK");
                if(esercizi.Count==0)
                {
                    // Torna alla pagina precedente
                    Navigation.PopAsync();
                }else
                {
                    OnAppearing();
                }

            };
            schedaContainer.Children.Add(eliminaButton);

            // Aggiunta del contenitore della scheda al contenitore principale
            stackLayout.Children.Add(schedaContainer);
        }
    }
}
