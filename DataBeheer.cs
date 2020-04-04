using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Escape_From_The_Woods
{
    public class DataBeheer
    {
        private string connectionString;

        public DataBeheer(string connectionString)
        {
            this.connectionString = connectionString;
        }

        private SqlConnection getConnection()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }

        public void VoegWoodRecToe(List<Map> mappenLijst)
        {
            SqlConnection connection = getConnection();
            string query1 = "SELECT * FROM  dbo.WoodRecords";

            try
            {
                connection.Open();
                using (SqlDataAdapter adapter = new SqlDataAdapter())
                {
                    SqlCommand command = new SqlCommand(query1, connection);
                    adapter.SelectCommand = command;

                    SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                    //builder.DataAdapter = adapter;
                    adapter.InsertCommand = builder.GetInsertCommand();

                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    foreach (Map m in mappenLijst)
                    {
                        foreach (Tree t in m.trees)
                        {
                            connection = getConnection();
                            string queryS = "INSERT INTO dbo.WoodRecords(woodID, treeID, x, y) VALUES(@woodID, @treeID, @x, @y)";
                            using (command = connection.CreateCommand())

                                connection.Open();
                            try
                            {
                                command.Parameters.Add(new SqlParameter("@woodID", SqlDbType.Int));
                                command.Parameters.Add(new SqlParameter("@treeID", SqlDbType.Int));
                                command.Parameters.Add(new SqlParameter("@x", SqlDbType.Int));
                                command.Parameters.Add(new SqlParameter("@y", SqlDbType.Int));

                                command.CommandText = queryS;
                                command.Parameters["@woodID"].Value = m.woodID;
                                command.Parameters["@treeID"].Value = t.treeID;
                                command.Parameters["@x"].Value = t.x;
                                command.Parameters["@y"].Value = t.y;
                                command.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex);
                            }
                            finally
                            {
                                connection.Close();
                            }
                        }
                    }
                    adapter.Update(table);
                }


            }


            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            finally
            {
                connection.Close();
            }
        }
    }
}
