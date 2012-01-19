using System;
using dokuku.sales.item.messages;
using NServiceBus;
using System.Threading;
namespace dokuku.sales.report.Handlers
{
    public class ItemCreatedHandler : IHandleMessages<ItemCreated>
    {
        public void Handle(ItemCreated message)
        {
            Thread.Sleep(300);
            Console.WriteLine("***********{0}", message.Id);
        }
    }
}