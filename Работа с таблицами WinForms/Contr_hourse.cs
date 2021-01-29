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
    public partial class Contr_hourse : UserControl
    {
        public Contr_hourse()
        {
            InitializeComponent();
        }

        DataTable tab = new DataTable();
        private void ReloadDB()
        {

            DB dB = new DB();

            

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command =
                new MySqlCommand("SELECT `жокеи`.`Имя` AS 'Жокей', " +
                "`владельцы`.`name` AS 'Владелец', `лошади`.`Кличка`, " +
                "`лошади`.`Пол`,`лошади`.`Возраст` FROM `лошади`,`жокеи`," +
                " `владельцы` WHERE `лошади`.`id_j`=`жокеи`.`id_j` " +
                "AND `лошади`.`id_v`=`владельцы`.`id_v`", dB.getConnection());

            adapter.SelectCommand = command;

            adapter.Fill(tab);

            table.DataSource = tab;

        }


        private void Contr_hourse_Load(object sender, EventArgs e)
        {
            ReloadDB();
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                DataView data = tab.DefaultView;
                data.RowFilter = string.Format("Кличка like '%{0}%'", txtSearch.Text);
                table.DataSource = data.ToTable();
            }
        }
    }
}
