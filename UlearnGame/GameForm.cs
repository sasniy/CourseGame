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
    public partial class GameForm : Form

    {
        public static Player player = new Player("player", new Point(0, 0), 10);
        Timer cooldownBarTimer = new Timer();
        Timer updateTimer = new Timer();
        Timer cooldownTimer = new Timer();
        Timer healthTimer = new Timer();
        Timer gameTimer = new Timer();

        List<PictureBox> healthList = new List<PictureBox>();
        List<PictureBox> enemiesList = new List<PictureBox>();
        Random randNum = new Random();
        PictureBox wallInterface = new PictureBox();
        public Label killLabel = new Label();
        public Label LoseLabel = new Label();
        public Label WinLabel = new Label();
        public Label goToMenu = new Label();
        public Label goAgain = new Label();
        public Form startForm;
        public static Label timerLabel = new Label();
        ProgressBar cooldownBar = new ProgressBar();
    
        public GameForm(Form FormStart)
        {
            startForm = FormStart;
            RestartGame();
            
        }
        
        private void cooldownAttackTimer(object sender, EventArgs e)

        {
            if(cooldownBar.Value < cooldownBar.Maximum )
            {
                cooldownBar.Value+=50;
            }
            else
            {
                cooldownBarTimer.Stop();
            }
        }
        public void timeUpdate(object sender, EventArgs e)
        {
            var min = int.Parse(Game.minutes);
            var sec = int.Parse(Game.seconds);
            if (min == 0 && sec == 0)
            {
                gameTimer.Stop();
                Game.win = true;
                Game.gameover = true;
            }


            if (sec == 0)
            {
                sec = 59;
                min--;
            }
            else
                sec--;
            if (sec < 10)
                Game.seconds = "0" + sec.ToString();
            else
                Game.seconds = sec.ToString();

            Game.minutes = min.ToString();
            GameForm.timerLabel.Text = GameForm.timerLabel.Text = Game.minutes + ":" + Game.seconds;
        }
        private void cooldownUpdate(object sender, EventArgs e)
        {
            player.cooldown = true;
            cooldownTimer.Stop();
        }
        private void RestartGame()
        {

            Game.healthCount = 3;
            Game.killsCount = 0;      
            Settings();
            Game.win = false;
            Game.gameover = false;
            Game.minutes = "2";
            Game.seconds = "00";
            Point startPosition = new Point(500,500);
            player.playerModel.Location = startPosition;
            
            this.Controls.Add(player.playerModel);
            for (int i = 0; i < Game.enemyCount; i++)
            {
                EnemySpawn();
            }
            gameTimer.Enabled =true;
        }
        private void Update(object sender, EventArgs e)
        {
            if (Game.healthCount == 1 && !Game.heal)
            {
                PictureBox heal = new PictureBox()
                {
                    Image = Resources.heal,
                    Size = new Size(60, 60),
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    BackColor = Color.Transparent,
                    Tag = "heal"
                };
                heal.Top = randNum.Next(150, 900);
                heal.Left = randNum.Next(50, 900);
                Controls.Add(heal);
                Game.heal = true;
            }
            if (int.Parse(Game.minutes) < 1)
                Game.enemySpeed = 5;
                
            killLabel.Text = "Убийств:" + Game.killsCount;
            if (player.up == true && player.playerModel.Top > 110)
            {
                player.playerModel.Top -= player.speed;
            }
            if (player.right == true && player.playerModel.Left + player.playerModel.Width < ClientSize.Width)
            {
                player.playerModel.Left = player.playerModel.Left + player.speed;
            }
            if (player.left == true && player.playerModel.Left > 0)
            {
                player.playerModel.Left = player.playerModel.Left - player.speed;
            }
            if (player.down == true && player.playerModel.Top + player.playerModel.Height < ClientSize.Height)
            {
                player.playerModel.Top = player.playerModel.Top + player.speed;
            }
            foreach (Control d in this.Controls)
            {
                if ((d is PictureBox && (string)d.Tag == "heal"))
                {
                    if (d.Bounds.IntersectsWith(player.playerModel.Bounds))
                    {
                        Sound.healthSound.Play();
                        d.Dispose();
                        wallInterface.Controls.Add(healthList[1]);
                        Game.healthCount++;
                        Game.heal = false;
                    };
                }
            }
            foreach (Control x in this.Controls)
            {
                if ((x is PictureBox && (string)x.Tag == "enemy"))
                {
                    if (x.Bounds.IntersectsWith(player.playerModel.Bounds))
                    {
                        if ((string)x.Tag == "enemy")
                        {
                            
                            if (Game.healthCount > 1)
                            {

                                wallInterface.Controls.Remove(healthList[Game.healthCount - 1]);
                                healthTimer.Start();
                            }
                            
                            else
                            {
                                Game.healthCount = 0;
                                wallInterface.Controls.Remove(healthList[0]);
                                Game.win = false;
                                Game.gameover = true;

                            }
                        }
                        
                                
                    }



                    if (x.Left > player.playerModel.Left)
                    {
                        x.Left -= Game.enemySpeed;
                        ((PictureBox)x).Image = Resources.EnemyLeft;
                    }
                    if (x.Left < player.playerModel.Left)
                    {
                        x.Left += Game.enemySpeed; ;
                        ((PictureBox)x).Image = Resources.EnemyRight;
                    }
                    if (x.Top > player.playerModel.Top)
                    {
                        x.Top -= Game.enemySpeed; ;
                        ((PictureBox)x).Image = Resources.EnemyUp_;
                    }
                    if (x.Top < player.playerModel.Top)
                    {
                        x.Top += Game.enemySpeed; ;
                        ((PictureBox)x).Image = Resources.EnemyDown;
                    }
                }
                foreach (Control j in this.Controls)
                {
                    if (((j is PictureBox && (string)j.Tag == "attack") || (j is PictureBox && (string)j.Tag == "heal")) && x is PictureBox && (string)x.Tag == "enemy" )
                    {
                        if (x.Bounds.IntersectsWith(j.Bounds))
                        {
                            if ((string)j.Tag == "attack")
                            {
                                this.Controls.Remove(j);
                                ((PictureBox)j).Dispose();
                                this.Controls.Remove(x);
                                ((PictureBox)x).Dispose();
                                enemiesList.Remove(((PictureBox)x));
                                Game.killsCount++;
                                Sound.deathSound.Play();
                                EnemySpawn();
                            }
                            if ((string)j.Tag == "heal")
                            {
                                this.Controls.Remove(j);
                                ((PictureBox)j).Dispose();
                                Game.heal = false;
                                Sound.damageSound.Play();
                            }    
                        }
                    }    
                }
            }
            if (Game.gameover)
            {
                Cursor.Show();
                updateTimer.Stop();
                cooldownBarTimer.Stop();
                healthTimer.Stop();
                gameTimer.Stop();
                if (Game.win)
                {
                    Sound.winSound.Play();
                    Controls.Add(WinLabel);
                }
                else
                {
                    Sound.loseSound.Play();
                    Controls.Add(LoseLabel);
                }
                Controls.Add(goAgain);
                Controls.Add(goToMenu);
            }
        }
           
        private void healthUpdate(object sender,EventArgs e)
        {
            Game.healthCount--;
           
            healthTimer.Stop();
        }
       

        private void PlayerAttack(string direction)
        {
            Attack attack = new Attack();
            Sound.attackSound.Play();
            attack.direction = direction;
            attack.attackLeft = player.playerModel.Left;
            attack.attackTop = player.playerModel.Top;
            attack.CreateAttack(this);
            player.cooldown = false;
            cooldownTimer.Start();
            cooldownBarTimer.Start();
        }
       
        
        private void EnemySpawn()
        {
            PictureBox enemy = new PictureBox { Image = Resources.EnemyDown };
            enemy.Tag = "enemy";
            enemy.BackColor = Color.Transparent;
            var firstposition = randNum.Next(-200, 1200);
            enemy.Left = firstposition;
            if (firstposition > 1200 || firstposition < 0)
                enemy.Top = randNum.Next(-100, 1100);
            else
            {
                enemy.Top = randNum.Next(900, 1100);
            }
            enemiesList.Add(enemy);
            this.Controls.Add(enemy);
            player.playerModel.BringToFront();
        }
        private void Settings()
        {
            InitializeComponent();
            this.KeyDown += new KeyEventHandler(KeyIsDown);
            this.KeyUp += new KeyEventHandler(KeyIsUp);
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.BackgroundImage = Resources.textureBack;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.DoubleBuffered = true;
            Cursor.Hide();
            updateTimer.Interval = 20;
            updateTimer.Tick += new EventHandler(Update);
            updateTimer.Start();
            cooldownBar.Size = new Size(200, 50);
            cooldownBar.Location = new Point(1200, 800);
            cooldownBar.Maximum = 1500;
            cooldownBar.Value = cooldownBar.Maximum;
            healthTimer.Interval = 2000;
            healthTimer.Tick += new EventHandler(healthUpdate);
            cooldownTimer.Interval = 1500;
            cooldownTimer.Tick += new EventHandler(cooldownUpdate);
            gameTimer.Interval = 1000;
            gameTimer.Tick += new EventHandler(timeUpdate);
            wallInterface.Image = Resources._interface;
            wallInterface.SizeMode = PictureBoxSizeMode.StretchImage;
            wallInterface.Location = new Point(0, 0);
            wallInterface.Size = new Size(1600, 110);
            killLabel.Font = new Font("Comic Sans", 40);
            killLabel.Location = new Point(10, 10);
            killLabel.Size = new Size(500, 100);
            killLabel.BackColor = Color.Transparent;
            killLabel.TextAlign = ContentAlignment.MiddleCenter;
            timerLabel.Font = new Font("", 40);
            timerLabel.Size = new Size(200, 100);
            timerLabel.TextAlign = ContentAlignment.MiddleCenter;
            timerLabel.Location = new Point(500, 10);
            timerLabel.BackColor = Color.Transparent;
            this.Controls.Add(wallInterface);
            this.Controls.Add(cooldownBar);

            wallInterface.Controls.Add(timerLabel);
            wallInterface.Controls.Add(killLabel);
            for (int i = 0; i < Game.healthCount; i++)
            {

                healthList.Add(new PictureBox
                {
                    Image = Resources.health,
                    BackColor = Color.Transparent,
                    Size = new Size(100, 100),
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Location = new Point(ClientSize.Width + 400 + 100 * i, 10)
                }
                );
                wallInterface.Controls.Add(healthList[i]);
            }
            Size defaultSize = new Size(600, 200);
            Point defaultLocation = new Point(400, 100);
            LoseLabel.Size = defaultSize;
            LoseLabel.Location = defaultLocation;
            LoseLabel.Text = "Вы проиграли";
            LoseLabel.TextAlign = ContentAlignment.MiddleCenter;
            LoseLabel.Font = new Font("Comic Sans", 50);
            LoseLabel.BackColor = Color.Transparent;
            WinLabel.Size = defaultSize;
            WinLabel.Location = defaultLocation;
            WinLabel.Text = "Вы выйграли!";
            WinLabel.Font = new Font("Comic Sans", 50);
            WinLabel.BackColor = Color.Transparent;
            WinLabel.TextAlign = ContentAlignment.MiddleCenter;
            goToMenu.Size = defaultSize;
            goToMenu.Location = new Point(LoseLabel.Location.X, LoseLabel.Bottom);
            goToMenu.Text = "Вернуться в меню";
            goToMenu.TextAlign = ContentAlignment.MiddleCenter;
            goToMenu.BackColor = Color.Transparent;
            goToMenu.Font = new Font("Comic Sans", 50);
            cooldownBarTimer.Interval = 1;
            cooldownBarTimer.Tick += new EventHandler(cooldownAttackTimer);
            goToMenu.Click += (sender, args) =>
            {
                startForm.Show();
                this.Hide();
            };

        }
        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                player.up = true;
                player.facing = "up";
                player.playerModel.Image = Resources.PlayerUp;
            }

            if (e.KeyCode == Keys.Left)
            {
                player.left = true;
                player.facing = "left";
                player.playerModel.Image = Resources.PlayerLeft;
            }
            if (e.KeyCode == Keys.Right)
            {
                player.right = true;
                player.facing = "right";
                player.playerModel.Image = Resources.PlayerRight;
            }
            if (e.KeyCode == Keys.Down)
            {
                player.down = true;
                player.facing = "down";
                player.playerModel.Image = Resources.PlayerDown;
            }
            if (e.KeyCode == Keys.Space && player.cooldown)
            {
                cooldownBar.Value = 0;
                PlayerAttack(player.facing);

            }
            if (e.KeyCode == Keys.Escape)
            {
                if (updateTimer.Enabled == true)
                {
                    gameTimer.Enabled = false;
                    updateTimer.Enabled = false;
                }
                else
                {
                    gameTimer.Enabled = true;
                    updateTimer.Enabled = true;
                }
            }

        }
        private void KeyIsUp(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Left)
            {
                player.left = false;
            }

            if (e.KeyCode == Keys.Right)
            {
                player.right = false;
            }

            if (e.KeyCode == Keys.Up)
            {
                player.up = false;
            }

            if (e.KeyCode == Keys.Down)
            {
                player.down = false;
            }

        }

    }
}
