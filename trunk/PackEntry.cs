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
        PackSlipInfo info;
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

        string U1, U2, U3, U4;
        string currentUPSBatch;
        bool moreRecords;
        int upsInvCount = 0;
        string processedInvoices;
        decimal handlingCharge = 2.50M;
        public PackEntry()
        {
            InitializeComponent();
            crlf = "\r\n";
            this.initArBatchNames();
            filename = string.Empty;
            InvoicePacks = new Hashtable();
            this.stats = new AppStats();
            setupDataTable();
            moreRecords = true;
            btnLogin.Click += new EventHandler(btnLogin_Click);
            tbPackNo.LostFocus += new EventHandler(tbPackNo_LostFocus);
            btnFedExFileOpen.Click += new EventHandler(btnFedExFileOpen_Click);
            btnInvoice.Click += new EventHandler(btnInvoice_Click);
            btnTrackOnly.Click += new EventHandler(btnTrackOnly_Click);
            btnSkip.Click += new EventHandler(btnSkip_Click);
            btnRunTillChange.Click += new EventHandler(btnRunTillChange_Click);
            btnRunTillDone.Click += new EventHandler(btnRunTillDone_Click);
            btnRunTrackTable.Click += new EventHandler(btnRunTrackTable_Click);
        }
        void initArBatchNames()
        {
            this.ARNoBg = "P1031121";
            this.ARBg   = "P1031122";
            this.ARNoBg2 = "P1031123";
            this.ARBg2   = "P1031124";

            this.U1 = "U1031121";
            this.U2 = "U1031122";
            this.U3 = "U1031123";
            this.U4 = "U1031124";
            this.currentUPSBatch = this.U1;
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
            return batchCount;
        }
        void btnRunTillChange_Click(object sender, EventArgs e)
        {
            moreRecords = true;
            Hashtable batchCount = InitBatchCount();    
            while (moreRecords)
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
                setScreenVars((Shipment)iter.Value);
                this.Refresh();
                this.saveArBatchTextBoxValues();
                Epicor.Mfg.Core.Session session = this.vanAccess.getSession();
                int PackSlipNo = ship.GetPackSlipNo();
                PackSlipInfo info = new PackSlipInfo(session, PackSlipNo);
                this.setARBatch(info,batchCount);
                string freightMessage = "Frt Free";
                ARInvoice arInvoice = new ARInvoice(session, this.ARBatchName, PackSlipNo.ToString());
                string trackingNo = ship.GetTrackingNumbers();
                if (!info.IsPackFF() && ship.GetTotalCharge().CompareTo(0.0M) > 0)
                {
                    decimal amount = ship.GetTotalCharge() + this.handlingCharge;
                    arInvoice.GetNewInvcMisc(amount, trackingNo);
                    freightMessage = "Bill Frt";
                }
                arInvoice.AddTrackingToInvcHead(PackSlipNo.ToString(), trackingNo);
                int InvoiceNo = arInvoice.GetInvoiceFromPack(PackSlipNo.ToString());
                processedInvoices += freightMessage + InvoiceNo.ToString();
                processedInvoices += " Pack " + PackSlipNo.ToString() + crlf;
                WriteBackTracking wb = new WriteBackTracking(PackSlipNo);
                moreRecords = iter.MoveNext();
            }
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
        void btnTrackOnly_Click(object sender, EventArgs e)
        {
            this.saveArBatchTextBoxValues();
            Epicor.Mfg.Core.Session session = this.vanAccess.getSession();
            int PackSlipNo = this.ship.GetPackSlipNo();
            ARInvoice arInvoice = new ARInvoice(session, this.ARBatchName, PackSlipNo.ToString());
            string trackingNo = ship.GetTrackingNumbers();
            arInvoice.AddTrackingToInvcHead(tbPackNo.Text,trackingNo);
            int InvoiceNo = arInvoice.GetInvoiceFromPack(PackSlipNo.ToString());
            processedInvoices += "Invoiced No Frt " + InvoiceNo.ToString();
            processedInvoices += " Pack " + PackSlipNo.ToString() + crlf;
            WriteBackTracking wb = new WriteBackTracking(PackSlipNo);
            nextRecord();
        }
        void btnInvoice_Click(object sender, EventArgs e)
        {
            this.saveArBatchTextBoxValues();
            Epicor.Mfg.Core.Session session = this.vanAccess.getSession();
            int PackSlipNo = this.ship.GetPackSlipNo();
            ARInvoice arInvoice = new ARInvoice(session, this.ARBatchName, PackSlipNo.ToString());
            string trackingNo = ship.GetTrackingNumbers();
            
            if (ship.GetTotalCharge().CompareTo(0.0M) > 0)
            {
                decimal amount = ship.GetTotalCharge() + this.handlingCharge;
                arInvoice.GetNewInvcMisc(amount, trackingNo);
            }
            arInvoice.AddTrackingToInvcHead(tbPackNo.Text,trackingNo);
            int InvoiceNo = arInvoice.GetInvoiceFromPack(PackSlipNo.ToString());
            processedInvoices += "Invoiced Now " + InvoiceNo.ToString();
            processedInvoices += " Pack " + PackSlipNo.ToString() + crlf;
            WriteBackTracking wb = new WriteBackTracking(PackSlipNo);
            nextRecord();
        }
        void WriteTrackingToPack(PackSlipInfo info)
        {
            string trackingNo = ship.GetTrackingNumbers();
            decimal weight = ship.GetTotalWeight();
            info.PostTrackingToPack(trackingNo,weight,ship);
        }
        void saveArBatchTextBoxValues()
        {
            this.ARBg = this.tbBGBatch.Text;
            this.ARNoBg = this.tbARBatch.Text;
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
                }
                else if ((int)batchCounts["UPS"] >= 100)
                {
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
        void setARBatchIfBuyGrp(PackSlipInfo info,Hashtable batchCounts)
        {
            if (info.IsBuyGroup)
            {
                int buyGroupCount = (int)batchCounts["buyGroup"];
                buyGroupCount += 1;
                batchCounts["buyGroup"] = buyGroupCount;
                this.ARBatchName = this.ARBg;
                if ((int)batchCounts["buyGroup"] > 50) this.ARBg = this.ARBg2;
            }
            else
            {
                this.ARBatchName = this.ARNoBg;
                int notBuyGroupCount = (int)batchCounts["notBuyingGroup"];
                notBuyGroupCount += 1;
                batchCounts["notBuyingGroup"] = notBuyGroupCount;
                if ((int)batchCounts["notBuyingGroup"] > 50) this.ARNoBg = this.ARNoBg2;
            }
        }
        void setPackNumInfo()
        {
            Epicor.Mfg.Core.Session session = this.vanAccess.getSession();
            int packNum = ship.GetPackSlipNo();
            PackSlipInfo info = new PackSlipInfo(session, packNum);
            // this.setARBatch(info);

            this.btnInvoice.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnTrackOnly.BackColor = System.Drawing.SystemColors.ButtonFace;
            // if (info.NeedsTracking)
            if (true)   // for code testing only
            {
                this.WriteTrackingToPack(info);
            }
            if (info.Invoiced)
            {
                btnInvoice.Enabled = false;
                lblInvoiceStatus.Text = "Already Invoiced";
                this.lblInvoiceStatus.ForeColor = System.Drawing.Color.Red;
                this.btnInvoice.BackColor = System.Drawing.Color.Black;
                // nextRecord();
            }
            else
            {
                btnInvoice.Enabled = true;
                lblInvoiceStatus.Text = "Ready to Invoice";
                this.lblInvoiceStatus.ForeColor = System.Drawing.Color.Black;
                this.btnInvoice.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            }
            if (info.CustomerFF)
            {
                btnInvoice.Enabled = false;
                lblInvoiceStatus.Text = "Customer Freight Free";
                this.lblInvoiceStatus.ForeColor = System.Drawing.Color.Red;
                this.btnInvoice.BackColor = System.Drawing.Color.Black;
            }
            if (info.OrderFF)
            {
                btnInvoice.Enabled = false;
                lblInvoiceStatus.Text = "Order Freight Free";
                this.lblInvoiceStatus.ForeColor = System.Drawing.Color.Red;
                this.btnInvoice.BackColor = System.Drawing.Color.Black;
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

        void processFedExFile()
        {
            FedExWriteBackReader reader = new FedExWriteBackReader(filename);
            ShipMgr fedExData = reader.GetShipMgr();
            fedExData.TotalShipments();
            stats.TotalFreightFile = fedExData.TotalFreight;
            stats.TotalWeightFile = fedExData.TotalWeight;
            stats.NPacksFile = fedExData.Packs;
            stats.NTrackingNumbersFile = fedExData.TrackingNumbers;

            shipments = fedExData.GetShipmentsHash();
            ShipKeys = shipments.Keys;
            iter = shipments.GetEnumerator();
            moreRecords = iter.MoveNext();
            setScreenVars((Shipment)iter.Value);
        }
        void btnFedExFileOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Text Files (*.txt)|*.txt";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                for (int i = 0; i < openFile.FileNames.Length; i++)
                {
                    filename = Path.GetFullPath(openFile.FileNames[i].ToString());
                    tbFedExFile.Text = filename;
                }
            }
            processFedExFile();
        }
        // void btnRunTrackTable
        void tbPackNo_LostFocus(object sender, EventArgs e)
        {
            
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
                this.btnInvoice.BackColor = System.Drawing.Color.Black;
                this.btnTrackOnly.BackColor = System.Drawing.Color.Black;
            }
        }
        void determineFFstatus()
        {
            string frtTerms = custship.CustFrtTerms;
            if (frtTerms.Equals("FF", StringComparison.Ordinal))
            {
                cbFreightFreeCust.Checked = true;
                btnInvoice.Enabled = false;
                lblInvoiceStatus.Text = "Customer Freight Free";
                this.lblInvoiceStatus.ForeColor = System.Drawing.Color.Red;
                this.btnInvoice.BackColor = System.Drawing.Color.Black;
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

    }
}