//Programmer: Brian Lee
//Date: 06/18/2024

//Title: CSI 120 Final: Calories Counter Application

using System;
using System.Text.RegularExpressions;

namespace Calorie_Counter_App
{
    internal class Program
    {
        #region
        public static FoodItem[] foodItem = new FoodItem[10];
        public static void Preload()
        {
            foodItem[0] = new FoodItem("Apple", "Fruit", 95, 1);
            foodItem[1] = new FoodItem("Banana", "Fruit", 105, 2);
            foodItem[2] = new FoodItem("Carrot", "Vegetable", 25, 3);
            foodItem[3] = new FoodItem("Broccoli", "Vegetable", 55, 2);
            foodItem[4] = new FoodItem("Chicken", "Protein", 165, 1);
            foodItem[5] = new FoodItem("Beef", "Protein", 250, 1);
            foodItem[6] = new FoodItem("Rice", "Grain", 205, 1);
            foodItem[7] = new FoodItem("Bread", "Grain", 80, 2);
            foodItem[8] = new FoodItem("Milk", "Dairy", 150, 1);
            foodItem[9] = new FoodItem("Cheese", "Dairy", 110, 2);
        }
        #endregion
        #region
        static void Main(string[] args)
        {
            Preload();
            bool exit = false;
            int userMenuInput;
            do
            {
                DisplayMenu();
                Console.Write("User Choice:  ");
                userMenuInput = MenuInputChecker();
                Console.WriteLine();
                exit = MenuSelection(userMenuInput);

                if (!exit)
                {
                    Console.WriteLine();
                    Console.WriteLine("Press Enter to continue...");
                    Console.ReadLine();
                    Console.Clear();
                }
            } while (exit == false);
        }
        #endregion
        //----------------------MENU METHODS---------------------------------
        #region
        public static void DisplayMenu()
        {
            Console.WriteLine("1. Display all the calories you have eaten");
            Console.WriteLine("2. Display all food eaten of a certain category");
            Console.WriteLine("3. Add New Items");
            Console.WriteLine("4. Search for a food item by name");
            Console.WriteLine("5. Exit");
        }
        public static bool MenuSelection(int userMenuInput)
        {
            switch (userMenuInput)
            {
                case 1:
                    Console.WriteLine("-----Display Food List-------");
                    Console.WriteLine();
                    DisplayFoodItemInfo();
                    return false;
                case 2:
                    Console.WriteLine("----Display Category Food List-----");
                    Console.WriteLine();
                    DisplayCategoryFoodItem();
                    return false;
                case 3:
                    Console.WriteLine("-----Add New Item------");
                    Console.WriteLine();
                    AddNewItem();
                    return false;
                case 4:
                    Console.WriteLine("-------Search Food By Name -------");
                    Console.WriteLine();
                    SearchFoodByName();
                    return false;
                case 5:
                    Console.WriteLine("-----Exiting the Program-----");
                    return true;
                default:
                    return false;
            }
        }//end of MenuSelection
        #endregion
        //----------------------APP METHODS---------------------------
        #region
        public static void DisplayFoodItemInfo()
        {
            Console.WriteLine($"{"Name",-10} | {"Category",-10} | {"Calories",-8} | {"Quantity",-8} | {"Total Calories",-14}");
            Console.WriteLine("--------------------------------------------------");
            foreach (var item in foodItem)
            {
                if(item != null)
                {
                    Console.WriteLine($"{item.Name,-10} | {item.Category,-10} | {item.Calories,-8} | {item.Quantity,-8} | {item.TotalCalories,-14}");
                }
            }
            Console.WriteLine();
            Console.WriteLine($"The Total Calories Consumed is:  {CalculateTotalCaloriesEaten()}");
            Console.WriteLine();
            Console.WriteLine($"The Average Calories Consumed is:  {CalculateAverageCaloriesEaten(CalculateTotalCaloriesEaten())}");
        }//end of DisplayFoodItem
        public static void DisplayCategoryFoodItem()
        {
            Console.WriteLine("Enter the Category you want listed");
            string userCategory = StringInputChecker();
            Console.WriteLine($"{"Name",-10} | {"Calories",-8} | {"Quantity",-8} | {"Total Calories",-14}");
            Console.WriteLine("--------------------------------------------------");
            int numberInCategory = 0;
            foreach (var item in foodItem)
            {
                if (item != null && item.Category.ToLower() == userCategory.ToLower())
                {
                    Console.WriteLine($"{item.Name,-10} |  {item.Calories,-8} | {item.Quantity,-8} | {item.TotalCalories,-14}");
                    numberInCategory = numberInCategory + 1;
                }
            }
            if(numberInCategory == 0)
            {
                Console.WriteLine("No Category Listed");
            }
        }//end of DisplayCategoryFoodItem
        public static void AddNewItem()
        {
            Console.WriteLine("Enter the Name of Food You Want to Add");
            string userName = StringInputChecker();
            int index = CheckFoodByName(userName);
            if (index != -1)
            {
                Console.WriteLine("Food Already Listed");
                Console.WriteLine();
                Console.WriteLine($"{"Name",-10} | {"Category",-10} | {"Calories",-8} | {"Quantity",-8} | {"Total Calories",-14}");
                Console.WriteLine("--------------------------------------------------");
                Console.WriteLine($"{foodItem[index].Name,-10} | {foodItem[index].Category,-10} | {foodItem[index].Calories,-10} | {foodItem[index].Quantity,-10} | {foodItem[index].TotalCalories,-14}");
            }
            else
            {
                Console.WriteLine("Food Not listed");
                Console.WriteLine();
                Console.WriteLine("Enter the Category:");
                string userCategory = StringInputChecker();
                Console.WriteLine("Enter the Caloires");
                int userCalories = IntInputChecker();
                Console.WriteLine("Enter the Quantity");
                int userQuantity = IntInputChecker();

                int empty = CheckSpace();
                Console.WriteLine("Empty = " + empty);
                if (empty == -1)
                {
                    doubleArray();
                    empty = CheckSpace();
                }
                foodItem[empty] = new FoodItem(userName, userCategory, userCalories, userQuantity);
                DisplayFoodItemInfo();
            }

        }//end of AddNewItem
        public static void SearchFoodByName()
        {
            Console.WriteLine("Enter the Name of Food You Want to Search");
            string userName = StringInputChecker();
            int index = CheckFoodByName(userName);
            if (index != -1)
            {
                Console.WriteLine();
                Console.WriteLine($"{"Name",-10} | {"Category",-10} | {"Calories",-8} | {"Quantity",-8} | {"Total Calories",-14}");
                Console.WriteLine("--------------------------------------------------");
                Console.WriteLine($"{foodItem[index].Name,-10} | {foodItem[index].Category,-10} | {foodItem[index].Calories,-10} | {foodItem[index].Quantity,-10} | {foodItem[index].TotalCalories,-14}");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Food Not Listed");
            }
        }//end of SearchFoodByName
        #endregion
        //-------------------UTILITY METHODS----------------------
        #region
        public static int CheckFoodByName(string userName)
        {
            for (int i = 0; i < foodItem.Length; i++)
            {
                if (foodItem[i] != null && foodItem[i].Name.ToLower() == userName.ToLower())
                {
                    return i;
                }
            }
            return -1;
        }//end of CheckFoodByName
        public static int CalculateTotalCaloriesEaten()
        {
            int totalCalories = 0;
            foreach (var item in foodItem)
            {
                if(item !=null)
                {
                    totalCalories += item.TotalCalories;
                }
            }
            return totalCalories;
        }//end of CalculateTotalCaloriesEaten()
        public static int CalculateAverageCaloriesEaten(int totalCalories)
        {
            int itemCount = 0;
            foreach (var item in foodItem)
            {
                if (item != null)
                {
                    itemCount += 1;
                }
            }
            return totalCalories / itemCount;
        }//end of CalculateAverageCaloriesEaten
        public static int CheckSpace()
        {
            for (int i = 0; i < foodItem.Length; i++)
            {
                if (foodItem[i] == null)
                {
                    return i;
                }
            }
            return -1;
        }//end of CheckSpace
        public static void doubleArray()
        {
            FoodItem[] newFoodItemArray = new FoodItem[foodItem.Length * 2];
            for (int i = 0; i < foodItem.Length; i++)
            {
                newFoodItemArray[i] = foodItem[i];
            }
            foodItem = newFoodItemArray;
        }//end of doubleArray
        #endregion
        //----------------INPUT CHECKER------------------
        #region
        public static int IntInputChecker()
        {
            int userIntInput;
            while (!int.TryParse(Console.ReadLine(), out userIntInput))
            {
                Console.WriteLine("Invalid Input. Try Again.");
            }
            return userIntInput;
        }//end of intInputChecker
        public static string StringInputChecker()
        {
            string userStringInput;
            string pattern = @"^[a-zA-z]+$";
            while ((userStringInput = Console.ReadLine()) != null && !Regex.IsMatch(userStringInput, pattern))
            {
                Console.WriteLine("Invalid Input. Try Again.");
            }
            return userStringInput ?? string.Empty;
        }//end of StringInputChecker
        public static int MenuInputChecker()
        {
            int userMenuInput;
            while (!int.TryParse(Console.ReadLine(), out userMenuInput) || userMenuInput < 1 || userMenuInput > 5)
            {
                Console.WriteLine("Invalid Input. Try Again.");
            }
            return userMenuInput;
        }//end of MenuInputChecker
    }//end of Program
    #endregion
    //----------------------------FOOD ITEM CLASS----------------------------------
    #region
    public class FoodItem
    {
            //Create a Constructor. (Highlight Field and ctrl + .)
            public FoodItem(string name, string category, int calories, int quantitiy)
            {
                Name = name;
                Category = category;
                Calories = calories;
                Quantity = quantitiy;
                TotalCalories = CalculateTotalCalories();
            }

            //Define the Field.(User prop tab for faster input)
            public string Name { get; set; }
            public string Category { get; set; }
            public int Calories { get; set; }
            public int Quantity { get; set; }
            public int TotalCalories { get; set; }

            //Create a default.(ctor tab for faster input)
            public FoodItem()
            {
                Name = "No Food Item";
                Category = "No Category";
                Calories = -1;
                Quantity = -1;
                TotalCalories = -1;
            }

            public int CalculateTotalCalories()
            {
                return Calories * Quantity;
            }//end of CalculateTotalCaloires(method)

    }//end of FoodItem(Class)
    #endregion
}