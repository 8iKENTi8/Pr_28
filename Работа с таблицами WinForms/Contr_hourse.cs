/*
 *  Интерфейс: Contr_hourse
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
 *      
 */

using System;
using System.Data;
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

        //Выгрузка данных в таблицу
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

        //Поиск по кличке
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
