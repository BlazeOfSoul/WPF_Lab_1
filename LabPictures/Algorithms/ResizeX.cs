using System;
using System.Drawing;

namespace LabPictures.Algorithms
{
    public class ResizeX
    {
        public Bitmap ResizeXTimes(Bitmap bitmap, double coefX, double coefY)
        {
            var destWidth = Math.Round(bitmap.Width * coefX);
            var destHeight = Math.Round(bitmap.Height * coefY);

            var destinationBitmap = new Bitmap((int)destWidth, (int)destHeight);

            for (int x = 0; x < destWidth - 2; x++)
            {
                for (int y = 0; y < destHeight - 2; y++)
                {
                    var sourceX = x / coefX;
                    var sourceY = y / coefY;

                    var firstX = (int)(sourceX);
                    var secondX = firstX;

                    if (firstX < bitmap.Width - 1)
                    {
                        secondX = firstX + 1;
                    }

                    var firstY = (int)(sourceY);
                    var secondY = firstY;
                    if (firstY < bitmap.Height - 1)
                    {
                        secondY += 1;
                    }
                    Color color = FindColor(bitmap, firstX, secondX, firstY, secondY, coefX, coefY, x, y);
                    destinationBitmap.SetPixel(x, y, color);
                }
            }
            return destinationBitmap;
        }

        private Color FindColor(Bitmap bitmap, int firstX, int secondX, int firstY, int secondY, double coefX, double coefY, int destX, int destY)
        {
            Color firstXYColor = bitmap.GetPixel(firstX, firstY);
            // Color secondXColor = _sourceBitmap.GetPixel(secondX, firstY);
            Color secondYColor = bitmap.GetPixel(firstX, secondY);

            //int diffX;
            int diffY;
            //if (firstX == 0)
            //{
            //    firstX++;
            //    destX++;
            //}

            if (firstY == 0)
            {
                firstY++;
                //destY++;
            }

            //diffX = destX % Convert.ToInt32(Math.Ceiling(firstX * coefX));
            diffY = destY % Convert.ToInt32(Math.Ceiling(firstY * coefY));

            //var colorRX = GetColorValue(firstXYColor.R, secondXColor.R, coefX, diffX);
            //var colorGX = GetColorValue(firstXYColor.G, secondXColor.G, coefX, diffX);
            //var colorBX = GetColorValue(firstXYColor.B, secondXColor.B, coefX, diffX);

            var colorRY = GetColorValue(firstXYColor.R, secondYColor.R, coefY, diffY);
            var colorGY = GetColorValue(firstXYColor.G, secondYColor.G, coefY, diffY);
            var colorBY = GetColorValue(firstXYColor.B, secondYColor.B, coefY, diffY);

            //var finalColorR = (colorRX + colorRY) / 2;
            //var finalColorG = (colorGX + colorGY) / 2;
            //var finalColorB = (colorBX + colorBY) / 2;

            return Color.FromArgb(colorRY, colorGY, colorBY);
        }

        private int GetColorValue(int firstColorValue, int secondColorValue, double coef, int diff)
        {
            double colorCoef = (secondColorValue - firstColorValue) / coef;
            return (int)(diff * colorCoef + firstColorValue);
        }
    }
}
