namespace BusGoMobile.Pages;

public partial class ProfilePage : ContentPage
{
    public ProfilePage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Oturum bilgisinden kullanýcý adýný al
        string isim = Preferences.Get("UserName", "Kullanýcý");
        UserNameLabel.Text = isim;

        // E-posta için kayýtlý deðer varsa göster (yoksa türet)
        string email = Preferences.Get("UserEmail", "");
        UserEmailLabel.Text = string.IsNullOrWhiteSpace(email)
            ? "BusGo üyesi"
            : email;
    }

    private async void OnEditProfileTapped(object sender, TappedEventArgs e)
        => await DisplayAlert("Profil", "Profil düzenleme sayfasý daha sonra eklenecek.", "Tamam");

    private async void OnSettingsTapped(object sender, TappedEventArgs e)
        => await DisplayAlert("Ayarlar", "Ayarlar sayfasý daha sonra eklenecek.", "Tamam");

    private async void OnPersonalInfoTapped(object sender, TappedEventArgs e)
        => await DisplayAlert("Kiþisel Bilgiler", "Kiþisel bilgiler sayfasý daha sonra eklenecek.", "Tamam");

    private async void OnMyTripsTapped(object sender, TappedEventArgs e)
        => await Shell.Current.GoToAsync("//MyTripsPage");

    private async void OnFavoritesTapped(object sender, TappedEventArgs e)
        => await Shell.Current.GoToAsync("//FavoritesPage");

    private async void OnPaymentMethodsTapped(object sender, TappedEventArgs e)
        => await DisplayAlert("Ödeme Yöntemleri", "Ödeme yöntemleri sayfasý daha sonra eklenecek.", "Tamam");

    private async void OnNotificationsTapped(object sender, TappedEventArgs e)
        => await DisplayAlert("Bildirimler", "Bildirim tercihleri sayfasý daha sonra eklenecek.", "Tamam");

    private async void OnSupportTapped(object sender, TappedEventArgs e)
        => await DisplayAlert("Yardým ve Destek", "Destek sayfasý daha sonra eklenecek.", "Tamam");

    private async void OnLogoutTapped(object sender, TappedEventArgs e)
    {
        bool onay = await DisplayAlert("Çýkýþ Yap",
            "Hesabýnýzdan çýkýþ yapmak istediðinize emin misiniz?",
            "Evet, çýkýþ yap", "Vazgeç");

        if (!onay) return;

        Preferences.Remove("UserId");
        Preferences.Remove("UserName");
        Preferences.Remove("UserEmail");

        await Shell.Current.GoToAsync("//LoginPage");
    }
}