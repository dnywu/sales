using System;
using System.Collections.Generic;
using dokuku.sales.payment.command;
using dokuku.sales.payment.domain;
using dokuku.sales.payment.messages;
using dokuku.sales.payment.query;
using MongoDB.Bson;
using Newtonsoft.Json;
using NServiceBus;
using dokuku.sales.payment.events;

namespace dokuku.sales.payment.service
{
    public class PaymentModeService : IPaymentModeService
    {
        IPaymentModeCommand _command;
        IPaymentModeQuery _query;
        IBus _bus;

        public PaymentModeService(IPaymentModeCommand command, IPaymentModeQuery query, IBus bus)
        {
            _command = command;
            _query = query;
            _bus = bus;
        }
        public PaymentMode Insert(string json)
        {
            PaymentMode paymentMode = JsonConvert.DeserializeObject<PaymentMode>(json);
            FailedIfNameAlreadyExistsOnInsert(paymentMode);
            _command.Save(paymentMode);

            _bus.Publish(new PaymentModeCreated { Data = paymentMode.ToJson() });
            return paymentMode;
        }
        public PaymentMode Get(Guid id)
        {
            return _query.Get(id);
        }
        public IEnumerable<PaymentMode> FindAll()
        {
            return _query.FindAll();
        }
        public PaymentMode Update(string json)
        {
            PaymentMode paymentMode = JsonConvert.DeserializeObject<PaymentMode>(json);
            FailedIfNameAlreadyExistsOnUpdate(paymentMode);
            _command.Update(paymentMode);

            _bus.Publish(new PaymentModeUpdated { Data = paymentMode.ToJson() });
            return paymentMode;
        }
        public void Delete(Guid id)
        {
            _command.Delete(id);
            _bus.Publish(new PaymentModeDeleted { Id = id });
        }
        public void FailedIfNameAlreadyExistsOnInsert(PaymentMode paymentMode)
        {
            if (_query.FindByName(paymentMode.Name) != null)
                throw new Exception(String.Format("Payment mode dengan nama {0} sudah ada!", paymentMode.Name));
        }
        public void FailedIfNameAlreadyExistsOnUpdate(PaymentMode paymentMode)
        {
            if (_query.FindByNameAndId(paymentMode.Name, paymentMode._id) != null)
                throw new Exception(String.Format("Payment mode dengan nama {0} sudah ada!", paymentMode.Name));
        }
    }
}