using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace ShipScrn
{
    public enum col
    {
        trackingNo,
        packSlipNo,
        shipDate,
        serviceClass,
        orderNo,
        weight,
        charge,
        tranType
    }
    class FedExWriteBackReader
    {
        StreamReader tr;
        ShipMgr shipMgr;
        public FedExWriteBackReader(string fullPath)
        {
            shipMgr = new ShipMgr();
            tr = new StreamReader(fullPath);
            processFile();
        }
        public FedExWriteBackReader()
        {
            string year = DateTime.Now.Year.ToString();  
            string month = DateTime.Now.Month.ToString();
            string day = DateTime.Now.Day.ToString();
            if (month.Length == 1) { month = "0" + month; }
            if (day.Length == 1) { day = "0" + day; }
            string yyyymmdd = year + month + day;
            string file = "D:/users/rich/data/fedEx/writeback" + yyyymmdd + ".txt";
            shipMgr = new ShipMgr();
            tr = new StreamReader(file);
            processFile();
        }
        public ShipMgr GetShipMgr() { return shipMgr; }

        void processFile()
        {
            string linePre = "";
            
            while ((linePre = tr.ReadLine()) != null)
            {
                string line = linePre.Replace("\"", "");
                string[] split = line.Split(new Char[] { ',' });
                int result = split[0].CompareTo("");
                if (result == 0) continue;
                
                string packSlipStr = split[(int)col.packSlipNo];
                
                result = packSlipStr.CompareTo("po");
                if (result == 0) continue;

                result = packSlipStr.CompareTo("");
                if (result == 0) continue;
                int packSlip = Convert.ToInt32(packSlipStr);                                
                string trackingNo = split[(int)col.trackingNo];
                
                string shipDateStr = split[(int)col.shipDate];

                System.DateTime shipDate = convertStrToDate(shipDateStr);
                string serviceClass = split[(int)col.serviceClass];

                string orderStr = split[(int)col.orderNo];
                int orderNo = 0;
                if (orderStr.Length > 0)
                {
                    orderNo = Convert.ToInt32(orderStr);
                }
                string weightStr = split[(int)col.weight];
                decimal weight = Convert.ToDecimal(weightStr);

                string chargeStr = split[(int)col.charge];
                decimal charge = Convert.ToDecimal(chargeStr);
                decimal zero = 0.0M;
                result = charge.CompareTo(zero);
                // if (result == 0) continue;  // if collect then do not process

                string tranType = split[(int)col.tranType];
                if (tranType.CompareTo("N") == 0)
                {
                    shipMgr.AddShipmentLine(packSlip,trackingNo,shipDate,
                                serviceClass,orderNo,weight,charge);
                }
                else
                {
                    shipMgr.RemoveShipmentLine(packSlip,trackingNo);
                }
            }
        }
        public System.DateTime convertStrToDate(string dateStr)
        {
            string year = dateStr.Substring(0, 4);
            string month = dateStr.Substring(4, 2);
            string day = dateStr.Substring(6, 2);

            System.DateTime dateObj = new DateTime(Convert.ToInt32(year),
                Convert.ToInt32(month), Convert.ToInt32(day));
            return dateObj;
        }
    }
}



