using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.sales.item;
using Machine.Specifications;
using dokuku.sales.config;

namespace dokuku.sales.fixture
{
    [Subject("Update Item")]
    public class When_update_item
    {
        static IItemCommand itemCmd;
        static IItemQuery itemQry;
        static Item item;
        static Guid guid;

        Establish initialize = () =>
        {
            MongoConfig mongoCfg = new MongoConfig();
            itemCmd = new ItemCommand(mongoCfg);
            itemQry = new ItemQuery(mongoCfg);
            guid = Guid.NewGuid();
        };

        Because of = () =>
        {
            itemCmd.Save(new Item()
            {
                _id = guid,
                OwnerId = "oetawan@inforsys.co.id",
                Name = "Honda Jazz",
                Description = "All new honda jazz",
                Rate = 200000000,
                Tax = new Tax() { Code = "PPN", Value = 0.1m }
            });
        };

        It should_create_item = () =>
        {
            item = itemQry.Get(guid);
            item.ShouldNotBeNull();
        };

        It must_be_success_on_update_item = () => 
        {
            item.OwnerId = "irfan";
            itemCmd.Save(item);
            item = itemQry.Get(guid);

            item.OwnerId.ShouldEqual("irfan");
        };

        Cleanup cleanup = () =>
        {
            itemCmd.Delete(guid);
        };
    }
}