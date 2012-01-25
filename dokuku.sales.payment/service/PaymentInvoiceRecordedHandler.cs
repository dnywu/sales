using dokuku.sales.domainevents;
using dokuku.sales.payment.domainevents;
using NServiceBus;
using dokuku.sales.payment.messages;
using MongoDB.Bson;
namespace dokuku.sales.payment.service
{
    public class PaymentInvoiceRecordedHandler : Handles<PaymentRecorded>
    {
        public IBus Bus { get; set; }
        public void Handle(PaymentRecorded args)
        {
            Bus.Publish(new PaymentIsRecorded
            {
                 PaymentJson = args.ToJson()
            });
        }
    }
}
