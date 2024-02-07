using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TicTacToe
{
    public partial class MainWindow : Window
    {
        private enum Player { Cross, Circle }

        private Player currentPlayer;
        private List<Button> buttons;

        public MainWindow()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            currentPlayer = Player.Cross;
            
            buttons = new List<Button> { button1, button2, button3, button4, button5, button6, button7, button8, button9 };

            foreach (Button button in buttons)
            {
                button.IsEnabled = true;
                button.Content = string.Empty; 
                button.Background = Brushes.White;
                button.Click -= Button_Click; 
                button.Click += Button_Click; 
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            button.Content = currentPlayer == Player.Cross ? "X" : "O";
            button.IsEnabled = false;

            if (CheckForWin())
            {
                DisableButtons();
                MessageBox.Show($"Игрок победил!"); 
            }
            else if (CheckForDraw())
            {
                DisableButtons();
                MessageBox.Show("Ничья!");
            }
            else
            {
                SwitchPlayer();
            }
        }

        private void SwitchPlayer()
        {
            currentPlayer = currentPlayer == Player.Cross ? Player.Circle : Player.Cross;
            if (currentPlayer == Player.Circle)
            {
                MakeRobotMove();
            }
        }

        private bool CheckForWin()
        {
           
            
                string[] marks = new string[9];

                for (int i = 0; i < buttons.Count; i++)
                {
                    marks[i] = buttons[i].Content.ToString();
                }
 
                for (int i = 0; i < 9; i += 3)
                {
                    if (marks[i] != "" && marks[i] == marks[i + 1] && marks[i] == marks[i + 2])
                    {
                        HighlightWinningButtons(i, i + 1, i + 2);
                        return true;
                    }
                }

                for (int i = 0; i < 3; i++)
                {
                    if (marks[i] != "" && marks[i] == marks[i + 3] && marks[i] == marks[i + 6])
                    {
                        HighlightWinningButtons(i, i + 3, i + 6);
                        return true;
                    }
                }

                if (marks[0] != "" && marks[0] == marks[4] && marks[0] == marks[8])
                {
                    HighlightWinningButtons(0, 4, 8);
                    return true;
                }
                if (marks[2] != "" && marks[2] == marks[4] && marks[2] == marks[6])
                {
                    HighlightWinningButtons(2, 4, 6);
                    return true;
                }

                return false;
            

        }

        private bool CheckForDraw()
        {
            foreach (Button button in buttons)
            {
                if (button.IsEnabled)
                {
                    return false;
                }
            }
            return true;
        }

        private void DisableButtons()
        {
            foreach (Button button in buttons)
            {
                button.IsEnabled = false;
            }
        }

        private void HighlightWinningButtons(int index1, int index2, int index3)
        {
            
        }

        private void MakeRobotMove()
        {
            
            Random random = new Random();
            int index;
            do
            {
                index = random.Next(buttons.Count);
            } while (!buttons[index].IsEnabled); 

            
            buttons[index].Content = "O";
            buttons[index].IsEnabled = false;

            if (CheckForWin())
            {
                DisableButtons();
                MessageBox.Show("Робот победил!");
            }
            else if (CheckForDraw())
            {
                DisableButtons();
                MessageBox.Show("Ничья!");
            }
            else
            {
                currentPlayer = Player.Cross; 
            }
        }

        private void NewGameButton_Click(object sender, RoutedEventArgs e)
        {
            InitializeGame();
        }
    }
}