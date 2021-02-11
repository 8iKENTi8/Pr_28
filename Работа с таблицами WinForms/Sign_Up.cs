/*
 *  Форма: Sign_up
 *  
 *  Язык: C#
 *  Разработал: Ролдугин Владимир Дмитриевич, ТИП - 62
 *  Дата: 04.02.2021г
 *  
 *  Задание: 
 *      Предоставляет пользователю возможность зарегистрироваться
 *      в систему.
 *      
 *  Переменные, используемые в данной форме:
 *      login - соединение с базой данных;
 *      pass - хранит ссылку на форму логина, используется
 *      pass_2 отображения формы логина.
 *      email - email
 * 
 *  Подпрограммы, используемые в данной форме:
 *      bunifuCheckbox1_OnChange - принятие пользовательских соглашений;
 *      button2_Click - переход на форму регистрации ;
 *      label8_Click - выход;
 *      isUserExists - проверка есть ли пользователь ;
 *      RegUser - Занесение данных в бд ;
 *      Reg_User - Регистрация пользователя в бд.
 *      
 */


using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Работа_с_таблицами_WinForms
{
    public partial class Sign_Up : Form
    {
        public Sign_Up()
        {
            InitializeComponent();
            button1.Enabled = false;
        }

        // Проверка существует ли ползователь
        private Boolean isUserExists()
        {
            DB dB = new DB();

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command =
                new MySqlCommand("SELECT * FROM `users` WHERE `login` = @ul",
                dB.getConnection());

            command.Parameters.Add("@ul",
                MySqlDbType.VarChar).Value = bunifuMaterialTextbox1.Text;

            adapter.SelectCommand = command;

            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Такой логин уже есть, введите другой");
                return true;
            }
            else
                return false;
        }

        // Закртыие приложения
        private void label8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Переход на форму авторизации
        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Sign_In sign = new Sign_In();
            sign.Show();
        }

        //Принятие соглашения на обработку данных 
        private void bunifuCheckbox1_OnChange(object sender, EventArgs e)
        {
            if (bunifuCheckbox1.Checked == true)
                button1.Enabled = true;
            else
                button1.Enabled = false;
        }

        // Занесение данных в бд
        private void RegUser(string log, string pass, string email)
        {
            DB dB = new DB();

            MySqlCommand command =
                new MySqlCommand("INSERT INTO `users` (`id`, `login`, `pass`, `email`)" +
                " VALUES (NULL, @ul, @up, @em);",
                dB.getConnection());

            command.Parameters.Add("@ul", MySqlDbType.VarChar).Value = log;
            command.Parameters.Add("@up", MySqlDbType.VarChar).Value = pass;
            command.Parameters.Add("@em", MySqlDbType.VarChar).Value = email;

            dB.openConnection();

            if (command.ExecuteNonQuery() == 1)
                MessageBox.Show("Аккаунт был создан!");
            else
                MessageBox.Show("Аккаунт не был создан!");

            dB.closeConnection();

        }

        //Регистрация пользователя в бд
        private void Reg_User(object sender, EventArgs e)
        {
            string login = bunifuMaterialTextbox1.Text.Trim(),
                   pass = bunifuMaterialTextbox2.Text.Trim(),
                   pass_2 = bunifuMaterialTextbox3.Text.Trim(),
                   email = bunifuMaterialTextbox4.Text.Trim().ToLower();

            if (login.Length < 5 || login.Length>20)
            {
                MessageBox.Show("Логин введен неверно!");
                return;
            }

            if ( login.Contains("@") || login.Contains("."))
            {
                MessageBox.Show("Логин содержит некорректные символы");
                return;
            }

            if (pass.Length < 5 && login.Length > 20)
            {
                MessageBox.Show("Пароль введен неверно!");
                return;
            }

            if (pass != pass_2)
            {
                MessageBox.Show("Пароли не совпадают!");
                return;
            }

            if (email.Length < 5 || !email.Contains("@") || !email.Contains("."))
            {
                MessageBox.Show("Некоректный email");
                return;
            }

            if (isUserExists())
                return;

            RegUser(login, pass, email);

            this.Hide();
            Sign_In sign = new Sign_In();
            sign.Show();
        }
    }
}
