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
    public partial class Contr_jokei : UserControl
    {
        public Contr_jokei()
        {
            InitializeComponent();
        }

        private void ReloadDB()
        {

            DB dB = new DB();

            DataTable tab = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command =
                new MySqlCommand("SELECT `жокеи`.`Имя`,`жокеи`.`Адрес`," +
                "`жокеи`.`Возраст`, `жокеи`.`Рейтинг` FROM `жокеи`", dB.getConnection());

            adapter.SelectCommand = command;

            adapter.Fill(tab);

            table.DataSource = tab;

        }

        private void Contr_jokei_Load(object sender, EventArgs e)
        {
            ReloadDB();
        }
    }
}
