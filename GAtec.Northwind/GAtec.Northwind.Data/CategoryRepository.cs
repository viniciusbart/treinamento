using System.Collections.Generic;
using GAtec.Northwind.Domain.Model;
using GAtec.Northwind.Domain.Repository;
using System.Data;
using System.Data.SqlClient;
using GAtec.Northwind.Util;

namespace GAtec.Northwind.Data
{
    public class CategoryRepository : ICategoryRepository
    {
        public void Add(Category item)
        {
            using (var con = new SqlConnection(NorthwindSettings.ConnectionString))
            {
                con.Open();

                using (var cmd = new SqlCommand("insert into categories (categoryname, description) values (@name, @description)", con))
                {
                    cmd.Parameters.Add("name", SqlDbType.NVarChar).Value = item.Name;
                    cmd.Parameters.Add("description", SqlDbType.NText).Value = item.Description;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(Category item)
        {
            using (var con = new SqlConnection(NorthwindSettings.ConnectionString))
            {
                con.Open();

                using (var cmd = new SqlCommand("update categories set categoryname=@name, description=@description where categoryid=@id", con))
                {
                    cmd.Parameters.Add("name", SqlDbType.NVarChar).Value = item.Name;
                    cmd.Parameters.Add("description", SqlDbType.NText).Value = item.Description;
                    cmd.Parameters.Add("id", SqlDbType.Int).Value = item.Id;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(object id)
        {
            using (var con = new SqlConnection(NorthwindSettings.ConnectionString))
            {
                con.Open();

                using (var cmd = new SqlCommand("delete from categories where categoryid=@id", con))
                {
                    cmd.Parameters.Add("id", SqlDbType.Int).Value = id;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Category Get(object id)
        {
            Category category = null;

            using (var con = new SqlConnection(NorthwindSettings.ConnectionString))
            {
                con.Open();

                using (var cmd = new SqlCommand("select categoryid, categoryname, description from categories where categoryid=@id order by categoryname", con))
                {
                    cmd.Parameters.Add("id", SqlDbType.Int).Value = id;

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            category = new Category();

                            category.Id = (int)reader["categoryid"];
                            category.Name = reader.GetString(1);
                            category.Description = reader.GetString(reader.GetOrdinal("Description"));
                        }
                    }
                }
                return category;
            }
        }

        public IEnumerable<Category> GetAll()
        {
            var categories = new List<Category>();

            using (var con = new SqlConnection(NorthwindSettings.ConnectionString))
            {
                con.Open();

                using (var cmd = new SqlCommand("select categoryid, categoryname, description from categories order by categoryname", con))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var category = new Category();

                            category.Id = (int)reader["categoryid"];
                            category.Name = reader.GetString(1);
                            category.Description = reader.GetString(reader.GetOrdinal("Description"));

                            categories.Add(category);
                        }
                    }
                }
            }
            return categories;
        }

        public bool ExistsName(string name, int id = 0)
        {
            bool result = false;
            using (var con = new SqlConnection(NorthwindSettings.ConnectionString))
            {
                con.Open();

                using (var cmd = new SqlCommand("select count(1) from categories where upper(categoryname)=@name and categoryid <> @id", con))
                {
                    cmd.Parameters.Add("id", SqlDbType.Int).Value = id;
                    cmd.Parameters.Add("name", SqlDbType.NVarChar).Value = name.ToUpper();
                    result = (int)cmd.ExecuteScalar() > 0;
                }
            }
            return result;
        }
    }
}
