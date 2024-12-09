using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace lab5.DrawingTypes
{
    public class DrawingType3 : DrawingTypeBase
    {
        private List<Point> _points = new List<Point>();
        private List<Ellipse> _ellipses = new List<Ellipse>();

        public override void Subscribe()
        {
            if (_isSubscribed) return;
            _canvas.MouseLeftButtonDown += LeftMouseButtonDown;
            _canvas.MouseRightButtonDown += RightMouseButtonDown;
        
            _isSubscribed = true;
        }

        public override void Unsubscribe()
        {
            if (!_isSubscribed) return;
            _canvas.MouseLeftButtonDown -= LeftMouseButtonDown;
            _canvas.MouseRightButtonDown -= RightMouseButtonDown;

            _isSubscribed = false;
        }

        public DrawingType3(Canvas canvas, int r, int g, int b) : base(3, canvas, r, g, b) { }

        private void LeftMouseButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point point = e.GetPosition(_canvas);
            Ellipse myEllipse = new Ellipse();
            SolidColorBrush mySolidColorBrush = new SolidColorBrush();
            mySolidColorBrush.Color = Color.FromArgb(255, Convert.ToByte(Red), Convert.ToByte(Green), Convert.ToByte(Blue));
            myEllipse.Fill = mySolidColorBrush;
            myEllipse.StrokeThickness = 2;
            myEllipse.Stroke = mySolidColorBrush;
            myEllipse.Width = 4;
            myEllipse.Height = 4;

            Canvas.SetLeft(myEllipse, point.X);
            Canvas.SetTop(myEllipse, point.Y);

            _points.Add(point);
            _canvas.Children.Add(myEllipse);
            _ellipses.Add(myEllipse);
        }

        private void RightMouseButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point prevPoint = _points.First();
            _points.Remove(prevPoint);
            SolidColorBrush mySolidColorBrush = new SolidColorBrush();
            mySolidColorBrush.Color = Color.FromArgb(255, Convert.ToByte(Red), Convert.ToByte(Green), Convert.ToByte(Blue));
            foreach (Point point in _points)
            {
                _line = new Line
                {
                    Stroke = mySolidColorBrush,
                    StrokeThickness = 2,
                    X1 = prevPoint.X,
                    Y1 = prevPoint.Y,
                    X2 = point.X,
                    Y2 = point.Y
                };

                _canvas.Children.Add(_line);
                LineDrawedSendEvent(this);
                _line = null;
                prevPoint = point;
            }

            _points.Clear();

            foreach (Ellipse elipse in _ellipses)
            {
                _canvas.Children.Remove(elipse);
            }

            _ellipses.Clear();
        }
    }
}
