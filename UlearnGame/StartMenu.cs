using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UlearnGame
{
    public partial class StartMenu : Form
    {
        public StartMenu()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.BackgroundImage = Resources._interface;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.DoubleBuffered = true;
            Size standartSize = new Size(400, 120);
            Font standartFont = new Font("Agency FB", 40);
        
            var GameName = new Label
            {
                Text = "DUNGEON FOR VDV",
                BackColor = Color.Transparent,
                ForeColor = Color.BurlyWood,
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(1000, 200),
                Font = new Font("Agency FB", 100),
                Location = new Point(250, 0)
            };
            var HowToPlayImage = new PictureBox
            {
                Image = Resources.HowToPlayBackground,
                BackColor = Color.Bisque,
                Size = new Size(500, 500),
                Location = new Point(500, GameName.Bottom)

            };  
            var startGameLabel = new Label
            {
                Text = "START GAME",
                BackColor = Color.Transparent,
                ForeColor = Color.BurlyWood,
                TextAlign = ContentAlignment.MiddleCenter,
                Size = standartSize,
                Font = standartFont,
                Location = new Point(ClientSize.Width/2+150,200)
            };
            var closeGameLabel = new Label
            {
                Text = "CLOSE GAME",
                BackColor = Color.Transparent,
                ForeColor = Color.BurlyWood,
                TextAlign = ContentAlignment.MiddleCenter,
                Size = standartSize,
                Font = standartFont,
                Location = new Point(ClientSize.Width / 2 + 150, startGameLabel.Bottom)
            };
            var vdvImage = new PictureBox
            {
                Image = Resources.PlayerLeft,
                Location = new Point(1300, 200),
                BackColor = Color.Transparent,
            };

            var gachiImage = new PictureBox
            {
                Image = Resources.EnemyRight,
                Location = new Point(200,200),
                BackColor=Color.Transparent
            };
            
            startGameLabel.Click += (sender, args) =>
              {
                  var form1 = new GameForm(this);
                  form1.Show();
                  this.Hide();
              };
            closeGameLabel.Click += (sender, args) =>
            {
                this.Close();
            };
            Controls.Add(startGameLabel);
            Controls.Add(GameName);
            Controls.Add(closeGameLabel);
            Controls.Add(vdvImage);
            Controls.Add(gachiImage);
           
        }
    }
}
