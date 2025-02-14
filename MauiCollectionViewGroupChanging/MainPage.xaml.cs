namespace MauiCollectionViewGroupChanging
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            DetailPage detailPage = new();
            var workItemViewModel = (sender as VisualElement).BindingContext;
            detailPage.BindingContext = workItemViewModel;
            Navigation.PushAsync(detailPage);
        }

        private void ItemTapped(object sender, TappedEventArgs e)
        {
            
        }
    }
}
