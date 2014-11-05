using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data.Odbc;


namespace ShipScrn
{
    public class WriteBackTracking
    {
        public WriteBackTracking(int packNum)
        {
            SqlConnection connection = new SqlConnection("Data Source=app1; Integrated Security=SSPI;" +
                                                         "Initial Catalog=tracking");
            connection.Open();
            string sql = @"
              update   
                 tracking.dbo.tracking_raw_data
                set frt_upd_flag = 1,
                    trk_upd_flag = 1
                where pack_num = '" + packNum.ToString() + "\'";
            SqlCommand myCommand = new SqlCommand(sql, connection);
            myCommand.ExecuteNonQuery();
        }
    }
    public enum trackDb
    {
        packSlip,
        trackingNo,
        serviceClass,
        orderNo,
        shipDate,
        weight,
        charge,
        deleted
    }
    public class TrackingReader
    {
        public SqlDataReader reader;
        private ShipMgr shipMgr;
        public TrackingReader()
        {
            reader = SetDataReader();
            shipMgr = new ShipMgr();
            LoadShipMgr();
        }
        public SqlDataReader GetReader()
        {
            return reader;
        }
        private string GetTrackingSql()
        {
            string sql = @"
                select 
		            pack_num as packSlip,
                    isnull(tracking_no,'NoTracking') as trackingNo,
                    service as serviceClass,
                    isnull(order_num,0) as orderNo,
                    ship_date as shipDate,
                    isnull(weight,0) as weight,
                    isnull(cost,0) as charge,
                    isnull(deleted,'N') as deleted
                from tracking.dbo.tracking_raw_data
                where ship_date = " + "'" + GetTodaysDateStr() + "'" +
                @" and pack_num is not null 
                and trk_upd_flag = 0
                and service in ('ground','standard','USPS1ST','USPSPRI','USPSPP','FGRB','F1DP','MGPP','20','F2DP','F1PA')
                -- and service in ('ground')
                and pack_num not in ( 323824 )
                 ";
            return sql;
        }
        void MySqlDataReader()
        {
            string baseQuery = @" 
                select 
		            pack_num as packSlip,
                    isnull(tracking_no,'NoTracking') as trackingNo,
                    service as serviceClass,
                    isnull(order_num, 0) as orderNo,
                    ship_date as shipDate,
                    isnull(weight,0) as weight,
                    isnull(cost,0) as charge,
                    isnull(deleted,'N') as deleted
                from tracking.dbo.tracking_raw_data
                where ship_date = " + "'" + GetTodaysDateStr() + "'" +
                @" and pack_num is not null 
                and trk_upd_flag = 0
                and service in ('ground','standard','USPS1ST','USPSPRI','USPSPP','FGRB','F1DP','MGPP','20','F2DP','F1PA')
                -- and service in ('ground')
                and pack_num not in ( 999999, 323824 )
                 ";
            StringBuilder querystring = new StringBuilder();
            querystring.Append(baseQuery);
            //   querystring.AppendLine(" where OrderNum = " + OrderNum.ToString());
            string Dsn =  GetMySqlDsn();
            // ArrayList al;
            using (OdbcConnection connection = new OdbcConnection(Dsn))
            {
                OdbcCommand command = new OdbcCommand(querystring.ToString(), connection);
                connection.Open();
                OdbcDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    // do all the stuff we do now, loop over the packs to bill them
                }
                reader.Close();
            }
        }
        public string GetMySqlDsn()
        {
            return "DSN=GC; HOST=gc.rlm5.com; DB=coinet_db1; UID=focus; PWD=focusgroup";
        }
        public SqlDataReader SetDataReader()
        {
            SqlConnection connection = new SqlConnection("Data Source=app1; Integrated Security=SSPI;" +
                                                        "Initial Catalog=tracking");
            connection.Open();
            string sql = GetTrackingSql(); 
            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            return myReader;
        }
        public ShipMgr GetShipMgr() { return shipMgr; }
        private string GetTodaysDateStr()
        {
            string year = DateTime.Now.Year.ToString();
            string month = DateTime.Now.Month.ToString();
            string day = DateTime.Now.Day.ToString();
            if (month.Length == 1) { month = "0" + month; }
            if (day.Length == 1) { day = "0" + day; }
            string yyyymmdd = year + month + day;
            // yyyymmdd = "20141024";
            return yyyymmdd;
        }
        public System.DateTime ConvertStrToDate(string dateStr)
        {
            string year = dateStr.Substring(0, 4);
            string month = dateStr.Substring(4, 2);
            string day = dateStr.Substring(6, 2);

            System.DateTime dateObj = new DateTime(Convert.ToInt32(year),
                Convert.ToInt32(month), Convert.ToInt32(day));
            return dateObj;
        }
        public void LoadShipMgr()
        {
            SqlDataReader reader = GetReader();
            if (reader.HasRows)
            {
                bool thingsToRead = reader.Read();
                while (thingsToRead)
                {
                    decimal packSlip_d = reader.GetDecimal((int)trackDb.packSlip);
                    if (packSlip_d.Equals(999999))
                    {
                        thingsToRead = reader.Read();
                        continue;
                    }
                    if (packSlip_d > 999999m)
                    {
                        thingsToRead = reader.Read();
                        continue;
                    }
                    int packSlip = System.Convert.ToInt32(packSlip_d);
                    string trackingNo = reader.GetString((int)trackDb.trackingNo);
                    string shipDate_s = reader.GetString((int)trackDb.shipDate);
                    System.DateTime shipDate = ConvertStrToDate(shipDate_s);
                    string serviceClass = reader.GetString((int)trackDb.serviceClass);
                    string orderNo_s = reader.GetString((int)trackDb.orderNo);

                    if (orderNo_s.Equals("")) orderNo_s = "0";
                    int orderNo = 0;
                    try
                    {
                        orderNo = System.Convert.ToInt32(orderNo_s);
                    }
                    catch (Exception e)
                    {
                        string reason = e.Message;
                        thingsToRead = reader.Read();
                        continue;
                    }
                    
                    decimal weight = reader.GetDecimal((int)trackDb.weight);
                    decimal charge = reader.GetDecimal((int)trackDb.charge);
                    string deleted = reader.GetString((int)trackDb.deleted);
                    if (deleted.Equals("Y"))
                    {
                        shipMgr.RemoveShipmentLine(packSlip, trackingNo);
                    }
                    else
                    {
                        shipMgr.AddShipmentLine(packSlip, trackingNo, shipDate,
                                    serviceClass, orderNo, weight, charge);
                    }
                    thingsToRead = reader.Read();
                }
            }
        }
    }
}