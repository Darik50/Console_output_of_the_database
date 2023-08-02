using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace Console_output_of_the_database
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string connectionString = "Data Source=DARIK;Initial Catalog=master;Integrated Security=True; TrustServerCertificate=True ";
            string start;
            bool flag = true;
            while (flag)
            {
                start = Console.ReadLine();
                Regex regex = new Regex(@"^myApp\s[1-5]");
                switch (regex.Match(start).ToString())
                {
                    case "myApp 1":
                        SQLQuery.CreateTable(connectionString);
                        break;
                    case "myApp 2":
                        SQLQuery.InsertTable(connectionString, start);
                        break;
                    case "myApp 3":
                        SQLQuery.SelectTable(connectionString);
                        break;
                    case "myApp 4":
                        SQLQuery.RandomInsertTable(connectionString);
                        SQLQuery.RandomFMInsertTable(connectionString);
                        break;
                    case "myApp 5":
                        Stopwatch stopwatch = new Stopwatch();
                        stopwatch.Start();
                        SQLQuery.FSelectTable(connectionString);
                        stopwatch.Stop();
                        Console.WriteLine("Время выполнения: " + stopwatch.Elapsed);
                        break;
                    default:
                        flag = false;
                        break;
                }
            }
        }
    }
}
