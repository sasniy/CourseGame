using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UlearnGame
{
    class Attack
    {
        private int speed = 40;
        public string direction;
        public int attackLeft;
        public int attackTop;

        public PictureBox attackModel = new PictureBox();
        Timer attackTimer = new Timer();

        public void CreateAttack(Form form)
        {
            attackModel.Tag = "attack";
            attackModel.Size = new Size(40, 40);
            attackModel.SizeMode = PictureBoxSizeMode.StretchImage;
            attackModel.Left = attackLeft;
            attackModel.BackColor = Color.Transparent;
            attackModel.Top = attackTop;
            attackModel.BringToFront();
            if (direction == "up")
                attackModel.Image = Resources.AttackUp;
            if (direction == "down")
                attackModel.Image = Resources.AttackDown;
            if (direction == "right")
                attackModel.Image = Resources.AttackRight;
            if (direction == "left")
                attackModel.Image = Resources.AttackLeft;

            form.Controls.Add(attackModel);
            attackTimer.Interval = speed;
            attackTimer.Tick += new EventHandler(BulletTimerEvent);
            attackTimer.Start();

        }

        private void BulletTimerEvent(object sender, EventArgs e)
        {

            if (direction == "left")
            {
              attackModel.Left -= speed;
            }

            if (direction == "right")
            {
                attackModel.Left += speed;
            }

            if (direction == "up")
            {
                attackModel.Top -= speed;
            }

            if (direction == "down")
            {
                attackModel.Top += speed;
            }


            if (attackModel.Left < 10 || attackModel.Left > 1500 || attackModel.Top < 10 || attackModel.Top > 1000)
            {
                attackTimer.Stop();
                attackTimer.Dispose();
                attackModel.Dispose();
                attackTimer = null;
                attackModel = null;
            }
        }

    }
}
