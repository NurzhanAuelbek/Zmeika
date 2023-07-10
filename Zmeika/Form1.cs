using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zmeika
{
    public partial class Form1 : Form
    {
        //дефолтный значение нашего окошка
        private int width = 900;
        private int height = 800;
        //размер нашего квадрата
        private int size_picture = 40;
        //переменные движение
        private int dvij_x, dvij_y;
        //ФруктЫ
        private PictureBox fruct;
        //рандомные коррдинаты для фруктов
        private int rI, rJ;
        //Структура змейки
        private PictureBox[] zmeika = new PictureBox[400];
        //Score
        private Label labelScore;
        private int score = 0;
        public Form1()
        {
            InitializeComponent();
            //При запуске нашей прогрраммы откроется окно который мы задали
            // width и height
            this.Width = width;
            this.Height = height;
            //Score
            labelScore = new Label();
            labelScore.Text = "Score: 0";
            labelScore.Location = new Point(810, 10);
            this.Controls.Add(labelScore);
            //В центре будет появляться наша змейка
            zmeika[0] = new PictureBox();
            zmeika[0].Location = new Point(200, 200);
            zmeika[0].Size = new Size(size_picture,size_picture);
            zmeika[0].BackColor = Color.Blue;
            this.Controls.Add(zmeika[0]);
            //Метод генератор карты
            Generator_Karty();
            //Движение коррдинат
            dvij_x = 1;
            dvij_y = 0;
            //Размер и цвет фрукта
            fruct = new PictureBox();
            fruct.BackColor = Color.Red;
            fruct.Size = new Size(size_picture,size_picture);
            //Вызываем метод фрукт
            Generator_Fruct();
            //Интервал таймера
            timer1.Tick += new EventHandler(Update);
            timer1.Interval = 200;
            timer1.Start();
            //Действие при нажатие
            this.KeyDown += new KeyEventHandler(Dvij);
        }
        private void Granica_Zmeika()
        {
            if (zmeika[0].Location.X < 0)
            {
                for (int _i = 1; _i <= score; _i++)
                {
                    this.Controls.Remove(zmeika[_i]);
                }
                score = 0;
                labelScore.Text = "Score: " + score;
                dvij_x = 1;
            }
            if (zmeika[0].Location.X > height)
            {
                for (int _i = 1; _i <= score; _i++)
                {
                    this.Controls.Remove(zmeika[_i]);
                }
                score = 0;
                labelScore.Text = "Score: " + score;
                dvij_x = -1;
            }
            if (zmeika[0].Location.Y < 0)
            {
                for (int _i = 1; _i <= score; _i++)
                {
                    this.Controls.Remove(zmeika[_i]);
                }
                score = 0;
                labelScore.Text = "Score: " + score;
                dvij_y = 1;
            }
            if (zmeika[0].Location.Y > height)
            {
                for (int _i = 1; _i <= score; _i++)
                {
                    this.Controls.Remove(zmeika[_i]);
                }
                score = 0;
                labelScore.Text = "Score: " + score;
                dvij_y = -1;
            }
        }
        private void Generator_Fruct()
        {
            Random r = new Random();
            rI = r.Next(0, height - size_picture);
            int tempI = rI % size_picture;
            rI -= tempI;
            rJ = r.Next(0, height - size_picture);
            int tempJ = rJ % size_picture;
            rJ -= tempJ;
            fruct.Location = new Point(rI,rJ);
            this.Controls.Add(fruct);

        }
        //Poedanie sebia
        private void Eat_Zmeika()
        {
            for (int _i = 1; _i < score; _i++)
            {
                if (zmeika[0].Location == zmeika[_i].Location)
                {
                    for (int _j = _i; _j <= score; _j++)
                        this.Controls.Remove(zmeika[_j]);
                    score = score - (score - _i + 1);
                    labelScore.Text = "Score: " + score;
                }
            }
        }
        
        //Dvijenie_Zmeiki
        private void Dvijenie_Zmeiki()
        {
            for (int i = score; i >= 1; i--)
            {
                zmeika[i].Location = zmeika[i - 1].Location;
            }
            zmeika[0].Location = new Point(zmeika[0].Location.X + dvij_x * (size_picture), zmeika[0].Location.Y + dvij_y * (size_picture));
            Eat_Zmeika();
        }
        private void Update(Object myObject, EventArgs eventArgs)
        {
            Granica_Zmeika();
            Eat_Fruit();
            Dvijenie_Zmeiki();
            //pictureBox1.Location = new Point(pictureBox1.Location.X + dvij_x * size_picture, pictureBox1.Location.Y + dvij_y * size_picture);
        }
        //Poedanie frukta
        private void Eat_Fruit()
        {
            if (zmeika[0].Location.X == rI && zmeika[0].Location.Y == rJ)
            {
                labelScore.Text = "Score: " + ++score;
                zmeika[score] = new PictureBox();
                zmeika[score].Location = new Point(zmeika[score - 1].Location.X + 40 * dvij_x, zmeika[score - 1].Location.Y - 40 * dvij_y);
                zmeika[score].Size = new Size(size_picture - 1, size_picture - 1);
                zmeika[score].BackColor = Color.Blue;
                this.Controls.Add(zmeika[score]);
                Generator_Fruct();
            }
        }
        private void Generator_Karty()
        {
            for(int i = 0; i < width / size_picture; i++)
            {
                //Что бы что то нарисовать вызываем класс PictureBox
                PictureBox pic = new PictureBox();
                //Задаем цвет
                pic.BackColor = Color.Blue;
                //Ресуем на указонный кординате
                pic.Location = new Point(0, i *size_picture);
                //задаем высоту и шерену нашего цвета.
                pic.Size = new Size(width - 100, 1);
                this.Controls.Add(pic);
            }
            for (int i = 0; i <= height / size_picture; i++)
            {
                PictureBox pic = new PictureBox();
                pic.BackColor = Color.Black;
                pic.Location = new Point(size_picture * i, 0);
                pic.Size = new Size(1, width);
                this.Controls.Add(pic);
            }
        }
        private void Dvij(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode.ToString()) 
            {
                case "Right":
                    dvij_x = 1;
                    dvij_y = 0;
                    break;
                case "Left":
                    dvij_x = -1;
                    dvij_y = 0;
                    break;
                case "Up":
                    dvij_y = -1;
                    dvij_x = 0;
                    break;
                case "Down":
                    dvij_y = 1;
                    dvij_x = 0;
                    break;
            }
        }
    }
}
