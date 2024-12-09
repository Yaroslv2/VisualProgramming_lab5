using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace lab5.DrawingTypes
{
    public class DrawingType2 : DrawingTypeBase
    {
        public DrawingType2(Canvas canvas, int r, int g, int b) : base(2, canvas, r, g, b)
        {}

        public override void Subscribe()
        {
            if (_isSubscribed) return;

            _canvas.MouseLeftButtonDown += LeftMouseButtonDown;
            _canvas.MouseLeftButtonUp += LeftMouseButtonUp;
            _canvas.MouseMove += MouseMove;
            _isSubscribed = true;
        }

        public override void Unsubscribe()
        {
            if (!_isSubscribed) return;
            
            _canvas.MouseLeftButtonDown -= LeftMouseButtonDown;
            _canvas.MouseLeftButtonUp -= LeftMouseButtonUp;
            _canvas.MouseMove -= MouseMove;
            
            _isSubscribed = false;
        }

        private void LeftMouseButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point point = e.GetPosition(_canvas);
            SolidColorBrush mySolidColorBrush = new SolidColorBrush();
            mySolidColorBrush.Color = Color.FromArgb(255, Convert.ToByte(Red), Convert.ToByte(Green), Convert.ToByte(Blue));
            _line = new Line
            {
                Stroke = mySolidColorBrush,
                StrokeThickness = 2,
                X1 = point.X,
                Y1 = point.Y,
                X2 = point.X,
                Y2 = point.Y
            };

            _canvas.Children.Add(_line);
        }

        private void MouseMove(object sender, MouseEventArgs e) 
        {
            if (_line == null || e.LeftButton != MouseButtonState.Pressed)
                return;

            Point point = e.GetPosition(_canvas);
            _line.X2 = point.X;
            _line.Y2 = point.Y;
        }

        private void LeftMouseButtonUp(object sender, MouseButtonEventArgs e)
        {
            LineDrawedSendEvent(this);
            _line = null;
        }
    }
}
