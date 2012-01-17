using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using dokuku.sales.item;
using dokuku.sales.config;
namespace dokuku.sales.fixture
{
    [Subject("Get item by id")]
    public class When_get_item_by_id
    {
        private static IItemCommand itemCmd;
        private static IItemQuery itemQry;
        private static Guid id;
        static MongoConfig mongo;
        static Item result;
        Establish context = () =>
            {
                mongo = new MongoConfig();
                itemCmd = new ItemCommand(mongo);
                itemQry = new ItemQuery(mongo);
                id = Guid.NewGuid();
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

        Because of = () =>
            {
                result = itemQry.Get(id);
            };

        It should_return_item = () =>
            {
                result.ShouldNotBeNull();
            };

        Cleanup cleanup = () =>
            {
                itemCmd.Delete(id);
            };
    }
}