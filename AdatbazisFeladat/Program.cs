using MySql.Data.MySqlClient;
using System;

namespace ConsoleAdatbazis
{
    class Program
    {
        static void Main(string[] args)
        {
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
            builder.Server = "localhost";
            builder.UserID = "root";
            builder.Password = "";
            builder.Database = "pizza";

            MySqlConnection connection = new MySqlConnection(builder.ConnectionString);
            Console.Write("Adja meg a feladat sorszámát 23-28 között: ");
            int feladat = Convert.ToInt32(Console.ReadLine());
            switch (feladat)
            {
                case 23:
                    Console.WriteLine("23. feladat: Hány házhoz szállítása volt az egyes futároknak?");
                    try
                    {
                        connection.Open();
                        MySqlCommand command = connection.CreateCommand();
                        command.CommandText = "SELECT fnev, COUNT(rendeles.fazon) FROM `futar`, `rendeles` WHERE rendeles.fazon = futar.fazon GROUP BY fnev;";
                        using (MySqlDataReader dr = command.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                string fnev = dr.GetString("fnev");
                                string fazon = dr.GetString("COUNT(rendeles.fazon)");
                                Console.WriteLine();
                                Console.Write($"{fnev} {fazon}db.");
                            }
                        }
                        connection.Close();
                    }
                    catch (MySqlException ex)
                    {
                        Console.WriteLine(ex.Message);
                        Environment.Exit(0);
                    }

                    break;

                case 24:

                    Console.WriteLine("24. feladat: A fogyasztás alapján mi a pizzák népszerűségi sorrendje?");
                    try
                    {
                        connection.Open();
                        MySqlCommand command = connection.CreateCommand();
                        command.CommandText = "SELECT pnev, SUM(db) FROM `pizza`, `tetel` WHERE tetel.pazon = pizza.pazon GROUP BY pnev ORDER BY SUM(db) DESC;";
                        using (MySqlDataReader dr = command.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                string pnev = dr.GetString("pnev");
                                string sum = dr.GetString("SUM(db)");
                                Console.WriteLine();
                                Console.Write($"{pnev} - {sum}");
                            }
                        }
                        connection.Close();
                    }
                    catch (MySqlException ex)
                    {
                        Console.WriteLine(ex.Message);
                        Environment.Exit(0);
                    }
                    break;

                case 25:
                    Console.WriteLine("25. feladat: A rendelések összértéke alapján mi a vevők sorrendje?");
                    try
                    {
                        connection.Open();
                        MySqlCommand command = connection.CreateCommand();
                        command.CommandText = "SELECT vnev, SUM(par*db) FROM `vevo`, `tetel`, `rendeles`, `pizza` WHERE tetel.razon = rendeles.razon AND rendeles.vazon = vevo.vazon AND tetel.pazon = pizza.pazon GROUP BY vnev ORDER BY SUM(Par * db) DESC;";
                        using (MySqlDataReader dr = command.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                string vnev = dr.GetString("vnev");
                                string sum = dr.GetString("SUM(par*db)");
                                Console.WriteLine();
                                Console.Write($"{vnev} - {sum}");
                            }
                        }
                        connection.Close();
                    }
                    catch (MySqlException ex)
                    {
                        Console.WriteLine(ex.Message);
                        Environment.Exit(0);
                    }
                    break;

                case 26:
                    Console.WriteLine("26. feladat: Melyik a legdrágább pizza?");
                    try
                    {
                        connection.Open();
                        MySqlCommand command = connection.CreateCommand();
                        command.CommandText = "SELECT par, pnev FROM `pizza` ORDER BY par DESC LIMIT 1;";
                        using (MySqlDataReader dr = command.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                int par = dr.GetInt32("par");
                                string pnev = dr.GetString("pnev");
                                Console.WriteLine();
                                Console.Write($" {pnev}, {par}");
                            }
                        }
                        connection.Close();
                    }
                    catch (MySqlException ex)
                    {
                        Console.WriteLine(ex.Message);
                        Environment.Exit(0);
                    }
                    break;


                case 27:
                    Console.WriteLine("27. feladat: Ki szállította házhoz a legtöbb pizzát?");
                    try
                    {
                        connection.Open();
                        MySqlCommand command = connection.CreateCommand();
                        command.CommandText = "SELECT fnev, SUM(db) FROM `futar`, `rendeles`, `tetel` WHERE rendeles.razon = tetel.razon AND rendeles.fazon = futar.fazon GROUP BY fnev ORDER BY SUM(db) DESC LIMIT 1;";
                        using (MySqlDataReader dr = command.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                string fnev = dr.GetString("fnev");
                                int sum = dr.GetInt32("SUM(db)");
                                Console.WriteLine();
                                Console.Write($"{fnev} - {sum}db.");
                            }
                        }
                        connection.Close();
                    }
                    catch (MySqlException ex)
                    {
                        Console.WriteLine(ex.Message);
                        Environment.Exit(0);
                    }
                    break;

                case 28:
                    Console.WriteLine("28. feladat: Ki ette a legtöbb pizzát?");
                    try
                    {
                        connection.Open();
                        MySqlCommand command = connection.CreateCommand();
                        command.CommandText = "SELECT vnev, SUM(db) FROM `vevo`, `rendeles`, `tetel` WHERE rendeles.razon = tetel.razon AND rendeles.vazon = vevo.vazon GROUP BY vnev ORDER BY SUM(db) DESC LIMIT 1;";
                        using (MySqlDataReader dr = command.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                string vnev = dr.GetString("vnev");
                                int sum = dr.GetInt32("SUM(db)");
                                Console.WriteLine();
                                Console.Write($" {vnev} {sum}db. ");
                            }
                        }
                        connection.Close();
                    }
                    catch (MySqlException ex)
                    {
                        Console.WriteLine(ex.Message);
                        Environment.Exit(0);
                    }
                    break;
            }
            Console.ReadKey();
        }
    }
}