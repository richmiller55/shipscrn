using System;
using System.Collections.Generic;
using System.Text;

namespace ShipScrn
{
    class BuyGroup
    {
        Epicor.Mfg.BO.Customer custObj;
        bool buyGroupMember = false;
        public BuyGroup(Epicor.Mfg.Core.Session session, string custId)
        {
            this.custObj = new Epicor.Mfg.BO.Customer(session.ConnectionPool);
            Epicor.Mfg.BO.CustomerDataSet ds = this.custObj.GetByCustID(custId);
            foreach (Epicor.Mfg.BO.CustomerDataSet.CustBillToRow btRow in
                (Epicor.Mfg.BO.CustomerDataSet.CustBillToRow)ds.CustBillTo.Rows)
            {
                if (btRow.DefaultBillTo)
                {
                    this.buyGroupMember = true;
                    break;
                }
            }
        }
        public bool GetBuyGroupMember()
        {
            return this.buyGroupMember;
        }
    }
}