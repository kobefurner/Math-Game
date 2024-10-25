using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Math_Game_Outline
{
    /// <summary>
    /// Class to keep the user's information instead of it being behind the GUI. Decide to move
    /// the seconds and number correct to the game window because I felt it didn't belong here.
    /// </summary>
    public class clsUser
    {
        //Name
        public static string Name { get; set; }

        //Age
        public static int Age { get; set; }
    }

}
