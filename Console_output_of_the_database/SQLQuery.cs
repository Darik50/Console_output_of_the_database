using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection;
using System.Diagnostics;

namespace Console_output_of_the_database
{
    public class SQLQuery
    {
        public static async Task CreateTable(string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand();
                command.CommandText = "drop table persons;";
                command.CommandText += "create table persons (full_name varchar(255) not null, date_of_birth date not null, gender varchar(1) not null)";
                command.Connection = connection;
                await command.ExecuteNonQueryAsync();
            }
        }
        public static async Task InsertTable(string connectionString, string query)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand();
                command.CommandText = "insert into persons (full_name, date_of_birth, gender) values (\'" + query.Split(' ')[2] + "\', cast(\'" + query.Split(' ')[3] + "\' as datetime), \'" + query.Split(' ')[4] + "\')";
                command.Connection = connection;
                await command.ExecuteNonQueryAsync();
            }
        }
        public static void SelectTable(string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(@"select p2.full_name, format(p2.date_of_birth, 'dd.MM.yyyy') as date_of_birth, p2.gender, datediff(year, p2.date_of_birth, getdate()) as old
                                        from(
                                                select full_name, date_of_birth
                                                from persons
                                                group by full_name, date_of_birth
                                                having count(*) = 1) as p1
                                        inner join persons as p2 on p2.full_name = p1.full_name
                                                and p2.date_of_birth = p1.date_of_birth
                                        order by p2.full_name", connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    Console.WriteLine( reader.GetName(0).PadLeft(33) + reader.GetName(1).PadLeft(15) + reader.GetName(2).PadLeft(15));
                    while (reader.Read())
                    {
                        object full_name = reader.GetValue(0);
                        object date_of_birth = reader.GetValue(1);
                        object gender = reader.GetValue(2);
                        Console.WriteLine(full_name.ToString().PadLeft(33) + date_of_birth.ToString().PadLeft(15) + gender.ToString().PadLeft(15));
                    }
                }

                reader.Close();
            }
        }
        public static async Task RandomInsertTable(string connectionString)
        {
            for (int i = 0; i < 1000000; i++)
            {
                string full_name = Data_Generator.GenerateRandomName();
                string date_of_birth = Data_Generator.GenerateRandomBirthDate().ToString("dd.MM.yyyy");
                string gender = Data_Generator.GenerateRandomGender();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    SqlCommand command = new SqlCommand();
                    command.CommandText = "insert into persons (full_name, date_of_birth, gender) values (\'" + full_name + "\', cast(\'" + date_of_birth + "\' as datetime), \'" + gender + "\')";
                    command.Connection = connection;
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
        public static async Task RandomFMInsertTable(string connectionString)
        {
            for (int i = 0; i < 100; i++)
            {
                string full_name = "F" + Data_Generator.GenerateRandomName();
                string date_of_birth = Data_Generator.GenerateRandomBirthDate().ToString("dd.MM.yyyy");
                string gender = "m";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    SqlCommand command = new SqlCommand();
                    command.CommandText = "insert into persons (full_name, date_of_birth, gender) values (\'" + full_name + "\', cast(\'" + date_of_birth + "\' as datetime), \'" + gender + "\')";
                    command.Connection = connection;
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
        public static void FSelectTable(string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();                
                SqlCommand command = new SqlCommand(@"select full_name, format(date_of_birth, 'dd.MM.yyyy') as date_of_birth, gender
                                                    from persons
                                                    where full_name like 'F%' and gender = 'm';", connection);
                SqlDataReader reader = command.ExecuteReader();                
                if (reader.HasRows)
                {
                    Console.WriteLine(reader.GetName(0).PadLeft(33) + reader.GetName(1).PadLeft(15) + reader.GetName(2).PadLeft(15));
                    while (reader.Read())
                    {
                        object full_name = reader.GetValue(0);
                        object date_of_birth = reader.GetValue(1);
                        object gender = reader.GetValue(2);
                        Console.WriteLine(full_name.ToString().PadLeft(33) + date_of_birth.ToString().PadLeft(15) + gender.ToString().PadLeft(15));
                    }
                }

                reader.Close();
            }
        }
    }
}
