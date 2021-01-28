using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Работа_с_таблицами_WinForms
{
    public partial class Sign_In : Form
    {
        public Sign_In()
        {
            InitializeComponent();
        }

        // Закрытие приложения
        private void label8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Переход на форму регистрации
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Sign_Up form = new Sign_Up();
            form.Show();
        }

        // Проверка есть ли пользователь в бд
        public Boolean isUserExists(string log, string pass)
        {
            DB dB = new DB();

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command =
                new MySqlCommand("SELECT * FROM `users` WHERE `login` = @ul AND" +
                "`pass`= @up", dB.getConnection());

            command.Parameters.Add("@ul", MySqlDbType.VarChar).Value = log;
            command.Parameters.Add("@up", MySqlDbType.VarChar).Value = pass;

            adapter.SelectCommand = command;

            adapter.Fill(table);

            if (table.Rows.Count > 0)
                return true;
            else
                return false;
            
        }

        // Авторизация
        private void Auth(object sender, EventArgs e)
        {
            string login = bunifuMaterialTextbox1.Text.Trim(),
                   pass = bunifuMaterialTextbox2.Text.Trim();


            if (login.Length < 5)
            {
                MessageBox.Show("Логин введен неверно!");
                return;
            }

            if (pass.Length < 5)
            {
                MessageBox.Show("Пароль введен неверно!");
                return;
            }

           if( isUserExists(login, pass) && login=="Admin")
            {
                MessageBox.Show("Вы вошли как admin");
                this.Hide();
                Admin_Form form = new Admin_Form();
                form.Show();
                return;
            } 
            
            else if (isUserExists(login, pass)) 
            {
                MessageBox.Show("Вы вошли как user");
                this.Hide();
                User_Form form = new User_Form();
                form.Show();
                return;
            }
            else
                MessageBox.Show("Пользователя не существует");

        }
    }
}
