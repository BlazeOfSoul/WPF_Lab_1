using System;
using System.Drawing;

namespace LabPictures.Algorithms
{
    public class BilinearInterpolation
    {
        public Bitmap ResizeBilinearInterpolation(Bitmap bitmap, double coefX, double coefY)
        {
            var destWidth = Math.Round(bitmap.Width * coefX);
            var destHeight = Math.Round(bitmap.Height * coefY);

            var destinationBitmap = new Bitmap((int)destWidth, (int)destHeight);

            for (int x = 0; x < destWidth; x++)
            {
                for (int y = 0; y < destHeight; y++)
                {
                    var sourceX = x / (coefX * bitmap.Width / (bitmap.Width - 1));
                    var sourceY = y / (coefY * bitmap.Height / (bitmap.Height - 1));

                    var firstX = (int)(sourceX);
                    var firstY = (int)(sourceY);
                    var secondX = firstX + 1;
                    var secondY = firstY + 1;

                    if (secondX != bitmap.Width && secondY != bitmap.Height)
                    {
                        Tuple<double, double> cord0 = new Tuple<double, double>(sourceX, sourceY);
                        Tuple<int, int> cord1 = new Tuple<int, int>(firstX, firstY);
                        Tuple<int, int> cord2 = new Tuple<int, int>(firstX, secondY);
                        Tuple<int, int> cord3 = new Tuple<int, int>(secondX, firstY);
                        Tuple<int, int> cord4 = new Tuple<int, int>(secondX, secondY);

                        var color1 = GetLinearInterpolationColor(bitmap, cord0, cord1, cord3);
                        var color2 = GetLinearInterpolationColor(bitmap, cord0, cord2, cord4);

                        var redValue = LinearInterpolation(cord1.Item2, cord2.Item2, cord0.Item2, color1.R, color2.R);
                        var greenValue = LinearInterpolation(cord1.Item2, cord2.Item2, cord0.Item2, color1.G, color2.G);
                        var blueValue = LinearInterpolation(cord1.Item2, cord2.Item2, cord0.Item2, color1.B, color2.B);

                        var finalColor = Color.FromArgb(redValue, greenValue, blueValue);
                        destinationBitmap.SetPixel(x, y, finalColor);
                    }
                }
            }
            return destinationBitmap;
        }

        private Color GetLinearInterpolationColor(Bitmap bitmap, Tuple<double, double> cord0, Tuple<int, int> cord1, Tuple<int, int> cord2)
        {
            var color1 = bitmap.GetPixel(cord1.Item1, cord1.Item2);
            var color2 = bitmap.GetPixel(cord2.Item1, cord2.Item2);

            var redValue = LinearInterpolation(cord1.Item1, cord2.Item1, cord0.Item1, color1.R, color2.R);
            var greenValue = LinearInterpolation(cord1.Item1, cord2.Item1, cord0.Item1, color1.G, color2.G);
            var blueValue = LinearInterpolation(cord1.Item1, cord2.Item1, cord0.Item1, color1.B, color2.B);

            return Color.FromArgb(redValue, greenValue, blueValue);
        }

        private int LinearInterpolation(int variable1, int variable2, double variable, int value1, int value2)
        {
            var coef1 = (variable2 - variable) / (variable2 - variable1);
            var coef2 = (variable - variable1) / (variable2 - variable1);

            return (int)(coef1 * value1) + (int)(coef2 * value2);
        }
    }
}
