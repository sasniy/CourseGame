using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UlearnGame
{
    public class Player
    {
        public bool up, right, left, down;
        public bool cooldown = true;
        public string facing = "down";      
        public int speed;
        public static Size size = new Size(50, 50);
        public PictureBox playerModel = new PictureBox
        {
            Image = Resources.PlayerDown,
            BackColor = Color.Transparent,
            Size = Player.size,
            SizeMode = PictureBoxSizeMode.StretchImage
        };
        public Player(string entity, Point location, int speed)
        {
            this.speed = speed;
            this.playerModel.Location = location;
        }
             
    }
}
