using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;

namespace Heeelp.Core.Common
{
    public class ImageUtility
    {
        public static Image CompressImage(Image image, long quality = 30L)
        {
            ImageCodecInfo jgpEncoder = GetEncoder(ImageFormat.Jpeg);
            Encoder myEncoder = Encoder.Quality;
            EncoderParameters myEncoderParameters = new EncoderParameters(1);
            myEncoderParameters.Param[0] = new EncoderParameter(myEncoder, 20L);
            MemoryStream ms = new MemoryStream();
            image.Save(ms, jgpEncoder, myEncoderParameters);
            Image imgImage = Image.FromStream(ms);
            return imgImage;
        }

        public static Image CompressImage(Bitmap bitmap, long quality = 30L)
        {
            return CompressImage((Image)bitmap, quality);
        }

        public static Image CompressImage(string imagemBase64, long quality = 30L)
        {
            byte[] imageBytes = Convert.FromBase64String(imagemBase64);
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);

            // Convert byte[] to Image
            ms.Write(imageBytes, 0, imageBytes.Length);
            Image image = Image.FromStream(ms, true);
            return CompressImage(image, quality);
        }

        public static byte[] ImageToByteArray(Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, ImageFormat.Gif);
                return ms.ToArray();
            }
        }

        public static Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        public static byte[] GetRemoteImageBytes(string imageUrl)
        {
            byte[] imageBytes;
            HttpWebRequest imageRequest = (HttpWebRequest)WebRequest.Create(imageUrl);
            WebResponse imageResponse = imageRequest.GetResponse();

            Stream responseStream = imageResponse.GetResponseStream();

            using (BinaryReader br = new BinaryReader(responseStream))
            {
                imageBytes = br.ReadBytes(500000);
                br.Close();
            }
            responseStream.Close();
            imageResponse.Close();

            return imageBytes;
        }

        public static Bitmap CropImage(Bitmap source, int x, int y, int width, int height)
        {
            Rectangle crop = new Rectangle(x, y, width, height);

            var bmp = new Bitmap(crop.Width, crop.Height);
            using (var gr = Graphics.FromImage(bmp))
            {
                gr.DrawImage(source, new Rectangle(0, 0, bmp.Width, bmp.Height), crop, GraphicsUnit.Pixel);
            }

            return bmp;
        }

        public static byte[] BitMapToByteArray(Bitmap bmp)
        {
            using (var memoryStream = new MemoryStream())
            {
                bmp.Save(memoryStream, ImageFormat.Jpeg);
                return memoryStream.ToArray();
            }
        }
        public static byte[] CropImage(Stream InputStream, int x, int y, int width, int height, float scale)
        {
            byte[] imgRet = null;

            using (Bitmap p = new Bitmap(InputStream))
            {

                float widthImg = p.Width;
                float heightImg = p.Height;
                float proportionalX = x * (widthImg / width);
                float proportionalY = y * (heightImg / height);

                float proportionalWidth = (widthImg / width) * 150;
                float proportionalHeight = (heightImg / height) * 150;


                using (var ms = new MemoryStream())
                {
                    var croppedImage = p.Clone(new Rectangle(Convert.ToInt32(proportionalX), Convert.ToInt32(proportionalY), Convert.ToInt32(proportionalWidth), Convert.ToInt32(proportionalHeight)), PixelFormat.Undefined);

                    var imgResized = ResizeImage(croppedImage, new Size(150, 150));

                    imgResized.Save(ms, ImageFormat.Png);

                    ms.Seek(0, SeekOrigin.Begin);

                    imgRet = ms.ToArray();

                }

            }
            return imgRet;
        }

        public static Image ResizeImage(Image imgToResize, Size size)
        {
            try
            {
                Image b = new Bitmap(size.Width, size.Height);
                using (Graphics g = Graphics.FromImage(b))
                {
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.DrawImage(imgToResize, 0, 0, size.Width, size.Height);
                }
                return b;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static byte[] CropImage(byte[] byteArrayIn, int x, int y, int width, int height)
        {
            Rectangle crop = new Rectangle(x, y, width, height);
            Image image = byteArrayToImage(byteArrayIn);
            var bmp = new Bitmap(crop.Width, crop.Height);
            using (var gr = Graphics.FromImage(bmp))
            {
                gr.DrawImage(image, new Rectangle(0, 0, bmp.Width, bmp.Height), crop, GraphicsUnit.Pixel);
            }

            return BitMapToByteArray(bmp);

        }

        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            return codecs.Where(x => x.FormatID == format.Guid).FirstOrDefault();
        }
    }
}
