namespace UBB_SE_2024_923_1_UI;
using Microsoft.Maui.Controls;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

public partial class AuthPage : ContentPage
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUri = "https://localhost:7025/Users/";
    public AuthPage()
	{
		InitializeComponent();
        _httpClient = new HttpClient();
    }

    private async void LoginButton_Clicked(object sender, EventArgs e)
    {
        var loginData = new
        {
            loginUsername = UsernameEntry.Text,
            loginPassword = PasswordEntry.Text
        };

        try
        {
            string userInfo = $"Username: {loginData.loginUsername}\nPassword: {loginData.loginPassword}";
            await DisplayAlert("New User Information", userInfo, "OK");
            await DisplayAlert("New User Information", Newtonsoft.Json.JsonConvert.SerializeObject(loginData), "OK");

            // var response = await _httpClient.PostAsync(_baseUri + "login", new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(loginData), Encoding.UTF8, "application/json"));

            // Construct the URI with query parameters
            var uriBuilder = new UriBuilder(_baseUri + "login");
            uriBuilder.Query = $"loginUsername={loginData.loginUsername}&loginPassword={loginData.loginPassword}";

            var response = await _httpClient.PostAsync(uriBuilder.Uri, null);

            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("muie", Newtonsoft.Json.JsonConvert.SerializeObject(loginData), "OK");

                var token = await response.Content.ReadAsStringAsync();
                // store token then navigate
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                LoginMessageLabel.Text = errorMessage;
            }
        }
        catch (Exception ex)
        {
            LoginMessageLabel.Text = "An error occurred: " + ex.Message;
        }
    }
    
    private async void RegisterButton_Clicked(object sender, EventArgs e)
    {
        var newUser = new
        {
            username = RegisterUsernameEntry.Text,
            password = RegisterPasswordEntry.Text,
            country = CountryEntry.Text,
            email = EmailEntry.Text,
            age = int.Parse(AgeEntry.Text)
        };

        try
        {
            string userInfo = $"Username: {newUser.username}\nPassword: {newUser.password}\nCountry: {newUser.country}\nEmail: {newUser.email}\nAge: {newUser.age}";
            await DisplayAlert("New User Information", userInfo, "OK");
            await DisplayAlert("New User Information", Newtonsoft.Json.JsonConvert.SerializeObject(newUser), "OK");


            // var response = await _httpClient.PostAsync(_baseUri + "register", new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(newUser), Encoding.UTF8, "application/json"));

            // Construct the URI with query parameters
            var uriBuilder = new UriBuilder(_baseUri + "register");
            uriBuilder.Query = $"newUserUsername={newUser.username}&newUserPassword={newUser.password}&newUserCountry={newUser.country}&newUserEmail={newUser.email}&newUserAge={newUser.age}";

            var response = await _httpClient.PostAsync(uriBuilder.Uri, null);

            if (response.IsSuccessStatusCode)
            {
                RegisterMessageLabel.Text = "Registration successful";
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                RegisterMessageLabel.Text = errorMessage;
            }
        }
        catch (Exception ex)
        {
            RegisterMessageLabel.Text = "An error occurred: " + ex.Message;
        }
    }

    private void ShowLogin_Clicked(object sender, EventArgs e)
    {
        LoginLayout.IsVisible = true;
        RegisterLayout.IsVisible = false;
    }

    private void ShowRegister_Clicked(object sender, EventArgs e)
    {
        LoginLayout.IsVisible = false;
        RegisterLayout.IsVisible = true;
    }
}