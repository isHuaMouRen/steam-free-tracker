using System.IO;
using System.Net.Http;
using System.Windows.Media.Imaging;

namespace SteamFreeTracker.Utils
{
    public static class BitmapLoader
    {
        private static readonly HttpClient _client = new HttpClient();

        public static async Task<BitmapImage?> LoadImageFromUrlAsync(string url)
        {
            byte[] imageBytes = await _client.GetByteArrayAsync(url);
            using var memoryStream = new MemoryStream(imageBytes);

            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();

            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapImage.StreamSource = memoryStream;

            bitmapImage.EndInit();
            bitmapImage.Freeze();

            return bitmapImage;
        }
    }
}
