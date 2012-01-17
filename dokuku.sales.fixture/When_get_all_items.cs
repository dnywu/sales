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
        private static Guid id;
        static MongoConfig mongo;
        Establish context = () =>
            {
                itemCmd = new ItemCommand(mongo);
                itemQry = new ItemQuery(mongo);
                id = Guid.NewGuid();
            };

        Because of = () =>
            {
                itemCmd.Save(new Item()
                {
                    _id = id,
                    OwnerId = "oetawan@inforsys.co.id",
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
            };

        Cleanup cleanup = () =>
            {
                itemCmd.Delete(id);
            };
    }
}