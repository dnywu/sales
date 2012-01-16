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
        static IItemCommand _itemCmd;
        static IItemQuery _itemQry;
        static Item _item;
        static Guid _id;

        Establish initialize = () =>
        {
            MongoConfig mongoCfg = new MongoConfig();
            _itemCmd = new ItemCommand(mongoCfg);
            _itemQry = new ItemQuery(mongoCfg);
            _id = Guid.NewGuid();
        };

        Because of = () =>
        {
            _itemCmd.Save(new Item()
            {
                _id = _id,
                OwnerId = "oetawan@inforsys.co.id",
                Name = "Honda Jazz",
                Description = "All new honda jazz",
                Rate = 200000000,
                Tax = new Tax() { Name = "PPN", Value = 0.1m }
            });
        };

        It should_create_item = () =>
        {
            _item = _itemQry.Get(_id);
            _item.ShouldNotBeNull();
        };

        It must_be_success_on_update_item = () => 
        {
            _item.OwnerId = "irfan";
            _itemCmd.Update(_item);
            _item = _itemQry.Get(_id);

            _item.OwnerId.ShouldEqual("irfan");
        };

        Cleanup cleanup = () =>
        {
            _itemCmd.Delete(_id);
        };
    }
}