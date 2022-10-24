using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace color_game
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string[] _colors = { "red", "green", "blue", "yellow" };
        private List<string> _randColors;
        private string _pickedColor;
        private Random _random;
        private int _score;
        public MainWindow()
        {
            InitializeComponent();
            Init();
            StartGame();
        }

        private void Init()
        {
            _random = new Random();
            _randColors = new List<string>();
            _score = 0;
            scoreLabel.Content = $"Score: {_score}";
        }

        private void StartGame()
        {
            int randNum1 = _random.Next(_colors.Length);
            int randNum2 = _random.Next(_colors.Length);
            string fakeLabelText = _colors[randNum2];

            _pickedColor = _colors[randNum1];
            colorDisplay.Foreground = PaintColor(_pickedColor);

            //if (fakeLabelText == _pickedColor) return;
            colorDisplay.Content = fakeLabelText;
            GenerateButtons();
        }

        private void GenerateButtons()
        {
            UpdateRandColors();
            for (int i = 0; i < _randColors.Count; i++)
            {
                Button btn = new Button();
                btn.Width = 200;
                btn.Height = 120;
                btn.BorderBrush = new SolidColorBrush(Colors.Transparent);
                btn.Background = PaintColor(_randColors[i]);
                btn.Tag = _randColors[i];
                btn.Click += Btn_Click;
                gridBtns.Children.Add(btn);
            }
            
        }

        private void ClearAllButtons()
        {
            gridBtns.Children.Clear();
            _randColors.Clear();
        }

        private void UpdateRandColors()
        {
            int filledColors = 0;
            while (filledColors < _colors.Length)
            {
                string pickedColor = _colors[_random.Next(_colors.Length)];
                if (_randColors.Contains(pickedColor)) continue;
                _randColors.Add(pickedColor);
                filledColors++;
            }
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            if(sender is Button)
            {
                Button btn = (Button)sender;
                string btnTag = btn.Tag.ToString();
                if(btnTag == _pickedColor)
                {
                    RefreshScore();
                }
                else
                {
                    MessageBox.Show($"You Lost :( \nYour score was {_score}", "You lost :(", MessageBoxButton.OK, MessageBoxImage.Error);
                    ResetScore();

                }
                ClearAllButtons();
                StartGame();
            }
        }

        private void RefreshScore()
        {
            _score++;
            scoreLabel.Content = $"Score: {_score}";
            scoreLabel.Foreground = new SolidColorBrush(Colors.White);

        }
        private void ResetScore()
        {
            _score = 0;
            scoreLabel.Content = $"Score: {_score}";
            scoreLabel.Foreground = new SolidColorBrush(Colors.Red);    
        }

        private SolidColorBrush PaintColor(string color)
        {
            SolidColorBrush result = new SolidColorBrush(Colors.White);
            switch (color)
            {
                case "red":
                    result = new SolidColorBrush(Colors.Red);
                    break;
                case "green":
                    result = new SolidColorBrush(Colors.Green);
                    break;
                case "blue":
                    result = new SolidColorBrush(Colors.Blue);
                    break;
                case "yellow":
                    result = new SolidColorBrush(Colors.Yellow);
                    break;
            }
            return result;
        }
    }
}
