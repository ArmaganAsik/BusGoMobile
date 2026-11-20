using Microsoft.Extensions.Logging;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.LifecycleEvents;

namespace BusGoMobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            MauiAppBuilder builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");

                    // Inter aileleri
                    fonts.AddFont("Inter_18pt-Regular.ttf", "InterRegular");
                    fonts.AddFont("Inter_18pt-Medium.ttf", "InterMedium");
                    fonts.AddFont("Inter_18pt-SemiBold.ttf", "InterSemiBold");
                    fonts.AddFont("Inter_18pt-Bold.ttf", "InterBold");
                    fonts.AddFont("Inter_18pt-ExtraBold.ttf", "InterExtraBold");

                    // Material Symbols ikon fontu
                    fonts.AddFont("MaterialSymbolsOutlined-Regular.ttf", "MaterialSymbols");
                });

            builder.Services.AddSingleton<BusGoMobile.Services.DatabaseService>();
            builder.Services.AddTransient<BusGoMobile.Pages.RegisterPage>();
            builder.Services.AddTransient<BusGoMobile.Pages.LoginPage>();
            builder.Services.AddTransient<BusGoMobile.Pages.HomePage>();
            builder.Services.AddTransient<BusGoMobile.Pages.SearchPage>();
            builder.Services.AddTransient<BusGoMobile.Pages.TripListPage>();
            builder.Services.AddTransient<BusGoMobile.Pages.SeatSelectionPage>();
            builder.Services.AddTransient<BusGoMobile.Pages.PassengerInfoPage>();
            builder.Services.AddTransient<BusGoMobile.Pages.PaymentPage>();
            builder.Services.AddTransient<BusGoMobile.Pages.SplashPage>();
            builder.Services.AddTransient<BusGoMobile.Pages.AccountLoadingPage>();
            builder.Services.AddTransient<BusGoMobile.Pages.LoginSuccessPage>();

#if WINDOWS
      
            // Windows'ta Entry'nin native kenarlığını kaldır
            EntryHandler.Mapper.AppendToMapping("NoUnderlineBusGo", (handler, view) =>
            {
                var tb = handler.PlatformView; // Windows native TextBox
                tb.BorderThickness = new Microsoft.UI.Xaml.Thickness(0);
                tb.Background = new Microsoft.UI.Xaml.Media.SolidColorBrush(Microsoft.UI.Colors.Transparent);
            });

            builder.ConfigureLifecycleEvents(events =>
            {
                events.AddWindows(windows => windows.OnWindowCreated(window =>
                {
                    var handle = WinRT.Interop.WindowNative.GetWindowHandle(window);
                    var id = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(handle);
                    var appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(id);
                    appWindow.Resize(new Windows.Graphics.SizeInt32(375, 750));
                }));
            });

#endif


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
