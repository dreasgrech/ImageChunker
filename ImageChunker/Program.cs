using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;

namespace ImageChunker
{
    class Program
    {
        //TODO: Command Line Args

        static void Main(string[] args)
        {
            string imageLocation = args[0];
            int width = Convert.ToInt32(args[1]);

            var chunker = new Chunker(imageLocation, width, 100);
            chunker.SplitAndSave();
        }
    }
}