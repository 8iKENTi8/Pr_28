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
    public partial class User_Form : MetroForm
    {
        public User_Form()
        {
            InitializeComponent();
        }

        private void ReloadDB()
        {
            table.Rows.Clear();

            DB dB = new DB();

            DataTable tab = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command =
                new MySqlCommand("SELECT *, 'Delete' " +
                "FROM `users` ", dB.getConnection());

            adapter.SelectCommand = command;

            adapter.Fill(tab);



            table.DataSource = tab;

            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                table[4, i] = linkCell;
                table[4, i].Style.BackColor = System.Drawing.Color.Tomato;
            }
        }

        private void User_Form_Load(object sender, EventArgs e)
        {
            ReloadDB();
        }
    }
}
