using System;
using System.Media;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace WPF_Math_Game_Outline
{
    /// <summary>
    /// Game Window. Once the user has pressed start, the question are shown on the screen. There
    /// is a timer to keep track of the total time. The user can press submit or enter to enter
    /// their score. Validates the user's input, tells them if it was wrong or right. Once there
    /// has been 10 questions, the final score window is displayed.
    /// </summary>
    public partial class wndGame : Window
    {
        #region Attributes
        int CurrentGameQuestion; //Keeps track of which question the user is on. 1 - 10
        int Seconds; //How many seconds the user has been playing the game
        DispatcherTimer myTimer;
        MathGameQuestion currentQuestion;
        bool hasGameStarted = false; //Has game started button to provide multipurpose start button
        int NumberCorrect; //How many the user has gotten correct
        int NumberIncorrect; //How many the user has gotten incorrect
        SoundPlayer simpleSound = new SoundPlayer("cheering.wav"); //Cheering sound for correct answers
        #endregion

        #region Methods
        /// <summary>
        /// Initialize window and create timer object
        /// </summary>
        public wndGame()
        {
            InitializeComponent();
            myTimer = new DispatcherTimer();
            myTimer.Interval = TimeSpan.FromSeconds(1);
            myTimer.Tick += new EventHandler(myTimer_Tick);
        }

        /// <summary>
        /// Multipurpose button. When the user presses start, the button now says submit so that
        /// theres no need for a third button. When the user hits submit, the input is validated.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            if (hasGameStarted == false)
            {
                Seconds = 0;
                myTimer.Start();
                GenerateNewQuestion();
                CurrentGameQuestion = 1;
                hasGameStarted = true;
                btnStart.Content = "Submit";
                txtBoxUserGuess.IsEnabled = true;
                txtBoxUserGuess.Clear();
                lblTitle.Content = "Let's Go!";
            }
            else
            {
                validateUserGuess();
            }
        }

        /// <summary>
        /// Generates new game question. Check to make sure that the game is performing correctly
        /// </summary>
        private void GenerateNewQuestion()
        {
            currentQuestion = clsGame.GenerateQuestion();

            if (currentQuestion != null)
            {
                lblQuestion.Content = currentQuestion.getQuesitionString();
            }
            else
            {
                lblError.Foreground = Brushes.Red;
                lblError.Content = "No valid question could be generated. Please check your game settings.";
            }
        }

        /// <summary>
        /// Validates that the user has entered a number. Increments the number correct and incorrect.
        /// </summary>
        private void validateUserGuess()
        {
            try
            {
                if (int.TryParse(txtBoxUserGuess.Text, out int userGuess))
                {
                    if (clsGame.UserGuess(userGuess, currentQuestion))
                    {
                        NumberCorrect++;
                        lblError.Foreground = Brushes.Black;
                        lblError.Content = "Correct!";
                        simpleSound.Play();
                    }
                    else
                    {
                        NumberIncorrect++;
                        lblError.Foreground = Brushes.Red;
                        lblError.Content = "Incorrect";
                    }

                    txtBoxUserGuess.Clear();

                    // Check if it's the last question
                    if (CurrentGameQuestion < 10)
                    {
                        CurrentGameQuestion++;
                        GenerateNewQuestion();
                    }
                    else
                    {
                        DisplayFinalScore();
                    }
                }
                else
                {
                    lblError.Foreground = Brushes.Red;
                    lblError.Content = "Please enter a valid number";
                }
            }
            catch (Exception ex)
            {
                lblError.Content = ex.Message;
            }
        }

        /// <summary>
        /// Hides the window if the user presses cancel. Goes back to the main window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        /// <summary>
        /// Timer method to keep track of the seconds and diplay the total time while playing
        /// the game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void myTimer_Tick(object sender, EventArgs e)
        {
            Seconds++;
            lblTimer.Content = "Timer: " + Seconds;
        }

        /// <summary>
        /// Sends the necessary information to the final score screen the display the information
        /// </summary>
        private void DisplayFinalScore()
        {
            wndFinalScoresScreen myFinalScoreScreen = new wndFinalScoresScreen();
            myFinalScoreScreen.DisplayUserScore(NumberCorrect, NumberIncorrect, Seconds);
            this.Hide();
            myFinalScoreScreen.ShowDialog();
            lblTitle.Content = "Ready?";
        }

        /// <summary>
        /// Allows the user to press enter to submit their guess
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBoxUserGuess_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;
                validateUserGuess();
            }
        }
    }
    #endregion
}
