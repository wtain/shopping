
using System;
using System.Collections.Generic;
using System.Linq;
using ShoppingApp.Interfaces;
using ShoppingApp.Types;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Diagnostics;
using ShoppingApp.Extensions;
using MongoDB.Bson.Serialization.Attributes;

namespace ShoppingApp.Core
{
    public class ShoppingItemListApplication : IShoppingItemListApplication
    {
        private static IMongoClient _client;
        private static IMongoDatabase _database;
        private static readonly string _databaseName = "shopping";
        private static IMongoCollection<ShoppingItem> _shoppingItems;

        protected static IMongoClient MongoClient
        {
            get
            {
                if (null == _client)
                    _client = new MongoClient();
                return _client;
            }
        }

        protected static IMongoDatabase MongoDatabase
        {
            get
            {
                if (null == _database)
                    _database = MongoClient.GetDatabase(_databaseName);
                return _database;
            }
        }

        protected static IMongoCollection<ShoppingItem> ShoppingItems
        {
            get
            {
                if (null == _shoppingItems)
                    _shoppingItems = MongoDatabase.GetCollection<ShoppingItem>("shoppingitems");
                return _shoppingItems;
            }
        }

        public ShoppingItemListApplication()
        {

        }

        public void AddShoppingItem(ShoppingItem item)
        {
            var filter = Builders<ShoppingItem>.Filter.Eq("Name", item.Name);
            var result = ShoppingItems.Find(filter).ToList();

            Debug.Assert(result.Count <= 1);
            if (result.Count == 0)
            {
                ShoppingItems.InsertOne(item);
                return;
            }

            ShoppingItem newItem = result[0];
            if (!item.Category.IsNullOrEmpty())
                newItem.Category = item.Category;
            newItem.ShopNames.AddRange(item.ShopNames);
            newItem.ShopNames = new List<string>(newItem.ShopNames.Distinct());

            var filterU = Builders<ShoppingItem>.Filter.Eq("Name", item.Name);
            var update  = Builders<ShoppingItem>.Update.Set("Category", newItem.Category)
                                                       .Set("ShopNames", newItem.ShopNames);
            var resultU = ShoppingItems.UpdateOne(filterU, update);
        }

        public IEnumerable<ShoppingItem> EnumItemsInShop(string shopName)
        {
            return ShoppingItems.Find(item => item.ShopNames.Contains(shopName)).ToEnumerable();
        }

        [BsonIgnoreExtraElements]
        private class UnwoundShoppingItem
        {
            public string ShopNames { get; set; }
        }

        public IEnumerable<string> EnumShops()
        {
            return ShoppingItems.Aggregate()
                .Unwind<ShoppingItem, UnwoundShoppingItem>(x => x.ShopNames)
                .Group(x => x.ShopNames, g => new { Id = g.Key } )
                .ToEnumerable().Select(x => x.Id);
        }
    }
}