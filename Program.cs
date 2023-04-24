using ConsoleTables;
using Microsoft.Data.SqlClient;
using System.Reflection.PortableExecutable;

namespace RecipeBook
{
    public class Program
    {
        public static string connString = "Data Source=DESKTOP-4L01UE8;Initial Catalog=TestDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        public static SqlConnection conn = new SqlConnection(connString);
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello, Welcome to Cook Book!\n");
            int ipData;
            var table = new ConsoleTable("List", "HotKey");
            table.AddRow("Get All Recipes", 1)
                 .AddRow("Get Recipe", 2)
                 .AddRow("Add New Recipe", 3)
                 .AddRow("Update Recipe Name", 4)
                 .AddRow("Update Recipe Ingredients", 5)
                 .AddRow("Update Recipe Description", 6)
                 .AddRow("Update Recipe", 7)
                 .AddRow("Delete Recipe", 8)
                 .AddRow("Exit", 9);
            table.Write();
            do
            {
                Console.WriteLine("Enter 9 to Exit...");
                Console.Write("\nEnter Hot Key - ");
                ipData = Convert.ToInt32(Console.ReadLine());
                switch (ipData)
                {
                    case 1:
                        getRecipes();
                        break;
                    case 2:
                        Console.Write("\nEnter Recipe ID to get Details - ");
                        int id = Convert.ToInt32(Console.ReadLine());
                        getRecipe(id);
                        break;
                    case 3:
                        Console.Write("\nEnter Recipe Name - ");
                        string name = Console.ReadLine();
                        Console.Write("\nEnter Recipe Ingredients - ");
                        string ingredients = Console.ReadLine();
                        Console.Write("\nEnter Description - ");
                        string description = Console.ReadLine();
                        AddRecipe(name, ingredients, description);
                        break;
                    case 4:
                        Console.Write("\nEnter Recipe ID to Update Name - ");
                        int id1 = Convert.ToInt32(Console.ReadLine());
                        Console.Write("\nEnter New Name for Recipe - ");
                        string ipname1 = Console.ReadLine();
                        updateRecipeName(id1, ipname1);
                        break;
                    case 5:
                        Console.Write("\nEnter Recipe Id to Update Ingredients - ");
                        int id2 = Convert.ToInt32(Console.ReadLine());
                        Console.Write("\nEnter All Ingredients - ");
                        string updateIngredients = Console.ReadLine();
                        updateRecipeIngredients(id2, updateIngredients);
                        break;
                    case 6:
                        Console.Write("\nEnter Recipe Id to Update Description - ");
                        int id4 = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("\nEnter Description");
                        string updateDesc = Console.ReadLine();
                        updateRecipeDescription(id4, updateDesc);
                        break;
                    case 7:
                        Console.Write("\nEnter Id to Update - ");
                        int updateId3 = Convert.ToInt32(Console.ReadLine());
                        Console.Write("\nEnter Recipe Name - ");
                        string updateName3 = Console.ReadLine();
                        Console.Write("\nEnter Recipe Ingredients - ");
                        string updateIngredients3 = Console.ReadLine();
                        Console.WriteLine("\nEnter Recipe Description");
                        string updateDescription3 = Console.ReadLine();
                        updateRecipe(updateId3, updateName3, updateIngredients3, updateDescription3);
                        break;
                    case 8:
                        Console.Write("\nEnter Recipe Id to Delete");
                        int deleteId = Convert.ToInt32(Console.ReadLine());
                        deleteRecipe(deleteId);
                        break;
                }
            } while (ipData != 9);
        }

        public static void getRecipes()
        {
            string cmdTxt = "SELECT RecipeId, RecipeName from RecipeBook";
            ExecuteCommand(cmdTxt, out SqlDataReader reader);
            Console.WriteLine();
            while (reader.Read())
            {
                Console.WriteLine($"{reader.GetInt32(0)} - {reader.GetString(1)}");
            }
            Console.WriteLine();
            conn.Close();
        }
        public static void getRecipe(int id)
        {
            string cmdTxt = $"SELECT RecipeId, RecipeName, RecipeIngredients, RecipeDescription FROM RecipeBook WHERE RecipeId={id}";
            ExecuteCommand(cmdTxt, out SqlDataReader reader);
            while (reader.Read())
            {
                Console.WriteLine($"{reader.GetInt32(0)} - {reader.GetString(1)}");
                Console.WriteLine($"Ingredients of Recipe - {reader.GetString(2)}");
                Console.WriteLine($"Description of Recipe - {reader.GetString(3)}");
            }
            conn.Close();
        }
        public static void AddRecipe(string name, string ingredients, string description)
        {
            string cmdTxt = $"INSERT INTO RecipeBook VALUES('{name}','{ingredients}','{description}')";
            ExecuteCommand(cmdTxt, out SqlDataReader reader);
            reader.Close();
            conn.Close();
            Console.WriteLine("Recipe Successfully Added");
        }
        public static void updateRecipeName(int id, string name)
        {
            string cmdTxt = $"UPDATE RecipeBook SET RecipeName='{name}' WHERE RecipeId={id}";
            ExecuteCommand(cmdTxt, out SqlDataReader reader);
            reader.Close();
            conn.Close();
            Console.WriteLine("Recipe Successfully Updated");
        }
        public static void updateRecipeIngredients(int id, string ingredients)
        {
            string cmdTxt = $"UPDATE RecipeBook SET RecipeIngredients='{ingredients}' WHERE RecipeId={id}";
            ExecuteCommand(cmdTxt, out SqlDataReader reader);
            reader.Close();
            conn.Close();
            Console.WriteLine("Recipe Successfully Updated");
        }
        public static void updateRecipeDescription(int id, string description)
        {
            string cmdTxt = $"UPDATE RecipeBook SET RecipeDescription='{description}' WHERE RecipeId={id}";
            ExecuteCommand(cmdTxt, out SqlDataReader reader);
            reader.Close();
            conn.Close();
            Console.WriteLine("Recipe Successfully Updated");
        }
        public static void updateRecipe(int id, string name, string ingredients, string description)
        {
            string cmdTxt = $"UPDATE RecipeBook SET RecipeName='{name}',RecipeIngredients='{ingredients}',RecipeDescription='{description}'  WHERE RecipeId={id}";
            ExecuteCommand(cmdTxt, out SqlDataReader reader);
            reader.Close();
            conn.Close();
            Console.WriteLine("Recipe Successfully Updated");
        }
        public static void deleteRecipe(int id)
        {
            string cmdTxt = $"DELETE FROM RecipeBook WHERE RecipeId={id}";
            ExecuteCommand(cmdTxt, out SqlDataReader reader);
            reader.Close();
            conn.Close();
            Console.WriteLine("Recipe Successfully Deleted");
        }
        public static void ExecuteCommand(string cmdTxt, out SqlDataReader reader)
        {
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = cmdTxt;
            //Console.WriteLine(cmdTxt);
            reader = cmd.ExecuteReader();
        }
    }
}