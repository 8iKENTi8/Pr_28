/*
 *  Форма: Add_us
 *  
 *  Язык: C#
 *  Разработал: Ролдугин Владимир Дмитриевич, ТИП - 62
 *  Дата: 04.02.2021г
 *  
 *  Задание: 
 *      Предоставляет пользователю возможность добавить пользователя.
 *      
 *  Переменные, используемые в данной форме:
 *             vladelec - владелец;
 *             jokei - жокей;
 *             name - имя;
 *             pol - пол;
 *             age - возраст.
 * 
 *  Подпрограммы, используемые в данной форме:
 *      button2_Click - Добавление записи в бд.
 *      
 */

using System;
using System.Windows.Forms;
using MetroFramework.Forms;
using MySql.Data.MySqlClient;

namespace Работа_с_таблицами_WinForms
{
    public partial class Add_us : MetroForm
    {
        public Add_us()
        {
            InitializeComponent();
            label6.Visible = false;
        }

        //Добавление записи в бд
        private void button2_Click(object sender, EventArgs e)
        {
            string login = textBox1.Text.Trim(),
                  pass = textBox2.Text.Trim(),
                  email = textBox3.Text.Trim();

            if (login.Length < 5)
            {
                label6.Visible = true;
                label6.Text="Логин введен неверно!";
                return;
            }

            if (pass.Length < 5)
            {
                label6.Visible = true;
                label6.Text = "Пароль введен неверно!";
                return;
            }

            if (email.Length < 5 || !email.Contains("@") || !email.Contains("."))
            {
                label6.Visible = true;
                label6.Text = "Email введен неверно!";
                return;
            }

           
            DB dB = new DB();

            MySqlCommand command =
                new MySqlCommand("INSERT INTO `users` (`id`, `login`, `pass`, `email`)" +
                " VALUES (NULL, @ul, @up, @em);",
                dB.getConnection());

            command.Parameters.Add("@ul", MySqlDbType.VarChar).Value = login;
            command.Parameters.Add("@up", MySqlDbType.VarChar).Value = pass;
            command.Parameters.Add("@em", MySqlDbType.VarChar).Value = email;

            dB.openConnection();

            if (command.ExecuteNonQuery() == 1)
                MessageBox.Show("Аккаунт был создан!");
            else
                MessageBox.Show("Аккаунт не был создан!");

            dB.closeConnection();

            this.Hide();

        }

    }
}
