using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace lab5.DrawingTypes
{
    public abstract  class DrawingTypeBase
    {
        protected Canvas _canvas;
        protected Line? _line = null;
        protected bool _isSubscribed = false;
        protected int Red { get; set; }
        protected int Green { get; set; }
        protected int Blue { get; set; }
        public int Id { get; }

        public event EventHandler<Line> LineDrawed;
        protected DrawingTypeBase(int id, Canvas canvas, int r, int g, int b) 
        { 
            Id = id; 
            _canvas = canvas;
            Red = r; Green = g; Blue = b;
        }

        public void UpdateColors(int r, int g, int b)
        {
            Red = r; Green = g; Blue = b;
        }

        public abstract void Subscribe();
        public abstract void Unsubscribe();

        protected void LineDrawedSendEvent(object sender) => LineDrawed?.Invoke(sender, _line);
    }
}
