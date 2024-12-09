using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace lab5
{
    public partial class ColorPickWindow : Window
    {
        private int Red { get; set; }
        private int Green { get; set; }
        private int Blue { get; set; }

        public ColorPickWindow(int r, int g, int b)
        {
            Red = r;
            Green = g;
            Blue = b;
            InitializeComponent();

            TextBoxRed.Text = Red.ToString();
            TextBoxGreen.Text = Green.ToString();
            TextBoxBlue.Text = Blue.ToString();
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");

            e.Handled = regex.IsMatch(e.Text);
        }

        private void TextBoxBlue_TextChanged(object sender, TextChangedEventArgs e)
        {
            Blue = Convert.ToInt32(TextBoxBlue.Text);
        }

        private void TextBoxGreen_TextChanged(object sender, TextChangedEventArgs e)
        {
            Green = Convert.ToInt32(TextBoxGreen.Text);
        }

        private void TextBoxRed_TextChanged(object sender, TextChangedEventArgs e)
        {
            Red = Convert.ToInt32(TextBoxRed.Text);
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            if (Red < 0 || Red > 255 || Blue < 0 || Blue > 255 || Green < 0 || Green > 255)
            {
                MessageBox.Show("Значения могут быть только в диапазоне 0-255");
                return;
            }

            MainWindow? window = this.Owner as MainWindow;

            if (window != null)
            {
                window.SetNewColors(Red, Green, Blue);
            }
            this.Close();
        }
    }
}
