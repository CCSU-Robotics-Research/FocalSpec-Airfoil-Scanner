using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using FocalSpec.GuiExample.Annotations;
using FocalSpec.GuiExample.Model.BatchMode;
using FocalSpec.GuiExample.Model.Camera;

namespace FocalSpec.GuiExample.View
{
    /// <summary>
    /// Interaction logic for BatchVisualizer2DUc.xaml
    /// </summary>
    public partial class BatchVisualizer2DUc : INotifyPropertyChanged
    {
        public BatchVisualizer2DUc()
        {
            ScaleY = 1;
            ScaleX = 1;
            InitializeComponent();
        }

        private readonly double _dpiX = 96d;
        private readonly double _dpiY = 96d;
        private readonly double _zoomFactor = 1.25;
        private readonly double _minZoomLevel = 0.001;
        private readonly double _maxZoomLevel = 50;
        private readonly double _minDotsPerUm = 264.58;
        private double _zoomRatio = 1;
        private Point _lastMousePosition;
        private Rect _displayedArea;
        private int _originalHeight;
        private int _originalWidth;
        private Point _pointerLocation;
        private bool _zoomNeeded;
        Point _startPoint, _endPoint;
        private bool _useZoomStepRounding;

        public WriteableBitmap MainWriteableBitmap { get; set; }

        private BitmapPalette _defaultBitmapPalette;

        public BitmapPalette DefaultBitmapPalette
        {
            get
            {
                if (_defaultBitmapPalette == null)
                    return BitmapPalettes.WebPaletteTransparent;
                return _defaultBitmapPalette;
            }
            set { _defaultBitmapPalette = value; }
        }

        /// <summary>
        /// Draws a batch.
        /// </summary>
        /// <param name="recordContainer">Batch data.</param>
        /// <param name="batchConf">Batch definitions.</param>
        /// <param name="pixelWidth">Average sensor pixel width [mm].</param>
        /// <param name="sensorWidth"></param>
        /// <param name="layer">Layer id.</param>
        /// <param name="uiWindowHeight"></param>
        public void ShowScan(RecordContainer recordContainer, BatchConfiguration batchConf, double pixelWidth, int sensorWidth, int layer, int uiWindowHeight)
        {
            pixelWidth *= 1000.0d;

            var uiWindowMax = (1000.0 * uiWindowHeight / 2);
            var uiWindowMin = (- 1000.0 * uiWindowHeight / 2);

            var profiles = recordContainer.GetProfiles(layer);

            if (profiles == null || profiles.Count <= 0) return;

            int imageSize = profiles.Count * sensorWidth;
            int profileIndex = 0;

            var imageR = new byte[imageSize];
            var imageG = new byte[imageSize];
            var imageB = new byte[imageSize];

            recordContainer.GetProfilesMinAndMaxZ(out var minZ, out var maxZ);

            if (minZ < uiWindowMin)
                minZ = uiWindowMin;

            if (maxZ > uiWindowMax)
                maxZ = uiWindowMax;

            foreach (Profile profile in profiles)
            {
                if (profile.LayerId < 0)
                {
                    for (int i = 0; i < profile.Points.Count; i++)
                    {
                        int pixelIndex = profileIndex * sensorWidth + Convert.ToInt32(Math.Round(profile.Points[i].X / pixelWidth));

                        if (pixelIndex < 0 || pixelIndex >= imageSize) continue;

                        float z = profile.Points[i].Y;

                        ColorConverter.HsvToRgb((1 - (z - minZ) / (maxZ - minZ)) * byte.MaxValue, out imageR[pixelIndex], out imageG[pixelIndex],
                            out imageB[pixelIndex]);
                    }
                }
                else
                {
                    for (int i = 0; i < profile.LineLength; i++)
                    {
                        // skip no measurement values
                        if (profile.ZValues[i] > maxZ)
                            continue;
                        int pixelIndex = profileIndex * sensorWidth + Convert.ToInt32(Math.Round((float)(i * profile.XStep) / pixelWidth));

                        if (pixelIndex < 0 || pixelIndex >= imageSize) continue;

                        double z = profile.ZValues[i];
                        ColorConverter.HsvToRgb((1 - (z - minZ) / (maxZ - minZ)) * byte.MaxValue, out imageR[pixelIndex], out imageG[pixelIndex], out imageB[pixelIndex]);
                    }
                }
                profileIndex++;
            }

            SetImage(imageR, imageG, imageB, sensorWidth, profiles.Count, pixelWidth, batchConf.ScanStepLength * 1000);
        }

        private unsafe void SetImage(byte[] abyRed, byte[] abyGreen, byte[] abyBlue, int iWidth, int iHeight, double resolutionX, double resolutionY)
        {
            MainWriteableBitmap = new WriteableBitmap(iWidth, iHeight, _dpiX, _dpiY, PixelFormats.Bgra32, BitmapPalettes.WebPaletteTransparent);

            ImageMain.Source = MainWriteableBitmap;

            ImageMain.Width = iWidth;
            ImageMain.Height = iHeight;

            OriginalWidth = iWidth;
            OriginalHeight = iHeight;

            MainWriteableBitmap.Lock();

            byte[] aby = new byte[iWidth * iHeight * 4];

            fixed (byte* abyArray = &aby[0])
            {
                for (int iY = 0; iY < iHeight; iY++)
                {
                    for (int iX = 0; iX < iWidth; iX++)
                    {
                        int channelOffset = iX + iY * iWidth;
                        int offset = 4 * channelOffset;
                        byte* row = &abyArray[offset];

                        aby[4 * (iX + iY * iWidth) + 0] = abyBlue[iX + iY * iWidth];
                        aby[4 * (iX + iY * iWidth) + 1] = abyGreen[iX + iY * iWidth];
                        aby[4 * (iX + iY * iWidth) + 2] = abyRed[iX + iY * iWidth];
                        aby[4 * (iX + iY * iWidth) + 3] = byte.MaxValue;

                        *(row++) = abyBlue[channelOffset];
                        *(row++) = abyGreen[channelOffset];
                        *(row++) = abyRed[channelOffset];
                        // Alpha channel is set to fixed 255.
                        *(row) = 255;
                    }
                }
            }

            MainWriteableBitmap.WritePixels(new Int32Rect(0, 0, iWidth, iHeight), aby, iWidth * 4, 0); //4 is 32bit RGB, 3 is 24bit RGB

            SetResolution(resolutionX, resolutionY);

            MainWriteableBitmap.Unlock();

            SetDisplayedArea();
        }

        public void ClearImage()
        {
            MainWriteableBitmap = new WriteableBitmap(1, 1, _dpiX, _dpiY, PixelFormats.Bgra32, BitmapPalettes.WebPaletteTransparent);

            ImageMain.Source = MainWriteableBitmap;

            ImageMain.Width = 0;
            ImageMain.Height = 0;

            OriginalWidth = 0;
            OriginalHeight = 0;

            SetResolution(0, 0);

            SetDisplayedArea();
        }

        /// <summary>
        /// Sets the resolution of the image and scales image accordingly.
        /// </summary>
        /// <param name="dotsPerUmX">Dots / µm for X-axis.</param>
        /// <param name="dotsPerUmY">Dots / µm for Y-axis.</param>
        private void SetResolution(double dotsPerUmX, double dotsPerUmY)
        {
            if (dotsPerUmX <= 0)
                dotsPerUmX = _minDotsPerUm;
            if (dotsPerUmY <= 0)
                dotsPerUmY = _minDotsPerUm;

            if (dotsPerUmX >= dotsPerUmY)
            {
                ScaleX = dotsPerUmX / dotsPerUmY;
                ScaleY = 1;
            }
            else
            {
                ScaleX = 1;
                ScaleY = dotsPerUmY / dotsPerUmX;
            }

            ZoomToFit();

            // ReSharper disable ExplicitCallerInfoArgument
            OnPropertyChanged("ResolutionX");
            OnPropertyChanged("ResolutionY");
            OnPropertyChanged("IsFullyVisible");
            OnPropertyChanged("IsFullyVisibleHorizontally");
            OnPropertyChanged("IsFullyVisibleVertically");
            OnPropertyChanged("ViewedArea");
            // ReSharper restore ExplicitCallerInfoArgument
        }

        public double ScaleY { get; set; }

        public double ScaleX { get; set; }

        public double ZoomRatio
        {
            get => _zoomRatio;
            set
            {
                if (double.IsNaN(value) || value <= 0) return;

                _zoomRatio = Math.Round(value, 5, MidpointRounding.ToEven);

                if (value < _minZoomLevel)
                    _zoomRatio = _minZoomLevel;
                else if (value > _maxZoomLevel)
                    _zoomRatio = _maxZoomLevel;

                ZoomCanvas.Scale = _zoomRatio;
                var scaleTransform = (ScaleTransform)((TransformGroup)ZoomCanvas.RenderTransform)
                    .Children.First(tr => tr is ScaleTransform);

                scaleTransform.ScaleX = ScaleX * _zoomRatio;
                scaleTransform.ScaleY = ScaleY * _zoomRatio;

                SetDisplayedArea();

                OnPropertyChanged();
                // ReSharper disable ExplicitCallerInfoArgument
                OnPropertyChanged("IsFullyVisible");
                OnPropertyChanged("IsFullyVisibleHorizontally");
                OnPropertyChanged("IsFullyVisibleVertically");
                OnPropertyChanged("ViewedArea");
                // ReSharper restore ExplicitCallerInfoArgument
            }
        }

        public Rect DisplayedArea
        {
            get => _displayedArea;
            set
            {
                if (value.Equals(_displayedArea)) return;
                _displayedArea = value;
                OnPropertyChanged();
            }
        }

        public int OriginalHeight
        {
            get => _originalHeight;
            set
            {
                if (value == _originalHeight) return;
                _originalHeight = value;
                OnPropertyChanged();
                // ReSharper disable once ExplicitCallerInfoArgument
                OnPropertyChanged("IsFullyVisibleVertically");
            }
        }

        public int OriginalWidth
        {
            get => _originalWidth;
            set
            {
                if (value == _originalWidth) return;
                _originalWidth = value;
                OnPropertyChanged();
                // ReSharper disable once ExplicitCallerInfoArgument
                OnPropertyChanged("IsFullyVisibleHorizontally");
            }
        }

        /// <summary>
        /// Gets a value indicating whether the image displayed by image viewer is fully visible.
        /// </summary>
        public bool IsFullyVisible => IsFullyVisibleHorizontally && IsFullyVisibleVertically;

        /// <summary>
        /// Gets a value indicating whether the image displayed by image viewer is fully visible horizontally.
        /// </summary>
        public bool IsFullyVisibleHorizontally => OriginalWidth * ScaleX * ZoomRatio <= ZoomCanvas.ActualWidth;

        /// <summary>
        /// Gets a value indicating whether the image displayed by image viewer is fully visible vertically.
        /// </summary>
        public bool IsFullyVisibleVertically => OriginalHeight * ScaleY * ZoomRatio <= ZoomCanvas.ActualHeight;

        public void ZoomCanvas_OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
                ZoomInToCursor();
            else
                ZoomOutOfCursor();
        }

        protected void ZoomCanvas_OnMouseMove(object sender, MouseEventArgs e)
        {
            var position = e.GetPosition(this);

            PointerLocation = e.GetPosition(ImageMain);

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                ZoomCanvas.Offset -= position - _lastMousePosition;
            }

            _lastMousePosition = position;
        }

        public Point PointerLocation
        {
            get => _pointerLocation;
            set
            {
                if (value.Equals(_pointerLocation)) return;
                _pointerLocation = value;
                OnPropertyChanged();
            }
        }

        public void ZoomToFit()
        {
            ZoomTo(new Point(0, 0), new Point(OriginalWidth, OriginalHeight), false);
            CenterCanvas();
        }

        public void ZoomTo(Point startPoint, Point endPoint, bool useZoomStepRounding = true)
        {
            if (Math.Abs(BorderForZoomableCanvas.ActualWidth) <= 1 || Math.Abs(BorderForZoomableCanvas.ActualHeight) <= 1)
            {
                _zoomNeeded = true;
                _startPoint = startPoint;
                _endPoint = endPoint;
                _useZoomStepRounding  = useZoomStepRounding;
                return;
            }

            ZoomToPoints(startPoint, endPoint, useZoomStepRounding);
        }

        private void ZoomToPoints(Point startPoint, Point endPoint, bool useZoomStepRounding)
        { 
            double scaleRatioWidth = BorderForZoomableCanvas.ActualWidth / (endPoint.X - startPoint.X) / ScaleX;
            double scaleRatioHeight = BorderForZoomableCanvas.ActualHeight / (endPoint.Y - startPoint.Y) / ScaleY;

            ZoomRatio = useZoomStepRounding
                ? Math.Round(Math.Min(scaleRatioWidth, scaleRatioHeight), 3) - _minZoomLevel
                : Math.Min(scaleRatioWidth, scaleRatioHeight) - _minZoomLevel;

            if (IsFullyVisible)
            {
                PanTo(startPoint);
                return;
            }

            PanToCenterPoint(new Point(startPoint.X + (endPoint.X - startPoint.X) / 2, startPoint.Y + (endPoint.Y - startPoint.Y) / 2));
        }

        public void PanTo(Point offset)
        {
            ZoomCanvas.Offset = new Point(offset.X * ScaleX * ZoomRatio, offset.Y * ScaleY * ZoomRatio);
            AdjustZoomCanvasViewArea();
        }

        public void PanToCenterPoint(Point centerPoint)
        {
            double offsetX = centerPoint.X - (ZoomCanvas.ActualViewbox.Width / ScaleX / ZoomRatio / 2);
            double offsetY = centerPoint.Y - (ZoomCanvas.ActualViewbox.Height / ScaleY / ZoomRatio / 2);

            ZoomCanvas.Offset = new Point(offsetX * ScaleX * ZoomRatio, offsetY * ScaleY * ZoomRatio);
            AdjustZoomCanvasViewArea();
        }

        private void AdjustZoomCanvasViewArea()
        {
            if (!IsFullyVisibleHorizontally)
            {
                if (ZoomCanvas.Offset.X < 0)
                    ZoomCanvas.Offset = new Point(0, ZoomCanvas.Offset.Y);
                if (ZoomCanvas.Offset.X > OriginalWidth * ScaleX * ZoomRatio - ZoomCanvas.ActualWidth)
                    ZoomCanvas.Offset = new Point(OriginalWidth * ScaleX * ZoomRatio - ZoomCanvas.ActualWidth, ZoomCanvas.Offset.Y);
            }

            if (!IsFullyVisibleVertically)
            {
                if (ZoomCanvas.Offset.Y < 0)
                    ZoomCanvas.Offset = new Point(ZoomCanvas.Offset.X, 0);
                if (ZoomCanvas.Offset.Y > OriginalHeight * ScaleY * ZoomRatio - ZoomCanvas.ActualHeight)
                    ZoomCanvas.Offset = new Point(ZoomCanvas.Offset.X, OriginalHeight * ScaleY * ZoomRatio - ZoomCanvas.ActualHeight);
            }

            CenterCanvas(IsFullyVisibleHorizontally, IsFullyVisibleVertically);

            SetDisplayedArea();
        }

        private void CenterCanvas(bool centerHorizontally = true, bool centerVertically = true)
        {
            var horizontalOffset = ZoomCanvas.Offset.X;
            var verticalOffset = ZoomCanvas.Offset.Y;

            if (centerHorizontally)
                horizontalOffset = ((OriginalWidth * ScaleX * ZoomRatio) - ZoomCanvas.ActualWidth) / 2;
            if (centerVertically)
                verticalOffset = ((OriginalHeight * ScaleY * ZoomRatio) - ZoomCanvas.ActualHeight) / 2;

            ZoomCanvas.Offset = new Point(horizontalOffset, verticalOffset);
            SetDisplayedArea();
        }

        public void ZoomIn()
        {
            var actualViewArea = new Rect(new Point(ZoomCanvas.ActualViewbox.Left / ZoomRatio / ScaleX,
                                                    ZoomCanvas.ActualViewbox.Top / ZoomRatio / ScaleY),
                                          new Point(ZoomCanvas.ActualViewbox.Right / ZoomRatio / ScaleX,
                                                    ZoomCanvas.ActualViewbox.Bottom / ZoomRatio / ScaleY));

            ZoomIn(new Point(actualViewArea.TopLeft.X + ((actualViewArea.BottomRight.X - actualViewArea.TopLeft.X) / 2),
                                  actualViewArea.TopLeft.Y + ((actualViewArea.BottomRight.Y - actualViewArea.TopLeft.Y) / 2)));
        }

        public void ZoomIn(Point zoomCenterPoint)
        {
            ZoomRatio = ZoomRatio * _zoomFactor;
            PanToCenterPoint(zoomCenterPoint);
        }

        public void ZoomInToCursor()
        {
            var previousZoomRatio = ZoomRatio;
            var newZoomRatio = ZoomRatio * _zoomFactor;
            var zoomStep = newZoomRatio - previousZoomRatio;

            var actualViewArea = new Rect(new Point(ZoomCanvas.ActualViewbox.Left / previousZoomRatio / ScaleX,
                                                    ZoomCanvas.ActualViewbox.Top / previousZoomRatio / ScaleY),
                                          new Point(ZoomCanvas.ActualViewbox.Right / previousZoomRatio / ScaleX,
                                                    ZoomCanvas.ActualViewbox.Bottom / previousZoomRatio / ScaleY));

            if (Math.Abs(newZoomRatio - previousZoomRatio) < _minZoomLevel) return;

            var position = (Vector)PointerLocation;

            double cursorRelationX = position.X - actualViewArea.Left;
            double cursorRelationY = position.Y - actualViewArea.Top;

            var newOffset = new Point(
                (actualViewArea.Left + (cursorRelationX / newZoomRatio) * zoomStep) * newZoomRatio * ScaleX,
                (actualViewArea.Top + (cursorRelationY / newZoomRatio) * zoomStep) * newZoomRatio * ScaleY);

            ZoomCanvas.Offset = newOffset;
            ZoomRatio = newZoomRatio;

            AdjustZoomCanvasViewArea();
        }

        public void ZoomOut()
        {
            var actualViewArea = new Rect(new Point(ZoomCanvas.ActualViewbox.Left / ZoomRatio / ScaleX,
                                                    ZoomCanvas.ActualViewbox.Top / ZoomRatio / ScaleY),
                                          new Point(ZoomCanvas.ActualViewbox.Right / ZoomRatio / ScaleX,
                                                    ZoomCanvas.ActualViewbox.Bottom / ZoomRatio / ScaleY));

            ZoomOut(new Point(actualViewArea.TopLeft.X + ((actualViewArea.BottomRight.X - actualViewArea.TopLeft.X) / 2),
                                  actualViewArea.TopLeft.Y + ((actualViewArea.BottomRight.Y - actualViewArea.TopLeft.Y) / 2)));
        }

        public void ZoomOut(Point zoomCenterPoint)
        {
            ZoomRatio = ZoomRatio / _zoomFactor;
            PanToCenterPoint(zoomCenterPoint);

            if (!IsFullyVisible) return;

            ZoomToFit();
        }

        public void ZoomOutOfCursor()
        {
            var previousZoomRatio = ZoomRatio;
            var newZoomRatio = ZoomRatio / _zoomFactor;
            var zoomStep = newZoomRatio - previousZoomRatio;

            var actualViewArea = new Rect(new Point(ZoomCanvas.ActualViewbox.Left / previousZoomRatio / ScaleX,
                                                    ZoomCanvas.ActualViewbox.Top / previousZoomRatio / ScaleY),
                                          new Point(ZoomCanvas.ActualViewbox.Right / previousZoomRatio / ScaleX,
                                                    ZoomCanvas.ActualViewbox.Bottom / previousZoomRatio / ScaleY));

            if (Math.Abs(newZoomRatio - previousZoomRatio) < _minZoomLevel) return;

            var position = (Vector)PointerLocation;

            double cursorRelationX = position.X - actualViewArea.Left;
            double cursorRelationY = position.Y - actualViewArea.Top;

            var newOffset = new Point(
                (actualViewArea.Left + (cursorRelationX / newZoomRatio) * zoomStep) * newZoomRatio * ScaleX,
                (actualViewArea.Top + (cursorRelationY / newZoomRatio) * zoomStep) * newZoomRatio * ScaleY);

            ZoomCanvas.Offset = newOffset;
            ZoomRatio = newZoomRatio;

            AdjustZoomCanvasViewArea();

            if (!IsFullyVisible) return;

            ZoomToFit();
        }

        private void SetDisplayedArea()
        {
            DisplayedArea = new Rect(new Point(ZoomCanvas.ActualViewbox.Left / ZoomRatio / ScaleX,
                                                    ZoomCanvas.ActualViewbox.Top / ZoomRatio / ScaleY),
                                          new Point(ZoomCanvas.ActualViewbox.Right / ZoomRatio / ScaleX,
                                                    ZoomCanvas.ActualViewbox.Bottom / ZoomRatio / ScaleY));
        }

        private void ButtonZoomOut_OnClick(object sender, RoutedEventArgs e)
        {
            ZoomOut();
        }

        private void ButtonZoomIn_OnClick(object sender, RoutedEventArgs e)
        {
            ZoomIn();
        }

        private void ButtonZoomToFit_OnClick(object sender, RoutedEventArgs e)
        {
            ZoomToFit();
        }

        #region [ INotifyPropertyChanged Members ]

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion [/ INotifyPropertyChanged Members ]

        private void BatchVisualizer2DUc_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (UniformGridControls.ActualHeight < ActualHeight)
            {
                BorderForZoomableCanvas.Height = ActualHeight - UniformGridControls.ActualHeight;
            }
            BorderForZoomableCanvas.Width = ActualWidth;
        }

        private void BatchVisualizer2DUc_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (_zoomNeeded && ActualHeight > 1 && ActualWidth > 1)
            {
                ZoomToPoints(_startPoint, _endPoint, _useZoomStepRounding);
                _zoomNeeded = false;
            }
        }
    }
}
