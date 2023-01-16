using System.Data;
using System.Data.SqlClient;

namespace AdoDataSetApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=LibraryDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sqlQuery = "SELECT * FROM books";
                SqlDataAdapter adapter = new(sqlQuery, connection);
                DataSet dataSet = new();
                adapter.Fill(dataSet);

                // вывод таблицы из DataSet
                foreach(DataTable table in dataSet.Tables)
                {
                    foreach(DataColumn column in table.Columns)
                        Console.Write($"{column.ColumnName}\t");
                    Console.WriteLine();
                    foreach(DataRow row in table.Rows)
                    {
                        var rowArr = row.ItemArray;
                        foreach(object item in rowArr)
                            Console.Write($"{item.ToString()}\t");
                        Console.WriteLine();
                    }
                }

                DataTable tableBooks = dataSet.Tables[0];

                // Добавление строки в таблицу INSERT
                /*
                DataRow rowBook = tableBooks.NewRow();
                rowBook["author"] = "Alexandr Block";
                rowBook["title"] = "Dvenadcat";
                rowBook["price"] = 270.50;

                tableBooks.Rows.Add(rowBook);
                */

                // строительство команд INSERT, UPDATE, DELETE
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);
                
                Console.WriteLine(commandBuilder.GetInsertCommand().CommandText);
                Console.WriteLine();
                Console.WriteLine(commandBuilder.GetUpdateCommand().CommandText);
                Console.WriteLine();
                Console.WriteLine(commandBuilder.GetDeleteCommand().CommandText);
                Console.WriteLine();



                // Изменение существующей строки UPDATE
                /*
                DataRow rowBook = null;
                foreach (DataRow row in tableBooks.Rows)
                    if ((int)row["id"] == 5)
                        rowBook = row;
                rowBook["title"] = "Alye parusa";
                rowBook["price"] = 410.95;
                */

                // Удаление строки DELETE
                /*
                DataRow rowBook = null;
                int index = -1;
                for (int i = 0; i < tableBooks.Rows.Count; i++)
                    if ((int)tableBooks.Rows[i]["id"] == 9)
                        //index = i;
                        rowBook = tableBooks.Rows[i];
                rowBook.Delete();
                */

                //foreach (DataRow row in tableBooks.Rows)
                //    if ((int)row["id"] == 9)
                //        rowBook = row;


                // Обновление БД со стороны DataSet
                adapter.Update(dataSet);

                dataSet.Clear();
                adapter.Fill(dataSet);
            }
        }
    }
}