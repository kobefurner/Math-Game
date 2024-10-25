using System;
using System.Windows;

namespace WPF_Math_Game_Outline
{
    /// <summary>
    /// Main Window class. Asks the user's for the name and age as well as the game mode.
    /// Validates their input before moving to the next window.
    /// </summary>
    public partial class wndEnterUserData : Window
    {
        #region Methods
        /// <summary>
        /// Initialize the window
        /// </summary>
        public wndEnterUserData()
        {
            InitializeComponent();
            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
        }

        /// <summary>
        /// Start button. Checks if the validation came back as true, then procedes to clear the 
        /// text boxes and labels, hide away the window and show the new game window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdStartGame_Click(object sender, RoutedEventArgs e)
        {
            if (validateUserData())
            {
                clsGame.resetGameType();
                getGameType();
                this.Hide();
                wndGame wndMyGameWindow = new wndGame();
                wndMyGameWindow.ShowDialog();
                this.Show();
                txtBoxAge.Clear();
                txtBoxName.Clear();
                lblError.Content = "";
            }
        }

        /// <summary>
        /// Validation method. Makes sure the user's name and age is not empty. Makes sure the age
        /// is between 3 - 10.
        /// </summary>
        /// <returns></returns>
        private bool validateUserData()
        {
            try
            {
                //Validate Name
                if (string.IsNullOrWhiteSpace(txtBoxName.Text))
                {
                    throw new Exception("Please enter a name.");
                }
                clsUser.Name = txtBoxName.Text;

                //Validate Age
                if (!int.TryParse(txtBoxAge.Text, out int age))
                {
                    throw new Exception("Please enter a valid number for age.");
                }

                if (age < 3 || age > 10)
                {
                    throw new Exception("Please enter an age between 3 and 10.");
                }
                clsUser.Age = age;
                return true;
            }
            catch (Exception ex)
            {
                lblError.Content = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Quick check to see what the game type is. I have set the addition radio button to 
        /// isChecked so there's always going to be a game mode.
        /// </summary>
        private void getGameType()
        {
            if (rdbtnAdd.IsChecked == true)
            {
                clsGame.Add = true;
            }
            if (rdbtnSub.IsChecked == true)
            {
                clsGame.Subtract = true;
            }
            if (rdbtnMult.IsChecked == true)
            {
                clsGame.Multiply = true;
            }
            if (rdbtnDiv.IsChecked == true)
            {
                clsGame.Divide = true;
            }
        }
    }
    #endregion
}
