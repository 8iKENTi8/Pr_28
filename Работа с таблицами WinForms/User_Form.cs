/*
 *  Форма: User_Form
 *  
 *  Язык: C#
 *  Разработал: Ролдугин Владимир Дмитриевич, ТИП - 62
 *  Дата: 04.02.2021г
 *  
 *  Задание: 
 *      Предоставляет пользователю просмотр  таблиц
 *      
 *  Подпрограммы, используемые в данной форме:
 *      pictureBox1_Click - переход на форму авторизации;
 *      button2_Click - переход на интерфейс лошади;
 *      button3_Click - переход на интерфейс жокеи;
 *      button1_Click - переход на интерфейс результаты.
 *      
 *      
 */

using System;
using MetroFramework.Forms;


namespace Работа_с_таблицами_WinForms
{
    public partial class User_Form : MetroForm
    {
        public User_Form()
        {
            InitializeComponent();
            SidePanel.Height = button1.Height;
            SidePanel.Top = button1.Top;
            contr_res1.BringToFront();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Sign_Up form = new Sign_Up();
            form.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SidePanel.Height = button2.Height;
            SidePanel.Top = button2.Top;
            contr_hourse1.BringToFront();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SidePanel.Height = button3.Height;
            SidePanel.Top = button3.Top;
            contr_jokei1.BringToFront();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SidePanel.Height = button4.Height;
            SidePanel.Top = button4.Top;
            //mySecondCustmControl1.BringToFront();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SidePanel.Height = button1.Height;
            SidePanel.Top = button1.Top;
            contr_res1.BringToFront();
        }
    }
}
