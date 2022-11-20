using LabPictures.Algorithms;
using System.Drawing;

namespace LabPictures.Domain
{
    public class AlgorithmFabric
    {
        private BilinearInterpolation _bilinearInterpolation = new();
        private NearestNeighbor _nearestNeighbor = new();
        private ResizeX _resizeX = new();
        public AlgorithmFabric(string resizeStatusBox)
        {
            resizeStatus = resizeStatusBox;
        }

        string _resizeStatus = "";
        public string resizeStatus
        {
            get { return _resizeStatus; }
            protected set
            {
                _resizeStatus = value;
            }
        }

        public Bitmap ChooseResizeMethod(Bitmap bitmap, double X, double Y, double XY)
        {
            switch (resizeStatus)
            {
                case ("NearestNeighbor"):
                    return _nearestNeighbor.ResizeNearestNeighbor(bitmap, X, Y);
                    break;
                case ("ResizeWithX"):
                    return _resizeX.ResizeXTimes(bitmap, X, Y);
                    break;
                case ("BilinearInterpolation"):
                    return _bilinearInterpolation.ResizeBilinearInterpolation(bitmap, X, Y);
                    break;
            }
            return bitmap;
        }
    }
}
