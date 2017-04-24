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
        private int amountActiveChess = 0;
        private int ActiveChess_i;
        private int ACtiveCHess_j;


        private TransparentPictureBox[,] Chess = new TransparentPictureBox[8, 8];

        public Graphics(Form form)
        {
            colourWhite = new Bitmap(@"H:\Users\denis\Documents\Visual Studio 2015\Projects\kurs\WindowsFormsApplication9\bin\Debug\image\white.png");
            colourWhite2 = new Bitmap(@"H:\Users\denis\Documents\Visual Studio 2015\Projects\kurs\WindowsFormsApplication9\bin\Debug\image\white2.png");

            colourBlack = new Bitmap(@"H:\Users\denis\Documents\Visual Studio 2015\Projects\kurs\WindowsFormsApplication9\bin\Debug\image\black.png");
            colourBlack2 = new Bitmap(@"H:\Users\denis\Documents\Visual Studio 2015\Projects\kurs\WindowsFormsApplication9\bin\Debug\image\black2.png");

            for (int i = 0; i <= 7; i++)
            {
                for (int j = 0; j <= 7; j++)
                {
                    if ((i <= 2) && (j >= 5)) // белые
                    {
                        Chess[i, j] = new TransparentPictureBox();
                        Chess[i, j].chs = "white";
                        Chess[i, j].Image = colourWhite;
                        Chess[i, j].Location = new Point(i * sizeImageX, j * sizeImageY);
                        Chess[i, j].SizeMode = PictureBoxSizeMode.AutoSize;
                        Chess[i, j].Click += (o, e) => NewClick(o, e, i, j);
                    }
                    else if ((i >= 5) && (j <= 2)) // черные
                    {
                        Chess[i, j] = new TransparentPictureBox();
                        Chess[i, j].chs = "black";
                        Chess[i, j].Image = colourBlack;
                        Chess[i, j].Location = new Point(i * sizeImageX, j * sizeImageY);
                        Chess[i, j].SizeMode = PictureBoxSizeMode.AutoSize;
                        Chess[i, j].Click += (o, e) => NewClick(o, e, i, j);
                    }
                    else // поле
                    {
                        Chess[i, j] = new TransparentPictureBox();
                        Chess[i, j].Location = new Point(1000, 1000);
                        Chess[i, j].SizeMode = PictureBoxSizeMode.AutoSize;
                        Chess[i, j].Click += NewClickAtPole;
                    }
                    form.Controls.Add(Chess[i, j]);
                }
            }
        } // конец конструктора

        private void NewClick(object Chess, EventArgs e, int i, int j) // метод, дополняющий Click у TransparentPictureBox : Picturebox
        {
            amountActiveChess++; //на данный момент количество выделенных шашек
            if (amountActiveChess >= 2)
            {
                SetNotActive();
                amountActiveChess = 1;
            }

            SetActive((TransparentPictureBox)Chess, i, j);
        }

        private void NewClickAtPole(object Pole, EventArgs e)
        {
            if (amountActiveChess == 1)
            {
                //CheckStroke((TransparentPictureBox)Pole);
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
                    }
                }
            }
        }

        private void SetActive(TransparentPictureBox Chess, int i, int j)
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
            ActiveChess_i = i;
            ACtiveCHess_j = j;
        }

        /*private bool CheckStroke(TransparentPictureBox Pole)
        {
            bool check = false;
            if (Pole[])
            return check;
        }*/


    } // конец Graphics
} // конец WindowsFormsApplication9
