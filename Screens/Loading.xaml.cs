using WordBlitz.tools;
namespace WordBlitz;


public partial class Loading : ContentPage
{
	public Loading()
	{
		InitializeComponent();
		IDispatcherTimer timer = Dispatcher.CreateTimer();
		timer.IsRepeating = false;
		timer.Interval = TimeSpan.FromMilliseconds(200);
		timer.Tick += RunPages;
        Dispatcher.Dispatch(Global.Init);
		Load.Toggle();
        timer.Start();
	}

	private static void RunPages(object sender, EventArgs e)
	{
		App.Current.MainPage.Navigation.PushAsync(Blitz.Get());
    }
}