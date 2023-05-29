namespace WordBlitz;

public partial class Blitz : ContentPage
{
	public Blitz()
	{
		InitializeComponent();
	}
    private void LetterSelected(object sender, EventArgs e)
    {
        ((Button)sender).Text=String.Empty;
    }
}