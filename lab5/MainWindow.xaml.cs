using lab5.DrawingTypes;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace lab5
{
    public partial class MainWindow : Window
    {
        private readonly string _typeTextSourse = "Используемый метод рисования: ";

        public int Red { 
            get; 
            set; } = 0;
        public int Green { get; set; } = 0;
        public int Blue { get; set; } = 0;

        private List<Line> _lines = new List<Line>();
        private Line? _selectedLine = null;
        private Line? SelectedLine
        {
            get { return _selectedLine; }
            set { CustomizeSelectedLine(false); _selectedLine = value; CustomizeSelectedLine(true); } 
        }

        private DrawingTypes.DrawingTypeBase activeDrawingType;
        private bool _isSelectingMode = false;

        public MainWindow()
        {
            InitializeComponent();
            activeDrawingType = new DrawingTypes.DrawingType1(MainCanvas, Red, Green, Blue);
            activeDrawingType.Subscribe();
            activeDrawingType.LineDrawed += LineDrawedHandler;

            SelectedTypeText.Text = _typeTextSourse + activeDrawingType.Id.ToString();
        }

        private void LineDrawedHandler(object sender, Line line)
        {
            _lines.Add(line);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                    break;
                case Key.D0:
                    SetSelectingMode();
                    break;
                case Key.D1:
                    UpdateDrawingType(1);
                    break;
                case Key.D2:
                    UpdateDrawingType(2);
                    break;
                case Key.D3:
                    UpdateDrawingType(3);
                    break;
                default:
                    if (_isSelectingMode)
                        ProcessSelectingMode(e.Key);
                    break;
            }
        }

        private void CustomizeSelectedLine(bool isSelected)
        {
            if (SelectedLine == null) return;

            if (isSelected)
            {
                _selectedLine.StrokeThickness = 4;
            }
            else
            {
                _selectedLine.StrokeThickness = 2;
            }
        }

        private void ProcessSelectingMode(Key key)
        {
            int idx = _lines.IndexOf(SelectedLine!);
            if (idx == -1) return;
            switch (key)
            {
                case Key.N:
                    SelectedLine = _lines[(idx + 1) % _lines.Count];
                    break;
                case Key.D:
                    _lines.Remove(SelectedLine);
                    MainCanvas.Children.Remove(SelectedLine);
                    SelectedLine = null;
                    if (_lines.Count == 0) return;
                    SelectedLine = _lines[(idx + 1) % _lines.Count];
                    break;
            }
        }

        private void SetSelectingMode()
        {
            if (_isSelectingMode) return;

            activeDrawingType.Unsubscribe();
            _isSelectingMode = true;
            SelectedTypeText.Text = "Режим выбора";

            if (_lines.Count > 0)
                SelectedLine = _lines[0];
        }

        private void DisableSelectingMode()
        {
            if (!_isSelectingMode) return;

            _isSelectingMode = false;

            CustomizeSelectedLine(false);
            SelectedLine = null;

            activeDrawingType.Subscribe();

            SelectedTypeText.Text = _typeTextSourse + activeDrawingType.Id.ToString();
        }



        private void UpdateDrawingType(int type)
        {
            DisableSelectingMode();
            if (activeDrawingType.Id == type)
                return;


            activeDrawingType.Unsubscribe();
            switch (type)
            {
                case 1:
                    activeDrawingType = new DrawingType1(MainCanvas, Red, Green, Blue);
                    break;
                case 2:
                    activeDrawingType = new DrawingType2(MainCanvas, Red, Green, Blue);
                    break;
                case 3:
                    activeDrawingType = new DrawingType3(MainCanvas, Red, Green, Blue);
                    break;
            }
            activeDrawingType.Subscribe();
            activeDrawingType.LineDrawed += LineDrawedHandler;
            SelectedTypeText.Text = _typeTextSourse + activeDrawingType.Id.ToString();
        }

        public void SetNewColors(int r, int g, int b)
        {
            Red = r; Green = g; Blue = b;

            activeDrawingType.UpdateColors(r, g, b);
        }

        private void ColorPicker_Click(object sender, RoutedEventArgs e)
        {
            ColorPickWindow window = new ColorPickWindow(Red, Green, Blue);
            window.Owner = this;
            window.Show();
        }
    }
}
