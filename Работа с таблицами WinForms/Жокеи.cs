/*
 *  Форма: Жокеи
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
 *      table_CellContentClick - обработка событий update и delete;
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
    public partial class Жокеи : MetroForm
    {
        public Жокеи()
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
                new MySqlCommand("SELECT *, 'Update','Delete'" +
                "FROM `жокеи`", dB.getConnection());

            adapter.SelectCommand = command;

            adapter.Fill(tab);

            table.DataSource = tab;

            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                table[5, i] = linkCell;
                table[5, i].Style.BackColor = Color.FromArgb(46, 169, 79);
            }

            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                table[6, i] = linkCell;
                table[6, i].Style.BackColor = Color.Tomato;
            }
        }

        private void table_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 5)
                {
                    string task = table.Rows[e.RowIndex].Cells[5].Value.ToString();
                    if (task == "Update")
                    {
                        // проверяем 1-й столбец на пустые ячейки

                        if (table.Rows[e.RowIndex].Cells[1].Value.ToString().Length >25)
                        {
                            // заодно покрасим
                            table[1, e.RowIndex].Style.BackColor = Color.Tomato; 
                            MessageBox.Show("Слишком длинное значение для имени");
                            return;
                        }

                        // проверяем 2-й столбец на пустые ячейки
                        if (table.Rows[e.RowIndex].Cells[2].Value.ToString().Length > 25)
                        {
                            // заодно покрасим
                            table[2, e.RowIndex].Style.BackColor = Color.Tomato; 
                            MessageBox.Show("Слишком длинное значение для адреса");
                            return;
                        }

                    

                        if (MessageBox.Show("Обновить эту строку",
                            "Обновление", MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            int rowIndex = e.RowIndex;

                            DB db = new DB();
                            MySqlCommand command = new MySqlCommand("UPDATE `жокеи` SET `id_j` = @ul, `Имя` = @lg," +
                                " `Адрес` = @ad, `Возраст` = @ag, `Рейтинг` = @rg" +
                                " WHERE `жокеи`.`id_j` = @ul", db.getConnection());

                            command.Parameters.Add("@ul", MySqlDbType.VarChar).Value = table[0, rowIndex].Value.ToString();
                            command.Parameters.Add("@lg", MySqlDbType.VarChar).Value = table[1, rowIndex].Value.ToString();
                            command.Parameters.Add("@ad", MySqlDbType.VarChar).Value = table[2, rowIndex].Value.ToString();
                            command.Parameters.Add("@ag", MySqlDbType.VarChar).Value = table[3, rowIndex].Value.ToString();
                            command.Parameters.Add("@rg", MySqlDbType.VarChar).Value = table[4, rowIndex].Value.ToString();

                            db.openConnection();
                            if (command.ExecuteNonQuery() == 1) { MessageBox.Show("Аккаунт был Обновлен"); }

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

            try
            {
                if (e.ColumnIndex == 6)
                {
                    string task = table.Rows[e.RowIndex].Cells[6].Value.ToString();
                    if (task == "Delete")
                    {
                        if (MessageBox.Show("Удалить эту строку",
                            "Удаление", MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            int rowIndex = e.RowIndex;

                            DB db = new DB();
                            MySqlCommand command = new MySqlCommand("DELETE FROM `жокеи`" +
                                " WHERE `жокеи`.`id_j` = @ul ", db.getConnection());
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

        private void Жокеи_Load(object sender, EventArgs e)
        {
            ReloadDB();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Add_jok().ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
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

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                    table[5, i] = linkCell;
                    table[5, i].Style.BackColor = Color.FromArgb(46, 169, 79);
                }

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                    table[6, i] = linkCell;
                    table[6, i].Style.BackColor = Color.Tomato;
                }
            }

            if (txtSearch.Text == "")
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                    table[5, i] = linkCell;
                    table[5, i].Style.BackColor = Color.FromArgb(46, 169, 79);
                }

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                    table[6, i] = linkCell;
                    table[6, i].Style.BackColor = Color.Tomato;
                }
            }
        }
    }
}
