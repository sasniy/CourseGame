using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace UlearnGame
{
    public class Game
    {
        public static bool win = false;
        public static bool gameover = false;
        public static int enemyCount = 3;
        public static bool heal = false;
        public static int healthCount = 3;
        public static int enemySpeed = 3;
        public static int killsCount = 0;
        public static string minutes = "2";
        public static string seconds = "00";
        //public static Timer gameTimer = new Timer();


        //public static void timeUpdate(object sender, EventArgs e)
        //{
        //    var min = int.Parse(minutes);
        //    var sec = int.Parse(seconds);
        //    if (min == 0 && sec == 0)
        //    {
        //        gameTimer.Stop();
        //        win = true;
        //        gameover = true;
        //    }


        //    if (sec == 0)
        //    {
        //        sec = 59;
        //        min--;
        //    }
        //    else
        //        sec--;
        //    if (sec < 10)
        //        seconds = "0" + sec.ToString();
        //    else
        //        seconds = sec.ToString();

        //    minutes = min.ToString();
        //    GameForm.timerLabel.Text = GameForm.timerLabel.Text = Game.minutes + ":" + Game.seconds;
        //}
    }
}
