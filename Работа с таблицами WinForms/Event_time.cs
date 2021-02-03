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
    public partial class Event_time : MetroForm
    {
        DataTable tab;
        public Event_time()
        {
            InitializeComponent();

            DB dB = new DB();

            tab = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command =
                new MySqlCommand("CALL `Full_time_event`()", dB.getConnection());

            adapter.SelectCommand = command;

            adapter.Fill(tab);

            table.DataSource = tab;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Admin_Form form = new Admin_Form();
            form.Show();
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                DataView data = tab.DefaultView;
                data.RowFilter = string.Format("Ипподром like '%{0}%'", txtSearch.Text);
                table.DataSource = data.ToTable();
            }
        }
    }
}
