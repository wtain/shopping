using ShoppingApp.Core;
using ShoppingApp.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingSandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            ShoppingItemListApplication app = new ShoppingItemListApplication();

            app.AddShoppingItem(new ShoppingItem("Хлеб", new List<string> { "Ашан" }, "Продукты"));
            app.AddShoppingItem(new ShoppingItem("Хлеб", new List<string> { "Окей" }, "Продукты"));
            app.AddShoppingItem(new ShoppingItem("Сыр",  new List<string> { "Ашан" }, "Продукты"));
            app.AddShoppingItem(new ShoppingItem("Яйца", new List<string> { "Ашан", "Окей", "Фасоль" }, "Продукты"));

            foreach (var item in app.EnumItemsInShop("Ашан"))
                Console.WriteLine(item);

            Console.WriteLine();

            foreach (var item in app.EnumItemsInShop("Окей"))
                Console.WriteLine(item);

            Console.WriteLine();

            foreach (var item in app.EnumShops())
                Console.WriteLine(item);
        }
    }
}
