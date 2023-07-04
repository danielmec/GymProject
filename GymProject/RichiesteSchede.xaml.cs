using FireSharp.Interfaces;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using FireSharp.Config;
using FireSharp.Response;
using Firebase;
using FireSharp;

namespace GymProject;

public partial class RichiesteSchede : ContentPage
{
	private List<Richiesta> richieste =null;
    IFirebaseClient client;

    public RichiesteSchede(List<Richiesta> richiestep, IFirebaseClient clientp)
	{
		InitializeComponent();
        client = clientp;

        richieste = richiestep;

        if (this.richieste != null)
        {
            VisualizzaRichieste();
        }
        else
        {
            Console.WriteLine("Non ci sono richieste è vuoto");
        }
    }

   


    private void VisualizzaRichieste()
    {
        foreach (Richiesta richiesta in richieste)
        {
            var contenitoreRichiesta = new StackLayout
            {
                BackgroundColor = Color.FromHex("#4C4C4C"),
                Padding = new Thickness(10),
                Spacing = 5
            };

            var tipoLabel = new Label
            {
                Text = $"Tipo: {richiesta.Tipo}",
                TextColor = Color.FromHex("#FFA500")
            };

            var descrizioneLabel = new Label
            {
                Text = $"Descrizione: {richiesta.Descrizione}",
                TextColor = Color.FromHex("#FFA500")
            };

            var etaLabel = new Label
            {
                Text = $"Eta: {richiesta.Eta}",
                TextColor = Color.FromHex("#FFA500")
            };

            var emailLabel = new Label
            {
                Text = $"Email: {richiesta.Email}",
                TextColor = Color.FromHex("#FFA500")
            };

            var pesoLabel = new Label
            {
                Text = $"Peso: {richiesta.Peso}",
                TextColor = Color.FromHex("#FFA500")
            };

            var altezzaLabel = new Label
            {
                Text = $"Altezza: {richiesta.Altezza}",
                TextColor = Color.FromHex("#FFA500")
            };

            var creaSchedaButton = new Button
            {
                Text = "Crea Scheda",
                BackgroundColor = Color.FromHex("#FFA500"),
                TextColor = Color.FromHex("#FFFFFF")
            };
            creaSchedaButton.Clicked += (sender, e) =>
            {
                Navigation.PushAsync(new CreaS(client,richiesta.Email));
            };

            contenitoreRichiesta.Children.Add(tipoLabel);
            contenitoreRichiesta.Children.Add(descrizioneLabel);
            contenitoreRichiesta.Children.Add(emailLabel);
            contenitoreRichiesta.Children.Add(etaLabel);
            contenitoreRichiesta.Children.Add(pesoLabel);
            contenitoreRichiesta.Children.Add(altezzaLabel);
            contenitoreRichiesta.Children.Add(creaSchedaButton);

            richiesteContainer.Children.Add(contenitoreRichiesta);

        }
    }
 

}
