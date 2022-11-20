using LabPictures.Services;
using LabPictures.Domain;
using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Diagnostics;

namespace LabPictures
{
public partial class MainWindow : Window
    {
        private TitleService _titleService = new();
        private FileService _fileService = new();
        private BoxInformation _boxInformation = new();
        private ImageBitmapConverter _imageConverter = new();
        private void BtnLoadFromFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == true)
            {
                Uri fileUri = new Uri(openFileDialog.FileName);
                imgDynamic.Source = new BitmapImage(fileUri);
                _fileService.initialazeInformation(imgDynamic.Source.Height, imgDynamic.Source.Width, fileUri);
            }
        }
        private void BtnResize_Click(object sender, RoutedEventArgs e) {
            try
            {
                _boxInformation.X = Convert.ToDouble(textBoxX.Text);
                _boxInformation.Y = Convert.ToDouble(textBoxY.Text);
                _boxInformation.XY = Convert.ToDouble(textBoxXY.Text);
                if(_boxInformation.X <= 0 || _boxInformation.Y <= 0 || _boxInformation.XY <= 0)
                {
                    return;
                }
            }
            catch
            {
                return;
            }

            if(_boxInformation.XY > 1)
            {
                _boxInformation.X = _boxInformation.XY;
                _boxInformation.Y = _boxInformation.XY;
            }
            
            if (imgDynamic.Source != null)
            {
                AlgorithmFabric algorithmFabric = new(_titleService.Status);
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                var image = _imageConverter.Bitmap2BitmapImage(algorithmFabric.ChooseResizeMethod
                    (_imageConverter.BitmapImage2Bitmap(_fileService.getUri()), _boxInformation.X, _boxInformation.Y, _boxInformation.XY));
                imgDynamic.Source = image;
                stopWatch.Stop();
                _fileService.initialazeInformation(image.PixelHeight, image.PixelWidth, _fileService.getUri(), stopWatch.ElapsedMilliseconds.ToString());

                SaveFileDialog dlg = new SaveFileDialog();
                dlg.FileName = "Document";
                dlg.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif";
                if (dlg.ShowDialog() == true)
                {
                    var encoder = new JpegBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(image));
                    using (var stream = dlg.OpenFile())
                    {
                        encoder.Save(stream);
                    }
                }
            }
        }
        private void menuItemNearest_Click(object sender, RoutedEventArgs e)
        {
            _titleService.NearestNeighbor();
        }
        private void menuItemWithX_Click(object sender, RoutedEventArgs e)
        {
            _titleService.ResizeWithX();
        }
        private void menuItemBilinear_Click(object sender, RoutedEventArgs e)
        {
            _titleService.Bilinear();
        }
        public MainWindow()
        {
            InitializeComponent();

            titleResize.DataContext = _titleService;
            fileInformationBlock.DataContext = _fileService;
            DataContext = _boxInformation;
        }
    }
}