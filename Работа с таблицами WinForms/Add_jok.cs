using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using MySql.Data.MySqlClient;

namespace Работа_с_таблицами_WinForms
{
    public partial class Add_jok : MetroForm
    {
        public Add_jok()
        {
            InitializeComponent();
            label6.Visible = false;
        }

        private Boolean isUserExists()
        {
            DB dB = new DB();

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command =
                new MySqlCommand("SELECT * FROM `жокеи` WHERE `Имя` = @ul",
                dB.getConnection());

            command.Parameters.Add("@ul",
                MySqlDbType.VarChar).Value = textBox1.Text;

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

        // Add Jok
        private void button2_Click(object sender, EventArgs e)
        {
            string login = textBox1.Text.Trim(),
                adres = textBox2.Text.Trim(),
                age = maskedTextBox1.Text,
                rate = maskedTextBox2.Text;

            if (login.Length < 3)
            {
                label6.Visible = true;
                label6.Text = "Логин введен неверно!";
                return;
            }

            if (adres.Length < 5)
            {
                label6.Visible = true;
                label6.Text = "Адрес введен неверно!";
                return;
            }

            if (age.Length < 2)
            {
                label6.Visible = true;
                label6.Text = " Введите возраст";
                return;
            }

            if (rate.Length < 2)
            {
                label6.Visible = true;
                label6.Text = " Введите рейтинг";
                return;
            }

            if (isUserExists())
                return;

            DB dB = new DB();

            MySqlCommand command =
                new MySqlCommand("INSERT INTO `жокеи` (`id_j`, `Имя`, " +
                "`Адрес`, `Возраст`, `Рейтинг`) " +
                "VALUES (NULL, @ul, @up, @ag, @rt)",
                dB.getConnection());

            command.Parameters.Add("@ul", MySqlDbType.VarChar).Value = login;
            command.Parameters.Add("@up", MySqlDbType.VarChar).Value = adres;
            command.Parameters.Add("@ag", MySqlDbType.VarChar).Value = age;
            command.Parameters.Add("@rt", MySqlDbType.VarChar).Value = rate;

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
