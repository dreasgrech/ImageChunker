using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;

namespace ImageChunker
{
    class Program
    {
        static void Main(string[] args)
        {
            string imageLocation = args[0];
            int width = Convert.ToInt32(args[1]);
            Image image = Image.FromFile(imageLocation,true);
            if (width >= image.Width)
            {
                throw new Exception("Your specified width is >= than the total width of your image! Wtf?");
            }

            var chunker = new Chunker(image, width, 100);
            chunker.SplitAndSave(imageLocation);
        }
    }
}