using dokuku.sales.payment.domainevents;
using dokuku.sales.domainevents;
using NServiceBus;
using dokuku.sales.payment.messages;
using MongoDB.Bson;
namespace dokuku.sales.payment.service
{
    public class PaymentRecordRevisedHandler : Handles<PaymentRevised>
    {
        public IBus Bus { get; set; }
        public void Handle(PaymentRevised args)
        {
            Bus.Publish(new PaymentIsRevised
            {
                PaymentRevisedJson = args.ToJson()
            });
        }
    }
}
