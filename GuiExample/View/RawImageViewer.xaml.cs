using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms.Integration;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using FocalSpec.GuiExample.Annotations;
using FocalSpec.GuiExample.Model.Camera;
using Color = System.Drawing.Color;
using Forms = System.Windows.Forms;

namespace FocalSpec.GuiExample.View
{
    /// <summary>
    /// Interaction logic for RawImageViewer.xaml
    /// </summary>
    public partial class RawImageViewer : INotifyPropertyChanged
    {
        private BitmapSource _bitmapSource;
        private Chart _profileChart;
        private RawImage _rawImage;

        private readonly object _rawImageLock = new object();

        Dictionary<int, double> _profilePoints;

        private int _imageHeight, _linePosition;

        public int LinePosition
        {
            get => _linePosition;
            set
            {
                if (_linePosition == value) return;
                _linePosition = value;
                OnPropertyChanged();
            }
        }

        public int ImageHeight
        {
            get => _imageHeight - (int)SystemParameters.HorizontalScrollBarHeight;
            set
            {
                if (_imageHeight == value) return;
                _imageHeight = value;
                OnPropertyChanged();
            }
        }

        public RawImageViewer()
        {
            InitializeComponent();

            DataContext = this;

            InitializeProfileChart();
        }

        public void InitializeProfileChart()
        {
            if (!(FindName("ProfileHost") is WindowsFormsHost host)) return;

            _profileChart = new Chart { Name = "ProfileChart", Dock = Forms.DockStyle.Fill };

            _profileChart.Titles.Add("");
            _profileChart.Titles[0].Font = new Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, 0);

            _profileChart.Series.Add(new Series
            {
                Name = "ProfileSeries",
                ChartType = SeriesChartType.Area,
                Color = Color.DarkGray,
                BorderColor = Color.Lime
            });
            _profileChart.Series[0].XValueMember = "Key";
            _profileChart.Series[0].YValueMembers = "Value";

            _profileChart.ChartAreas.Add(new ChartArea());
            _profileChart.ChartAreas[0].AxisX.IsStartedFromZero = true;
            _profileChart.ChartAreas[0].AxisX.Minimum = 0;
            _profileChart.ChartAreas[0].AxisX.Interval = 100;
            _profileChart.ChartAreas[0].AxisY.Minimum = 0;
            _profileChart.ChartAreas[0].AxisY.Maximum = 255;
            _profileChart.ChartAreas[0].AxisY.Interval = 50;
            _profileChart.ChartAreas[0].AxisY.Title = "Intensity";
            _profileChart.ChartAreas[0].AxisY.TitleFont = new Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, 0);

            host.Child = _profileChart;
        }

        public void SetImage(RawImage rawImage)
        {
            Dispatcher?.Invoke(() =>
            {
                lock (_rawImageLock)
                {
                    _rawImage = rawImage;
                }
                _bitmapSource = BitmapSource.Create(rawImage.Width, rawImage.Height, 96, 96, PixelFormats.Indexed8, BitmapPalettes.Gray256, rawImage.Image, rawImage.Width);
                RawImagePicture.Source = _bitmapSource;

                if (rawImage.Flipped)
                {
                	RawImagePicture.RenderTransform = new RotateTransform(180);
                }
            });
        }

        public Bitmap GetLatestImage()
        {
            var bitmap = new Bitmap(_bitmapSource.PixelWidth, _bitmapSource.PixelHeight, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
            ColorPalette palette = bitmap.Palette;
            for (int i = 0; i < 256; i++)
                palette.Entries[i] = Color.FromArgb(255, i, i, i);
            bitmap.Palette = palette;
            var rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            BitmapData data = bitmap.LockBits(rect, ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
            _bitmapSource.CopyPixels(Int32Rect.Empty, data.Scan0, data.Height * data.Stride, data.Stride);
            bitmap.UnlockBits(data);
            if (_rawImage.Flipped)
                bitmap.RotateFlip(RotateFlipType.Rotate180FlipNone);
            return bitmap;
        }

        private void RawImageViewer_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            ImageHeight = (int)GridRoot.RowDefinitions[0].ActualHeight;
        }

        private void RawImagePicture_OnMouseMove(object sender, MouseEventArgs e)
        {
            int x = (int) e.GetPosition(RawImagePicture).X;

            int dpiOffset = ProfileGrid.ActualWidth > _rawImage.Width ? (int) (ProfileGrid.ActualWidth - _rawImage.Width) / 2 : 0;

            if(_rawImage.Flipped)
                LinePosition = _rawImage.Width -  x - (int)RawImageScroll.HorizontalOffset + dpiOffset;
            else
                LinePosition = x - (int)RawImageScroll.HorizontalOffset + dpiOffset;
            UpdateProfileChart(x);
        }

        private void UpdateProfileChart(int x)
        {
            if (_profileChart == null) return;
            if (x < 0 || x >= _rawImage.Width)
            {
                if (_profilePoints == null)
                    return;
                _profilePoints.Clear();
                _profileChart.Titles[0].Text = "";
                return;
            }

            _profilePoints = new Dictionary<int, double>();
            lock (_rawImageLock)
            {
                for (var y = 0; y < _rawImage.Height; y++)
                {
                    int newX = _rawImage.Flipped ? x + (_rawImage.Height - y - 1) * _rawImage.Width : x + y * _rawImage.Width;
                    _profilePoints.Add(y, _rawImage.Image[newX]);
                }
            }

            var position = _rawImage.Flipped ? _rawImage.Width - x : x;
            _profileChart.Titles[0].Text = position.ToString();
            _profileChart.DataSource = _profilePoints;
            _profileChart.ChartAreas[0].AxisX.Maximum = _rawImage.Height;

            _profileChart.Invalidate();
        }

        #region [ INotifyPropertyChanged Members ]

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion [/ INotifyPropertyChanged Members ]
    }
}
