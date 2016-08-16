using ShoppingApp.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Interfaces
{
    public interface IShoppingItemListApplication
    {
        void AddShoppingItem(ShoppingItem item);
        IEnumerable<ShoppingItem> EnumItemsInShop(string shopName);
        IEnumerable<string> EnumShops();
    }
}
