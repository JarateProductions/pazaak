using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;
//version 1.0, it works (I think)
/*Changelog:
 * RC3:
 * No longer erroneously starts a new set automatically after a player wins the match
 * 
 * RC2:
 * Standing no longer causes the other player to stand
 * 
 * RC1:
 * First release candidate
 */
namespace pazaak
{
    enum mainCards //these cards are randomly generated and dealt at the beginning of each turn
    {
        Back,
        i1,
        i2,
        i3,
        i4,
        i5,
        i6,
        i7,
        i8,
        i9,
        i10
    }
    enum sideCards //these cards are in player hands
    {              //a player chooses ten and then is dealt four at the beginning of a match                     
        SideBack,  //and must make those four last through the whole game
        Plus1,     //adds its value to the player's sum
        Plus2,
        Plus3,
        Plus4,
        Plus5,
        Plus6,
        Minus1,    //subtracts its value from the player's sum
        Minus2,
        Minus3,
        Minus4,
        Minus5,
        Minus6,
        PM1, //PM = plus/minus, can be flipped by the player
        PM2,
        PM3,
        PM4,
        PM5,
        PM6,
        PM12, //can be flipped between pos/neg and 1/2
        PM1T, //plus/minus 1, if both players stand at the same sum or both have 20, the player who uses this card wins the set
        DoubleLast, //doubles the last card on the player's side of the table
        Flip24, //flips signs of all 2s and 4s on the player's side of the table
        Flip36  //flips signs of all 3s and 6s on the player's side of the table
    }
    public partial class frmMain : Form
    {
        SoundPlayer startTurn = new SoundPlayer(pazaak.Properties.Resources.mgs_startturn);
        SoundPlayer drawCard = new SoundPlayer(pazaak.Properties.Resources.mgs_drawmain);
        SoundPlayer playSide = new SoundPlayer(pazaak.Properties.Resources.mgs_playside);
        public static int c11, c12, c13, c14, c21, c22, c23, c24 = 0;
        public static int p1Sum, p2Sum, p1sets, p2sets = 0;
        public static int p1Space, p2Space = 0;
        public static int currentTurn = 0;
        public static bool p1st, p2st = false;
        public frmMain()
        {
            InitializeComponent();
        }
        public int pl1s()
        {
            return Convert.ToInt32(pMain11.Tag) + Convert.ToInt32(pMain12.Tag) + Convert.ToInt32(pMain13.Tag) + Convert.ToInt32(pMain14.Tag) + Convert.ToInt32(pMain15.Tag) + Convert.ToInt32(pMain16.Tag) + Convert.ToInt32(pMain17.Tag) + Convert.ToInt32(pMain18.Tag) + Convert.ToInt32(pMain19.Tag);
        }
        public int pl2s()
        {
            return Convert.ToInt32(pMain21.Tag) + Convert.ToInt32(pMain22.Tag) + Convert.ToInt32(pMain23.Tag) + Convert.ToInt32(pMain24.Tag) + Convert.ToInt32(pMain25.Tag) + Convert.ToInt32(pMain26.Tag) + Convert.ToInt32(pMain27.Tag) + Convert.ToInt32(pMain28.Tag) + Convert.ToInt32(pMain29.Tag);
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            initPicMain();
        }
        public void initPicMain()
        {
            pMain11.BackgroundImage = mainCardImages[0];
            pMain12.BackgroundImage = mainCardImages[0];
            pMain13.BackgroundImage = mainCardImages[0];
            pMain14.BackgroundImage = mainCardImages[0];
            pMain15.BackgroundImage = mainCardImages[0];
            pMain16.BackgroundImage = mainCardImages[0];
            pMain17.BackgroundImage = mainCardImages[0];
            pMain18.BackgroundImage = mainCardImages[0];
            pMain19.BackgroundImage = mainCardImages[0];
            pMain21.BackgroundImage = mainCardImages[0];
            pMain22.BackgroundImage = mainCardImages[0];
            pMain23.BackgroundImage = mainCardImages[0];
            pMain24.BackgroundImage = mainCardImages[0];
            pMain25.BackgroundImage = mainCardImages[0];
            pMain26.BackgroundImage = mainCardImages[0];
            pMain27.BackgroundImage = mainCardImages[0];
            pMain28.BackgroundImage = mainCardImages[0];
            pMain29.BackgroundImage = mainCardImages[0];
            pSide11.BackgroundImage = sideCardImages[0];
            pSide12.BackgroundImage = sideCardImages[0];
            pSide13.BackgroundImage = sideCardImages[0];
            pSide14.BackgroundImage = sideCardImages[0];
            pSide21.BackgroundImage = sideCardImages[0];
            pSide22.BackgroundImage = sideCardImages[0];
            pSide23.BackgroundImage = sideCardImages[0];
            pSide24.BackgroundImage = sideCardImages[0];
            pMain11.Tag = 0;
            pMain12.Tag = 0;
            pMain13.Tag = 0;
            pMain14.Tag = 0;
            pMain15.Tag = 0;
            pMain16.Tag = 0;
            pMain17.Tag = 0;
            pMain18.Tag = 0;
            pMain19.Tag = 0;
            pMain21.Tag = 0;
            pMain22.Tag = 0;
            pMain23.Tag = 0;
            pMain24.Tag = 0;
            pMain25.Tag = 0;
            pMain26.Tag = 0;
            pMain27.Tag = 0;
            pMain28.Tag = 0;
            pMain29.Tag = 0;
            pSide11.Tag = 0;
            pSide12.Tag = 0;
            pSide13.Tag = 0;
            pSide14.Tag = 0;
            pSide21.Tag = 0;
            pSide22.Tag = 0;
            pSide23.Tag = 0;
            pSide24.Tag = 0;
        }
        public void initPicSet()
        {
            pMain11.BackgroundImage = mainCardImages[0];
            pMain12.BackgroundImage = mainCardImages[0];
            pMain13.BackgroundImage = mainCardImages[0];
            pMain14.BackgroundImage = mainCardImages[0];
            pMain15.BackgroundImage = mainCardImages[0];
            pMain16.BackgroundImage = mainCardImages[0];
            pMain17.BackgroundImage = mainCardImages[0];
            pMain18.BackgroundImage = mainCardImages[0];
            pMain19.BackgroundImage = mainCardImages[0];
            pMain21.BackgroundImage = mainCardImages[0];
            pMain22.BackgroundImage = mainCardImages[0];
            pMain23.BackgroundImage = mainCardImages[0];
            pMain24.BackgroundImage = mainCardImages[0];
            pMain25.BackgroundImage = mainCardImages[0];
            pMain26.BackgroundImage = mainCardImages[0];
            pMain27.BackgroundImage = mainCardImages[0];
            pMain28.BackgroundImage = mainCardImages[0];
            pMain29.BackgroundImage = mainCardImages[0];
            pMain11.Tag = 0;
            pMain12.Tag = 0;
            pMain13.Tag = 0;
            pMain14.Tag = 0;
            pMain15.Tag = 0;
            pMain16.Tag = 0;
            pMain17.Tag = 0;
            pMain18.Tag = 0;
            pMain19.Tag = 0;
            pMain21.Tag = 0;
            pMain22.Tag = 0;
            pMain23.Tag = 0;
            pMain24.Tag = 0;
            pMain25.Tag = 0;
            pMain26.Tag = 0;
            pMain27.Tag = 0;
            pMain28.Tag = 0;
            pMain29.Tag = 0;
        }
        public void newgame()
        {
            
            c11 = roll();
            c12 = roll();
            while (c12 == c11)
            {
                c12 = roll();
            }
            c13 = roll();
            while (c13 == c11 || c13 == c12)
            {
                c13 = roll();
            }
            c14 = roll();
            while (c14 == c11 || c14 == c12 || c14 == c13)
            {
                c14 = roll();
            }
            pSide11.BackgroundImage = defaultSideDeckImages[c11];
            pSide11.Tag = c11;
            pSide12.BackgroundImage = defaultSideDeckImages[c12];
            pSide12.Tag = c12;
            pSide13.BackgroundImage = defaultSideDeckImages[c13];
            pSide13.Tag = c13;
            pSide14.BackgroundImage = defaultSideDeckImages[c14];
            pSide14.Tag = c14;

            c21 = roll();
            c22 = roll();
            while (c22 == c21)
            {
                c22 = roll();
            }
            c23 = roll();
            while (c23 == c21 || c23 == c22)
            {
                c23 = roll();
            }
            c24 = roll();
            while (c24 == c21 || c24 == c22 || c24 == c23)
            {
                c24 = roll();
            }
            pSide21.BackgroundImage = defaultSideDeckImages[c21];
            pSide21.Tag = c21;
            pSide22.BackgroundImage = defaultSideDeckImages[c22];
            pSide22.Tag = c22;
            pSide23.BackgroundImage = defaultSideDeckImages[c23];
            pSide23.Tag = c23;
            pSide24.BackgroundImage = defaultSideDeckImages[c24];
            pSide24.Tag = c24;
            p1Space = 1;
            p2Space = 1;
            button1.Enabled = false;
            currentTurn = 1;
            p1Turn();

        }
        public void sTurn()
        {
            if (currentTurn == 1)
            {
                if (pl1s() >= 20)
                {
                    p1st = true;
                }
                currentTurn++;
                p2Turn();
            }
            else if (currentTurn == 2)
            {
                if (pl2s() >= 20)
                {
                    p2st = true;
                }
                currentTurn--;
                p1Turn();
            }
        }
        public void p1Turn()
        {
            if (p1st == true && p2st == false)
            {
                sTurn();
            }
            else if (p1st == true && p2st == true)
            {
                p1Sum = pl1s();
                p2Sum = pl2s();
                checkWin(p1Sum, p2Sum, ref p1sets, ref p2sets);
            }
            else
            {
            MessageBox.Show("Player 1's turn");
            startTurn.Play();
            System.Threading.Thread.Sleep(555);
            int newRoll = roll();
            switch (p1Space)
            {
                case 1:
                    {
                        pMain11.BackgroundImage = mainCardImages[newRoll];
                        pMain11.Tag = newRoll;
                        break;
                    }
                case 2:
                    {
                        pMain12.BackgroundImage = mainCardImages[newRoll];
                        pMain12.Tag = newRoll;
                        break;
                    }
                case 3:
                    {
                        pMain13.BackgroundImage = mainCardImages[newRoll];
                        pMain13.Tag = newRoll;
                        break;
                    }
                case 4:
                    {
                        pMain14.BackgroundImage = mainCardImages[newRoll];
                        pMain14.Tag = newRoll;
                        break;
                    }
                case 5:
                    {
                        pMain15.BackgroundImage = mainCardImages[newRoll];
                        pMain15.Tag = newRoll;
                        break;
                    }
                case 6:
                    {
                        pMain16.BackgroundImage = mainCardImages[newRoll];
                        pMain16.Tag = newRoll;
                        break;
                    }
                case 7:
                    {
                        pMain17.BackgroundImage = mainCardImages[newRoll];
                        pMain17.Tag = newRoll;
                        break;
                    }
                case 8:
                    {
                        pMain18.BackgroundImage = mainCardImages[newRoll];
                        pMain18.Tag = newRoll;
                        break;
                    }
                case 9:
                    {
                        pMain19.BackgroundImage = mainCardImages[newRoll];
                        pMain19.Tag = newRoll;
                        break;
                    }
            }
            drawCard.Play();
            p1Space++;
        }
        }

        public void p2Turn()
        {
            if (p2st == true && p1st == false)
            {
                sTurn();
            }
            else if (p1st == true && p2st == true)
            {
                p1Sum = pl1s();
                p2Sum = pl2s();
                checkWin(p1Sum, p2Sum, ref p1sets, ref p2sets);
            }
            else
            {
                MessageBox.Show("Player 2's turn");
                startTurn.Play();
                System.Threading.Thread.Sleep(555);
                int newRoll = roll();
                switch (p2Space)
                {
                    case 1:
                        {
                            pMain21.BackgroundImage = mainCardImages[newRoll];
                            pMain21.Tag = newRoll;
                            break;
                        }
                    case 2:
                        {
                            pMain22.BackgroundImage = mainCardImages[newRoll];
                            pMain22.Tag = newRoll;
                            break;
                        }
                    case 3:
                        {
                            pMain23.BackgroundImage = mainCardImages[newRoll];
                            pMain23.Tag = newRoll;
                            break;
                        }
                    case 4:
                        {
                            pMain24.BackgroundImage = mainCardImages[newRoll];
                            pMain24.Tag = newRoll;
                            break;
                        }
                    case 5:
                        {
                            pMain25.BackgroundImage = mainCardImages[newRoll];
                            pMain25.Tag = newRoll;
                            break;
                        }
                    case 6:
                        {
                            pMain26.BackgroundImage = mainCardImages[newRoll];
                            pMain26.Tag = newRoll;
                            break;
                        }
                    case 7:
                        {
                            pMain27.BackgroundImage = mainCardImages[newRoll];
                            pMain27.Tag = newRoll;
                            break;
                        }
                    case 8:
                        {
                            pMain28.BackgroundImage = mainCardImages[newRoll];
                            pMain28.Tag = newRoll;
                            break;
                        }
                    case 9:
                        {
                            pMain29.BackgroundImage = mainCardImages[newRoll];
                            pMain29.Tag = newRoll;
                            break;
                        }
                }
                drawCard.Play();
                p2Space++;
            }
        }
        public int roll()
        {
            Random dice = new Random();
            return dice.Next(1, 11);
        }
        public static Image[] mainCardImages = { pazaak.Properties.Resources.back, pazaak.Properties.Resources.i1, pazaak.Properties.Resources.i2, pazaak.Properties.Resources.i3, pazaak.Properties.Resources.i4, pazaak.Properties.Resources.i5, pazaak.Properties.Resources.i6, pazaak.Properties.Resources.i7, pazaak.Properties.Resources.i8, pazaak.Properties.Resources.i9, pazaak.Properties.Resources.i10 };
        public static Image[] sideCardImages = { pazaak.Properties.Resources.sideback, pazaak.Properties.Resources.p1, pazaak.Properties.Resources.p2, pazaak.Properties.Resources.p3, pazaak.Properties.Resources.p4, pazaak.Properties.Resources.p5, pazaak.Properties.Resources.p6, pazaak.Properties.Resources.m1, pazaak.Properties.Resources.m2, pazaak.Properties.Resources.m3, pazaak.Properties.Resources.m4, pazaak.Properties.Resources.m5, pazaak.Properties.Resources.m6, pazaak.Properties.Resources.pm1, pazaak.Properties.Resources.pm2, pazaak.Properties.Resources.pm3, pazaak.Properties.Resources.pm4, pazaak.Properties.Resources.pm5, pazaak.Properties.Resources.pm6, pazaak.Properties.Resources.pm12, pazaak.Properties.Resources.pm1T, pazaak.Properties.Resources.doubleLast, pazaak.Properties.Resources.f24, pazaak.Properties.Resources.f36 }; //holy fuck that's a lot of images
        public static Image[] defaultSideDeckImages = { sideCardImages[0], sideCardImages[1], sideCardImages[2], sideCardImages[3], sideCardImages[4], sideCardImages[5], sideCardImages[6], sideCardImages[7], sideCardImages[8], sideCardImages[9], sideCardImages[10] };
/*
#################################################
#########FUCK THIS SECTION IN PARTICULAR#########
#################################################
        public static pazaak.mainCards[] mainDeckArray = { mainCards.Back, mainCards.i1, mainCards.i2, mainCards.i3, mainCards.i4, mainCards.i5, mainCards.i6, mainCards.i7, mainCards.i8, mainCards.i9, mainCards.i10 }; 
        //all positions in array are equal to card value
        public static pazaak.sideCards[] defSideDeckArray = { sideCards.SideBack, sideCards.Plus1, sideCards.Plus2, sideCards.Plus3, sideCards.Plus4, sideCards.Plus5, sideCards.Plus6, sideCards.Minus1, sideCards.Minus2, sideCards.Minus3, sideCards.Minus4 };
            //positions in this array are not necessarily related to card value, but real cards start at 1
*/
        public void initSet() //after a set is concluded, this sets sums to 0 and starts a new set
            {
                p1Sum = 0;
                p2Sum = 0;
                p1st = false;
                p2st = false;
                p1Space = 1;
                p2Space = 1;
                initPicSet();
                currentTurn = 2;
                sTurn();
            }
        public void checkWin(int p1, int p2, ref int p1s, ref int p2s) //refs are hard, whining about them is easy
            {
                bool win = false;
                SoundPlayer setWin = new SoundPlayer(pazaak.Properties.Resources.mgs_winset);
                SoundPlayer matchWin = new SoundPlayer(pazaak.Properties.Resources.mgs_winmatch);
                SoundPlayer setDraw = new SoundPlayer(pazaak.Properties.Resources.mgs_loseset);
                if (p1 < 21 && (p1 > p2 || p2 >= 21))
                {
                    
                    p1s++;
                    switch (p1s)
                    {
                        case 1:
                            {
                                P1S1.BackColor = Color.Blue;
                                break;
                            }
                        case 2:
                            {
                                P1S2.BackColor = Color.Blue;
                                break;
                            }
                        case 3:
                            {
                                P1S3.BackColor = Color.Blue;
                                break;
                            }
                    }
                    setWin.Play();
                    MessageBox.Show("Player 1 wins the set.");
                    if (p1s == 3)
                    {
                        win = true;
                        matchWin.Play();
                        MessageBox.Show("Player 1 wins the game.");
                        initPicMain();
                        button2.Enabled = false;
                        button3.Enabled = false;
                        button1.Enabled = true;
                    }
                }
                else if (p2 < 21 && (p2 > p1 || p1 >= 21))
                {
                    p2s++;
                    switch (p2s)
                    {
                        case 1:
                            {
                                P2S1.BackColor = Color.Red;
                                break;
                            }
                        case 2:
                            {
                                P2S2.BackColor = Color.Red;
                                break;
                            }
                        case 3:
                            {
                                P2S3.BackColor = Color.Red;
                                break;
                            }
                    }
                    setWin.Play();
                    MessageBox.Show("Player 2 wins the set.");
                    if (p2s == 3)
                    {
                        win = true;
                        matchWin.Play();
                        MessageBox.Show("Player 2 wins the game.");
                        initPicMain();
                        button2.Enabled = false;
                        button3.Enabled = false;
                        button1.Enabled = true;
                    }
                }
                else if (p1 == p2 || (p1 >= 21 && p2 >= 21))
                {
                    setDraw.Play();
                    MessageBox.Show("Draw");
                }
                if (win == false)
                {
                    initSet();
                }
            }

        private void button1_Click(object sender, EventArgs e)
        {
            newgame();
            button2.Enabled = true;
            button3.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            sTurn();
        }

        private void pSide11_Click(object sender, EventArgs e)
        {
            string st = pSide11.Tag.ToString();
            int i = Convert.ToInt32(st);
            if (currentTurn == 1 && i != 0)
            {
                playSide.Play();
                if (i > 6)
                {
                    i = 6 - i;
                }
                switch (p1Space)
                {
                    case 1:
                        {
                            pMain11.BackgroundImage = pSide11.BackgroundImage;
                            pMain11.Tag = i;
                            break;
                        }
                    case 2:
                        {
                            pMain12.BackgroundImage = pSide11.BackgroundImage;
                            pMain12.Tag = i;
                            break;
                        }
                    case 3:
                        {
                            pMain13.BackgroundImage = pSide11.BackgroundImage;
                            pMain13.Tag = i;
                            break;
                        }
                    case 4:
                        {
                            pMain14.BackgroundImage = pSide11.BackgroundImage;
                            pMain14.Tag = i;
                            break;
                        }
                    case 5:
                        {
                            pMain15.BackgroundImage = pSide11.BackgroundImage;
                            pMain15.Tag = i;
                            break;
                        }
                    case 6:
                        {
                            pMain16.BackgroundImage = pSide11.BackgroundImage;
                            pMain16.Tag = i;
                            break;
                        }
                    case 7:
                        {
                            pMain17.BackgroundImage = pSide11.BackgroundImage;
                            pMain17.Tag = i;
                            break;
                        }
                    case 8:
                        {
                            pMain18.BackgroundImage = pSide11.BackgroundImage;
                            pMain18.Tag = i;
                            break;
                        }
                    case 9:
                        {
                            pMain19.BackgroundImage = pSide11.BackgroundImage;
                            pMain19.Tag = i;
                            break;
                        }
                }
                p1Space++;
                pSide11.Tag = 0;
                pSide11.BackgroundImage = sideCardImages[0];
            }
        }
        private void pSide12_Click(object sender, EventArgs e)
        {
            string st = pSide12.Tag.ToString();
            int i = Convert.ToInt32(st);
            if (currentTurn == 1 && i != 0)
            {
                playSide.Play();
                if (i > 6)
                {
                    i = 6 - i;
                }
                switch (p1Space)
                {
                    case 1:
                        {
                            pMain11.BackgroundImage = pSide12.BackgroundImage;
                            pMain11.Tag = i;
                            break;
                        }
                    case 2:
                        {
                            pMain12.BackgroundImage = pSide12.BackgroundImage;
                            pMain12.Tag = i;
                            break;
                        }
                    case 3:
                        {
                            pMain13.BackgroundImage = pSide12.BackgroundImage;
                            pMain13.Tag = i;
                            break;
                        }
                    case 4:
                        {
                            pMain14.BackgroundImage = pSide12.BackgroundImage;
                            pMain14.Tag = i;
                            break;
                        }
                    case 5:
                        {
                            pMain15.BackgroundImage = pSide12.BackgroundImage;
                            pMain15.Tag = i;
                            break;
                        }
                    case 6:
                        {
                            pMain16.BackgroundImage = pSide12.BackgroundImage;
                            pMain16.Tag = i;
                            break;
                        }
                    case 7:
                        {
                            pMain17.BackgroundImage = pSide12.BackgroundImage;
                            pMain17.Tag = i;
                            break;
                        }
                    case 8:
                        {
                            pMain18.BackgroundImage = pSide12.BackgroundImage;
                            pMain18.Tag = i;
                            break;
                        }
                    case 9:
                        {
                            pMain19.BackgroundImage = pSide12.BackgroundImage;
                            pMain19.Tag = i;
                            break;
                        }
                }
                p1Space++;
                pSide12.Tag = 0;
                pSide12.BackgroundImage = sideCardImages[0];
            }
        }

        private void pSide13_Click(object sender, EventArgs e)
        {
            string st = pSide13.Tag.ToString();
            int i = Convert.ToInt32(st);
            if (currentTurn == 1 && i != 0)
            {
                playSide.Play();
                if (i > 6)
                {
                    i = 6 - i;
                }
                switch (p1Space)
                {
                    case 1:
                        {
                            pMain11.BackgroundImage = pSide13.BackgroundImage;
                            pMain11.Tag = i;
                            break;
                        }
                    case 2:
                        {
                            pMain12.BackgroundImage = pSide13.BackgroundImage;
                            pMain12.Tag = i;
                            break;
                        }
                    case 3:
                        {
                            pMain13.BackgroundImage = pSide13.BackgroundImage;
                            pMain13.Tag = i;
                            break;
                        }
                    case 4:
                        {
                            pMain14.BackgroundImage = pSide13.BackgroundImage;
                            pMain14.Tag = i;
                            break;
                        }
                    case 5:
                        {
                            pMain15.BackgroundImage = pSide13.BackgroundImage;
                            pMain15.Tag = i;
                            break;
                        }
                    case 6:
                        {
                            pMain16.BackgroundImage = pSide13.BackgroundImage;
                            pMain16.Tag = i;
                            break;
                        }
                    case 7:
                        {
                            pMain17.BackgroundImage = pSide13.BackgroundImage;
                            pMain17.Tag = i;
                            break;
                        }
                    case 8:
                        {
                            pMain18.BackgroundImage = pSide13.BackgroundImage;
                            pMain18.Tag = i;
                            break;
                        }
                    case 9:
                        {
                            pMain19.BackgroundImage = pSide13.BackgroundImage;
                            pMain19.Tag = i;
                            break;
                        }
                }
                p1Space++;
                pSide13.Tag = 0;
                pSide13.BackgroundImage = sideCardImages[0];
            }
        }

        private void pSide14_Click(object sender, EventArgs e)
        {
            string st = pSide14.Tag.ToString();
            int i = Convert.ToInt32(st);
            if (currentTurn == 1 && i != 0)
            {
                playSide.Play();
                if (i > 6)
                {
                    i = 6 - i;
                }
                switch (p1Space)
                {
                    case 1:
                        {
                            pMain11.BackgroundImage = pSide14.BackgroundImage;
                            pMain11.Tag = i;
                            break;
                        }
                    case 2:
                        {
                            pMain12.BackgroundImage = pSide14.BackgroundImage;
                            pMain12.Tag = i;
                            break;
                        }
                    case 3:
                        {
                            pMain13.BackgroundImage = pSide14.BackgroundImage;
                            pMain13.Tag = i;
                            break;
                        }
                    case 4:
                        {
                            pMain14.BackgroundImage = pSide14.BackgroundImage;
                            pMain14.Tag = i;
                            break;
                        }
                    case 5:
                        {
                            pMain15.BackgroundImage = pSide14.BackgroundImage;
                            pMain15.Tag = i;
                            break;
                        }
                    case 6:
                        {
                            pMain16.BackgroundImage = pSide14.BackgroundImage;
                            pMain16.Tag = i;
                            break;
                        }
                    case 7:
                        {
                            pMain17.BackgroundImage = pSide14.BackgroundImage;
                            pMain17.Tag = i;
                            break;
                        }
                    case 8:
                        {
                            pMain18.BackgroundImage = pSide14.BackgroundImage;
                            pMain18.Tag = i;
                            break;
                        }
                    case 9:
                        {
                            pMain19.BackgroundImage = pSide14.BackgroundImage;
                            pMain19.Tag = i;
                            break;
                        }
                }
                p1Space++;
                pSide14.Tag = 0;
                pSide14.BackgroundImage = sideCardImages[0];
            }
        }

        private void pSide21_Click(object sender, EventArgs e)
        {
            string st = pSide21.Tag.ToString();
            int i = Convert.ToInt32(st);
            if (currentTurn == 2 && i != 0)
            {
                playSide.Play();
                if (i > 6)
                {
                    i = 6 - i;
                }
                switch (p2Space)
                {
                    case 1:
                        {
                            pMain21.BackgroundImage = pSide21.BackgroundImage;
                            pMain21.Tag = i;
                            break;
                        }
                    case 2:
                        {
                            pMain22.BackgroundImage = pSide21.BackgroundImage;
                            pMain22.Tag = i;
                            break;
                        }
                    case 3:
                        {
                            pMain23.BackgroundImage = pSide21.BackgroundImage;
                            pMain23.Tag = i;
                            break;
                        }
                    case 4:
                        {
                            pMain24.BackgroundImage = pSide21.BackgroundImage;
                            pMain24.Tag = i;
                            break;
                        }
                    case 5:
                        {
                            pMain25.BackgroundImage = pSide21.BackgroundImage;
                            pMain25.Tag = i;
                            break;
                        }
                    case 6:
                        {
                            pMain26.BackgroundImage = pSide21.BackgroundImage;
                            pMain26.Tag = i;
                            break;
                        }
                    case 7:
                        {
                            pMain27.BackgroundImage = pSide21.BackgroundImage;
                            pMain27.Tag = i;
                            break;
                        }
                    case 8:
                        {
                            pMain28.BackgroundImage = pSide21.BackgroundImage;
                            pMain28.Tag = i;
                            break;
                        }
                    case 9:
                        {
                            pMain29.BackgroundImage = pSide21.BackgroundImage;
                            pMain29.Tag = i;
                            break;
                        }
                }
                p2Space++;
                pSide21.Tag = 0;
                pSide21.BackgroundImage = sideCardImages[0];
            }
        }

        private void pSide22_Click(object sender, EventArgs e)
        {
            string st = pSide22.Tag.ToString();
            int i = Convert.ToInt32(st);
            if (currentTurn == 2 && i != 0)
            {
                playSide.Play();
                if (i > 6)
                {
                    i = 6 - i;
                }
                switch (p2Space)
                {
                    case 1:
                        {
                            pMain21.BackgroundImage = pSide22.BackgroundImage;
                            pMain21.Tag = i;
                            break;
                        }
                    case 2:
                        {
                            pMain22.BackgroundImage = pSide22.BackgroundImage;
                            pMain22.Tag = i;
                            break;
                        }
                    case 3:
                        {
                            pMain23.BackgroundImage = pSide22.BackgroundImage;
                            pMain23.Tag = i;
                            break;
                        }
                    case 4:
                        {
                            pMain24.BackgroundImage = pSide22.BackgroundImage;
                            pMain24.Tag = i;
                            break;
                        }
                    case 5:
                        {
                            pMain25.BackgroundImage = pSide22.BackgroundImage;
                            pMain25.Tag = i;
                            break;
                        }
                    case 6:
                        {
                            pMain26.BackgroundImage = pSide22.BackgroundImage;
                            pMain26.Tag = i;
                            break;
                        }
                    case 7:
                        {
                            pMain27.BackgroundImage = pSide22.BackgroundImage;
                            pMain27.Tag = i;
                            break;
                        }
                    case 8:
                        {
                            pMain28.BackgroundImage = pSide22.BackgroundImage;
                            pMain28.Tag = i;
                            break;
                        }
                    case 9:
                        {
                            pMain29.BackgroundImage = pSide22.BackgroundImage;
                            pMain29.Tag = i;
                            break;
                        }
                }
                p2Space++;
                pSide22.Tag = 0;
                pSide22.BackgroundImage = sideCardImages[0];
            }
        }

        private void pSide23_Click(object sender, EventArgs e)
        {
            string st = pSide23.Tag.ToString();
            int i = Convert.ToInt32(st);
            if (currentTurn == 2 && i != 0)
            {
                playSide.Play();
                if (i > 6)
                {
                    i = 6 - i;
                }
                switch (p2Space)
                {
                    case 1:
                        {
                            pMain21.BackgroundImage = pSide23.BackgroundImage;
                            pMain21.Tag = i;
                            break;
                        }
                    case 2:
                        {
                            pMain22.BackgroundImage = pSide23.BackgroundImage;
                            pMain22.Tag = i;
                            break;
                        }
                    case 3:
                        {
                            pMain23.BackgroundImage = pSide23.BackgroundImage;
                            pMain23.Tag = i;
                            break;
                        }
                    case 4:
                        {
                            pMain24.BackgroundImage = pSide23.BackgroundImage;
                            pMain24.Tag = i;
                            break;
                        }
                    case 5:
                        {
                            pMain25.BackgroundImage = pSide23.BackgroundImage;
                            pMain25.Tag = i;
                            break;
                        }
                    case 6:
                        {
                            pMain26.BackgroundImage = pSide23.BackgroundImage;
                            pMain26.Tag = i;
                            break;
                        }
                    case 7:
                        {
                            pMain27.BackgroundImage = pSide23.BackgroundImage;
                            pMain27.Tag = i;
                            break;
                        }
                    case 8:
                        {
                            pMain28.BackgroundImage = pSide23.BackgroundImage;
                            pMain28.Tag = i;
                            break;
                        }
                    case 9:
                        {
                            pMain29.BackgroundImage = pSide23.BackgroundImage;
                            pMain29.Tag = i;
                            break;
                        }
                }
                p2Space++;
                pSide23.Tag = 0;
                pSide23.BackgroundImage = sideCardImages[0];
            }
        }

        private void pSide24_Click(object sender, EventArgs e)
        {
            string st = pSide24.Tag.ToString();
            int i = Convert.ToInt32(st);
            if (currentTurn == 2 && i != 0)
            {
                playSide.Play();
                if (i > 6)
                {
                    i = 6 - i;
                }
                switch (p2Space)
                {
                    case 1:
                        {
                            pMain21.BackgroundImage = pSide24.BackgroundImage;
                            pMain21.Tag = i;
                            break;
                        }
                    case 2:
                        {
                            pMain22.BackgroundImage = pSide24.BackgroundImage;
                            pMain22.Tag = i;
                            break;
                        }
                    case 3:
                        {
                            pMain23.BackgroundImage = pSide24.BackgroundImage;
                            pMain23.Tag = i;
                            break;
                        }
                    case 4:
                        {
                            pMain24.BackgroundImage = pSide24.BackgroundImage;
                            pMain24.Tag = i;
                            break;
                        }
                    case 5:
                        {
                            pMain25.BackgroundImage = pSide24.BackgroundImage;
                            pMain25.Tag = i;
                            break;
                        }
                    case 6:
                        {
                            pMain26.BackgroundImage = pSide24.BackgroundImage;
                            pMain26.Tag = i;
                            break;
                        }
                    case 7:
                        {
                            pMain27.BackgroundImage = pSide24.BackgroundImage;
                            pMain27.Tag = i;
                            break;
                        }
                    case 8:
                        {
                            pMain28.BackgroundImage = pSide24.BackgroundImage;
                            pMain28.Tag = i;
                            break;
                        }
                    case 9:
                        {
                            pMain29.BackgroundImage = pSide24.BackgroundImage;
                            pMain29.Tag = i;
                            break;
                        }
                }
                p2Space++;
                pSide24.Tag = 0;
                pSide24.BackgroundImage = sideCardImages[0];
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (currentTurn == 1)
            {
                p1st = true;
                sTurn();
            }
            else if (currentTurn == 2)
            {
                p2st = true;
                sTurn();
            }
        }

    }
}
