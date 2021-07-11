using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplication_Test.Modules
{
    public class ModulesDb
    {
        private SqlConnection connect = new SqlConnection();

       
        private void Open()
        {

                string cs = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
                connect.ConnectionString = cs;
                connect.Open();

        }
        public List<Models.Model_Test> ListUserGet(string query)
        {
            List<Models.Model_Test> models = new List<Models.Model_Test>();
            using (connect)
            {

                SqlCommand command = new SqlCommand(query, connect);

                command.CommandType = CommandType.Text;
                Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Models.Model_Test model = new Models.Model_Test
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            LastName = reader.GetString(2),
                            Age = reader.GetInt32(3),
                            Dni = reader.GetInt32(4)
                        };
                        models.Add(model);
                    }
                }
            }
                return models;
        }

        public Models.Model_Test UserGet(string query, List<SqlParameter> parameters)
        {
            Models.Model_Test model = new Models.Model_Test();

            using (connect)
            {
               
                SqlCommand command = new SqlCommand(query, connect);

                command.CommandType = CommandType.Text;
                command.Parameters.AddRange(parameters.ToArray());
                Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        model.Id = reader.GetInt32(0);
                        model.Name = reader.GetString(1);
                        model.LastName = reader.GetString(2);
                        model.Age = reader.GetInt32(3);
                        model.Dni = reader.GetInt32(4);

                    }
                }
                
                return model;
            }
        }
        public SqlParameter ParameterString(string nameParam, string value)
        {
            SqlParameter param = new SqlParameter();

            param.ParameterName = nameParam;
            param.Value = value;
            param.DbType = DbType.String;

            return param;


        }

    }
}