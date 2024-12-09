using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace lab5.DrawingTypes
{
    public class DrawingType1 : DrawingTypeBase
    {
        private Point _startPoint;
        private bool _isDrawing = false;
        public DrawingType1(Canvas canvas, int r, int g, int b) : base(1, canvas, r, g, b)
        {}

        public override void Subscribe()
        {
            if (_isSubscribed) return;

            _canvas.MouseLeftButtonUp += LeftMouseButtonUp;
            _canvas.MouseMove += MouseMove;
            _isSubscribed = true;
        }

        public override void Unsubscribe()
        {
            if (!_isSubscribed) return;

            _canvas.MouseLeftButtonUp -= LeftMouseButtonUp;
            _canvas.MouseMove -= MouseMove;
            _isSubscribed = false;
        }

        private void LeftMouseButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!_isDrawing)
            {
                _startPoint = e.GetPosition(_canvas);
                SolidColorBrush mySolidColorBrush = new SolidColorBrush();
                mySolidColorBrush.Color = Color.FromArgb(255, Convert.ToByte(Red), Convert.ToByte(Green), Convert.ToByte(Blue));
                _line = new Line 
                {
                    Stroke = mySolidColorBrush,
                    StrokeThickness = 2,
                    X1 = _startPoint.X,
                    Y1 = _startPoint.Y,
                    X2 = _startPoint.X,
                    Y2 = _startPoint.Y
                };

                _canvas.Children.Add(_line);
                _isDrawing = true;
                return;
            }

            LineDrawedSendEvent(this);
            _line = null;
            _isDrawing = false;
        }

        private void MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDrawing && _line != null)
            {
                Point point = e.GetPosition(_canvas);
                _line.X2 = point.X;
                _line.Y2 = point.Y;
            }
        }
    }
}
