using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using VMCoffeе.Properties;

namespace VMCoffeе
{
   
    public partial class Form1 : Form
    {
        //количество элементов(монет и напитков)
        const int el = 4;

        //ширина и высота кнопок напитков по умолчанию
        int wid, heig;

        //ширина и высота монет по умолчанию
        int widM, heigM;
        
        //подгружаемый шрифт
        Font f20,f28;
        System.Drawing.Text.PrivateFontCollection fBuf = new System.Drawing.Text.PrivateFontCollection();
            
        //Внесенная сумма
        int total;
        
        //деньги в кошельке
        Money[] purse=new Money[el];
        
        //деньги у vm
        Money[] vm=new Money[el];
        
        //напитки
        Drink[] drink=new Drink[el];

        //кнопки для напитков
        PictureBox[] arrBut = new PictureBox[el];

        //монеты кошелька изображение 
        PictureBox[] arrP = new PictureBox[el];
        
        //монеты кошелька количество
        PictureBox[] arrPС = new PictureBox[el];

        //монеты кошелька изображение 
        PictureBox[] arrVM = new PictureBox[el];

        //монеты кошелька количество
        PictureBox[] arrVMС = new PictureBox[el];
        //***********************************************************//
        
        //инициализируем начальные значения и создаем нужные компоненты
        public Form1()
        {
            InitializeComponent();
            
            //начальное количество денег в кошелке
            //монеты должны быть отсортированны по их наминалу(от min к max)
            purse[0] = new Money(10,1);
            purse[1] = new Money(20,2);    
            purse[2] = new Money(30,5);
            purse[3] = new Money(15,10);

            //начальное количество денег в vm
            //монеты должны быть отсортированны по их наминалу(от min к max)
            vm[0] = new Money(100, 1);
            vm[1] = new Money(100, 2);
            vm[2] = new Money(100, 5);
            vm[3] = new Money(100, 10);
                       
            //начальная внесенная сумма
            total = 0;
            
            //напитки в vm
            drink[0] = new Drink(10, 13, "Чай");
            drink[1] = new Drink(20, 18, "Кофе");
            drink[2] = new Drink(20, 21, "Кофе с молоком");
            drink[3] = new Drink(15, 35, "Сок");
            
            //начальное значение ширины и высоты кнопок
            wid = 270;
            heig = 60;
   
            //создание кнопок
            for (int i = 0; i < arrBut.Length;i++)
            {
                arrBut[i] = new PictureBox();
                arrBut[i].Width = wid;
                arrBut[i].Height = heig;
                arrBut[i].BackColor = Color.Transparent;
                arrBut[i].Tag = i;
                arrBut[i].BackgroundImageLayout = ImageLayout.Stretch;
                arrBut[i].Paint += new PaintEventHandler(But_Paint);
                arrBut[i].MouseDown += new MouseEventHandler(But_MouseDown);
                arrBut[i].MouseUp += new MouseEventHandler(But_MouseUp);
                Controls.Add(arrBut[i]);
            }

            //начальное значение ширины и высоты монет 
            widM = 50;
            heigM = 50;

            //создание монет кошелька
            for (int i = 0; i < el;i++ )
            {
                arrP[i] = new PictureBox();
                arrP[i].Width = widM;
                arrP[i].Height = heigM;
                arrP[i].BackColor = Color.Transparent;
                arrP[i].Tag = i;
                arrP[i].BackgroundImageLayout = ImageLayout.Stretch;
                arrP[i].MouseDown += new MouseEventHandler(p_MouseDown);
                arrP[i].MouseUp += new MouseEventHandler(p_MouseUp);
                Controls.Add(arrP[i]);
            }

            //загружаем изображения монет кошелька
            arrP[0].BackgroundImage = Resources.rub1;
            arrP[1].BackgroundImage = Resources.rub2;
            arrP[2].BackgroundImage = Resources.rub5;
            arrP[3].BackgroundImage = Resources.rub10;

            //создаем дисплеи кол-ва монет кошелька
            for (int i = 0; i < el; i++)
            {
                arrPС[i] = new PictureBox();
                arrPС[i].Width = widM;
                arrPС[i].Height = 34;
                arrPС[i].BackColor = Color.Transparent;
                arrPС[i].Tag = i;
                arrPС[i].BackgroundImageLayout = ImageLayout.Stretch;
                arrPС[i].Paint += new PaintEventHandler(PC_Paint);
                arrPС[i].BackgroundImage = Resources.disp;
                Controls.Add(arrPС[i]);
            }

            //создание монет vm
            for (int i = 0; i < el; i++)
            {
                arrVM[i] = new PictureBox();
                arrVM[i].Width = widM;
                arrVM[i].Height = heigM;
                arrVM[i].BackColor = Color.Transparent;
                arrVM[i].Tag = i;
                arrVM[i].BackgroundImageLayout = ImageLayout.Stretch;
                Controls.Add(arrVM[i]);
            }

            //загружаем изображения монет vm
            arrVM[0].BackgroundImage = Resources.rub1;
            arrVM[1].BackgroundImage = Resources.rub2;
            arrVM[2].BackgroundImage = Resources.rub5;
            arrVM[3].BackgroundImage = Resources.rub10;
            
            //создаем дисплеи кол-ва монет vm
            for (int i = 0; i < el; i++)
            {
                arrVMС[i] = new PictureBox();
                arrVMС[i].Width = widM;
                arrVMС[i].Height = 34;
                arrVMС[i].BackColor = Color.Transparent;
                arrVMС[i].Tag = i;
                arrVMС[i].BackgroundImageLayout = ImageLayout.Stretch;
                arrVMС[i].Paint += new PaintEventHandler(vmC_Paint);
                Controls.Add(arrVMС[i]);
            }

            //Загружаем шрифт
            fBuf.AddFontFile("Content//font.ttf");
            f20 = new Font(fBuf.Families[0], 20);
            f28 = new Font(fBuf.Families[0], 28);
        }
        //***********************************************************//
   
        //Обработка закрытия формы
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //диалог подтверждения выхода из программы
            DialogResult DlgRes = MessageBox.Show("Вы хотите выйте?", "Подтверждение выхода", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DlgRes == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
        //***********************************************************//
        
        //рисование кнопки
        private void But_Paint(object sender, PaintEventArgs e)
        {
            PictureBox temp = sender as PictureBox;
            //добавляем текс с назавание и стоимостью напитка на кнопку
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            e.Graphics.DrawString(drink[Convert.ToInt32(temp.Tag)].Name + " "
                + Convert.ToString(drink[Convert.ToInt32(temp.Tag)].Price) + " руб", 
                new Font("Cambria", 14), Brushes.Black, new PointF(60, heig / 4));
            //добавляем текс с кол-ом напитка на кнопку
            if (Convert.ToInt32(temp.Tag)%2==0)
            e.Graphics.DrawString(Convert.ToString(drink[Convert.ToInt32(temp.Tag)].Count), 
                f20, Brushes.Blue, new PointF(12, heig / 4));
            else
                e.Graphics.DrawString(Convert.ToString(drink[Convert.ToInt32(temp.Tag)].Count),
                f20, Brushes.Blue, new PointF(temp.Width-52, heig / 4));
        }
        //***********************************************************//
        
        //обработка изменения размера формы
        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            displa.Left = Form1.ActiveForm.Width / 2 - displa.Width / 2;
            button1.Left = Form1.ActiveForm.Width / 2 - button1.Width / 2;
        }
        //***********************************************************//
   
        //рисование дисплея
        private void displa_Paint(object sender, PaintEventArgs e)
        {
            //добавление текста на дисплей
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            e.Graphics.DrawString(Convert.ToString(total), f28, Brushes.LightGreen, new PointF(20, heig / 4));
        }
        //***********************************************************//
   
        //обработка нажатие клавиша мыши на кнопке
        private void But_MouseDown(object sender, MouseEventArgs e)
        {
            //при нажатии левой клавиши мыши смещаем кнопку
            if (e.Button ==MouseButtons.Left)
            {
                PictureBox temp = sender as PictureBox;
                temp.Height = heig-1;
                temp.Width = wid-1;
            }
        }
        //***********************************************************//
        
        //обработка события клавиша мыши отпущена на кнопке
        private void But_MouseUp(object sender, MouseEventArgs e)
        {
            //при отпускании левой кнопки мыши
            if (e.Button == MouseButtons.Left)
            {
                // смещаем кнопку
                PictureBox temp = sender as PictureBox;
                temp.Height = heig;
                temp.Width = wid;
                
                //выдаем напиток если это возможно
                if ((drink[Convert.ToInt32(temp.Tag)].Price <= total) && (drink[Convert.ToInt32(temp.Tag)].Count>0))
                {
                    //вычитаем из внесенных денег стоимость напитка
                    total -= drink[Convert.ToInt32(temp.Tag)].Price;

                    //уменьшаем количество напитков
                    drink[Convert.ToInt32(temp.Tag)].sub(1);
                    
                    //обновляем изображение кнопок
                    refreshImage();

                    //обновляем изображение дисплея
                    displa.Refresh();

                    //выдаем сообщение
                    MessageBox.Show("Спасибо!", "Кофемашина", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    string buf;
                    buf = (drink[Convert.ToInt32(temp.Tag)].Price > total) ? "Недостаточно средств" : "Данного напитка нет. \nПриносим свои извенения.";
                    MessageBox.Show(buf, "Кофемашина", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }
        //***********************************************************//
        
        //обработка нажатие клавиша мыши на монете
        private void p_MouseDown(object sender, MouseEventArgs e)
        {
            //при отпускании левой клавиши мыши смещаем монету
            if (e.Button == MouseButtons.Left)
            {
                PictureBox tmp = sender as PictureBox;
                tmp.Height = heigM + 1;
                tmp.Width = widM + 1;
            }
            
        }
        //***********************************************************//
        
        //изменение изображений кнопок
        private void refreshImage()
        {
            for (int i = 0; i < el; i++)
            {
                if ((drink[i].Price <= total) && (drink[i].Count > 0))
                {
                    //левые                                     активная                не активная
                    arrBut[i].BackgroundImage = (i % 2 == 0) ? Resources.ButtonL1 : Resources.ButtonR1;
                }
                    //правые                                     активная                не активная
                else arrBut[i].BackgroundImage = (i % 2 == 0) ? Resources.buttonLR : Resources.buttonRR; 

                //обновляем кнопку
                arrBut[i].Refresh();
            }
        }
        //***********************************************************//
   
        //обновления дисплеев количества монет
        private void refreshDispMoney()
        {
            for (int i = 0; i < el;i++ )
            {
                //кошелька
                arrPС[i].Refresh();

                //vm
                arrVMС[i].Refresh();
            }
        }
        //***********************************************************//
   
        //обработка добавления монеты из кошелка в vm
        private void ifPV(ref  Money p, ref  Money v)
        {
            if (p.sub(1) == 0)
            {
                total += p.Price;
                v.add(1);
            }
        }
        //***********************************************************//
   
        //обработка события клавиша мыши отпущена на монете
        private void p_MouseUp(object sender, MouseEventArgs e)
        {
            //при отпускании левой клавиши мыши смещаем монету
            if (e.Button == MouseButtons.Left)
            {
                PictureBox tmp = sender as PictureBox;
                tmp.Height = heigM;
                tmp.Width = widM;

                //добавляем монету из кошелька в vm
                ifPV(ref purse[Convert.ToInt32(tmp.Tag)], ref vm[Convert.ToInt32(tmp.Tag)]);
                
                //обновляем дисплеи с кол-ом монет
                refreshDispMoney();
                
                //обновляем дисплей с внесенной суммой
                displa.Refresh();
                
                //обновляем кнопки
                refreshImage();

            }

        }
        //***********************************************************//
   

        //рисование дисплея с количесвом монет в кошельке
        private void PC_Paint(object sender, PaintEventArgs e)
        {
            PictureBox tmp = sender as PictureBox;
            //добавление текста на дисплей
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            e.Graphics.DrawString(Convert.ToString(purse[Convert.ToInt32(tmp.Tag)].Count), f20, Brushes.LightGreen, new PointF(0, heig / 7));
            
        }
        //***********************************************************//
   
        //рисование дисплея с количесвом монет в vm
        private void vmC_Paint(object sender, PaintEventArgs e)
        {
            PictureBox tmp = sender as PictureBox;
            //добавление текста на дисплей
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            e.Graphics.DrawString(Convert.ToString(vm[Convert.ToInt32(tmp.Tag)].Count), f20, Brushes.LightGreen, new PointF(0, heig / 7));

        }
        //***********************************************************//
   
        //нажатие кнопки выдачи сдачи
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //даем сдачу
                Money.getMoney(Money.giveMoney(ref vm, total),ref purse);

                //обнуляем внесенную сумму
                total = 0;

                //обновляем дисплей
                refreshDispMoney();

                //обновляем лисплей с внесенной суммой
                displa.Refresh();

                //обновляем кнопки
                refreshImage();
            }
                //обработка исключения
            catch
            {
                MessageBox.Show("Ошибка при выдачи монет", "Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
        //***********************************************************//
   
        //событие при активации формы
        private void Form1_Activated(object sender, EventArgs e)
        {
            for (int i = 0; i < el; i++)
            {
                //размещаем изображения монет кошелька
                arrP[i].Left = Form1.ActiveForm.Width-(20 + widM) * (el-i)-50;
                arrP[i].Top = Form1.ActiveForm.Height - 150 - heigM;

                //размещаем дисплеи количества монет кошелька
                arrPС[i].Left = Form1.ActiveForm.Width - (20 + widM) * (el - i) - 50;
                arrPС[i].Top = Form1.ActiveForm.Height - 70 - heigM;

                //размещаем изображения монет vm
                arrVM[i].Left = (20 + widM) * (el - i) - 50;
                arrVM[i].Top = Form1.ActiveForm.Height - 150 - heigM;

                //размещаем дисплеи количества монет vm
                arrVMС[i].Left = (20 + widM) * (el - i) - 50;
                arrVMС[i].Top = Form1.ActiveForm.Height - 70 - heigM;
                arrVMС[i].BackgroundImage = Resources.disp;

                //размещаем кнопки напитков и загружаем картинки
                arrBut[i].Top = (20 + heig) * ((int)(i / 2) + 1);
                if (i % 2 == 0)
                {
                    //левые неактивные
                    arrBut[i].Left = 10;
                    //arrBut[i].BackgroundImage = Resources.buttonLR;
                }
                else
                {
                    //правые неактивные
                    arrBut[i].Left = Form1.ActiveForm.Width - 10 - wid;
                   // arrBut[i].BackgroundImage = Resources.buttonRR;
                }
            }
            refreshImage();
        }
        //***********************************************************//
   


    }
}
