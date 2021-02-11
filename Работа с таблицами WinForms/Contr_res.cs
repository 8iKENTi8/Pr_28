/*
 *  Интерфейс: Contr_res
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
    public partial class Contr_res : UserControl
    {
        public Contr_res()
        {
            InitializeComponent();
        }

        DataTable tab = new DataTable();
        private void ReloadDB()
        {

            DB dB = new DB();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command =
                new MySqlCommand("SELECT `лошади`.`Кличка`, `резултаты`.`Место`, " +
                "`резултаты`.`Время_заезда`, `состязания`.`event_name` AS" +
                " 'Название события', `состязания`.`Date_time` AS " +
                "'Время начала события', `состязания`.`Ипподром` FROM `состязания`," +
                "`резултаты`,`лошади` WHERE `состязания`.`id_r`=`резултаты`.`id_r`" +
                " AND `резултаты`.`id_l`=`лошади`.`id_l`", dB.getConnection());

            adapter.SelectCommand = command;

            adapter.Fill(tab);

            table.DataSource = tab;

        }

        private void Contr_res_Load(object sender, EventArgs e)
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
