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
    public partial class Add_res : MetroForm
    {
        public Add_res()
        {
            InitializeComponent();
            label6.Visible = false;

            DB dB = new DB();

            DataTable tab = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command =
                new MySqlCommand("SELECT * " +
                "FROM `лошади`", dB.getConnection());

            adapter.SelectCommand = command;

            adapter.Fill(tab);

          
            for (int i = 0; i < tab.Rows.Count; i++)
            {
                comboBox1.Items.Add(tab.Rows[i][3].ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string horse = comboBox1.Text,
                place = maskedTextBox2.Text,
                time = maskedTextBox1.Text;

            if (string.IsNullOrEmpty(comboBox1.Text))
            {
                label6.Visible = true;
                label6.Text = "Выберете лошадь";
                return;
            }
            else
            {
               
            }


            if (place.Length < 1)
            {
                label6.Visible = true;
                label6.Text = "Введите место";
                return;
            }

            if (time.Length < 7)
            {
                label6.Visible = true;
                label6.Text = "Введите полное время";
                return;
            }

            

                DB dB = new DB();

                MySqlCommand command =
                    new MySqlCommand("CALL `Add-res`(@p0, @p1, @p2);",
                    dB.getConnection());
            command.Parameters.Add("@p0", MySqlDbType.VarChar).Value = horse;
            command.Parameters.Add("@p1", MySqlDbType.VarChar).Value = place;
            command.Parameters.Add("@p2", MySqlDbType.VarChar).Value = time;
            

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
