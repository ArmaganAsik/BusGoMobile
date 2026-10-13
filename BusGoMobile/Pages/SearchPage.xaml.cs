using BusGoMobile.Models;
using BusGoMobile.Services;

namespace BusGoMobile.Pages;

public partial class SearchPage : ContentPage
{
    private readonly DatabaseService _db;

    public SearchPage(DatabaseService db)
    {
        InitializeComponent();
        _db = db;

        // Tarih: bugünden itibaren
        GoDatePicker.MinimumDate = DateTime.Today;
        GoDatePicker.Date = DateTime.Today;

        // Yolcu sayısı stepper'ı
        PassengerStepper.ValueChanged += (s, e) =>
        {
            PassengerLabel.Text = $"{(int)e.NewValue} Yolcu";
        };

        // Yer değiştir: Nereden ↔ Nereye
        SwapButton.Clicked += (s, e) =>
        {
            int temp = FromPicker.SelectedIndex;
            FromPicker.SelectedIndex = ToPicker.SelectedIndex;
            ToPicker.SelectedIndex = temp;
        };
    }

    // Sayfa açılınca şehirleri DB'den çek ve Picker'lara doldur
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Zaten doluysa tekrar yükleme
        if (FromPicker.ItemsSource != null)
            return;

        List<City> cities = await _db.GetCitiesAsync();

        FromPicker.ItemsSource = cities;
        ToPicker.ItemsSource = cities;
    }
}