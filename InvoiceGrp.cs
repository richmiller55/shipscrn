using System;
using System.Collections.Generic;
using System.Text;

namespace ShipScrn
{
    class InvoiceGrp
    {
        Epicor.Mfg.BO.InvcGrp invcGrpObj;
        Epicor.Mfg.BO.InvcGrpDataSet invcGrpDs;
        string invcGroupName;
        decimal totalInvAmt;
        DateTime invoiceDate;
        string newInvoices = string.Empty;
        public InvoiceGrp(Epicor.Mfg.Core.Session vanSession, string groupName)
        {
            this.invcGroupName = groupName;
            invcGrpObj = new Epicor.Mfg.BO.InvcGrp(vanSession.ConnectionPool);

            try
            {
                invcGrpDs = invcGrpObj.GetByID(groupName);
                Epicor.Mfg.BO.InvcGrpDataSet.InvcGrpRow row =
                    (Epicor.Mfg.BO.InvcGrpDataSet.InvcGrpRow)invcGrpDs.InvcGrp.Rows[0];
                this.totalInvAmt = row.TotalInvAmt;
                this.invoiceDate = row.InvoiceDate;
            }
            catch (Exception e)
            {
                invcGrpDs = new Epicor.Mfg.BO.InvcGrpDataSet();
                invcGrpObj.GetNewInvcGrp(invcGrpDs);
                Epicor.Mfg.BO.InvcGrpDataSet.InvcGrpRow row =
                    (Epicor.Mfg.BO.InvcGrpDataSet.InvcGrpRow)invcGrpDs.InvcGrp.Rows[0];
                row.GroupID = groupName;
                this.totalInvAmt = row.TotalInvAmt;
                DateTime today = new DateTime();
                row.InvoiceDate = today;
                this.invoiceDate = today;
                string message;
                try
                {
                    invcGrpObj.Update(invcGrpDs);
                }
                catch (Exception e2)
                {
                    message = e.Message;
                }
            }
        }
    }
//        public void GetInvoice(string packNo, out string invoices, out string errors)
//        {
}
