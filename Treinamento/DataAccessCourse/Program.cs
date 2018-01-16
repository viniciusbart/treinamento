using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessCourse
{
    class Program
    {

        static string ConnectionString = @"Server=localhost;Database=NORTHWND;Trusted_Connection=True;";

        public static DataSet GetDataSet(string sql)
        {
            var ds = new DataSet();
            using (var conexao = new SqlConnection(ConnectionString))
            {
                conexao.Open();
                using (var adapter = new SqlDataAdapter(sql,conexao))
                {
                    adapter.Fill(ds);
                }
            }
            return ds;
        }

        static void Main(string[] args)
        {
            var ordersDataSet = GetDataSet("select * from Orders");

            var ordersTable = ordersDataSet.Tables[0];

            foreach (DataRow row in ordersTable.Rows)
            {
                var shipName = row["ShipName"].ToString();
                var shipCity = row["ShipCity"].ToString();

                Console.WriteLine($"{shipName} - {shipCity}");
            }

            //Objeto de conexão
            //SqlConnection conexao = new SqlConnection(ConnectionString);

            using (var conexao = new SqlConnection(ConnectionString))
            {
                conexao.Open();

                //Console.WriteLine("Conexão realizada com sucesso!");

                using (var comando = new SqlCommand("select * from Categories", conexao))
                {
                    using (var reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            /*
                            var categoryIdValue = reader["CategoryId"];
                            var categoryId = categoryIdValue == DBNull.Value ? 0 : (int)categoryIdValue;
                            */

                            var categoryId = reader["CategoryId"] as int?;
                            var categoryName = reader["CategoryName"].ToString();
                            var description = reader["Description"].ToString();

                            Console.WriteLine($"{categoryId} - {categoryName} - {description}");

                        }

                        reader.Close();
                    }
                }

                using (var contagem = new SqlCommand("select count(*) from categories", conexao))
                {
                    var total = (int)contagem.ExecuteScalar();

                    Console.WriteLine("Total: " + total);
                }

                using (var soma = new SqlCommand("select sum(UnitPrice) from Products where CategoryId=@categoryId", conexao))
                {
                    soma.Parameters.Add("@categoryId", SqlDbType.Int).Value = 1;
                    var somaTotal = Convert.ToDecimal(soma.ExecuteScalar());
                    Console.WriteLine("Total: " + somaTotal.ToString("C2"));

                }

                //using (var transacao = conexao.BeginTransaction())
                //{
                //    Console.Write("Category Name: ");
                //    string categoryName = Console.ReadLine();

                //    Console.Write("Category Description: ");
                //    string categoryDescription = Console.ReadLine();

                //    try
                //    {
                //        using (var comando = new SqlCommand("insert into Categories(CategoryName,Description) VALUES (@categoryName,@Description)", conexao, transacao))
                //        {
                //            comando.Parameters.Add("@categoryName", SqlDbType.NVarChar).Value = categoryName;
                //            comando.Parameters.AddWithValue("@Description", categoryDescription);

                //            comando.ExecuteNonQuery();
                //        }

                //        transacao.Commit();
                //    }
                //    catch (Exception e)
                //    {
                //        transacao.Rollback();
                //        Console.WriteLine(e);
                //    }
                //}


                conexao.Close();
            }
            Console.ReadLine();

        }
    }
}
