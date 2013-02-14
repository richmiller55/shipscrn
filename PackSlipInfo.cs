using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data.Odbc;

namespace ShipScrn
{
    class PackSlipInfo
    {
        Epicor.Mfg.Core.Session session;
        Epicor.Mfg.BO.CustShip custShipObj;
        Epicor.Mfg.BO.CustShipDataSet custShipDs;
        bool invoiced;
        int packNum;
        int CustNum;
        int OrderNum;
        bool orderFF;
        bool shipToLocation;
        bool packFound;
        bool packNeedsTracking;
        bool isBuyGroup;
        bool invoiceInABox;
        string orderShipVia;
        string customerTerms;
        string customerState;
        bool customerFF;
        bool isUPS;
        string newInvoices = string.Empty;
        public PackSlipInfo(Epicor.Mfg.Core.Session vanSession, int pack)
        {
            packFound = false;
            packNum = pack;
            session = vanSession;
            orderFF = false;
            customerState = "";
            packNeedsTracking = true;
            orderShipVia = "";
            shipToLocation = false;
            customerFF = false;
            invoiceInABox = true;
            InitCustShip();
            if (packFound)
            {
                GetOrderInfo();
                GetCustomerInfo();
            }
        }
        void InitCustShip()
        {
            custShipObj = new Epicor.Mfg.BO.CustShip(session.ConnectionPool);
            bool result = true;
            string message;
            try
            {
                custShipDs = custShipObj.GetByID(packNum);
                Epicor.Mfg.BO.CustShipDataSet.ShipHeadRow custShipRow;
                custShipRow = (Epicor.Mfg.BO.CustShipDataSet.ShipHeadRow)custShipDs.ShipHead.Rows[0];
                CustNum = custShipRow.CustNum;
                BuyGroup bgCheck = new BuyGroup(CustNum);
                this.IsBuyGroup = bgCheck.GetBuyGroupMember();
                string trackingNum = custShipRow.TrackingNumber;
                
                if (trackingNum.Length > 0)
                {
                    packNeedsTracking = false;
                }
                Epicor.Mfg.BO.CustShipDataSet.ShipDtlRow dtlRow;
                dtlRow = (Epicor.Mfg.BO.CustShipDataSet.ShipDtlRow)custShipDs.ShipDtl.Rows[0];
                this.OrderNum = dtlRow.OrderNum;
                invoiced = custShipRow.Invoiced;
                custShipDs.Dispose();
            }
            catch (Exception e)
            {
                // header did not post
                message = e.Message;
                result = false;
            }
            if (result)
            {
                packFound = true;
            }
        }
        private void PostDetailTrackingNumbers(string[] trackingSplit,Shipment ship)
        {
            Epicor.Mfg.BO.TrackingDtl trackDtlObj = new Epicor.Mfg.BO.TrackingDtl(session.ConnectionPool);
            string blank = "";
            foreach (string trackNumber in trackingSplit)
            {

                if (trackNumber.Equals(blank)) // .Equals(blank,StringComparison.Ordinal))
                {
                    continue;
                }
                Epicor.Mfg.BO.TrackingDtlDataSet ds = new
                    Epicor.Mfg.BO.TrackingDtlDataSet();
                trackDtlObj.GetNewTrackingDtl(ds, this.OrderNum);
                Epicor.Mfg.BO.TrackingDtlDataSet.TrackingDtlRow row =
                    (Epicor.Mfg.BO.TrackingDtlDataSet.TrackingDtlRow)ds.TrackingDtl.Rows[0];
                row.OrderNum = this.OrderNum;
                row.CaseNum = "";
                row.Company = "CA";
                row.PackNum = this.PackNum;
                row.TrackingNumber = trackNumber;
                Hashtable hashWeights = ship.GetWeights();
                row.Weight = (decimal)hashWeights[trackNumber];
                row.ShipmentType = "CUST";
                string message = "OK";

                try
                {
                    trackDtlObj.Update(ds);
                }
                catch (Exception e)
                {
                    // tracking no did not post
                    message = e.Message;
                }
            }
        }
        public void PostTrackingToPack(string trackingStr, decimal weight, Shipment ship)
        {
            string message;
            try
            {
                Epicor.Mfg.BO.CustShipDataSet.ShipHeadRow custShipRow;
                custShipRow = (Epicor.Mfg.BO.CustShipDataSet.ShipHeadRow)custShipDs.ShipHead.Rows[0];
                custShipRow.Weight = weight;
                string[] trackingSplit = trackingStr.Split(':');
                this.PostDetailTrackingNumbers(trackingSplit, ship);
                string tracking = trackingSplit[0];
                if (tracking.Length > 50)
                {
                    custShipRow.TrackingNumber = tracking.Substring(0, 49);
                }
                else
                {
                    custShipRow.TrackingNumber = tracking;
                }
                if (trackingStr.Length > 1000)
                {
                    custShipRow.Character01 = trackingStr.Substring(0, 999);
                }
                else
                {
                    custShipRow.Character01 = trackingStr;
                }
                try
                {
                    custShipObj.Update(custShipDs);
                }
                catch (Exception e)
                {
                    // tracking no did not post
                    message = e.Message;
                }
            }
            catch (Exception e)
            {
                // tracking no did not post
                message = e.Message;
            }
        }
        void GetOrderInfo()
        {
            string baseQuery = @" 
             SELECT 
	        oh.OrderNum as OrderNum,
                oh.freightFree as freightFree,
                oh.ShipViaCode as ShipViaCode,
                oh.ShipToNum as ShipToNum
                FROM orderHed as oh ";
          StringBuilder querystring = new StringBuilder();
          querystring.Append(baseQuery);
          querystring.AppendLine(" where OrderNum = " + OrderNum.ToString());
          string Dsn = GetMySqlDsn();
          // ArrayList al;
          using (OdbcConnection connection = new OdbcConnection(Dsn))
            {
                OdbcCommand command = new OdbcCommand(querystring.ToString(), connection);
                connection.Open();
                OdbcDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    OrderShipVia = reader["ShipViaCode"].ToString();
                    string ffFlag = reader["freightFree"].ToString();
                    if (ffFlag.Equals("1"))
                    {
                        OrderFF = true;
                    }
                    string shiptoNum = reader["ShipToNum"].ToString();
                    if (reader["ShipToNum"].Equals("1")) {
                        ShipToLocation = false;
                    }
                    else if (reader["ShipToNum"].Equals(""))
                    {
                        ShipToLocation = false;
                    }
                    else
                    {
                        ShipToLocation = true;
                    }
                }
                reader.Close();
            }
        }
        void GetCustomerInfo()
        {
            string baseQuery = @" 
             SELECT 
	           cm.CustNum as CustNum,
               cm.FreightTerms as FreightTerms,
               cm.State as state,
               cm.InvoiceInABox as InvoiceInABox
               FROM Customer as cm ";
          StringBuilder querystring = new StringBuilder();
          querystring.Append(baseQuery);
          querystring.AppendLine(" where CustNum = " + CustNum.ToString());
          string Dsn = GetMySqlDsn();
          using (OdbcConnection connection = new OdbcConnection(Dsn))
            {
              OdbcCommand command = new OdbcCommand(querystring.ToString(), connection);
                connection.Open();
                OdbcDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    if (reader["InvoiceInABox"].Equals(0)) InvoiceInABox = false;

                    customerTerms = reader["FreightTerms"].ToString();
                    if (customerTerms.CompareTo("FF") == 0)
                    {
                        CustomerFF = true;
                    }
                }
                reader.Close();
            }
        }
        void GetCustomerInfoVan()
        {
            Epicor.Mfg.BO.Customer customerObj;
            customerObj = new Epicor.Mfg.BO.Customer(session.ConnectionPool);
            Epicor.Mfg.BO.CustomerDataSet ds;
            ds = customerObj.GetByID(CustNum);
            Epicor.Mfg.BO.CustomerDataSet.CustomerRow row = (Epicor.Mfg.BO.CustomerDataSet.CustomerRow)ds.Customer.Rows[0];
            customerTerms = row.ShortChar01;
            if (customerTerms.CompareTo("FF") == 0)
            {
                customerFF = true;
            }
        }
        void GetOrderInfoVan()
        {
            Epicor.Mfg.BO.SalesOrder salesOrderObj;
            salesOrderObj = new Epicor.Mfg.BO.SalesOrder(session.ConnectionPool);
            Epicor.Mfg.BO.SalesOrderDataSet soDs = new Epicor.Mfg.BO.SalesOrderDataSet();
            
            string message;
            try
            {
                soDs = salesOrderObj.GetByID(OrderNum);
                Epicor.Mfg.BO.SalesOrderDataSet.OrderHedRow row = (Epicor.Mfg.BO.SalesOrderDataSet.OrderHedRow)soDs.OrderHed.Rows[0];
                orderFF = row.CheckBox03;
                orderShipVia = row.ShipViaCode;
                soDs.Dispose();
            }
            catch (Exception e)
            {
                // header did not post
                message = e.Message;
            }
        }
        public bool NeedsTracking
        {
            get
            {
                return packNeedsTracking;
            }
            set
            {
                packNeedsTracking = value;
            }
        }
        public string OrderShipVia
        {
            get
            {
                return orderShipVia;
            }
            set
            {
                orderShipVia = value;
            }
        }
        public bool CustomerFF
        {
            get
            {
                return customerFF;
            }
            set
            {
                customerFF = value;
            }
        }
        public bool Invoiced
        {
            get
            {
                return invoiced;
            }
            set
            {
                invoiced = value;
            }
        }
        public bool ShipToLocation
        {
            get
            {
                return shipToLocation;
            }
            set
            {
                shipToLocation = value;
            }
        }
        public bool InvoiceInABox
        {
            get
            {
                return invoiceInABox;
            }
            set
            {
                invoiceInABox = value;
            }
        }
        public bool IsPackFF()
        {
            bool packFF = false;
            if (orderFF || customerFF) packFF = true;
            return packFF;
        }
        public bool OrderFF
        {
            get
            {
                return orderFF;
            }
            set
            {
                orderFF = value;
            }
        }
        public bool IsFreightTaxed()
        {
            bool freightTaxed = false;
            if (CustomerState.Equals("TX")) freightTaxed = true;
            return freightTaxed;
        }
        public string CustomerState
        {
            get
            {
                return customerState;
            }
            set
            {
                customerState = value;
            }
        }

        public int PackNum
        {
            get
            {
                return packNum;
            }
            set
            {
                packNum = value;
            }
        }
        public bool IsUPS
        {
            get
            {
                return isUPS;
            }
            set
            {
                isUPS = value;
            }
        }
        public bool IsBuyGroup
        {
            get
            {
                return isBuyGroup;
            }
            set
            {
                isBuyGroup = value;
            }
        }
        private string GetMySqlDsn()
        {
            return "DSN=GC; HOST=gc.rlm5.com; DB=coinet_db1; UID=focus; PWD=focusgroup";
        }

    }
}
        