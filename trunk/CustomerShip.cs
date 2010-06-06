using System;
using System.Collections.Generic;
using System.Text;

namespace ShipScrn
{
    class CustomerShip
    {
        Int32 packNo;
        Int32 CustomerNo;
        Epicor.Mfg.Core.Session session;
        Epicor.Mfg.BO.CustShip custShipObj;
        Epicor.Mfg.BO.CustShipDataSet custShipDs;
        Epicor.Mfg.BO.CustShipDataSet.ShipHeadRow shipHeadRow;
        bool packExists;
        string shipToName;
        string shipToAddress1;
        string shipToAddress2;
        string shipToCity;
        string shipToState;
        string shipToPostalCode;
        string shipToCountry;
        string customerId;
        string custFrtTerms;
        
        public CustomerShip(Epicor.Mfg.Core.Session vanSession,Int32 vanPackNo)
        {
            this.session = vanSession;
            packNo = vanPackNo;
            init();
        }
        void init()
        {
            custShipObj = new Epicor.Mfg.BO.CustShip(session.ConnectionPool);
            this.packExists = true;
            try
            {
                custShipDs = custShipObj.GetByID(packNo);
            }
            catch (Exception e)
            {
                this.packExists = false;
            }
            if (this.packExists)
            {
                this.shipHeadRow = (Epicor.Mfg.BO.CustShipDataSet.ShipHeadRow)custShipDs.ShipHead.Rows[0];
                splitAddress();
            }
        }
        enum addrListCol5
        {
            name,
            address1,
            address2,
            cityStateZip,
            country
        }
        enum addrListCol4
        {
            name,
            address1,
            cityStateZip,
            country
        }
        public bool PackExists
        {
            get
            {
                return packExists;
            }
            set
            {
                packExists = value;
            }
        }
        public string Name
        {
            get
            {
                return shipToName;
            }
            set
            {
                shipToName = value;
            }
        }
        public string Address1
        {
            get
            {
                return shipToAddress1;
            }
            set
            {
                shipToAddress1 = value;
            }
        }
        public string Address2
        {
            get
            {
                return shipToAddress2;
            }
            set
            {
                shipToAddress2 = value;
            }
        }
        public string City
        {
            get
            {
                return shipToCity;
            }
            set
            {
                shipToCity = value;
            }
        }
        public string CustFrtTerms
        {
            get
            {
                return custFrtTerms;
            }
            set
            {
                custFrtTerms = value;
            }
        }
        public string CustomerId
        {
            get 
            {
                return customerId;
            }
            set
            {
                customerId = value;
            }
        }

        void splitAddress()
        {
            string addrList = shipHeadRow.AddrList;
            string[] addressAry = addrList.Split(new Char[] { '~' });
            if (addressAry.GetLength(0) == 5) 
            {
                Name = addressAry[(int)addrListCol5.name];
                Address1 = addressAry[(int)addrListCol5.address1];
                Address2 = addressAry[(int)addrListCol5.address2];
                string cityStZip = addressAry[(int)addrListCol5.cityStateZip];
                City = cityStZip;
                shipToCountry = addressAry[(int)addrListCol5.country];
            }
            else
            {
                Name = addressAry[(int)addrListCol4.name];
                Address1 = addressAry[(int)addrListCol4.address1];
                string cityStZip = addressAry[(int)addrListCol4.cityStateZip];
                City = cityStZip;
                shipToCountry = addressAry[(int)addrListCol4.country];
            }
            this.CustomerNo = shipHeadRow.CustNum;
            this.CustomerId = shipHeadRow.CustomerCustID;
            getCustomerInfo();
        }
        void getCustomerInfo()
        {
            Epicor.Mfg.BO.Customer customerObj;
            Epicor.Mfg.BO.CustomerDataSet custDs;
            session = new Epicor.Mfg.Core.Session("rich", "homefed55", "AppServerDC://VantageDB1:8301", Epicor.Mfg.Core.Session.LicenseType.Default);
            customerObj = new Epicor.Mfg.BO.Customer(session.ConnectionPool);
            string message = "OK";
            bool okFound = true;
            try
            {
                custDs = customerObj.GetByID(this.CustomerNo);
                Epicor.Mfg.BO.CustomerDataSet.CustomerRow custRow = (Epicor.Mfg.BO.CustomerDataSet.CustomerRow)custDs.Customer.Rows[0];
                this.CustFrtTerms = custRow.ShortChar01;
            }
            catch (Exception e)
            {
                message = e.Message;
                okFound = false;
            }
        }
    }
}
