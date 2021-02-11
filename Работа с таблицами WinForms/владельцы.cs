/*
 *  Форма: Владельцы
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
using System.Text.RegularExpressions;
using System.Windows.Forms;
using MetroFramework.Forms;
using MySql.Data.MySqlClient;

namespace Работа_с_таблицами_WinForms
{
    public partial class владельцы : MetroForm
    {
        public владельцы()
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
                new MySqlCommand("SELECT *, 'Update','Delete'" +
                "FROM `владельцы`", dB.getConnection());

            adapter.SelectCommand = command;

            adapter.Fill(tab);

            table.DataSource = tab;

            //Перекрашивание ячеек таблицы update и delete
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

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Admin_Form form = new Admin_Form();
            form.Show();
        }

        // Обработка событий update и delete
        private void table_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            try
            {
                if (e.ColumnIndex == 4)
                {
                    string task = table.Rows[e.RowIndex].Cells[4].Value.ToString();
                    if (task == "Update")
                    {
                        //Проверка пустое ли значение
                        if (table.Rows[e.RowIndex].Cells[3].Value.ToString() == "")// проверяем 4-й столбец на пустые ячейки
                        {
                            table[3, e.RowIndex].Style.BackColor = Color.Tomato; // заодно покрасим
                            MessageBox.Show("Не введен телефон");
                            return;
                        }

                        //Проверка на корректность данных
                            if (Regex.Match(table.Rows[e.RowIndex].Cells[0].Value.ToString(), @"[а-яА-Я]|[a-zA-Z]").Success)
                        {
                            MessageBox.Show("Может содержать только цифры");
                            return;
                        }

                        if (Regex.Match(table.Rows[e.RowIndex].Cells[2].Value.ToString(), @"[0-9|[+]").Success)
                        {
                            MessageBox.Show("Может содержать только буквы");
                            return;
                        }

                        if (Regex.Match(table.Rows[e.RowIndex].Cells[3].Value.ToString(), @"[а-яА-Я]|[a-zA-Z]").Success)
                        {
                            MessageBox.Show("Может содержать только цифры");
                            return;
                        }

                        //Вопрос вы точно хотите обновить строку , да или нет?
                        if (MessageBox.Show("Обновить эту строку",
                            "Обновление", MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            int rowIndex = e.RowIndex;

                            DB db = new DB();
                            MySqlCommand command = new MySqlCommand("UPDATE `владельцы` SET `id_v` = @ul," +
                                " `name` = @lg, `address` = @ad, `phone` = @ph " +
                                "WHERE `владельцы`.`id_v` = @ul", db.getConnection());

                            command.Parameters.Add("@ul", MySqlDbType.VarChar).Value = table[0, rowIndex].Value.ToString();
                            command.Parameters.Add("@lg", MySqlDbType.VarChar).Value = table[1, rowIndex].Value.ToString();
                            command.Parameters.Add("@ad", MySqlDbType.VarChar).Value = table[2, rowIndex].Value.ToString();
                            command.Parameters.Add("@ph", MySqlDbType.VarChar).Value = table[3, rowIndex].Value.ToString();

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
                            MySqlCommand command = new MySqlCommand("DELETE FROM `владельцы`" +
                                " WHERE `владельцы`.`id_v` = @ul ", db.getConnection());
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

        private void владельцы_Load(object sender, EventArgs e)
        {
            ReloadDB();
        }

        //Поиск по имени
        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                DataView data = tab.DefaultView;
                data.RowFilter = string.Format("name like '%{0}%'", txtSearch.Text);
                table.DataSource = data.ToTable();

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

            if (txtSearch.Text == "")
            {
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
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ReloadDB();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Добавление_Вл().ShowDialog();
        }
    }
}
