using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace ImageChunker
{
    class Chunker
    {
        public int TotalImages
        {
            get
            {
                return Rows * Columns;
            }
        }

        public int Rows { get; private set; }
        public int Columns { get; private set; }
        public int CropWidth { get; set; }
        public int CropHeight { get; set; }

        private readonly Bitmap image;
        private readonly string imagePath;

        /// <summary>
        /// Initializes the image chunker
        /// </summary>
        /// <param name="imagePath">The image you want chunked!</param>
        /// <param name="cropWidth">The full width of a chunk</param>
        /// <param name="cropHeight">The full height of a chunk</param>
        public Chunker(string imagePath, int cropWidth, int cropHeight)
        {
            this.imagePath = imagePath;
            image = new Bitmap(imagePath);
            if (cropWidth >= image.Width)
            {
                throw new Exception("Your specified width is >= than the total width of your image! Wtf?");
            }

            CropWidth = cropWidth;
            CropHeight = cropHeight;

            Columns = (int)Math.Ceiling((double)image.Width / CropWidth);
            Rows = (int)Math.Ceiling((double)image.Height / CropHeight);
        }

        /// <summary>
        /// Splits and saves the images to a specified location
        /// </summary>
        /// <param name="imageLocation">Where do you want to save your chunks?</param>
        public void SplitAndSave(string imageLocation)
        {
            string baseName = Path.GetFileNameWithoutExtension(imagePath), extension = Path.GetExtension(imagePath);
            for (int row = 0; row < Rows; row++)
            {
                int remainingHeight = CropHeight;

                for (int column = 0; column < Columns; column++)
                {
                    var remainingWidth = CropWidth;
                    if (column == Columns - 1 || row == Rows - 1) // last column || last row
                    {
                        if (column == Columns - 1)
                        {
                            remainingWidth = image.Width - (CropWidth * column);
                        }
                        if (row == Rows - 1)
                        {
                            remainingHeight = image.Height - (CropHeight * row);
                        }
                    }

                    var cropped = image.Clone(new Rectangle(column*CropWidth, row*CropHeight, remainingWidth, remainingHeight), image.PixelFormat);
                    cropped.Save(GetChunkLocation(imageLocation, baseName, column + (row * Columns), extension));
                }
            }
        }

        /// <summary>
        /// Splits the image and saves the chunks to the current directory
        /// </summary>
        public void SplitAndSave()
        {
            SplitAndSave("");
        }

        private static string GetChunkLocation(string imageLocation, string baseName, int chunkIndex, string extension)
        {
            return String.Format("{0}{1}{2}{3}", imageLocation, baseName, chunkIndex, extension);
        }
    }
}
