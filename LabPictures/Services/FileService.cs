using System;
using System.ComponentModel;
using System.IO;
using System.Text;

namespace LabPictures.Services
{
    public class FileService : INotifyPropertyChanged
    {
        Uri _uri;
        public Uri getUri()
        {
            return _uri;
        }

        string _fileInformation = "";
        public string fileInformation
        {
            get { return _fileInformation; }
            protected set
            {
                _fileInformation = value;
                NotifyPropertyChanged("fileInformation");
            }
        }

        public void initialazeInformation(double height, double width, Uri uri)
        {
            _uri = uri;
            StringBuilder sb = new();
            sb.Append("Height: ");
            sb.Append(height);
            sb.Append("\nWidth: ");
            sb.Append(width);
            sb.Append("\nFile extension: ");
            sb.Append(splitForExtension(getUri()));
            fileInformation = sb.ToString();
            NotifyPropertyChanged("fileInformation");
        }

        public void initialazeInformation(double height, double width, Uri uri, string time)
        {
            _uri = uri;
            StringBuilder sb = new();
            sb.Append("Height: ");
            sb.Append(height);
            sb.Append("\nWidth: ");
            sb.Append(width);
            sb.Append("\nTime of work: ");
            sb.Append(time);
            sb.Append("\nFile extension: ");
            sb.Append(splitForExtension(getUri()));
            fileInformation = sb.ToString();
            NotifyPropertyChanged("fileInformation");
        }

        private string splitForExtension(Uri uri)
        {
            return Path.GetExtension(uri.ToString());
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
