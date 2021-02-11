/*
 *  Форма: результаты
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
 *      table_CellContentClick - обработка событий update и delete.
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
    public partial class Результаты : MetroForm
    {
        public Результаты()
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
                "FROM `резултаты`", dB.getConnection());

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

        private void Результаты_Load(object sender, EventArgs e)
        {
            ReloadDB();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Add_res().ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ReloadDB();
        }

        private void table_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 4)
                {
                    string task = table.Rows[e.RowIndex].Cells[4].Value.ToString();
                    if (task == "Update")
                    {
                        if (MessageBox.Show("Обновить эту строку",
                            "Обновление", MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            int rowIndex = e.RowIndex;

                            DB db = new DB();
                            MySqlCommand command = new MySqlCommand("UPDATE `резултаты` SET `id_r` = @ul," +
                                " `id_l` = @lg, `Место` = @ps, `Время_заезда` = @em " +
                                "WHERE `резултаты`.`id_r` = @ul", db.getConnection());

                            command.Parameters.Add("@ul", MySqlDbType.VarChar).Value = table[0, rowIndex].Value.ToString();
                            command.Parameters.Add("@lg", MySqlDbType.VarChar).Value = table[1, rowIndex].Value.ToString();
                            command.Parameters.Add("@ps", MySqlDbType.VarChar).Value = table[2, rowIndex].Value.ToString();
                            command.Parameters.Add("@em", MySqlDbType.VarChar).Value = table[3, rowIndex].Value.ToString();

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
                            MySqlCommand command = new MySqlCommand("DELETE FROM `резултаты`" +
                                " WHERE `резултаты`.`id_r` = @ul ", db.getConnection());
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
    }
}
