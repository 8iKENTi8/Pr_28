/*
 *  Форма: состязания
 *  
 *  Язык: C#
 *  Разработал: Ролдугин Владимир Дмитриевич, ТИП - 62
 *  Дата: 04.02.2021г
 *  
 *  Задание: 
 *      Просмотр , изменение и удаление данных в таблице
 *      
 *  Подпрограммы, используемые в данной форме:
 *      ReloadDB - обновление таблицы;
 *      button3_Click - переход на форму админ;
 *      button1_Click - переход на форму добавление записи;
 *      table_CellContentClick - обработка события delete;
 *      txtSearch_KeyPress - поиск .
 *      
 *      
 */

using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MetroFramework.Forms;
using MySql.Data.MySqlClient;

namespace Работа_с_таблицами_WinForms
{
    public partial class состязание : MetroForm
    {
        public состязание()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Admin_Form form = new Admin_Form();
            form.Show();
        }
        DataTable tab;
        private void ReloadDB()
        {

            DB dB = new DB();

             tab = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command =
                new MySqlCommand("SELECT `состязания`.`id_s`, `состязания`.`id_r`," +
                " `состязания`.`event_name` AS 'Название события', `состязания`.`Date_time`" +
                " AS 'Время начала события', `состязания`.`Ипподром`," +
                " 'Delete' FROM `состязания`", dB.getConnection());

            adapter.SelectCommand = command;

            adapter.Fill(tab);

            table.DataSource = tab;

            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                table[5, i] = linkCell;
                table[5, i].Style.BackColor = Color.Tomato;
            }
        }

        private void состязание_Load(object sender, EventArgs e)
        {
            ReloadDB();
        }

        private void table_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
            try
            {
                if (e.ColumnIndex == 5)
                {
                    string task = table.Rows[e.RowIndex].Cells[5].Value.ToString();
                    if (task == "Delete")
                    {
                        if (MessageBox.Show("Удалить эту строку",
                            "Удаление", MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            int rowIndex = e.RowIndex;

                            DB db = new DB();
                            MySqlCommand command = new MySqlCommand("DELETE FROM `состязания`" +
                                " WHERE `состязания`.`id_s` = @ul ", db.getConnection());
                            command.Parameters.Add("@ul", MySqlDbType.VarChar).Value = table[0, rowIndex].Value.ToString();

                            table.Rows.RemoveAt(rowIndex);

                            db.openConnection();
                            if (command.ExecuteNonQuery() == 1) { MessageBox.Show("Аккаунт был Удален"); }

                            db.closeConnection();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Ошибка!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Add_sos().ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ReloadDB();
        }

        // Поиск
        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                DataView data = tab.DefaultView;
                data.RowFilter = string.Format("event_name like '%{0}%'", txtSearch.Text);
                table.DataSource = data.ToTable();

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                    table[5, i] = linkCell;
                    table[5, i].Style.BackColor = Color.Tomato;
                }
            }
            

            if (txtSearch.Text == "")
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                    table[5, i] = linkCell;
                    table[5, i].Style.BackColor = Color.Tomato;
                }
            }
        }
    }
}
