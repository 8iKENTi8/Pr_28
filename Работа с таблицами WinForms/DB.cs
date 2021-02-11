/*
 *  Класс: DB
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
 *      openConnection - Открывает соединение;
 *      closeConnection - Закрывает соединение;
 *      getConnection - Получаем соединение.
 *      
 */

using MySql.Data.MySqlClient;


namespace Работа_с_таблицами_WinForms
{
    class DB
    {
        //Строка подключения к бд

        MySqlConnection connection = new MySqlConnection("server=localhost;" +
            "port=3306;username=root;password=root;database=ekz");

        //Открывает соединение
        public void openConnection()
        {
            //Если соединение закрыто, то открываем
            if (connection.State == System.Data.ConnectionState.Closed)
                connection.Open();
        }

        //Закрывает соединение
        public void closeConnection()
        {
            //Если соединение открыто, то закрывавем
            if (connection.State == System.Data.ConnectionState.Open)
                connection.Close();
        }

        //Получаем соединение
        public MySqlConnection getConnection()
        {
            return connection;
        }
    }
}
