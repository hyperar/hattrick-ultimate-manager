namespace Hyperar.HUM.Application
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Hyperar.HUM.Domain.Interfaces;
    using Hyperar.HUM.Shared.Models.Chpp;

    public static class ImageHelper
    {
        internal const string FlagUrlMask = "/Img/flags/{0}.png";

        internal const string InactiveFlagUrlMask = "/Img/flags/{0}_inactive.png";

        private const string App = "HUM";

        private const string Company = "Hyperar";

        private const string host = "www.hattrick.org";

        private const string scheme = "https";

        internal static async Task<byte[]?> BuildAvatarAsync(
            Avatar? avatar,
            CancellationToken cancellationToken)
        {
            if (avatar is null)
            {
                return null;
            }

            var backgroundImage = await CreateAvatarImageAsync(avatar.BackgroundImage, cancellationToken);

            var avatarImage = new Bitmap(
                backgroundImage.Width,
                backgroundImage.Height,
                PixelFormat.Format32bppArgb);

            var graphics = Graphics.FromImage(avatarImage);

            graphics.DrawImage(
                backgroundImage,
                0,
                0,
                backgroundImage.Width,
                backgroundImage.Height);

            if (avatar.Layers is not null)
            {
                for (var i = 0; i < avatar.Layers.Length; i++)
                {
                    var layer = avatar.Layers[i];

                    var layerImage = GetImageFromBytes(
                        await ReadFileFromCacheAsync(
                            layer.Image,
                            cancellationToken));

                    graphics.DrawImage(
                        layerImage,
                        layer.X,
                        layer.Y,
                        layerImage.Width,
                        layerImage.Height);
                }
            }

            return GetBytesFromImage(avatarImage);
        }

        internal static async Task<byte[]> GetAvatarBytesAsync(
            IAvatarLayer[] avatarLayers,
            bool useFramelessAvatar,
            CancellationToken cancellationToken)
        {
            var initialIndex = useFramelessAvatar ? 1 : 0;
            var xCoordinateOffset = useFramelessAvatar ? 9 : 0;
            var yCoordinateOffset = useFramelessAvatar ? 10 : 0;

            var backgroundImage = await CreateAvatarImageAsync(
                avatarLayers.Single(x => x.Index == initialIndex).ImageUrl,
                cancellationToken);

            var avatarImage = new Bitmap(
                backgroundImage.Width,
                backgroundImage.Height,
                PixelFormat.Format32bppArgb);

            var graphics = Graphics.FromImage(avatarImage);

            graphics.DrawImage(
                backgroundImage,
                0,
                0,
                backgroundImage.Width,
                backgroundImage.Height);

            foreach (var curLayer in avatarLayers.Where(x => x.Index > initialIndex).OrderBy(x => x.Index))
            {
                var layerImage = GetImageFromBytes(
                    await ReadFileFromCacheAsync(
                        curLayer.ImageUrl,
                        cancellationToken));

                graphics.DrawImage(
                    layerImage,
                    curLayer.XCoordinate - xCoordinateOffset,
                    curLayer.YCoordinate - yCoordinateOffset,
                    layerImage.Width,
                    layerImage.Height);
            }

            var avatarBytes = Array.Empty<byte>();

            using (var memoryStream = new MemoryStream())
            {
                avatarImage.Save(memoryStream, ImageFormat.Png);

                avatarBytes = memoryStream.ToArray();
            }

            return avatarBytes;
        }

        internal static string GetFilePathFromUrl(string url)
        {
            ArgumentException.ThrowIfNullOrEmpty(url);

            var normalizedUrl = NormalizeUrl(url);

            var relativePath = new Uri(normalizedUrl).LocalPath.Replace("/", "\\");

            return Path.Combine(
                Environment.GetFolderPath(
                    Environment.SpecialFolder.ApplicationData),
                Company,
                App) + relativePath;
        }

        internal static bool ImageFileExists(string url)
        {
            return File.Exists(
                GetFilePathFromUrl(
                    url));
        }

        internal static string NormalizeUrl(string rawUrl)
        {
            ArgumentException.ThrowIfNullOrEmpty(rawUrl);

            rawUrl = rawUrl.Trim().ToLower();

            if (rawUrl.StartsWith("//"))
            {
                rawUrl = $"{scheme}:{rawUrl}";
            }
            else if (rawUrl.StartsWith("/img/"))
            {
                rawUrl = $"{scheme}://{host}{rawUrl}";
            }
            else if (!rawUrl.StartsWith("http://") && !rawUrl.StartsWith("https://"))
            {
                rawUrl = $"{scheme}://{rawUrl}";
            }

            return Uri.TryCreate(rawUrl, new UriCreationOptions { DangerousDisablePathAndQueryCanonicalization = true }, out var rawUri)
                ? rawUri.ToString()
                : throw new ArgumentException(rawUrl, nameof(rawUrl));
        }

        internal static async Task<byte[]> ReadFileFromCacheAsync(string url, CancellationToken cancellationToken)
        {
            var filePath = GetFilePathFromUrl(url);

            return await File.ReadAllBytesAsync(filePath, cancellationToken);
        }

        internal static async Task WriteFileToCacheAsync(string url, byte[] fileContent, CancellationToken cancellationToken)
        {
            var filePath = GetFilePathFromUrl(url);

            if (!Directory.Exists(filePath.Substring(0, filePath.LastIndexOf('\\'))))
            {
                Directory.CreateDirectory(filePath.Substring(0, filePath.LastIndexOf('\\')));
            }

            await File.WriteAllBytesAsync(filePath, fileContent, cancellationToken);
        }

        private static async Task<Bitmap> CreateAvatarImageAsync(string url, CancellationToken cancellationToken)
        {
            return GetImageFromBytes(
                await ReadFileFromCacheAsync(
                    url,
                    cancellationToken));
        }

        private static byte[] GetBytesFromImage(Image image)
        {
            using (var memoryStream = new MemoryStream())
            {
                image.Save(memoryStream, ImageFormat.Png);

                return memoryStream.ToArray();
            }
        }

        private static Bitmap GetImageFromBytes(byte[] imageBytes)
        {
            using (var memoryStream = new MemoryStream(imageBytes))
            {
                var bitmap = new Bitmap(Image.FromStream(memoryStream));

                bitmap.SetResolution(120, 120);

                return bitmap;
            }
        }
    }
}