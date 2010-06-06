using System;
using System.Collections.Generic;
using System.Text;

namespace ShipScrn
{
    class InvcGrp
    {
        Epicor.Mfg.BO.InvcGrp invcGrp;
        string invGroup;
        string newInvoices = string.Empty;
        public InvcGrp(Epicor.Mfg.Core.Session vanSession,string invcGroupName)
        {
	    
            arInvoice = new Epicor.Mfg.BO.ARInvoice(vanSession.ConnectionPool);
            this.invGroup = "FX001";
        }
        public void GetInvoice(string packNo, out string invoices, out string errors)
        {
