/*
 *  Форма: Admin_Form
 *  
 *  Язык: C#
 *  Разработал: Ролдугин Владимир Дмитриевич, ТИП - 62
 *  Дата: 04.02.2021г
 *  
 *  Задание: 
 *      Предоставляет админу просмотр всех таблиц
 *      
 *  Подпрограммы, используемые в данной форме:
 *      button1_Click - переход на форму пользователи;
 *      button2_Click - переход на форму владельцы;
 *      button3_Click - переход на форму лошади;
 *      button4_Click - переход на форму жокеи;
 *      button5_Click - переход на форму состязания; 
 *      button6_Click - переход на форму результаты;
 *      button7_Click - переход на форму время проведения событий;
 *      pictureBox1_Click - переход на форму авторизации (или выход из учетной записи);
 *      
 *      
 */

using System;
using MetroFramework.Forms;

namespace Работа_с_таблицами_WinForms
{
    public partial class Admin_Form : MetroForm
    {
    
        public Admin_Form()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Результаты form = new Результаты();
            form.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            пользователи form = new пользователи();
            form.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            владельцы form = new владельцы();
            form.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Жокеи form = new Жокеи();
            form.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            лошади form = new лошади();
            form.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            состязание form = new состязание();
            form.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Sign_Up form = new Sign_Up();
            form.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            Event_time form = new Event_time();
            form.Show();
        }
    }
}
