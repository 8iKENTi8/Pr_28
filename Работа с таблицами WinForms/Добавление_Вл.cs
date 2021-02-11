/*
 *  Форма: Добавление_Вл
 *  
 *  Язык: C#
 *  Разработал: Ролдугин Владимир Дмитриевич, ТИП - 62
 *  Дата: 04.02.2021г
 *  
 *  Задание: 
 *      Предоставляет пользователю возможность добавить пользователя.
 *      
 *  Переменные, используемые в данной форме:
 *             login - имя;
 *             address - адрес;
 *             phone - телефон.
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
    public partial class Добавление_Вл : MetroForm
    {
        public Добавление_Вл()
        {
            InitializeComponent();
            label6.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string login = textBox1.Text.Trim(),
                  address = textBox2.Text.Trim(),
                  phone = maskedTextBox1.Text.Trim();

            //Проверки на корректность данных
            if (login.Length < 5)
            {
                label6.Visible = true;
                label6.Text = "Логин введен неверно!";
                return;
            }

            if (address.Length < 5)
            {
                label6.Visible = true;
                label6.Text = "Пароль введен неверно!";
                return;
            }

            if (phone.Length < 11 )
            {
                label6.Visible = true;
                label6.Text = "Телефон введен неверно!";
                return;
            }

            DB dB = new DB();

            MySqlCommand command =
                new MySqlCommand("INSERT INTO `владельцы` (`id_v`, `name`," +
                " `address`, `phone`)" +
                " VALUES (NULL, @ul, @up, 8+@ph)",
                dB.getConnection());

            command.Parameters.Add("@ul", MySqlDbType.VarChar).Value = login;
            command.Parameters.Add("@up", MySqlDbType.VarChar).Value = address;
            command.Parameters.Add("@ph", MySqlDbType.VarChar).Value = phone;

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
