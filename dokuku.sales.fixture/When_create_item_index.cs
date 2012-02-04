using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using dokuku.sales.item;
using dokuku.sales.config;
using dokuku.sales.item.model;

namespace dokuku.sales.fixture
{
    [Subject("Create Index")]
    public class When_create_item_index
    {
        private static IItemCommand itemCmd;
        private static IItemQuery itemQry;
        private static Item item;
        private static Guid id1;
        private static Guid id2;
        private static Guid id3;
        static MongoConfig mongo;
        Establish context = () =>
            {
                mongo = new MongoConfig();
                itemCmd = new ItemCommand(mongo);
                itemQry = new ItemQuery(mongo);
                id1 = Guid.NewGuid();
                id2 = Guid.NewGuid();
                id3 = Guid.NewGuid();
            };
        Because of = () =>
        {
            itemCmd.Save(new Item()
            {
                _id = id1,
                OwnerId = "oetawan@inforsys.co.id",
                Name = "Honda Jazz",
                Description = "All new honda jazz",
                Rate = 200000000,
                Tax = new Tax() { Code = "PPN", Value = 0.1m }
            });
            itemCmd.Save(new Item()
            {
                _id = id2,
                OwnerId = "oetawan@inforsys.co.id",
                Name = "Honda Jazz",
                Description = "All new honda jazz",
                Rate = 200000000,
                Tax = new Tax() { Code = "PPN", Value = 0.1m }
            });
            itemCmd.Save(new Item()
            {
                _id = id3,
                OwnerId = "oetawan@inforsys.co.id",
                Name = "Honda Pop",
                Description = "All new honda pop",
                Rate = 200000000,
                Tax = new Tax() { Code = "PPN", Value = 0.1m }
            });
        };
        It should_create_item_index = () =>
            {
                IEnumerable<ItemReports> items = itemQry.Search("oetawan@inforsys.co.id", new string[] { "Honda Pop" });
                items.Count().Equals(1);
                items = itemQry.Search("oetawan@inforsys.co.id", new string[] { "Honda Jazz" });
                items.Count().Equals(2);
                items = itemQry.Search("oetawan@inforsys.co.id", new string[] { "Honda" });
                items.Count().Equals(3);
            };
        Cleanup cleanup = () =>
            {
                itemCmd.Delete(id1);
                itemCmd.Delete(id2);
                itemCmd.Delete(id3);
            };
    }
}
