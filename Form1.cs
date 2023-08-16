using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        
        Font drawFont = new Font("Unispace", 50);
        Font losefont = new Font("Unispace", 50);
        Font scorefont = new Font("Unispace", 30);
        Bitmap explosion = new Bitmap("explosion.png");
        Timer t = new Timer();
        Bitmap bluechicken = new Bitmap("bluechicken.png");
        Bitmap redchicken = new Bitmap("redchicken.png");
        Bitmap bg = new Bitmap("bg.jpg");
        Bitmap ship = new Bitmap("ship4.png");
        Bitmap egg = new Bitmap("FallingEggProjectile.png");
        Bitmap laser = new Bitmap("NeutronProjectile.png");
        Bitmap heart = new Bitmap("heart.png");
        Bitmap playagain = new Bitmap("playagain.png");

        Bitmap offimage;
        Random r = new Random();
        List<bluechicken> blue = new List<bluechicken>();
        List<redchicken> red = new List<redchicken>();
        List<laser> l = new List<laser>();
        List<hearts> h = new List<hearts>();
        WMPLib.WindowsMediaPlayer player = new WMPLib.WindowsMediaPlayer();
        WMPLib.WindowsMediaPlayer intro = new WMPLib.WindowsMediaPlayer();
        WMPLib.WindowsMediaPlayer gameplay = new WMPLib.WindowsMediaPlayer();
        WMPLib.WindowsMediaPlayer gameover = new WMPLib.WindowsMediaPlayer();
        WMPLib.WindowsMediaPlayer explode = new WMPLib.WindowsMediaPlayer();
        int playx=550, playy=400,xheart=20,yheart=20,xover=300,yover=200,playgameover=0;
        int olxship=670,oldyship=700,xship = 670, yship = 700, backx = 0, backy = 0, scroll = 1, ichicken = 3,score=0, countchicken = 0, aftercrash = 0, lives = 5,crash=0,countcheck=0,counttheme=0,mousex=0,mousey=0,counteggblue=0, counteggred=0, ieggred=0,ieggblue=0;
        public Form1()
        {
            
            InitializeComponent();
            mousex = xship + ship.Width / 2;
            mousey = yship + ship.Height / 2;
            Cursor.Position = new Point(mousex,mousey);
           
            Cursor.Hide();
            this.Load += Form1_Load;
            this.Paint += Form1_Paint;
            this.KeyDown += Form1_KeyDown;
            this.MouseMove += Form1_MouseMove;
            this.MouseDown += Form1_MouseDown;
            FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            //draw lives
            for (int i = 0; i < lives; i++)
            {
                hearts pnn = new hearts();
                pnn.x = xheart;
                pnn.y = yheart;
                h.Add(pnn);
                xheart += heart.Width+5;
            }
            t.Interval=( 1000 / 60);
            t.Start();
            t.Tick += T_Tick;

        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button==MouseButtons.Left)
            {
                if (crash == 0 && lives != 0)
                {
                    laser pnn = new laser();
                    pnn.x = xship + ship.Width / 2;
                    pnn.y = yship;
                    l.Add(pnn);
                    player.URL = ("fire.mp3");
                    player.controls.play();
                }
            }
            if (e.Button == MouseButtons.Left)
            { 
                if (lives == 0)
                {
                    if((e.X>playx&&e.X<=playx+playagain.Width)&&(e.Y>playy&&e.Y<=playy+playagain.Height))
                    {
                        
                        Application.Restart();
                        Environment.Exit(0);
                    }
                }
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (crash == 0&&lives!=0)
            {
                xship = e.X-ship.Width/2;
                yship = e.Y-ship.Height/2;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Graphics g = this.CreateGraphics();
            Pen p = new Pen(Color.Gray, 15);
            if (crash == 0 && lives != 0)
            {
                if (e.KeyCode == Keys.Left)
                {
                    xship -= 20;
                }

                if (e.KeyCode == Keys.Right)
                {
                    xship += 20;
                }
                if (e.KeyCode == Keys.Up)
                {
                    yship -= 20;
                }
                if (e.KeyCode == Keys.Down)
                {
                    yship += 20;
                }
                if (e.KeyCode == Keys.Space)
                {
                    
                    laser pnn = new laser();
                    pnn.x = xship + ship.Width / 2;
                    pnn.y = yship;
                    l.Add(pnn);
                    player.URL = ("fire.mp3");
                    player.controls.play();

                }

            }
           
        }

        private void T_Tick(object sender, EventArgs e)
        {
            Graphics g = this.CreateGraphics();

            if (lives != 0)
            {
                
                countchicken++;
                countcheck++;
                counttheme++;
                counteggred++;
                counteggblue++;
                if (counttheme == 200)
                {
                    intro.controls.stop();
                    player.controls.stop();
                    gameplay.URL = ("gameplay.mp3");
                    gameplay.controls.play();
                }
                //creating the chickens and the eggs
                if (countchicken == 40)
                {
                    ichicken = r.Next(0, 2);
                    if (ichicken == 0)//blue
                    {
                        bluechicken pnn = new bluechicken();
                        pnn.x = r.Next(0, this.ClientSize.Width - 20);
                        pnn.y = -20;
                        pnn.dir = 0;
                        pnn.drop = 0;
                        pnn.moves = 0;
                        blue.Add(pnn);
                    }
                    if (ichicken == 1)//red
                    {
                        redchicken pnn = new redchicken();
                        pnn.x = r.Next(0, this.ClientSize.Width - 20);
                        pnn.y = -20;
                        pnn.dir = 0;
                        pnn.drop = 0;
                        pnn.moves = 0;

                        red.Add(pnn);
                    }

                    countchicken = 0;

                }
                if (counteggblue == 30)//blue chicken eggs
                {
                    if (blue.Count > 1)
                    {
                        eggs pnn3 = new eggs();
                        ieggblue = r.Next(1, blue.Count - 1);
                        pnn3.x = blue[ieggblue].x + bluechicken.Width / 2;
                        pnn3.y = blue[ieggblue].y + bluechicken.Height;
                        blue[ieggblue].e.Add(pnn3);
                    }
                    ieggblue = 0;
                    counteggblue = 0;

                }
                if (counteggred == 30)//red chicken eggs
                {
                    if (red.Count > 1)
                    {
                        ieggred = r.Next(1, red.Count - 1);
                        eggs pnn2 = new eggs();
                        pnn2.x = red[ieggred].x + redchicken.Width / 2;
                        pnn2.y = red[ieggred].y + redchicken.Height;
                        red[ieggred].e.Add(pnn2);
                    }
                    counteggred = 0;
                    ieggred = 0;
                }
                //moving the chickens
                for (int i = 0; i < blue.Count; i++)//blue chickens
                {
                    if (blue[i].dir == 0)//down
                    {
                        blue[i].y += 3;
                        blue[i].moves++;
                        if (blue[i].moves == 15)
                        {
                            blue[i].dir = r.Next(0, 4);
                            blue[i].moves = 0;
                        }
                    }
                    if (blue[i].dir == 1)//up
                    {
                        blue[i].y -= 3;
                        blue[i].moves++;
                        if (blue[i].moves == 2)
                        {
                            blue[i].dir = r.Next(0, 4);
                            blue[i].moves = 0;
                        }
                    }
                    if (blue[i].dir == 2)//left
                    {
                        if (blue[i].x > 0) // 34an mttl34 bra el border ymen
                        {
                            blue[i].x -= 3;
                            blue[i].moves++;
                        }
                        else
                        {
                            blue[i].dir = 3;
                        }

                        if (blue[i].moves == 10)
                        {
                            blue[i].dir = r.Next(0, 4);
                            blue[i].moves = 0;
                        }
                    }
                    if (blue[i].dir == 3)//right
                    {
                        if (blue[i].x + bluechicken.Width < this.ClientSize.Width) // 34an mttl34 bra el border 4mal
                        {
                            blue[i].x += 3;
                            blue[i].moves++;
                        }
                        else
                        {
                            blue[i].dir = 2;
                        }
                        if (blue[i].moves == 10)
                        {
                            blue[i].dir = r.Next(0, 4);
                            blue[i].moves = 0;
                        }
                    }

                }
                for (int i = 0; i < blue.Count; i++) //move blue chickens eggs
                {
                    for (int j = 0; j < blue[i].e.Count; j++)
                    {
                        blue[i].e[j].y += 8;
                    }
                }
                for (int j = 0; j < red.Count; j++)//red chickens
                {
                    if (red[j].dir == 0)//down
                    {
                        red[j].y += 3;
                        red[j].moves++;
                        if (red[j].moves == 15)
                        {
                            red[j].dir = r.Next(0, 4);
                            red[j].moves = 0;
                        }
                    }
                    if (red[j].dir == 1)//up
                    {
                        red[j].y -= 3;
                        red[j].moves++;
                        if (red[j].moves == 2)
                        {
                            red[j].dir = r.Next(0, 4);
                            red[j].moves = 0;
                        }
                    }
                    if (red[j].dir == 2)//left
                    {
                        if (red[j].x > 0) // 34an mttl34 bra el border ymen
                        {
                            red[j].x -= 3;
                            red[j].moves++;
                        }
                        else
                        {
                            red[j].dir = 3;
                        }

                        if (red[j].moves == 10)
                        {
                            red[j].dir = r.Next(0, 4);
                            red[j].moves = 0;
                        }
                    }
                    if (red[j].dir == 3)//right
                    {
                        if (red[j].x + bluechicken.Width < this.ClientSize.Width) // 34an mttl34 bra el border 4mal
                        {
                            red[j].x += 3;
                            red[j].moves++;
                        }
                        else
                        {
                            red[j].dir = 2;
                        }

                        if (red[j].moves == 10)
                        {
                            red[j].dir = r.Next(0, 4);
                            red[j].moves = 0;
                        }
                    }


                }
                for (int j = 0; j < red.Count; j++)//move red chickens eggs
                {
                    for (int k = 0; k < red[j].e.Count; k++)
                    {
                        red[j].e[k].y += 8;
                    }
                }

                //background animation
                if (scroll == 0)
                {
                    backy++;
                    if (backy == 0)
                    {
                        scroll = 1;
                    }
                }

                if (scroll == 1)
                {
                    backy--;
                    if (backy == -50)
                    {
                        scroll = 0;
                    }
                }
                //check if the ship got hitted by chicken

                for (int i = 0; i < blue.Count; i++)
                {
                    if ((xship >= blue[i].x && xship <= blue[i].x + bluechicken.Width) || xship + ship.Width >= blue[i].x && xship + ship.Width <= blue[i].x + bluechicken.Width)
                    {
                        if ((yship >= blue[i].y && yship <= blue[i].y + bluechicken.Height) || (yship + ship.Height / 2 - 20 >= blue[i].y && yship + ship.Height / 2 - 20 <= blue[i].y + bluechicken.Height))
                        {
                            crash = 1;
                            explode.URL = ("explotion.mp3");
                            explode.controls.play();
                            blue.RemoveAt(i);
                            player.URL = ("fire.mp3");
                            player.controls.play();
                            lives--;
                            h.RemoveAt(h.Count - 1);

                        }
                    }

                }
                for (int j = 0; j < red.Count; j++)
                {
                    if ((xship >= red[j].x && xship <= red[j].x + redchicken.Width) || xship + ship.Width >= red[j].x && xship + ship.Width <= red[j].x + redchicken.Width)
                    {
                        if ((yship >= red[j].y && yship <= red[j].y + redchicken.Height) || (yship + ship.Height / 2 - 20 >= red[j].y && yship + ship.Height / 2 - 20 <= red[j].y + redchicken.Height))
                        {
                            crash = 1;
                            explode.URL = ("explotion.mp3");
                            explode.controls.play();
                            red.RemoveAt(j);
                            player.URL = ("fire.mp3");
                            player.controls.play();
                            lives--;
                            h.RemoveAt(h.Count - 1);

                        }
                    }
                }
                //check if the ship got hitted by egg

                for (int i = 0; i < blue.Count; i++)
                {
                    for (int j = 0; j < blue[i].e.Count; j++)
                    {
                        if ((blue[i].e[j].x >= xship && blue[i].e[j].x <= xship + ship.Width))
                        {
                            if (blue[i].e[j].y >= yship && blue[i].e[j].y <= yship + ship.Height)
                            {
                                crash = 1;
                                explode.URL = ("explotion.mp3");
                                explode.controls.play();
                                blue[i].e.RemoveAt(j);
                                lives--;
                                h.RemoveAt(h.Count - 1);

                            }
                        }
                    }

                }
                for (int l = 0; l < red.Count; l++)
                {
                    for (int k = 0; k < red[l].e.Count; k++)
                    {
                        if ((red[l].e[k].x >= xship && red[l].e[k].x <= xship + ship.Width))
                        {
                            if (red[l].e[k].y >= yship && red[l].e[k].y <= yship + ship.Height)
                            {
                                crash = 1;
                                player.URL = ("explotion.mp3");
                                player.controls.play();
                                red[l].e.RemoveAt(k);
                                lives--;
                                h.RemoveAt(h.Count - 1);

                            }
                        }
                    }
                }
                //remove chickens after clientsize.height
                for (int j = 0; j < blue.Count; j++)
                {
                    if (blue[j].y >= this.ClientSize.Height)
                    {
                        blue.RemoveAt(j);
                    }
                }
                for (int j = 0; j < red.Count; j++)
                {
                    if (red[j].y >= this.ClientSize.Height)
                    {
                        red.RemoveAt(j);
                    }
                }
                if (crash == 1)
                {
                    aftercrash++;
                }
                if (aftercrash == 30)
                {
                    crash = 0;
                    xship = olxship;
                    yship = oldyship;
                    Cursor.Position = new Point(olxship + ship.Width / 2, oldyship + ship.Height / 2);
                    aftercrash = 0;

                }
                //moving the laser
                for (int i = 0; i < l.Count; i++)
                {
                    l[i].y -= 15;
                    if (l[i].y <= 0)
                    {
                        l.RemoveAt(i);
                    }
                }
                //check if laser hits a chicken
                
               for (int k = 0; k < blue.Count; k++)//blue
               {
                    for (int i = 0; i < l.Count; i++)
                    {
                        if (l[i].x > blue[k].x && l[i].x < blue[k].x + bluechicken.Width)
                        {
                            if (l[i].y <= blue[k].y + bluechicken.Height)
                            {
                                player.URL = ("death.mp3");
                                player.controls.play();
                                blue[k].y = -200;
                                l.RemoveAt(i);
                                score += 177;
                            }
                        }
                    }
                }
                
                for (int n = 0; n < red.Count; n++)//red
                {
                    for (int y = 0; y < l.Count; y++)
                    {
                        if (l[y].x >= red[n].x && l[y].x <= red[n].x + redchicken.Width)
                        {
                            if (l[y].y <= red[n].y + redchicken.Height)
                            {
                                player.URL = ("death.mp3");
                                player.controls.play();
                                red[n].y=-200;
                                l.RemoveAt(y);
                                score += 177;
                            }
                        }
                    }
                }

                countcheck = 0;
            }
            Drawdoublebuffer(this.CreateGraphics());
            
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {    
            ship.MakeTransparent();
            heart.MakeTransparent(heart.GetPixel(0,0));

        }

        private void Form1_Load(object sender, EventArgs e)
        {   
            offimage = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            
            intro.URL = ("Intro.mp3");
            intro.controls.play();
            if(lives==0)
            {
                gameover.URL = ("gameover.mp3");
                gameover.controls.play();
            }
            
        }
        void Drawdoublebuffer(Graphics g) 
        {
            Graphics g2 = Graphics.FromImage(offimage);
            Drawscene(g2);
            g.DrawImage(offimage, 0, 0);
        }
        void Drawscene(Graphics g) 
        {
            g.Clear(Color.Black);
            g.DrawImage(bg, backx, backy);
            for (int i = 0; i < h.Count; i++)
            {
                g.DrawImage(heart, h[i].x, h[i].y);
            }

            if (lives != 0)
            {
                
                if (counttheme < 200)
                {

                    g.DrawString("Chicken Invaders", drawFont, Brushes.Yellow, 400, 50);
                    g.DrawString("3", drawFont, Brushes.Yellow, 700, 150);
                }
                for (int i = 0; i < blue.Count; i++)
                {
                    g.DrawImage(bluechicken, blue[i].x, blue[i].y);
                }
                for (int i = 0; i < blue.Count; i++)
                {
                    for (int k = 0; k < blue[i].e.Count; k++)
                    {
                        g.DrawImage(egg, blue[i].e[k].x, blue[i].e[k].y);
                    }

                }
                for (int j = 0; j < red.Count; j++)
                {
                    g.DrawImage(redchicken, red[j].x, red[j].y);
                }
                for (int j = 0; j < red.Count; j++)
                {
                    for (int k = 0; k < red[j].e.Count; k++)
                    {
                        g.DrawImage(egg, red[j].e[k].x, red[j].e[k].y);
                    }
                }
                //draw laser
                for (int i = 0; i < l.Count; i++)
                {
                    g.DrawImage(laser, l[i].x, l[i].y);
                }
                if (crash == 0)
                {
                    g.DrawImage(ship, xship, yship);
                }
                if (crash == 1)
                {
                    g.DrawImage(explosion, xship, yship);
                }
                g.DrawString("S C O R E ", scorefont, Brushes.Yellow, this.ClientSize.Width-250, 10);
                g.DrawString(score.ToString(), scorefont, Brushes.Yellow, this.ClientSize.Width - 200, 80);
            }
            if(lives==0)
            {
                playgameover++;
                Cursor.Show();
                g.DrawString("G  A  M  E   O  V  E  R ", drawFont, Brushes.Yellow, xover, yover);
                g.DrawImage(playagain, playx, playy);
                gameplay.controls.stop();
                if (playgameover == 1)
                {
                    gameover.URL = ("maintheme.mp3");
                    gameover.controls.play();
                }


            }
        }
    }
    class bluechicken
    {
        public int x;
        public int y;
        public int drop;
        public int dir;
        public int moves;
        public List<eggs> e = new List<eggs>();
    }
    class redchicken
    {
        public int x;
        public int y;
        public int drop;
        public int dir;
        public int moves;
        public List<eggs> e = new List<eggs>();
    }
    class eggs
    {
        public int x;
        public int y;
    }
    class laser
    {
        public int x;
        public int y;
    }
    class hearts
    {
        public int x;
        public int y;
    }




}

