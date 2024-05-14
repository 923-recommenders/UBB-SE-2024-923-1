namespace UBB_SE_2024_923_1_UI
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void GoToAuthPage_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AuthPage());
        }

    }

}
