using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using dokuku.sales.item;
namespace dokuku.sales.fixture
{
    [Subject("Creating item")]
    public class When_create_item
    {
        private static IItemRepository itemRepo;
        private static Item item;
        private static Guid id;

        Establish context = () =>
            {
                itemRepo = new ItemRepository();
                id = Guid.NewGuid();
            };

        Because of = () =>
            {
                itemRepo.Save(new Item()
                {
                    _id = id,
                    OwnerId = "oetawan@inforsys.co.id",
                    Name = "Honda Jazz",
                    Description = "All new honda jazz",
                    Rate = 200000000,
                    Tax = new Tax() { Name = "PPN", Value = 0.1m }
                });
            };

        It should_create_organization = () =>
            {
                item = itemRepo.Get(id);
                item.ShouldNotBeNull();
            };

        Cleanup cleanup = () =>
            {
                itemRepo.Delete(id);
            };
    }
}