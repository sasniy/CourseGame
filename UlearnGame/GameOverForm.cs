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
    public partial class GameOverForm : Form
    {
        public GameOverForm()
        {
            InitializeComponent();

            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            DoubleBuffered = true;
            BackColor = Color.Gray;
            Size defaultSize = new Size(200, 200);
            Point defaultLocation = new Point(200, 400);
            Label LoseLabel = new Label
            {
                Size = defaultSize,
                Location = defaultLocation,
                Text = "HAHA,YOU LOSE"
            };
            Label WinLabel = new Label
            {
                Size = defaultSize,
                Location = defaultLocation,
                Text = "YES,YOU WIN"
            };
            Label killLabel = new Label
            {
                Size = defaultSize,
                Location = new Point(200, LoseLabel.Bottom),
                Text = "Количество убийств:" + Game.killsCount
              
            };
            
            Controls.Add(killLabel);          
        }

    }
}
