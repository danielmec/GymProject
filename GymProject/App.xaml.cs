namespace GymProject;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}
    protected override void OnStart()
    {
        string message = "È disponibile un nuovo aggiornamento. Vuoi procedere con l'aggiornamento?";
       // UpdatePopup.ShowUpdatePopup(message);
    }
}

