using Microsoft.Maui.Controls;

public class UpdatePopup
{
    public static async Task ShowUpdatePopup(string message)
    {
        await Application.Current.MainPage.DisplayAlert("Aggiornamento disponibile", message, "Aggiorna", "Annulla");

        // Esegui la logica di aggiornamento qui dopo la conferma dell'utente
    }
}

