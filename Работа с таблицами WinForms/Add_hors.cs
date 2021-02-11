/*
 *  Форма: Add_hors
 *  
 *  Язык: C#
 *  Разработал: Ролдугин Владимир Дмитриевич, ТИП - 62
 *  Дата: 04.02.2021г
 *  
 *  Задание: 
 *      Предоставляет пользователю возможность авторизоватся в системе.
 *      
 *  Переменные, используемые в данной форме:
 *             vladelec - владелец;
 *             jokei - жокей;
 *             name - имя;
 *             pol - пол;
 *             age - возраст.
 * 
 *  Подпрограммы, используемые в данной форме:
 *      Joke - Заполнение комбобокса для жокеев;
 *      Add_hors - Заполнение комбобокса для владельцов;
 *      button2_Click - Добавление записи в бд.
 *      
 */

using System;
using System.Data;
using System.Windows.Forms;
using MetroFramework.Forms;
using MySql.Data.MySqlClient;

namespace Работа_с_таблицами_WinForms
{
    public partial class Add_hors : MetroForm
    {
        // Заполнение комбобокса для жокеев
        public void Joke()
        {
            DB dB = new DB();

            DataTable tab = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command =
                new MySqlCommand("SELECT * " +
                "FROM `жокеи`", dB.getConnection());

            adapter.SelectCommand = command;

            adapter.Fill(tab);


            for (int i = 0; i < tab.Rows.Count; i++)
            {
                comboBox2.Items.Add(tab.Rows[i][1].ToString());
            }
        }

        // Заполнение комбобокса для владельцов
        public Add_hors()
        {
            InitializeComponent();
            label6.Visible = false;

            DB dB = new DB();

            DataTable tab = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command =
                new MySqlCommand("SELECT * " +
                "FROM `владельцы`", dB.getConnection());

            adapter.SelectCommand = command;

            adapter.Fill(tab);


            for (int i = 0; i < tab.Rows.Count; i++)
            {
                comboBox1.Items.Add(tab.Rows[i][1].ToString());
            }

            Joke();
            
        }
        //Добавление записи в бд
        private void button2_Click(object sender, EventArgs e)
        {
            string vladelec = comboBox1.Text,
              jokei = comboBox2.Text,
              name = textBox1.Text,
              pol = maskedTextBox3.Text,
              age = maskedTextBox4.Text;

            // Проверка выбран ли комбобокс

            if (string.IsNullOrEmpty(comboBox1.Text))
            {
                label6.Visible = true;
                label6.Text = "Выберете владельца";
                return;
            }
            else
            {

            }

            if (string.IsNullOrEmpty(comboBox2.Text))
            {
                label6.Visible = true;
                label6.Text = "Выберете жокея";
                return;
            }
            else
            {

            }


            if (name.Length < 4)
            {
                label6.Visible = true;
                label6.Text = "Введите кличку";
                return;
            }

            if (pol != "М" && pol != "Ж")
            {
                label6.Visible = true;
                label6.Text = "Введите пол (М - мужской, Ж - женский";
                return;
            }

            if (age.Length < 1)
            {
                label6.Visible = true;
                label6.Text = "Введите возраст";
                return;
            }


            //Добавление записи в бд
            DB dB = new DB();

            MySqlCommand command =
                new MySqlCommand("CALL `add_horse`(@p0, @p1, @p2, @p3, @p4);",
                dB.getConnection());
            command.Parameters.Add("@p0", MySqlDbType.VarChar).Value = vladelec;
            command.Parameters.Add("@p1", MySqlDbType.VarChar).Value = jokei;
            command.Parameters.Add("@p2", MySqlDbType.VarChar).Value = name;
            command.Parameters.Add("@p3", MySqlDbType.VarChar).Value = pol;
            command.Parameters.Add("@p4", MySqlDbType.VarChar).Value = age;



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
