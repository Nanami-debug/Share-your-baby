using System;
using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;

namespace Form.Util
{
    public class PictureUtil
    {
        public static BitmapImage Base64StringToBitmapImage(string base64String)
        {
            if (string.IsNullOrEmpty(base64String))
            {
                return null;
            }
            byte[] byteBuffer = Convert.FromBase64String(base64String);
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.StreamSource = new MemoryStream(byteBuffer);
            bi.EndInit();
            return bi;
        }

        public static string ImgToBase64String(string Imagefilename)
        {
            try
            {
                Bitmap bmp = new Bitmap(Imagefilename);
                return ImgToBase64String(bmp);
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static string ImgToBase64String(Bitmap bmp)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] arr = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(arr, 0, (int)ms.Length);
                ms.Close();
                return Convert.ToBase64String(arr);
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static Bitmap Base64StringToBitmap(string base64String)
        {
            byte[] imageBytes = Convert.FromBase64String(base64String);
            using (MemoryStream ms = new MemoryStream(imageBytes))
            {
                Bitmap bitmap = new Bitmap(ms);
                return bitmap;
            }
        }
    }
}