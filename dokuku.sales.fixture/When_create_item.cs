using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using dokuku.sales.item;
using dokuku.sales.config;
namespace dokuku.sales.fixture
{
    [Subject("Creating item")]
    public class When_create_item
    {
        private static IItemCommand itemCmd;
        private static IItemQuery itemQry;
        private static Item item;
        private static Guid id;
        Establish context = () =>
            {
                MongoConfig mongoCfg = new MongoConfig();
                itemCmd = new ItemCommand(mongoCfg);
                itemQry = new ItemQuery(mongoCfg);
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

        It should_create_item = () =>
            {
                item = itemQry.Get(id);
                item.OwnerId.ShouldEqual<string>("oetawan@inforsys.co.id");
                item.Name.ShouldEqual("Honda Jazz");
                item.ShouldNotBeNull();

                item.Tax.Name.ShouldEqual("PPN");
            };

        Cleanup cleanup = () =>
            {
                itemCmd.Delete(id);
            };
    }
}