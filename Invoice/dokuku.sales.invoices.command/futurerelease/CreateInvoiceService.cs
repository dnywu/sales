using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ncqrs.Commanding.ServiceModel;
namespace dokuku.sales.invoices.commands
{
    public class CreateInvoiceHandler : ICommandServiceInterceptor
    {
        
        public void OnAfterExecution(CommandContext context)
        {
            
        }

        public void OnBeforeBeforeExecutorResolving(CommandContext context)
        {
        }

        public void OnBeforeExecution(CommandContext context)
        {
        }
    }
}