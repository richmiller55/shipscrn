using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace ARInvoiceLoad
{
    public class ARInvoiceLoadDataReader
    {
        public SqlDataReader reader;
        public ARInvoiceLoadDataReader()
        {
            reader = SetDataReader();
        }
        public SqlDataReader GetReader()
        {
            return reader;
        }
        public SqlDataReader SetDataReader()
        {
            SqlConnection connection = new SqlConnection("Data Source=localhost; Integrated Security=SSPI;" +
                                                        "Initial Catalog=IntoVantage");
            connection.Open();
            string sql = @"
                select 
		            cust as cust,
		            invoice as invoice,
		            terms as terms,
		            openAmt as openAmt,
		            credit as credit,
		            absAmt as absAmt,
		            tranDate as tranDate,
		            fiscalYear as fiscalYear,
		            fiscalMth  as fiscalMth
                from IntoVantage.dbo.arInvoiceLoad 
                where invoice <= 603983 and cust <> 'SUSPEN'
                and cust <> '6903' and cust <> '6903 ' 
                and cust <> '1   ' and cust <> '1'
                and cust <> '6610' and cust <> '6610 ' 
            ";

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            return myReader;
        }
 
    }
}