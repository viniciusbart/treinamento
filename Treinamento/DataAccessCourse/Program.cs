using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Configuration;

namespace DataAccessCourse
{
    class Program
    {
        private static string providerName = ConfigurationManager.ConnectionStrings["Northwind"].ProviderName;
        private static string ConnectionString = ConfigurationManager.ConnectionStrings["Northwind"].ConnectionString;

        public static DataSet ExecuteDataSet(string sql)
        {
            var ds = new DataSet();

            var factory = DbProviderFactories.GetFactory(providerName);

            using (var conexao = factory.CreateConnection())
            {
                conexao.ConnectionString = ConnectionString;
                conexao.Open();

                using (var comando = conexao.CreateCommand())
                {
                    comando.CommandText = sql;
                    using (var adapter = factory.CreateDataAdapter())
                    {
                        adapter.SelectCommand = comando;
                        adapter.Fill(ds);
                    }
                }
            }

            return ds;
        }

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

        public static void Aula1()
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
                    //ExecuteScalar PEGA APENAS A PRIMEIRA LINHA E PRIMEIRA COLUNA.
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
        }

        static void PrintProviders()
        {
            var factoriesDataTable = DbProviderFactories.GetFactoryClasses();

            foreach (DataRow row in factoriesDataTable.Rows)
            {
                Console.WriteLine($"{row["name"]} - {row["InvariantName"]}");
            }
        }

        static void Main(string[] args)
        {
            var customersDataSet = ExecuteDataSet("select * from customers");

            var customersTable = customersDataSet.Tables[0];

            foreach (DataRow row in customersTable.Rows)
            {
                var contactName = row["ContactName"].ToString();
                //var contactTitle = row["ContactTitle"].ToString();

                Console.WriteLine($"{contactName} - {row["ContactTitle"]}");
            }

            Console.ReadLine();
        }
    }
}
