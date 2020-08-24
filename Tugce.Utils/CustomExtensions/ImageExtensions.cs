using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Tugce.Utils
{
    public static class ImageExtensions
    {
        public static Image CropImage(this HttpPostedFileBase postedImage,NameValueCollection form)
        {
            Image orgBitmap = Image.FromStream(postedImage.InputStream);

            //kişi kırpma yapmadıysa o zaman resmi olduğu gibi göndermeliyim.
            if (string.IsNullOrEmpty(form["left"]))
                return Image.FromStream(postedImage.InputStream);

            //formdan postalanan kırpma alanına ait bilgiler
            var left = double.Parse(form["left"].Replace(".", ","));
            var top = double.Parse(form["top"].Replace(".", ","));
            var width = double.Parse(form["width"].Replace(".", ","));
            var height = double.Parse(form["height"].Replace(".", ","));
            var ratio = double.Parse(form["ratio"].Replace(".", ","));
            left = left * ratio;
            top = top * ratio;
            width = width * ratio;
            height = height * ratio;
            Rectangle cropRec = new Rectangle((int)left, (int)top, (int)width, (int)height);
            Bitmap target = new Bitmap(cropRec.Width, cropRec.Height);

            using (Graphics g = Graphics.FromImage(target))
            {
                g.DrawImage(orgBitmap, new Rectangle(0, 0, target.Width, target.Height),
                                 cropRec,
                                 GraphicsUnit.Pixel);
            }
            return target;
        }
        
        public static string GenerateFileName(this string fileName)
        {
            string extension = Path.GetExtension(fileName);
            DateTime now=DateTime.Now;
            string newFileName = string.Format("{0}_{1}_{2}_{3}_{4}_{5}_{6}{7}", now.Day, now.Month, now.Year, now.Hour, now.Minute, now.Second, now.Millisecond, extension);
            return newFileName;
        }

        public static Image GetThumbnailImage(this Image image,int width,int height)
        {
            Bitmap bmp = null;
            Graphics g;

            // Scale:
            double scaleY = (double)image.Width / width;
            double scaleX = (double)image.Height / height;
            double scale = scaleY < scaleX ? scaleX : scaleY;

            // Create new bitmap:
            bmp = new Bitmap(
                (int)((double)image.Width / scale),
                (int)((double)image.Height / scale));

            // Set resolution of the new image:
            bmp.SetResolution(
                image.HorizontalResolution,
                image.VerticalResolution);

            // Create graphics:
            g = Graphics.FromImage(bmp);

            // Set interpolation mode:
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            // Draw the new image:
            g.DrawImage(
                image,
                new Rectangle(            // Destination
                    0, 0,
                    bmp.Width, bmp.Height),
                new Rectangle(            // Source
                    0, 0,
                    image.Width, image.Height),
                GraphicsUnit.Pixel);

            // Release the resources of the graphics:
            g.Dispose();

            // Release the resources of the origin image:
            image.Dispose();

            return bmp;
        }

    }
}
