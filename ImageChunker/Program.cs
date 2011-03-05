using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using NDesk.Options;

namespace ImageChunker
{
    class Program
    {
        static void Main(string[] args)
        {
            var settings = HandleArgs(args);
            string imageLocation = settings.Image;

            var chunker = new Chunker(imageLocation, settings.Width, settings.Height);
            chunker.SplitAndSave();

        }

        static Settings HandleArgs(IEnumerable<string> args)
        {
            int width = 0, height = 0;
            string image = "", output = "";
            var options = new OptionSet
                              {
                                  {"w|width=", "The full width of the chunks", (int w) => { width = w; }},
                                  {"h|height=", "The full height of the chunks", (int h) => { height = h; }},
                                  {"o|output=", "Where do you want the chunks to be saved?  By default, they will be saved here.", (string o) => { output = o; }},
                                  {"<>","The image path",(string i) => { image = i;}}
                              };
            options.Parse(args);
            return new Settings {Image = image, Height = height, Width = width, OutputPath = output};
        }
    }
}