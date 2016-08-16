
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingApp.Types
{
    public class ShoppingList
    {
        public string Name;
        public DateTime DateCreated;
        public DateTime DateExecuted;
        public List<ShoppingItem> Items;

        [BsonConstructor]
        public ShoppingList(string Name, DateTime Date, List<ShoppingItem> Items)
        {
            this.Name = Name;
            this.DateCreated = Date;
            this.DateExecuted = DateTime.MinValue;
            this.Items = Items;
        }

        public ShoppingList Execute()
        {
            DateExecuted = DateTime.Now;
            return new ShoppingList(Name, DateExecuted, new List<ShoppingItem>(Items.Where(x => !x.Bought)));
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
