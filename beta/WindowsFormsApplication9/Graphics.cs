using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApplication9.Properties;

namespace WindowsFormsApplication9
{
    class Graphics
    {
        private int sizeImageX = 70; // размер шашек по x
        private int sizeImageY = 70; // размер шашек по y
        private static Image colourWhite;
        private static Image colourBlack;
        private static Image colourWhite2;
        private static Image colourBlack2;
        private static Image colourNullWhite;
        private static Image colourNullBlack;
        private int amountActiveChess = 0;
        public static TransparentPictureBox ChessActive = new TransparentPictureBox();
        public static bool isStroke = false;
        private static ClientServer clientServer;
        private static Form copyForm;


        private static TransparentPictureBox[,] Chess = new TransparentPictureBox[8, 8];

        public Graphics(Form form, ClientServer clientServer_)
        {
            copyForm = form;
            clientServer = clientServer_;

            if (clientServer.nameClient == "clientServer1")
            {
                isStroke = true;
            }

            int Pol = 1;
            colourWhite = new Bitmap(Resources.white);
            colourWhite2 = new Bitmap(Resources.white2);

            colourBlack = new Bitmap(Resources.black);
            colourBlack2 = new Bitmap(Resources.black2);

            colourNullWhite = new Bitmap(Resources.nullWhite);
            colourNullBlack = new Bitmap(Resources.nullBlack);

            for (int i = 0; i <= 7; i++)
            {
                for (int j = 0; j <= 7; j++)
                {
                    if ((i <= 2) && (j >= 5)) // белые
                    {
                        Chess[i, j] = new TransparentPictureBox();
                        Chess[i, j].j = i;
                        Chess[i, j].i = j;
                        Chess[i, j].chs = "white";
                        Chess[i, j].Image = colourWhite;
                        Chess[i, j].Location = new Point(i * sizeImageX, j * sizeImageY);
                        Chess[i, j].SizeMode = PictureBoxSizeMode.AutoSize;
                        Chess[i, j].Click += NewClick;
                    }
                    else if ((i >= 5) && (j <= 2)) // черные
                    {
                        Chess[i, j] = new TransparentPictureBox();
                        Chess[i, j].j = i;
                        Chess[i, j].i = j;
                        Chess[i, j].chs = "black";
                        Chess[i, j].Image = colourBlack;
                        Chess[i, j].Location = new Point(i * sizeImageX, j * sizeImageY);
                        Chess[i, j].SizeMode = PictureBoxSizeMode.AutoSize;
                        Chess[i, j].Click += NewClick;
                    }
                    else // поле
                    {
                        Chess[i, j] = new TransparentPictureBox();
                        Chess[i, j].j = i;
                        Chess[i, j].i = j;
                        if (Pol%2 == 0)
                            Chess[i, j].Image = colourNullBlack;
                        else
                            Chess[i, j].Image = colourNullWhite;

                        Chess[i, j].Location = new Point(i * sizeImageX, j * sizeImageY);
                        Chess[i, j].SizeMode = PictureBoxSizeMode.AutoSize;
                        Chess[i, j].Click += NewClick;
                    }
                    Pol++;
                    form.Controls.Add(Chess[i, j]);
                }
                Pol++;
            }
            
        } // конец конструктора


        private void NewClick(object Chess, EventArgs e) // метод, дополняющий Click у TransparentPictureBox : Picturebox
        {
            if (isStroke)
            {
                if ((((TransparentPictureBox)Chess).chs == "white" && (clientServer.nameClient == "clientServer1")) || (((TransparentPictureBox)Chess).chs == "black" && (clientServer.nameClient == "clientServer2")))
                {
                    amountActiveChess++; //на данный момент количество выделенных шашек
                    if (amountActiveChess == 2)
                    {
                        SetNotActive();
                        amountActiveChess = 1;
                    }

                    SetActive((TransparentPictureBox)Chess);
                    ChessActive = (TransparentPictureBox)Chess;
                }
                else if (((TransparentPictureBox)Chess).chs == "pole")
                {
                    try
                    {
                        if (ChessActive.active == true)
                        {
                            if (CheckStroke((TransparentPictureBox)Chess) > 0)
                            {
                                int method = CheckStroke((TransparentPictureBox)Chess);
                                Stroke((TransparentPictureBox)Chess, method);
                                SetNotActive();
                                ((TransparentPictureBox)Chess).Update();
                                if (!isStroke)
                                {
                                    isStroke = false;
                                    clientServer.WriteStroke(ChessActive.i, ChessActive.j, ((TransparentPictureBox)Chess).i, ((TransparentPictureBox)Chess).j, method);                                    
                                }
                            }
                        }
                    }
                    catch { }
                }
            }
        }

        private static void SetNotActive()
        {
            for (int i = 0; i <= 7; i++)
            {
                for (int j = 0; j <= 7; j++)
                {
                    if (Chess[i, j].active == true)
                    {
                        if (Chess[i, j].chs == "white")
                        {
                            Chess[i, j].Image = colourWhite;
                            Chess[i, j].active = false;
                        }
                        else if (Chess[i, j].chs == "black")
                        {
                            Chess[i, j].Image = colourBlack;
                            Chess[i, j].active = false;
                        }
                        else if (Chess[i, j].chs == "pole")
                        {
                            Chess[i, j].active = false;
                        }
                    }
                }
            }
        }
        //turn
        private void SetActive(TransparentPictureBox Chess)
        {
            if (Chess.chs == "white")
            {
                Chess.Image = colourWhite2;
                Chess.active = true;
            }
            else if (Chess.chs == "black")
            {
                Chess.Image = colourBlack2;
                Chess.active = true;
            }
        }

        private static int CheckStroke(TransparentPictureBox Pole)
        {
            int check = 0;
            if (Pole.chs == "pole")
            {                
                if (((Math.Abs(Pole.j - ChessActive.j) == 1) && (Pole.i == ChessActive.i)) || ((Math.Abs(Pole.i - ChessActive.i) == 1) && (Pole.j == ChessActive.j)))
                {
                    check = 1;
                }
                else if (
                            ((Math.Abs(Pole.j - ChessActive.j) == 2) && (Pole.i == ChessActive.i) && ((Pole.j - ChessActive.j) > 0) && ((Chess[Pole.j - 1, Pole.i].chs == "white") || (Chess[Pole.j - 1, Pole.i].chs == "black"))) ||
                            ((Math.Abs(Pole.j - ChessActive.j) == 2) && (Pole.i == ChessActive.i) && ((Pole.j - ChessActive.j) < 0) && ((Chess[Pole.j + 1, Pole.i].chs == "white") || (Chess[Pole.j + 1, Pole.i].chs == "black"))) ||
                            ((Math.Abs(Pole.i - ChessActive.i) == 2) && (Pole.j == ChessActive.j) && ((Pole.i - ChessActive.i) > 0) && ((Chess[Pole.j, Pole.i - 1].chs == "white") || (Chess[Pole.j, Pole.i - 1].chs == "black"))) ||
                            ((Math.Abs(Pole.i - ChessActive.i) == 2) && (Pole.j == ChessActive.j) && ((Pole.i - ChessActive.i) < 0) && ((Chess[Pole.j, Pole.i + 1].chs == "white") || (Chess[Pole.j, Pole.i + 1].chs == "black")))
                        )
                {
                    check = 2;
                }                
            }
            return check;
        }

        private void Stroke(TransparentPictureBox Pole, int i)
        {
            Image copyPole = Pole.Image;

            if (ChessActive.Image == colourWhite2)
                Pole.Image = colourWhite;
            else
                Pole.Image = colourBlack;

            if (i == 1)
            {
                if (copyPole == colourNullWhite)
                    ChessActive.Image = colourNullBlack;
                else
                    ChessActive.Image = colourNullWhite;
            }
            else if (i == 2)
            {
                if (copyPole == colourNullWhite)
                    ChessActive.Image = colourNullWhite;
                else
                    ChessActive.Image = colourNullBlack;
            }

            Pole.active = false;
            Pole.chs = ChessActive.chs;
            ChessActive.active = false;
            ChessActive.chs = "pole";

            TransparentPictureBox ChessActive2 = new TransparentPictureBox();
            ChessActive2 = ChessActive;
            ChessActive = Pole;
            try
            {
                if ((CheckStroke(Chess[Pole.j + 2, Pole.i]) > 0) || (CheckStroke(Chess[Pole.j - 2, Pole.i]) > 0) || (CheckStroke(Chess[Pole.j, Pole.i + 2]) > 0) || (CheckStroke(Chess[Pole.j, Pole.i - 2]) > 0))
                {
                    isStroke = true;
                    ChessActive = ChessActive2;
                }
                else
                {
                    isStroke = false;
                    ChessActive = ChessActive2;
                }
            }
            catch { }

            if (CheckHome())
                copyForm.Close();
        }

        public static bool CheckHome()
        {
            bool end = false;
            int valHome1 = 0;
            int valHome2 = 0;

            for (int i = 0; i <= 7; i++)
            {
                for (int j = 0; j <= 7; j++)
                {
                    if ((i <= 2) && (j >= 5)) //  дом белых
                    {
                        if (Chess[i, j].chs == "black")
                            valHome1++;
                    }
                    else if ((i >= 5) && (j <= 2)) // дом черных
                    {
                        if (Chess[i, j].chs == "white")
                            valHome2++;
                    }
                }
            }

            if ((valHome1 == 9) || (valHome2 == 9))
                end = true;

            return end;
        }

        public static void Stroke(int ChessJ, int ChessI, int PoleJ, int PoleI, int method)
        {
            Image copyPole = Chess[PoleI, PoleJ].Image;

            if (Chess[ChessI, ChessJ].Image == colourWhite)
                Chess[PoleI, PoleJ].Image = colourWhite;
            else
                Chess[PoleI, PoleJ].Image = colourBlack;

            if (method == 1)
            {
                if (copyPole == colourNullWhite)
                    Chess[ChessI, ChessJ].Image = colourNullBlack;
                else
                    Chess[ChessI, ChessJ].Image = colourNullWhite;
            }
            else if (method == 2)
            {
                if (copyPole == colourNullWhite)
                    Chess[ChessI, ChessJ].Image = colourNullWhite;
                else
                    Chess[ChessI, ChessJ].Image = colourNullBlack;
            }

            Chess[PoleI, PoleJ].active = false;
            Chess[PoleI, PoleJ].chs = Chess[ChessI, ChessJ].chs;
            Chess[ChessI, ChessJ].active = false;
            Chess[ChessI, ChessJ].chs = "pole";

            if (CheckHome())
                copyForm.Close();
        }

    } // конец Graphics
} // конец WindowsFormsApplication9
