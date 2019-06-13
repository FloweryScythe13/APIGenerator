using System;

namespace API_Generator
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var dataProvider = new DataProvider(connectionString: "Server= localhost; Database= FleetManagement; Integrated Security=True;");

            var tables = dataProvider.GetTables();

            foreach (var table in tables)
            {
                Console.WriteLine(table.Name);

                foreach (var column in table)
                {
                    Console.WriteLine($"   {column.Type.Name}{(column.Nullable ? "?" : "")} {column.Name}");
                }
                
                Console.WriteLine();
            }
            Console.ReadLine();
        }
    }
}