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
    public partial class Пользователи : UserControl
    {
        public Пользователи()
        {
            InitializeComponent();
        }

        // Загрузка данных из БД
        private void ReloadDB()
        {
            DB dB = new DB();

            DataTable tab = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command =
                new MySqlCommand("SELECT *, 'Update','Delete'" +
                "FROM `users`", dB.getConnection());

            adapter.SelectCommand = command;

            adapter.Fill(tab);

            table.DataSource = tab;

            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                table[4, i] = linkCell;
                table[4, i].Style.BackColor = Color.FromArgb(46, 169, 79);
            }

            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                table[5, i] = linkCell;
                table[5, i].Style.BackColor = Color.Tomato;
            }
        }

        private void Пользователи_Load(object sender, EventArgs e)
        {
            ReloadDB();
        }
    }
}
