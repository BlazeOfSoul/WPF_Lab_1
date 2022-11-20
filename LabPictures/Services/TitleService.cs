using System.ComponentModel;

namespace LabPictures.Services
{
    public class TitleService : INotifyPropertyChanged
    {
        string _status = "NearestNeighbor";
        public string Status
        {
            get { return _status; }
            protected set
            {
                _status = value;
                NotifyPropertyChanged("Status");
            }
        }
        public void NearestNeighbor()
        {
            Status = "NearestNeighbor";
            NotifyPropertyChanged("Status");
        }
        public void ResizeWithX()
        {
            Status = "ResizeWithX";
            NotifyPropertyChanged("Status");
        }

        public void Bilinear()
        {
            Status = "BilinearInterpolation";
            NotifyPropertyChanged("Status");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
