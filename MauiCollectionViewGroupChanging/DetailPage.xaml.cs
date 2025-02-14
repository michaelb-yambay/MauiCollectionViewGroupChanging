namespace MauiCollectionViewGroupChanging;

public partial class DetailPage : ContentPage
{
	public DetailPage()
	{
		InitializeComponent();
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
		Navigation.PopAsync();	
    }
}