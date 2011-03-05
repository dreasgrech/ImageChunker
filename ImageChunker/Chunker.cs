﻿using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;

namespace ImageChunker
{
    class Chunker
    {
        public int TotalImages { get; set; }
        public int CropWidth { get; set; }
        public int CropHeight { get; set; }

        private readonly Image image;

        /// <summary>
        /// Initializes the image chunker
        /// </summary>
        /// <param name="image">The image you want chunked!</param>
        /// <param name="cropWidth">The full width of a chunk</param>
        /// <param name="cropHeight">The full height of a chunk (NOTE: THIS IS NOT YET USED!)</param>
        public Chunker(Image image, int cropWidth, int cropHeight)
        {
            this.image = image;
            CropWidth = cropWidth;
            CropHeight = cropHeight;

            TotalImages = (int)Math.Ceiling((double)image.Width / CropWidth);
        }

        /// <summary>
        /// Splits and saves the images to a location
        /// </summary>
        /// <param name="imageLocation">Where do you want to save your chunks?</param>
        public void SplitAndSave(string imageLocation)
        {
            var bmp = new Bitmap(CropWidth, image.Height);
            Graphics g = GetGraphicsFromImage(bmp);

            var baseName = Path.GetFileNameWithoutExtension(imageLocation);
            for (int i = 0; i < TotalImages; i++)
            {
                var remainingWidth = CropWidth;
                if (i == TotalImages - 1) // last image
                {
                    remainingWidth = image.Width - (CropWidth * (i));
                    g = GetGraphicsFromImage(new Bitmap(remainingWidth, image.Height));
                }
                g.Clear(Color.Transparent);
                g.DrawImage(image, new Rectangle(0, 0, remainingWidth, image.Height), new Rectangle(i * CropWidth, 0, remainingWidth, image.Height), GraphicsUnit.Pixel);
                bmp.Save(String.Format("{0}{1}{2}", baseName, i, Path.GetExtension(imageLocation)));
            }
        }

        private static Graphics GetGraphicsFromImage(Bitmap bmp)
        {
            Graphics g = Graphics.FromImage(bmp);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            return g;
        }
    }
}
