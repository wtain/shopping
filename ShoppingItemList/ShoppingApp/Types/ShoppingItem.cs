
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace ShoppingApp.Types
{
    public class ShoppingItem
    {
        public ObjectId _id;
        //[BsonId]
        public string Name;
        public List<string> ShopNames;
        public string Category;
        public bool Bought;

        [BsonConstructor]
        public ShoppingItem(string Name, List<string> ShopNames, string Category)
        {
            this.Name = Name;
            this.ShopNames = ShopNames;
            this.Category = Category;
        }

        public override string ToString()
        {
            return string.Format("{0} ({1})", Name, Category);
        }
    }
}