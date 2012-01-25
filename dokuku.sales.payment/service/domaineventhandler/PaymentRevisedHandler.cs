using dokuku.sales.payment.domainevents;
using dokuku.sales.domainevents;
using NServiceBus;
using dokuku.sales.payment.messages;
using MongoDB.Bson;
using dokuku.sales.payment.events;
namespace dokuku.sales.payment.service
{
    public class PaymentRevisedHandler : Handles<dokuku.sales.payment.domainevents.PaymentRevised>
    {
        public IBus Bus { get; set; }
        public void Handle(dokuku.sales.payment.domainevents.PaymentRevised args)
        {
            Bus.Publish(new dokuku.sales.payment.events.PaymentRevised
            {
                PaymentJson = args.ToJson()
            });
        }
    }
}