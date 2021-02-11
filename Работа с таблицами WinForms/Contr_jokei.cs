/*
 *  Интерфейс: Contr_jokei
 *  
 *  Язык: C#
 *  Разработал: Ролдугин Владимир Дмитриевич, ТИП - 62
 *  Дата: 04.02.2021г
 *  
 *  Задание: 
 *      Вывод данных в таблицу и поиск по ней.
 *      
 * 
 *  Подпрограммы, используемые в данной форме:
 *      ReloadDB - Выгрузка данных в таблицу;
 *      Contr_hourse_Load - при загрузке интерфейса выгружаем таблицу;
 *      txtSearch_KeyPress - поиск по кличке.
 *      
 */

using System;
using System.Data;
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

        DataTable tab;
        private void ReloadDB()
        {

            DB dB = new DB();

             tab = new DataTable();

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

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                DataView data = tab.DefaultView;
                data.RowFilter = string.Format("Имя like '%{0}%'", txtSearch.Text);
                table.DataSource = data.ToTable();
            }
        }
    }
}
