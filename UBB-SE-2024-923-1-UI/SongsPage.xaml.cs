namespace UBB_SE_2024_923_1_UI;
using Microsoft.Maui.Controls;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
public class Song
{
    public int SongId { get; set; }
    public string ArtistName { get; set; }
    public string Name { get; set; }
    public string Genre { get; set; }
    public string Subgenre { get; set; }
    public string Language { get; set; }
    public string Country { get; set; }
    public bool IsExplicit { get; set; }
}

public partial class SongsPage : ContentPage
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl = "https://localhost:7025/Song/";
    public List<Song> Songs { get; set; }
    public Song SelectedSong { get; set; }

    public SongsPage()
    {
        InitializeComponent();
        BindingContext = this;
        _httpClient = new HttpClient();
        LoadSongs();
    }

    private async void LoadSongs()
    {
        try
        {
            // Get the token from wherever you've stored it
            string token = await SecureStorage.GetAsync("AuthToken"); // Get your token here

            // Set the authorization header with the token
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Make GET request to the API
            var response = await _httpClient.GetFromJsonAsync<List<Song>>(_baseUrl);

            if (response != null)
            {
                Songs = response;
                OnPropertyChanged(nameof(Songs));
            }
            else
            {
                // Handle error response
                await DisplayAlert("Error", "Failed to fetch songs", "OK");
            }
        }
        catch (Exception ex)
        {
            // Handle exception
            await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
        }
    }

    private void songListView_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        if (e.Item is Song selectedSong)
        {
            SelectedSong = selectedSong;
            OnPropertyChanged(nameof(SelectedSong));
            songDetailsLayout.IsVisible = true;
        }
    }
}