using System.Collections.Generic;
using FocalSpec.FsApiNet.Model;

namespace FocalSpec.GuiExample.Model.Camera
{
    public class RawImage
    {
        public FsApi.Header Header { get; set; }

        public byte[] Image { get; set; }

        public int Bytes { get; set; }

		public int Width { get; set; }

        public int Height { get; set; }

        public bool Flipped { get; }

        public RawImage(byte[] image, int bytes, FsApi.Header header, int width, bool flipped)
        {
            Image = image;
            Bytes = bytes;
            Header = header;
			Width = width;
			Height = bytes / Width;
            Flipped = flipped;
        }
    }
}
