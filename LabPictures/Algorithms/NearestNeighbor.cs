using System;
using System.Drawing;

namespace LabPictures.Algorithms
{
    public class NearestNeighbor
    {
        public Bitmap ResizeNearestNeighbor(Bitmap bitmap, double coefX, double coefY)
        {
            var destinationWidth = Math.Round(bitmap.Width * coefX);
            var destinationHeight = Math.Round(bitmap.Height * coefY);

            Bitmap? destinationBitmap = new Bitmap((int)destinationWidth, (int)destinationHeight);

            for (int x = 0; x < destinationWidth; x++)
            {
                for (int y = 0; y < destinationHeight; y++)
                {
                    var sourceX = (int)(x / coefX);
                    var sourceY = (int)(y / coefY);

                    destinationBitmap.SetPixel(x, y, bitmap.GetPixel(sourceX, sourceY));
                }
            }
            return destinationBitmap;
        }
    }
}
