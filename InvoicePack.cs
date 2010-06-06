using System;
using System.Collections.Generic;
using System.Text;

namespace ShipScrn
{
    // this may not be needed, call the ARInvoice direct
    class InvoicePack
    {
        Epicor.Mfg.Core.Session session;
        ARInvoice arInvoice;        
        string packNo;
        public InvoicePack(Epicor.Mfg.Core.Session vanSession, Int32 vanPackNo)
        {
            session = vanSession;
            packNo = vanPackNo.ToString();
            init();
        }
        void init()
        {
            // arInvoice = new Epicor.Mfg.BO.ARInvoice(session.ConnectionPool);
            //arInvoice = new ARInvoice(this.session);
            //string invoices = string.Empty;
            //string errors = string.Empty;
            //arInvoice.GetInvoice(this.packNo, out invoices, out errors);
        }

    }
}
