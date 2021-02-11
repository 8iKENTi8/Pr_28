/*
 *  Форма: Add_sos
 *  
 *  Язык: C#
 *  Разработал: Ролдугин Владимир Дмитриевич, ТИП - 62
 *  Дата: 04.02.2021г
 *  
 *  Задание: 
 *      Предоставляет пользователю возможность авторизоваться  в системе.
 *      
 *  Переменные, используемые в данной форме:
 *             vladelec - владелец;
 *             jokei - жокей;
 *             name - имя;
 *             pol - пол;
 *             age - возраст.
 * 
 *  Подпрограммы, используемые в данной форме:
 *      Add_sos - заполнение комбобокса результаты;
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
    public partial class Add_sos : MetroForm
    {
        public Add_sos()
        {
            InitializeComponent();
            label6.Visible = false;

            DB dB = new DB();

            DataTable tab = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command =
                new MySqlCommand("SELECT * " +
                "FROM `резултаты`", dB.getConnection());

            adapter.SelectCommand = command;

            adapter.Fill(tab);


            for (int i = 0; i < tab.Rows.Count; i++)
            {
                comboBox1.Items.Add(tab.Rows[i][0].ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string id_r = comboBox1.Text,
             ev_name = textBox3.Text,
             dat_time = maskedTextBox1.Text,
             ipodrom = textBox2.Text;

            //Проверка выбран ли комбобокс
            if (string.IsNullOrEmpty(comboBox1.Text))
            {
                label6.Visible = true;
                label6.Text = "Выберете id";
                return;
            }
            else
            {

            }

            if (ev_name.Length < 4)
            {
                label6.Visible = true;
                label6.Text = "Введите название мероприятия";
                return;
            }

            if (dat_time.Length<18)
            {
                label6.Visible = true;
                label6.Text = "Введите дату полностью";
                return;
            }

            if (ipodrom.Length < 5)
            {
                label6.Visible = true;
                label6.Text = "Введите иподром";
                return;
            }


            DB dB = new DB();

            MySqlCommand command =
                new MySqlCommand("CALL `Add_sos`(@p0, @p1, @p2, @p3);",
                dB.getConnection());
            command.Parameters.Add("@p0", MySqlDbType.VarChar).Value = id_r;
            command.Parameters.Add("@p1", MySqlDbType.VarChar).Value = ev_name;
            command.Parameters.Add("@p2", MySqlDbType.VarChar).Value = dat_time;
            command.Parameters.Add("@p3", MySqlDbType.VarChar).Value = ipodrom;

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
