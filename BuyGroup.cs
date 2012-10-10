using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Odbc;

namespace ShipScrn
{
    class BuyGroup
    {
        bool buyGroupMember = false;
        public BuyGroup(int custNum)
        {
            buyGroupMember = BuyGrupQuery(custNum);
        }
        public bool GetBuyGroupMember()
        {
            return this.buyGroupMember;
        }
        bool BuyGrupQuery(int custNum)
        {
            string baseQuery = @" 
           select
             cm.CustNum,
             cm.CustID,
             cm.Name,
             ifnull(cb.DefaultBillTo,0) as DefaultBullTo
           from Customer as cm
           left join CustBillTo as cb
             on cb.CustNum = cm.CustNum
           where cb.DefaultBillTo = 1
           ";
            StringBuilder querystring = new StringBuilder();
            querystring.Append(baseQuery);
            querystring.AppendLine(" and cm.CustNum = " + custNum.ToString());
            string Dsn = GetMySqlDsn();
            bool member = false;
            using (OdbcConnection connection = new OdbcConnection(Dsn))
            {
                OdbcCommand command = new OdbcCommand(querystring.ToString(), connection);
                connection.Open();
                OdbcDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    string DefaultBillTo = reader["DefaultBullTo"].ToString();
                    if (DefaultBillTo.Equals("1"))
                    {
                        member = true;
                    }
                }
                reader.Close();
            }
            return member;
        }
        private string GetMySqlDsn()
        {
            return "DSN=GC; HOST=gc.rlm5.com; DB=coinet_db1; UID=focus; PWD=focusgroup";
        }
    }
}