using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ShipScrn
{
    public partial class PackEntry : Form
    {
        VanAccess vanAccess;
        CustomerShip custship;
        Hashtable shipments;
        ICollection ShipKeys;
        DataTable dtTrack;
        IDictionaryEnumerator iter;
        Hashtable InvoicePacks;
        Shipment ship;
        AppStats stats;
        string filename;
        string crlf;
        string arBatchName;
        string arNoBg;
        string arBg;
        string arNoBg2;
        string arBg2;
        string arNoBg3;
        string arBg3;
        string arNoBg4;
        string arBg4;
        string ShipTo1, ShipTo2;
        string U1, U2, U3, U4;
        string currentUPSBatch;
        bool moreRecords;
        string processedInvoices;
        decimal handlingCharge = 2.50M;
        public PackEntry()
        {
            InitializeComponent();
            crlf = "\r\n";
            
            filename = string.Empty;
            InvoicePacks = new Hashtable();
            this.stats = new AppStats();
            setupDataTable();
            moreRecords = true;
            btnLogin.Click += new EventHandler(btnLogin_Click);
            btnRunTillChange.Click += new EventHandler(btnRunTillChange_Click);
            btnRunTillDone.Click += new EventHandler(btnRunTillDone_Click);
            btnRunTrackTable.Click += new EventHandler(btnRunTrackTable_Click);
        }
        void initArBatchNames()
        {
            // string baseDate = "080113";
            string baseDate = "";
            this.ARNoBg = "P" + baseDate + "1";
            this.ARBg = "P" + baseDate + "2";
            this.arNoBg2 = "P" + baseDate + "3";
            this.arBg2 = "P" + baseDate + "4";
            this.arNoBg3 = "P" + baseDate + "5";
            this.arBg3 = "P" + baseDate + "6";
            this.arNoBg4 = "P" + baseDate + "7";
            this.arBg4 = "P" + baseDate + "8";

            this.ShipTo1 = "S" + baseDate + "1";
            this.ShipTo2 = "S" + baseDate + "2";
            this.U1 = "U" + baseDate + "1"; 
            this.U2 = "U" + baseDate + "2";
            this.U3 = "U" + baseDate + "3";
            this.U4 = "U" + baseDate + "4";
            this.ARBatchName = this.ARNoBg;
            this.currentUPSBatch = this.U1;
            this.CreateARBatch(this.ARNoBg.ToString());
            this.CreateARBatch(this.ARBg.ToString());
            this.CreateARBatch(this.ShipTo1.ToString());
            this.CreateARBatch(this.U1.ToString());
        }
        void btnRunTillDone_Click(object sender, EventArgs e)
        {
            string user = "rich";
            string password = "homefed55";
            string database = "System";
            VanAccess va = new VanAccess(user, password, database);
            SetVanAccess(va);  // maybe do this on the VanAccess class

            // throw new Exception("The method or operation is not implemented.");
        }
        Hashtable InitBatchCount()
        {
            Hashtable batchCount = new Hashtable();
            batchCount.Add("buyGroup",0);
            batchCount.Add("notBuyingGroup",0);
            batchCount.Add("UPS", 0);
            batchCount.Add("ShipTo", 0);
            return batchCount;
        }
        void btnRunTillChange_Click(object sender, EventArgs e)
        {
            moreRecords = true;
            Hashtable batchCount = InitBatchCount();
            this.initArBatchNames();
            while (moreRecords)
            {
                moreRecords = iter.MoveNext();
                processRecord(batchCount);
            }
        }
        void processRecord(Hashtable batchCount)
        {
            try
            {
                this.ship = (Shipment)iter.Value;
            }
            catch (Exception e2)
            {
                string message = e2.Message;
                this.Refresh();
            }
            Epicor.Mfg.Core.Session session = this.vanAccess.getSession();
            int PackSlipNo = ship.GetPackSlipNo();

            PackSlipInfo info = new PackSlipInfo(session, PackSlipNo);
            if (info.Invoiced)
            {
                moreRecords = iter.MoveNext();
                return;
            }
            this.setARBatch(info, batchCount);

            setScreenVars((Shipment)iter.Value);
            this.Refresh();
            this.saveArBatchTextBoxValues();
            string freightMessage = "Frt Free";
            ARInvoice arInvoice = new ARInvoice(session, this.ARBatchName, PackSlipNo.ToString());
            string trackingNo = ship.GetTrackingNumbers();
            if (!info.IsPackFF() && ship.GetTotalCharge().CompareTo(0.0M) > 0)
            {
                string frtMiscCode = "1";
                string taxCodeID = "FREIGHT";
                if (info.IsFreightTaxed())
                {
                    frtMiscCode = "2";
                    taxCodeID = "PRODFRT";
                }
                decimal amount = ship.GetTotalCharge() + this.handlingCharge;
                arInvoice.GetNewInvcMisc(amount,
                        trackingNo,
                        frtMiscCode,
                        taxCodeID);
                freightMessage = "Bill Frt";
            }
            arInvoice.AddTrackingToInvcHead(PackSlipNo.ToString(), trackingNo);
            int InvoiceNo = arInvoice.GetInvoiceFromPack(PackSlipNo.ToString());
            processedInvoices += freightMessage + InvoiceNo.ToString();
            processedInvoices += " Pack " + PackSlipNo.ToString() + crlf;
            WriteBackTracking wb = new WriteBackTracking(PackSlipNo);
        }
        void setTrackingGrid()
        {
            dtTrack.Rows.Clear();
            string trackingNums = ship.GetTrackingNumbers();
            Hashtable weights = ship.GetWeights();
            Hashtable charges = ship.GetCharges();
            string[] trackArray = trackingNums.Split(':');
            foreach (string track in trackArray)
            {
                if (track.Length > 2)
                {
                    DataRow row = dtTrack.NewRow();
                    row["TrackNo"] = track;
                    row["Weight"] = weights[track];
                    row["Charge"] = charges[track];
                    dtTrack.Rows.Add(row);
                    this.stats.NTrackingProcessed += 1;
                }
            }
            this.stats.NPacksProcessed += 1;
        }
        void setupDataTable()
        {
            dtTrack = new DataTable("dtTrack");
            DataColumn dcTrackNo = new DataColumn("TrackNo");
            DataColumn dcWeight = new DataColumn("Weight");
            DataColumn dcCharge = new DataColumn("Charge");

            dcTrackNo.ColumnName = "TrackNo";
            dcWeight.ColumnName = "Weight";
            dcCharge.ColumnName = "Charge";

            dcTrackNo.DataType = System.Type.GetType("System.String");
            dcWeight.DataType = System.Type.GetType("System.Decimal");
            dcCharge.DataType = System.Type.GetType("System.Decimal");

            dcTrackNo.DefaultValue = "default string";
            dcWeight.DefaultValue = 0.0;
            dcCharge.DefaultValue = 0.0;

            dtTrack.Columns.Add(dcTrackNo);
            dtTrack.Columns.Add(dcWeight);
            dtTrack.Columns.Add(dcCharge);

            // maybe do this each record you read?
            // dig up the data writing code
            dgvTracking.DataSource = dtTrack;
        }
        void btnARBatch_Click(object sender, EventArgs e)
        {
            string arBatch = tbARBatch.Text;
            Epicor.Mfg.Core.Session session = this.vanAccess.getSession();
            InvoiceGrp invGrp = new InvoiceGrp(session, arBatch);
        }
        void btnSkip_Click(object sender, EventArgs e)
        {
            this.saveArBatchTextBoxValues();
            nextRecord(); 
        }
        void nextRecord()
        {
            moreRecords = iter.MoveNext();
            if (moreRecords)
            {
                this.ship = (Shipment)iter.Value;
                setScreenVars((Shipment)iter.Value);
                this.Refresh();
            }
        }
        void WriteTrackingToPack(PackSlipInfo info)
        {
            string trackingNo = ship.GetTrackingNumbers();
            decimal weight = ship.GetTotalWeight();
            info.PostTrackingToPack(trackingNo,weight,ship);
        }
        void saveArBatchTextBoxValues()
        {
            // this.ARBg = this.tbBGBatch.Text;
            // this.ARNoBg = this.tbARBatch.Text;
        }
        void CreateARBatch(string batchName)
        {
            //
        }
        void CreateARBatchOld(string batchName)
        {
            Epicor.Mfg.Core.Session session = this.vanAccess.getSession();
            Epicor.Mfg.BO.InvcGrp invcGrp = new Epicor.Mfg.BO.InvcGrp(session.ConnectionPool);
            try
            {
                Epicor.Mfg.BO.InvcGrpDataSet ds = invcGrp.GetByID(batchName);
                invcGrp.DeleteByID(batchName);
                invcGrp.Update(ds);
            }
                /*
            catch ( e)
            {
                string message = e.Message;
                lblInvoiceStatus.Text = "No Packs left to invoice:";
                allOK = false;
            } */

            catch (Exception e)
            {
                Epicor.Mfg.BO.InvcGrpDataSet ds = new Epicor.Mfg.BO.InvcGrpDataSet();
                // row.GroupID = batchName;
                invcGrp.GetNewInvcGrp(ds);
                Epicor.Mfg.BO.InvcGrpDataSet.InvcGrpRow row =
                    (Epicor.Mfg.BO.InvcGrpDataSet.InvcGrpRow)ds.InvcGrp.Rows[0];
                row.Company = "CA";
                row.GroupID = batchName;
                row.FiscalPeriod = 12;
                row.CreatedBy = "rich";
                row.FiscalYear = 2012;
                invcGrp.Update(ds);
                invcGrp.UnlockGroup(batchName, ds);
            }
        }
        void setARBatch(PackSlipInfo info,Hashtable batchCounts)
        {
            string trackingNums = ship.GetTrackingNumbers();
            string[] trackArray = trackingNums.Split(':');
            string trackingNum = trackArray[0];
            string prefix = trackingNum.Substring(0,3);
            if (prefix.Equals("1Z9"))
            {
                this.ARBatchName = this.currentUPSBatch;
                int upsCount = (int)batchCounts["UPS"];
                upsCount += 1;
                batchCounts["UPS"] = upsCount;
                if ((int)batchCounts["UPS"] >= 50)
                {
                    this.currentUPSBatch = this.U2;
                    this.CreateARBatch(this.U2.ToString());
                }
                else if ((int)batchCounts["UPS"] >= 100)
                {
                    this.CreateARBatch(this.U3.ToString());
                    this.currentUPSBatch = this.U3;
                }
                else if ((int)batchCounts["UPS"] >= 150)
                {
                    this.currentUPSBatch = this.U4;
                }
            }
            else
            {
                setARBatchIfBuyGrp(info,batchCounts);
            }
        }
        void setARBatchIfBuyGrp(PackSlipInfo info, Hashtable batchCounts)
        {
            if (info.IsBuyGroup)
            {
                int buyGroupCount = (int)batchCounts["buyGroup"];
                buyGroupCount += 1;
                batchCounts["buyGroup"] = buyGroupCount;
                this.ARBatchName = this.ARBg;
                if ((int)batchCounts["buyGroup"] > 150)
                {
                    this.ARBg = this.ARBg4;
                }
                else if ((int)batchCounts["buyGroup"] > 100)
                {
                    this.ARBg = this.ARBg3;
                }
                else if ((int)batchCounts["buyGroup"] > 50)
                {
                    this.ARBg = this.ARBg2;
                }
            }

            else if (info.ShipToLocation || !info.InvoiceInABox)
            {
                if ((int)batchCounts["ShipTo"] > 50)
                {
                    this.ShipTo1 = this.ShipTo2;
                    this.CreateARBatch(this.ShipTo2.ToString());
                }
                int shipToCount = (int)batchCounts["ShipTo"];
                shipToCount += 1;
                batchCounts["ShipTo"] = shipToCount;
                this.ARBatchName = this.ShipTo1;
            }
            else
            {
                this.ARBatchName = this.ARNoBg;
                int notBuyGroupCount = (int)batchCounts["notBuyingGroup"];
                notBuyGroupCount += 1;
                batchCounts["notBuyingGroup"] = notBuyGroupCount;
                if ((int)batchCounts["notBuyingGroup"] > 150)
                {
                    this.ARNoBg = this.ARNoBg4;
                }
                else if ((int)batchCounts["notBuyingGroup"] > 100)
                {
                    this.ARNoBg = this.ARNoBg3;
                }
                else if ((int)batchCounts["notBuyingGroup"] > 50)
                {
                    this.ARNoBg = this.ARNoBg2;
                }
            }
        }
        void setPackNumInfo()
        {
            Epicor.Mfg.Core.Session session = this.vanAccess.getSession();
            int packNum = ship.GetPackSlipNo();
            PackSlipInfo info = new PackSlipInfo(session, packNum);
            // this.setARBatch(info);
            
            // if (info.NeedsTracking)
            if (true)   // for code testing only
            {
                this.WriteTrackingToPack(info);
            }
            if (info.Invoiced)
            {
                lblInvoiceStatus.Text = "Already Invoiced";
                this.lblInvoiceStatus.ForeColor = System.Drawing.Color.Red;
                // nextRecord();
            }
            else
            {
                lblInvoiceStatus.Text = "Ready to Invoice";
                this.lblInvoiceStatus.ForeColor = System.Drawing.Color.Black;
            }
            if (info.CustomerFF)
            {
                lblInvoiceStatus.Text = "Customer Freight Free";
                this.lblInvoiceStatus.ForeColor = System.Drawing.Color.Red;
            }
            if (info.OrderFF)
            {
                lblInvoiceStatus.Text = "Order Freight Free";
                this.lblInvoiceStatus.ForeColor = System.Drawing.Color.Red;
            }
        }
        void setScreenVars(Shipment ship)
        {
            tbProcessedInvoices.Text = processedInvoices;
            tbARBatch.Text = this.ARNoBg;
            tbBGBatch.Text = this.ARBg;
            this.ship = ship;
            setFreightFileVars();
            setPackNumInfo();
            setTrackingGrid();
            // setScreenShipTo();
            displayStats();
        }
        void setFreightFileVars()
        {
            tbPackNo.Text = ship.GetPackSlipNo().ToString();
            decimal charge = ship.GetTotalCharge();

            tbCharge.Text = charge.ToString("N");

            tbWeight.Text = ship.GetTotalWeight().ToString("N");
            System.DateTime shipDate = ship.GetShipDate();
            tbShipDate.Text = shipDate.ToShortDateString();
            tbClassOfService.Text = ship.GetClassOfService();
            tbOrderNo.Text = ship.GetOrderNo().ToString();
        }
        void displayStats()
        {
            this.tbNpacksFile.Text = this.stats.NPacksFile.ToString();
            this.tbNtrackingNumFile.Text = this.stats.NTrackingNumbersFile.ToString();
            this.tbNpacksProcessed.Text = this.stats.NPacksProcessed.ToString();
            this.tbNtrackingNumProcessed.Text = this.stats.NTrackingProcessed.ToString();
        }
        void btnRunTrackTable_Click(object sender, EventArgs e)
        {
            processTracking();
        }
        void processTracking()
        {
            TrackingReader reader = new TrackingReader();
            ShipMgr trackData = reader.GetShipMgr();
            trackData.TotalShipments();
            stats.TotalFreightFile = trackData.TotalFreight;
            stats.TotalWeightFile = trackData.TotalWeight;
            stats.NPacksFile = trackData.Packs;
            stats.NTrackingNumbersFile = trackData.TrackingNumbers;

            shipments = trackData.GetShipmentsHash();
            ShipKeys = shipments.Keys;
            iter = shipments.GetEnumerator();
            bool allOK = true;
            try
            {
                moreRecords = iter.MoveNext();
            }
            catch (System.InvalidOperationException e)
            {
                string message = e.Message;
                lblInvoiceStatus.Text = "No Packs left to invoice:";
                allOK = false;
            }
            catch (Exception e)
            {
                string message = e.Message;
                lblInvoiceStatus.Text = "No Packs left to invoice:";
                allOK = false;
            }
            if (allOK)
            {
                setScreenVars((Shipment)iter.Value);
            }
        }
        void setScreenShipTo()
        {
            string packNoStr = tbPackNo.Text;
            int packNo = Convert.ToInt32(packNoStr);
            Epicor.Mfg.Core.Session vanSession = vanAccess.getSession();
            custship = new CustomerShip(vanSession, packNo);
            if (custship.PackExists)
            {
                tbName.Text = custship.Name;
                tbAddr1.Text = custship.Address1;
                tbAddr2.Text = custship.Address2;
                tbCity.Text = custship.City;
                cbFreightFreeCust.Checked = false;
                determineFFstatus();
            }
            else
            {
                lblInvoiceStatus.Text = "FedEx Pack not Found in Vantage";
                this.lblInvoiceStatus.ForeColor = System.Drawing.Color.Red;
            }
        }
        void determineFFstatus()
        {
            string frtTerms = custship.CustFrtTerms;
            if (frtTerms.Equals("FF", StringComparison.Ordinal))
            {
                cbFreightFreeCust.Checked = true;
                lblInvoiceStatus.Text = "Customer Freight Free";
                this.lblInvoiceStatus.ForeColor = System.Drawing.Color.Red;
            }
        }
        void btnLogin_Click(object sender, EventArgs e)
        {
            LoginDlg dlg = new LoginDlg(this);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                // enable the file open controls
                // string result = "testOk";
            }
        }
        public void SetVanAccess(VanAccess va)
        {
            vanAccess = va;
        }
        public string ARBatchName
        {
            get
            {
                return arBatchName;
            }
            set
            {
                arBatchName = value;
            }
        }
        public string ARBg
        {
            get
            {
                return arBg;
            }
            set
            {
                arBg = value;
            }
        }
        public string ARNoBg
        {
            get
            {
                return arNoBg;
            }
            set
            {
                arNoBg = value;
            }
        }
        public string ARBg2
        {
            get
            {
                return arBg2;
            }
            set
            {
                arBg2 = value;
            }
        }
        public string ARNoBg2
        {
            get
            {
                return arNoBg2;
            }
            set
            {
                arNoBg2 = value;
            }
        }
        public string ARBg3
        {
            get
            {
                return arBg3;
            }
            set
            {
                arBg3 = value;
            }
        }
        public string ARNoBg3
        {
            get
            {
                return arNoBg3;
            }
            set
            {
                arNoBg3 = value;
            }
        }
        public string ARBg4
        {
            get
            {
                return arBg4;
            }
            set
            {
                arBg4 = value;
            }
        }
        public string ARNoBg4
        {
            get
            {
                return arNoBg4;
            }
            set
            {
                arNoBg4 = value;
            }
        }

    }
}