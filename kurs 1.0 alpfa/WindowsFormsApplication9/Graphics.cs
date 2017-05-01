using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication9
{
    class Graphics
    {
        private int sizeImageX = 70; // размер шашек по x
        private int sizeImageY = 70; // размер шашек по y
        private Image colourWhite;
        private Image colourBlack;
        private Image colourWhite2;
        private Image colourBlack2;
        private Image colourNullWhite;
        private Image colourNullBlack;
        private int amountActiveChess = 0;
        private TransparentPictureBox ChessActive = new TransparentPictureBox();


        private TransparentPictureBox[,] Chess = new TransparentPictureBox[8, 8];

        public Graphics(Form form)
        {
            int Pol = 1;
            colourWhite = new Bitmap(@"image\white.png");
            colourWhite2 = new Bitmap(@"image\white2.png");

            colourBlack = new Bitmap(@"image\black.png");
            colourBlack2 = new Bitmap(@"image\black2.png");

            colourNullWhite = new Bitmap(@"image\nullWhite.png");
            colourNullBlack = new Bitmap(@"image\nullBlack.png");

            for (int i = 0; i <= 7; i++)
            {
                for (int j = 0; j <= 7; j++)
                {
                    if ((i <= 2) && (j >= 5)) // белые
                    {
                        Chess[i, j] = new TransparentPictureBox();
                        Chess[i, j].i = i;
                        Chess[i, j].j = j;
                        Chess[i, j].chs = "white";
                        Chess[i, j].Image = colourWhite;
                        Chess[i, j].Location = new Point(i * sizeImageX, j * sizeImageY);
                        Chess[i, j].SizeMode = PictureBoxSizeMode.AutoSize;
                        Chess[i, j].Click += NewClick;
                    }
                    else if ((i >= 5) && (j <= 2)) // черные
                    {
                        Chess[i, j] = new TransparentPictureBox();
                        Chess[i, j].i = i;
                        Chess[i, j].j = j;
                        Chess[i, j].chs = "black";
                        Chess[i, j].Image = colourBlack;
                        Chess[i, j].Location = new Point(i * sizeImageX, j * sizeImageY);
                        Chess[i, j].SizeMode = PictureBoxSizeMode.AutoSize;
                        Chess[i, j].Click += NewClick;
                    }
                    else // поле
                    {
                        Chess[i, j] = new TransparentPictureBox();
                        Chess[i, j].i = i;
                        Chess[i, j].j = j;
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
            if (((TransparentPictureBox)Chess).chs != "pole")
            {
                amountActiveChess++; //на данный момент количество выделенных шашек
                if (amountActiveChess >= 2)
                {
                    SetNotActive();
                    amountActiveChess = 1;
                }

                SetActive((TransparentPictureBox)Chess);
                ChessActive = (TransparentPictureBox)Chess;
            }
            else if (((TransparentPictureBox)Chess).chs == "pole")
            {
                if (ChessActive.active == true)
                    CheckStroke((TransparentPictureBox)Chess);
            }
        }

        private void SetNotActive()
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

            /*ActiveChess_i = Chess.i;
            ActiveChess_j = Chess.j;*/
        }

        private bool CheckStroke(TransparentPictureBox Pole)
        {
            bool check = false;
            if (((Math.Abs(Pole.i - ChessActive.i) == 1) && (Pole.j == ChessActive.j)) || ((Math.Abs(Pole.j - ChessActive.j) == 1) && (Pole.i == ChessActive.i)))
            {
                Stroke(Pole, 1);
                check = true;
            }
            else if ( 
                        ((Math.Abs(Pole.i - ChessActive.i) == 2) && (Pole.j == ChessActive.j) && ((Pole.i - ChessActive.i) > 0) && (Chess[Pole.i - 1, Pole.j].chs == "white") || (Chess[Pole.i - 1, Pole.j].chs == "black")) ||
                        ((Math.Abs(Pole.i - ChessActive.i) == 2) && (Pole.j == ChessActive.j) && ((Pole.i - ChessActive.i) < 0) && (Chess[Pole.i + 1, Pole.j].chs == "white") || (Chess[Pole.i + 1, Pole.j].chs == "black")) ||
                        ((Math.Abs(Pole.j - ChessActive.j) == 2) && (Pole.i == ChessActive.i) && ((Pole.j - ChessActive.j) > 0) && (Chess[Pole.i, Pole.j - 1].chs == "white") || (Chess[Pole.i, Pole.j - 1].chs == "black")) ||
                        ((Math.Abs(Pole.j - ChessActive.j) == 2) && (Pole.i == ChessActive.i) && ((Pole.j - ChessActive.j) < 0) && (Chess[Pole.i, Pole.j + 1].chs == "white") || (Chess[Pole.i, Pole.j + 1].chs == "black"))
                    )
            {
                Stroke(Pole, 2);
                check = true;
            }

            if (check)
            {
                SetNotActive();
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
        }

    } // конец Graphics
} // конец WindowsFormsApplication9
