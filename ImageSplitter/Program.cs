using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;

namespace ImageSplitter
{
    class Program
    {
        static void Main(string[] args)
        {
            string imageLocation = args[0];
            int width = Convert.ToInt32(args[1]);
            Image image = Image.FromFile(imageLocation,true);
            if (width > image.Width)
            {
                return;
            }
            var totalImages = (int)Math.Ceiling((double)image.Width/width);

            var bmp = new Bitmap(width, image.Height);
            Graphics g = Graphics.FromImage(bmp);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            var baseName = Path.GetFileNameWithoutExtension(imageLocation);
            for (int i = 0; i < totalImages; i++)
            {
                var remainingWidth = width;
                if (i == totalImages - 1) // last image
                {
                    remainingWidth = image.Width - (width*(i));
                    g = Graphics.FromImage(bmp = new Bitmap(remainingWidth, image.Height));
                }
                g.Clear(Color.Transparent);
                g.DrawImage(image, new Rectangle(0, 0, remainingWidth, image.Height), new Rectangle(i * width, 0, remainingWidth, image.Height), GraphicsUnit.Pixel);
                bmp.Save(String.Format("{0}{1}.png",baseName, i));
            }
        }
    }
}