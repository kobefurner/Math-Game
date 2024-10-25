using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Math_Game_Outline
{
    /// <summary>
    /// Logic for the game so that it's not behind the GUI
    /// </summary>
    public class clsGame
    {
        #region Properties
        //Game mode properties. Set as static so that they can be used in other classes
        public static bool Add { get; set; }
        public static bool Subtract { get; set; }
        public static bool Divide { get; set; }
        public static bool Multiply { get; set; }
        #endregion

        //Random generator
        private static Random random = new Random();

        #region Methods
        /// <summary>
        /// Reset all game modes
        /// </summary>
        public static void resetGameType()
        {
            Add = false;
            Subtract = false;
            Divide = false;
            Multiply = false;
        }

        /// <summary>
        /// Method that generates the math question. Takes two random numbers and depending
        /// on the game mody, creates an answer to the question.
        /// </summary>
        /// <returns></returns>
        public static MathGameQuestion GenerateQuestion()
        {
            int num1 = random.Next(1,11);
            int num2 = random.Next(1,11);

            //Add
            if (Add)
            {
                return new MathGameQuestion(num1, num2, num1 + num2, "+");
            }

            //Subtract
            if (Subtract)
            {
                if (num1 < num2)
                {
                    (num1, num2) = (num2, num1);
                }
                return new MathGameQuestion(num1, num2, num1 - num2, "-");
            }

            //Divide
            if (Divide)
            {
                num1 = num1 * num2;
                while (num1 % num2 != 0)
                {
                    num2 = random.Next(1, 11); //Keep generating until num2 divides num1 evenly
                }
                return new MathGameQuestion(num1, num2, num1 / num2, "/");
            }

            //Multiply
            if (Multiply)
            {
                return new MathGameQuestion(num1, num2, num1 * num2, "*");
            }
         return null;
        }

        /// <summary>
        /// compares the user's guess to the answer
        /// </summary>
        /// <param name="userGuess"></param>
        /// <param name="currentQuestion"></param>
        /// <returns></returns>
        public static bool UserGuess(int userGuess, MathGameQuestion currentQuestion)
        {
            return userGuess == currentQuestion.answer;
        }
    }

    /// <summary>
    /// Helper class to make it easier to get and set the numbers to the question
    /// </summary>
    public class MathGameQuestion {
        public int leftNumber { get; } 
        public int rightNumber { get; }
        public int answer { get; }
        public string operation { get; }
    
        /// <summary>
        /// Method that passes in the numbers and sets them to their respective variables
        /// </summary>
        /// <param name="leftNumber"></param>
        /// <param name="rightNumber"></param>
        /// <param name="answer"></param>
        /// <param name="operation"></param>
        public MathGameQuestion(int leftNumber, int rightNumber, int answer, string operation)
        {
            this.leftNumber = leftNumber;
            this.rightNumber = rightNumber;
            this.answer = answer;
            this.operation = operation;
        }

        /// <summary>
        /// Generate the string for the label in the GUI
        /// </summary>
        /// <returns></returns>
        public string getQuesitionString()
        {
            return leftNumber + " " + operation + " " + rightNumber + ": ";
        }
    }
    #endregion
}
