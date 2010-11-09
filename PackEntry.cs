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
        bool moreRecords;
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
        }
        void initArBatchNames()
        {
            this.ARBg = "BG";
            this.ARNoBg = "NoBG";
        }
        void btnRunTillDone_Click(object sender, EventArgs e)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void btnRunTillChange_Click(object sender, EventArgs e)
        {
            throw new Exception("The method or operation is not implemented.");
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
            ARInvoice arInvoice = new ARInvoice(session, this.ARBatchName, tbPackNo.Text);
            string trackingNo = ship.GetTrackingNumbers();
            arInvoice.AddTrackingToInvcHead(tbPackNo.Text,trackingNo);
            int InvoiceNo = arInvoice.GetInvoiceFromPack(tbPackNo.Text);
            processedInvoices += "Invoiced Prior " + InvoiceNo.ToString();
            processedInvoices += " Pack " + tbPackNo.Text + crlf;
            nextRecord();
        }
        void btnInvoice_Click(object sender, EventArgs e)
        {
            this.saveArBatchTextBoxValues();
            Epicor.Mfg.Core.Session session = this.vanAccess.getSession();
            ARInvoice arInvoice = new ARInvoice(session, this.ARBatchName, tbPackNo.Text);
            string trackingNo = ship.GetTrackingNumbers();
            
            if (ship.GetTotalCharge().CompareTo(0.0M) > 0)
            {
                decimal amount = ship.GetTotalCharge() + this.handlingCharge;
                arInvoice.GetNewInvcMisc(amount, trackingNo);
            }
            arInvoice.AddTrackingToInvcHead(tbPackNo.Text,trackingNo);
            int InvoiceNo = arInvoice.GetInvoiceFromPack(tbPackNo.Text);
            processedInvoices += "Invoiced Now " + InvoiceNo.ToString();
            processedInvoices += " Pack " + tbPackNo.Text + crlf;
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
        void setARBatchIfBuyGrp(PackSlipInfo info)
        {
            if (info.IsBuyGroup)
            {
                this.ARBatchName = this.ARBg;
            }
            else
            {
                this.ARBatchName = this.ARNoBg;
            }
        }
        void setPackNumInfo()
        {
            Epicor.Mfg.Core.Session session = this.vanAccess.getSession();
            int packNum = ship.GetPackSlipNo();
            PackSlipInfo info = new PackSlipInfo(session, packNum);
            this.setARBatchIfBuyGrp(info);

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
            setScreenShipTo();
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


    }
}