using BusGoMobile.Models;
using BusGoMobile.Services;

namespace BusGoMobile.Pages;

public partial class TripListPage : ContentPage
{
    private readonly DatabaseService _db;

    public TripListPage(DatabaseService db)
    {
        InitializeComponent();
        _db = db;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        List<Trip> trips = await _db.GetAllTripsWithAmenitiesAsync();

        TripsView.ItemsSource = trips;
        ResultCountLabel.Text = $"Toplam {trips.Count} sefer bulundu";
    }
}