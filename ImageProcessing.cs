using System.Drawing;
using System.IO;
using System.Web;
using ZXing;
using ZXing.Common;
using ZXing.QrCode.Internal;
using ZXing.Rendering;

namespace Clients.Helpers
{
    public class ImageProcessing
    {

        public static Bitmap GenQrCodeWithLogo(string url, int size)
        { 
            var path = HttpContext.Current.Server.MapPath("~/Content/profile/img/favicon.png");

            return GenerateQR(size, size, url, path);
        }

        private static Bitmap GenerateQR(int width, int height, string text,string imagePath)
        {
            var bw = new  BarcodeWriter();
            var encOptions = new EncodingOptions
            {
                Width = width,
                Height = height,
                Margin = 0,
                PureBarcode = false
            };

            encOptions.Hints.Add(EncodeHintType.ERROR_CORRECTION, ErrorCorrectionLevel.H);

            bw.Renderer = new BitmapRenderer();
            bw.Options = encOptions;
            bw.Format = BarcodeFormat.QR_CODE;
            var bm = bw.Write(text);
            var overlay = new Bitmap(imagePath);

            int deltaHeigth = bm.Height - overlay.Height;
            int deltaWidth = bm.Width - overlay.Width;

            var g = Graphics.FromImage(bm);
            g.DrawImage(overlay, new Point(deltaWidth / 2, deltaHeigth / 2));

            return bm;
        }
    }
}