using System.Windows;

namespace WPF_Math_Game_Outline
{
    /// <summary>
    /// Final Score window. Displays the user's name, age, correct guesses, incorrect guess, and
    /// total time.
    /// </summary>
    public partial class wndFinalScoresScreen : Window
    {
        #region Methods
        /// <summary>
        /// Initialize the window
        /// </summary>
        public wndFinalScoresScreen()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Takes the user back to the main menu when closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdCloseHighScores_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        /// <summary>
        /// Display method. Has parameters to be passed in from the game window.
        /// </summary>
        /// <param name="numberCorrect"></param>
        /// <param name="numberIncorrect"></param>
        /// <param name="seconds"></param>
        public void DisplayUserScore(int numberCorrect, int numberIncorrect, int seconds)
        {
            lblName.Content = "Name: " + clsUser.Name;
            lblAge.Content = "Age: " + clsUser.Age;
            lblNumCor.Content = "Number Correct: " + numberCorrect;
            lblNumInc.Content = "Number Incorrect: " + numberIncorrect;
            lblSeconds.Content = "Total Time: " + seconds;
        }

    }
    #endregion
}
