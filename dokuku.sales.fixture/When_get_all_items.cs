using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using dokuku.sales.item;
using dokuku.sales.config;
namespace dokuku.sales.fixture
{
    [Subject("Get all items")]
    public class When_get_all_item
    {
        private static IItemCommand itemCmd;
        private static IItemQuery itemQry;
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
                    Tax = new Tax() { Name = "PPN", Value = 0.1m }
                });
                itemCmd.Save(new Item()
                {
                    _id = id2,
                    OwnerId = "oetawan@inforsys.co.id",
                    Name = "Avanza",
                    Description = "All new avanza",
                    Rate = 150000000,
                    Tax = new Tax() { Name = "PPN", Value = 0.1m }
                });
                itemCmd.Save(new Item()
                {
                    _id = id3,
                    OwnerId = "martin@inforsys.co.id",
                    Name = "Honda Jazz",
                    Description = "All new honda jazz",
                    Rate = 200000000,
                    Tax = new Tax() { Name = "PPN", Value = 0.1m }
                });
            };

        It should_return_all_items = () =>
            {
                IEnumerable<Item> result = itemQry.AllItems("oetawan@inforsys.co.id");
                result.First().ShouldNotBeNull();
                result.ToArray().Length.ShouldEqual(2);
            };

        Cleanup cleanup = () =>
            {
                System.Threading.Thread.Sleep(3000);
                itemCmd.Delete(id1);
                itemCmd.Delete(id2);
                itemCmd.Delete(id3);
            };
    }
}