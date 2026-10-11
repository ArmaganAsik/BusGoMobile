using BusGoMobile.Models;
using SQLite;

namespace BusGoMobile.Services
{
    public class DatabaseService
    {
        private SQLiteAsyncConnection _db;

        // Bağlantıyı hazırla: gömülü dosyayı yazılabilir klasöre kopyala, sonra bağlan
        private async Task InitAsync()
        {
            if (_db is not null)
                return;

            // GEÇİCİ: Geliştirme için masaüstündeki sabit dosyaya bağlan.
            // İleride proje içine gömülü dosyaya geçilecek.
            string targetPath = @"C:\Users\Armagan\Desktop\MuratYucedag\DbBusGo.db";

            _db = new SQLiteAsyncConnection(targetPath);

            System.Diagnostics.Debug.WriteLine($"VERITABANI YOLU: {targetPath}");
        }

        // Yeni kullanıcı ekle (ham INSERT query'si)
        public async Task<int> AddUserAsync(AppUser appUser)
        {
            await InitAsync();

            return await _db.ExecuteAsync(
                @"INSERT INTO User (FirstName, LastName, Email, Phone, Password, MarketingOptIn, CreatedAt)
              VALUES (?, ?, ?, ?, ?, ?, ?);",
                appUser.FirstName, appUser.LastName, appUser.Email, appUser.Phone,
                appUser.Password, appUser.MarketingOptIn, appUser.CreatedAt);
        }

        // E-posta zaten kayıtlı mı?
        public async Task<bool> EmailExistsAsync(string email)
        {
            await InitAsync();

            int count = await _db.ExecuteScalarAsync<int>(
                "SELECT COUNT(*) FROM User WHERE Email = ?;", email);

            return count > 0;
        }

        // Login için: e-posta + şifre ile kullanıcı bul
        public async Task<AppUser> GetUserAsync(string email, string password)
        {
            await InitAsync();

            List<AppUser> results = await _db.QueryAsync<AppUser>(
                "SELECT * FROM User WHERE Email = ? AND Password = ? LIMIT 1;",
                email, password);

            return results.FirstOrDefault();
        }
    }
}
