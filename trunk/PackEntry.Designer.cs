namespace ShipScrn
{
    partial class PackEntry
    {

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbPackNo = new System.Windows.Forms.TextBox();
            this.lblPackNo = new System.Windows.Forms.Label();
            this.btnLogin = new System.Windows.Forms.Button();
            this.tbName = new System.Windows.Forms.TextBox();
            this.gbShipAdr = new System.Windows.Forms.GroupBox();
            this.cbFreightFreeCust = new System.Windows.Forms.CheckBox();
            this.lblPostalCode = new System.Windows.Forms.Label();
            this.tbPostalCode = new System.Windows.Forms.TextBox();
            this.lblState = new System.Windows.Forms.Label();
            this.tbState = new System.Windows.Forms.TextBox();
            this.lblCity = new System.Windows.Forms.Label();
            this.tbCity = new System.Windows.Forms.TextBox();
            this.lblAddr2 = new System.Windows.Forms.Label();
            this.tbAddr2 = new System.Windows.Forms.TextBox();
            this.lblAddr1 = new System.Windows.Forms.Label();
            this.tbAddr1 = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.tbOrderNo = new System.Windows.Forms.TextBox();
            this.lbOrderNo = new System.Windows.Forms.Label();
            this.tbShipDate = new System.Windows.Forms.TextBox();
            this.lbShipDate = new System.Windows.Forms.Label();
            this.lbClassOfService = new System.Windows.Forms.Label();
            this.lbWeight = new System.Windows.Forms.Label();
            this.lbFrtCharge = new System.Windows.Forms.Label();
            this.tbClassOfService = new System.Windows.Forms.TextBox();
            this.tbWeight = new System.Windows.Forms.TextBox();
            this.tbCharge = new System.Windows.Forms.TextBox();
            this.gbTrackingNos = new System.Windows.Forms.GroupBox();
            this.dgvTracking = new System.Windows.Forms.DataGridView();
            this.tbARBatch = new System.Windows.Forms.TextBox();
            this.tbProcessedInvoices = new System.Windows.Forms.TextBox();
            this.lbInvoiceGroup = new System.Windows.Forms.Label();
            this.grpAction = new System.Windows.Forms.GroupBox();
            this.btnRunTrackTable = new System.Windows.Forms.Button();
            this.btnRunTillDone = new System.Windows.Forms.Button();
            this.btnRunTillChange = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblInvoiceStatus = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.gbStatistics = new System.Windows.Forms.GroupBox();
            this.lbTracking = new System.Windows.Forms.Label();
            this.lbPacksStats = new System.Windows.Forms.Label();
            this.tbNtrackingNumProcessed = new System.Windows.Forms.TextBox();
            this.tbNpacksProcessed = new System.Windows.Forms.TextBox();
            this.tbNtrackingNumFile = new System.Windows.Forms.TextBox();
            this.tbNpacksFile = new System.Windows.Forms.TextBox();
            this.tbBGBatch = new System.Windows.Forms.TextBox();
            this.lbBGBatch = new System.Windows.Forms.Label();
            this.gbArBatches = new System.Windows.Forms.GroupBox();
            this.btnBatchCheck = new System.Windows.Forms.Button();
            this.cbMainBatchPrefix = new System.Windows.Forms.ComboBox();
            this.cbPrintPrefix = new System.Windows.Forms.ComboBox();
            this.cbLuxPrefix = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gbShipAdr.SuspendLayout();
            this.gbTrackingNos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTracking)).BeginInit();
            this.grpAction.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.gbStatistics.SuspendLayout();
            this.gbArBatches.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbPackNo
            // 
            this.tbPackNo.Location = new System.Drawing.Point(92, 19);
            this.tbPackNo.Name = "tbPackNo";
            this.tbPackNo.Size = new System.Drawing.Size(100, 20);
            this.tbPackNo.TabIndex = 2;
            // 
            // lblPackNo
            // 
            this.lblPackNo.AutoSize = true;
            this.lblPackNo.Location = new System.Drawing.Point(6, 19);
            this.lblPackNo.Name = "lblPackNo";
            this.lblPackNo.Size = new System.Drawing.Size(72, 13);
            this.lblPackNo.TabIndex = 3;
            this.lblPackNo.Text = "Pack Number";
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(403, 244);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(126, 22);
            this.btnLogin.TabIndex = 1;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            // 
            // tbName
            // 
            this.tbName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbName.Location = new System.Drawing.Point(106, 19);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(188, 20);
            this.tbName.TabIndex = 4;
            // 
            // gbShipAdr
            // 
            this.gbShipAdr.Controls.Add(this.cbFreightFreeCust);
            this.gbShipAdr.Controls.Add(this.lblPostalCode);
            this.gbShipAdr.Controls.Add(this.tbPostalCode);
            this.gbShipAdr.Controls.Add(this.lblState);
            this.gbShipAdr.Controls.Add(this.tbState);
            this.gbShipAdr.Controls.Add(this.lblCity);
            this.gbShipAdr.Controls.Add(this.tbCity);
            this.gbShipAdr.Controls.Add(this.lblAddr2);
            this.gbShipAdr.Controls.Add(this.tbAddr2);
            this.gbShipAdr.Controls.Add(this.lblAddr1);
            this.gbShipAdr.Controls.Add(this.tbAddr1);
            this.gbShipAdr.Controls.Add(this.lblName);
            this.gbShipAdr.Controls.Add(this.tbName);
            this.gbShipAdr.Location = new System.Drawing.Point(15, 331);
            this.gbShipAdr.Name = "gbShipAdr";
            this.gbShipAdr.Size = new System.Drawing.Size(335, 213);
            this.gbShipAdr.TabIndex = 5;
            this.gbShipAdr.TabStop = false;
            this.gbShipAdr.Text = "Shipping Address";
            // 
            // cbFreightFreeCust
            // 
            this.cbFreightFreeCust.AutoSize = true;
            this.cbFreightFreeCust.Location = new System.Drawing.Point(108, 154);
            this.cbFreightFreeCust.Name = "cbFreightFreeCust";
            this.cbFreightFreeCust.Size = new System.Drawing.Size(129, 17);
            this.cbFreightFreeCust.TabIndex = 17;
            this.cbFreightFreeCust.Text = "Freight Free Customer";
            this.cbFreightFreeCust.UseVisualStyleBackColor = true;
            // 
            // lblPostalCode
            // 
            this.lblPostalCode.AutoSize = true;
            this.lblPostalCode.Location = new System.Drawing.Point(184, 130);
            this.lblPostalCode.Name = "lblPostalCode";
            this.lblPostalCode.Size = new System.Drawing.Size(22, 13);
            this.lblPostalCode.TabIndex = 15;
            this.lblPostalCode.Text = "Zip";
            // 
            // tbPostalCode
            // 
            this.tbPostalCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbPostalCode.Location = new System.Drawing.Point(225, 126);
            this.tbPostalCode.Name = "tbPostalCode";
            this.tbPostalCode.Size = new System.Drawing.Size(69, 20);
            this.tbPostalCode.TabIndex = 14;
            // 
            // lblState
            // 
            this.lblState.AutoSize = true;
            this.lblState.Location = new System.Drawing.Point(19, 129);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(32, 13);
            this.lblState.TabIndex = 13;
            this.lblState.Text = "State";
            // 
            // tbState
            // 
            this.tbState.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbState.Location = new System.Drawing.Point(106, 126);
            this.tbState.Name = "tbState";
            this.tbState.Size = new System.Drawing.Size(57, 20);
            this.tbState.TabIndex = 12;
            // 
            // lblCity
            // 
            this.lblCity.AutoSize = true;
            this.lblCity.Location = new System.Drawing.Point(19, 102);
            this.lblCity.Name = "lblCity";
            this.lblCity.Size = new System.Drawing.Size(24, 13);
            this.lblCity.TabIndex = 11;
            this.lblCity.Text = "City";
            // 
            // tbCity
            // 
            this.tbCity.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbCity.Location = new System.Drawing.Point(106, 99);
            this.tbCity.Name = "tbCity";
            this.tbCity.Size = new System.Drawing.Size(188, 20);
            this.tbCity.TabIndex = 10;
            // 
            // lblAddr2
            // 
            this.lblAddr2.AutoSize = true;
            this.lblAddr2.Location = new System.Drawing.Point(18, 75);
            this.lblAddr2.Name = "lblAddr2";
            this.lblAddr2.Size = new System.Drawing.Size(54, 13);
            this.lblAddr2.TabIndex = 9;
            this.lblAddr2.Text = "Address 2";
            // 
            // tbAddr2
            // 
            this.tbAddr2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbAddr2.Location = new System.Drawing.Point(106, 72);
            this.tbAddr2.Name = "tbAddr2";
            this.tbAddr2.Size = new System.Drawing.Size(188, 20);
            this.tbAddr2.TabIndex = 8;
            // 
            // lblAddr1
            // 
            this.lblAddr1.AutoSize = true;
            this.lblAddr1.Location = new System.Drawing.Point(19, 48);
            this.lblAddr1.Name = "lblAddr1";
            this.lblAddr1.Size = new System.Drawing.Size(54, 13);
            this.lblAddr1.TabIndex = 7;
            this.lblAddr1.Text = "Address 1";
            // 
            // tbAddr1
            // 
            this.tbAddr1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbAddr1.Location = new System.Drawing.Point(106, 46);
            this.tbAddr1.Name = "tbAddr1";
            this.tbAddr1.Size = new System.Drawing.Size(188, 20);
            this.tbAddr1.TabIndex = 6;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(19, 26);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(35, 13);
            this.lblName.TabIndex = 5;
            this.lblName.Text = "Name";
            // 
            // tbOrderNo
            // 
            this.tbOrderNo.Location = new System.Drawing.Point(92, 45);
            this.tbOrderNo.Name = "tbOrderNo";
            this.tbOrderNo.Size = new System.Drawing.Size(100, 20);
            this.tbOrderNo.TabIndex = 11;
            // 
            // lbOrderNo
            // 
            this.lbOrderNo.AutoSize = true;
            this.lbOrderNo.Location = new System.Drawing.Point(5, 43);
            this.lbOrderNo.Name = "lbOrderNo";
            this.lbOrderNo.Size = new System.Drawing.Size(58, 13);
            this.lbOrderNo.TabIndex = 13;
            this.lbOrderNo.Text = "Order Num";
            // 
            // tbShipDate
            // 
            this.tbShipDate.Location = new System.Drawing.Point(92, 71);
            this.tbShipDate.Name = "tbShipDate";
            this.tbShipDate.Size = new System.Drawing.Size(100, 20);
            this.tbShipDate.TabIndex = 14;
            // 
            // lbShipDate
            // 
            this.lbShipDate.AutoSize = true;
            this.lbShipDate.Location = new System.Drawing.Point(5, 71);
            this.lbShipDate.Name = "lbShipDate";
            this.lbShipDate.Size = new System.Drawing.Size(54, 13);
            this.lbShipDate.TabIndex = 15;
            this.lbShipDate.Text = "Ship Date";
            // 
            // lbClassOfService
            // 
            this.lbClassOfService.AutoSize = true;
            this.lbClassOfService.Location = new System.Drawing.Point(198, 21);
            this.lbClassOfService.Name = "lbClassOfService";
            this.lbClassOfService.Size = new System.Drawing.Size(32, 13);
            this.lbClassOfService.TabIndex = 16;
            this.lbClassOfService.Text = "Class";
            // 
            // lbWeight
            // 
            this.lbWeight.AutoSize = true;
            this.lbWeight.Location = new System.Drawing.Point(198, 45);
            this.lbWeight.Name = "lbWeight";
            this.lbWeight.Size = new System.Drawing.Size(41, 13);
            this.lbWeight.TabIndex = 17;
            this.lbWeight.Text = "Weight";
            // 
            // lbFrtCharge
            // 
            this.lbFrtCharge.AutoSize = true;
            this.lbFrtCharge.Location = new System.Drawing.Point(198, 74);
            this.lbFrtCharge.Name = "lbFrtCharge";
            this.lbFrtCharge.Size = new System.Drawing.Size(41, 13);
            this.lbFrtCharge.TabIndex = 18;
            this.lbFrtCharge.Text = "Charge";
            // 
            // tbClassOfService
            // 
            this.tbClassOfService.Location = new System.Drawing.Point(254, 18);
            this.tbClassOfService.Name = "tbClassOfService";
            this.tbClassOfService.Size = new System.Drawing.Size(100, 20);
            this.tbClassOfService.TabIndex = 19;
            // 
            // tbWeight
            // 
            this.tbWeight.Location = new System.Drawing.Point(254, 45);
            this.tbWeight.Name = "tbWeight";
            this.tbWeight.Size = new System.Drawing.Size(100, 20);
            this.tbWeight.TabIndex = 20;
            // 
            // tbCharge
            // 
            this.tbCharge.Location = new System.Drawing.Point(254, 71);
            this.tbCharge.Name = "tbCharge";
            this.tbCharge.Size = new System.Drawing.Size(100, 20);
            this.tbCharge.TabIndex = 21;
            // 
            // gbTrackingNos
            // 
            this.gbTrackingNos.Controls.Add(this.dgvTracking);
            this.gbTrackingNos.Location = new System.Drawing.Point(15, 192);
            this.gbTrackingNos.Name = "gbTrackingNos";
            this.gbTrackingNos.Size = new System.Drawing.Size(366, 133);
            this.gbTrackingNos.TabIndex = 26;
            this.gbTrackingNos.TabStop = false;
            this.gbTrackingNos.Text = "Tracking Numbers";
            // 
            // dgvTracking
            // 
            this.dgvTracking.AllowUserToAddRows = false;
            this.dgvTracking.AllowUserToDeleteRows = false;
            this.dgvTracking.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTracking.Location = new System.Drawing.Point(16, 19);
            this.dgvTracking.Name = "dgvTracking";
            this.dgvTracking.ReadOnly = true;
            this.dgvTracking.Size = new System.Drawing.Size(336, 108);
            this.dgvTracking.TabIndex = 0;
            // 
            // tbARBatch
            // 
            this.tbARBatch.Location = new System.Drawing.Point(403, 350);
            this.tbARBatch.Name = "tbARBatch";
            this.tbARBatch.Size = new System.Drawing.Size(100, 20);
            this.tbARBatch.TabIndex = 27;
            // 
            // tbProcessedInvoices
            // 
            this.tbProcessedInvoices.Location = new System.Drawing.Point(18, 19);
            this.tbProcessedInvoices.MaxLength = 0;
            this.tbProcessedInvoices.Multiline = true;
            this.tbProcessedInvoices.Name = "tbProcessedInvoices";
            this.tbProcessedInvoices.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbProcessedInvoices.Size = new System.Drawing.Size(267, 286);
            this.tbProcessedInvoices.TabIndex = 29;
            // 
            // lbInvoiceGroup
            // 
            this.lbInvoiceGroup.AutoSize = true;
            this.lbInvoiceGroup.Location = new System.Drawing.Point(15, 21);
            this.lbInvoiceGroup.Name = "lbInvoiceGroup";
            this.lbInvoiceGroup.Size = new System.Drawing.Size(59, 13);
            this.lbInvoiceGroup.TabIndex = 30;
            this.lbInvoiceGroup.Text = "Main Prefix";
            // 
            // grpAction
            // 
            this.grpAction.Controls.Add(this.btnBatchCheck);
            this.grpAction.Controls.Add(this.btnRunTrackTable);
            this.grpAction.Controls.Add(this.btnRunTillDone);
            this.grpAction.Controls.Add(this.btnRunTillChange);
            this.grpAction.Location = new System.Drawing.Point(397, 40);
            this.grpAction.Name = "grpAction";
            this.grpAction.Size = new System.Drawing.Size(138, 198);
            this.grpAction.TabIndex = 31;
            this.grpAction.TabStop = false;
            this.grpAction.Text = "Actions";
            // 
            // btnRunTrackTable
            // 
            this.btnRunTrackTable.Location = new System.Drawing.Point(6, 83);
            this.btnRunTrackTable.Name = "btnRunTrackTable";
            this.btnRunTrackTable.Size = new System.Drawing.Size(126, 23);
            this.btnRunTrackTable.TabIndex = 28;
            this.btnRunTrackTable.Text = "Load Tracking";
            this.btnRunTrackTable.UseVisualStyleBackColor = true;
            // 
            // btnRunTillDone
            // 
            this.btnRunTillDone.Location = new System.Drawing.Point(6, 23);
            this.btnRunTillDone.Name = "btnRunTillDone";
            this.btnRunTillDone.Size = new System.Drawing.Size(126, 23);
            this.btnRunTillDone.TabIndex = 27;
            this.btnRunTillDone.Text = "Fast Login";
            this.btnRunTillDone.UseVisualStyleBackColor = true;
            // 
            // btnRunTillChange
            // 
            this.btnRunTillChange.Location = new System.Drawing.Point(6, 113);
            this.btnRunTillChange.Name = "btnRunTillChange";
            this.btnRunTillChange.Size = new System.Drawing.Size(126, 23);
            this.btnRunTillChange.TabIndex = 26;
            this.btnRunTillChange.Text = "Run";
            this.btnRunTillChange.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblInvoiceStatus);
            this.groupBox1.Controls.Add(this.lblPackNo);
            this.groupBox1.Controls.Add(this.tbPackNo);
            this.groupBox1.Controls.Add(this.tbOrderNo);
            this.groupBox1.Controls.Add(this.lbOrderNo);
            this.groupBox1.Controls.Add(this.tbShipDate);
            this.groupBox1.Controls.Add(this.lbShipDate);
            this.groupBox1.Controls.Add(this.lbClassOfService);
            this.groupBox1.Controls.Add(this.tbCharge);
            this.groupBox1.Controls.Add(this.lbWeight);
            this.groupBox1.Controls.Add(this.tbWeight);
            this.groupBox1.Controls.Add(this.lbFrtCharge);
            this.groupBox1.Controls.Add(this.tbClassOfService);
            this.groupBox1.Location = new System.Drawing.Point(15, 40);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(366, 146);
            this.groupBox1.TabIndex = 32;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Pack Slip";
            // 
            // lblInvoiceStatus
            // 
            this.lblInvoiceStatus.AutoSize = true;
            this.lblInvoiceStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInvoiceStatus.ForeColor = System.Drawing.Color.Black;
            this.lblInvoiceStatus.Location = new System.Drawing.Point(34, 111);
            this.lblInvoiceStatus.Name = "lblInvoiceStatus";
            this.lblInvoiceStatus.Size = new System.Drawing.Size(138, 18);
            this.lblInvoiceStatus.TabIndex = 22;
            this.lblInvoiceStatus.Text = "Ready To Invoice";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tbProcessedInvoices);
            this.groupBox2.Location = new System.Drawing.Point(541, 252);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(303, 327);
            this.groupBox2.TabIndex = 33;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Processed Invoices";
            // 
            // gbStatistics
            // 
            this.gbStatistics.Controls.Add(this.lbTracking);
            this.gbStatistics.Controls.Add(this.lbPacksStats);
            this.gbStatistics.Controls.Add(this.tbNtrackingNumProcessed);
            this.gbStatistics.Controls.Add(this.tbNpacksProcessed);
            this.gbStatistics.Controls.Add(this.tbNtrackingNumFile);
            this.gbStatistics.Controls.Add(this.tbNpacksFile);
            this.gbStatistics.Location = new System.Drawing.Point(541, 138);
            this.gbStatistics.Name = "gbStatistics";
            this.gbStatistics.Size = new System.Drawing.Size(302, 110);
            this.gbStatistics.TabIndex = 34;
            this.gbStatistics.TabStop = false;
            this.gbStatistics.Text = "Statistics";
            // 
            // lbTracking
            // 
            this.lbTracking.AutoSize = true;
            this.lbTracking.Location = new System.Drawing.Point(14, 52);
            this.lbTracking.Name = "lbTracking";
            this.lbTracking.Size = new System.Drawing.Size(49, 13);
            this.lbTracking.TabIndex = 5;
            this.lbTracking.Text = "Tracking";
            // 
            // lbPacksStats
            // 
            this.lbPacksStats.AutoSize = true;
            this.lbPacksStats.Location = new System.Drawing.Point(14, 31);
            this.lbPacksStats.Name = "lbPacksStats";
            this.lbPacksStats.Size = new System.Drawing.Size(37, 13);
            this.lbPacksStats.TabIndex = 4;
            this.lbPacksStats.Text = "Packs";
            // 
            // tbNtrackingNumProcessed
            // 
            this.tbNtrackingNumProcessed.Location = new System.Drawing.Point(153, 49);
            this.tbNtrackingNumProcessed.Name = "tbNtrackingNumProcessed";
            this.tbNtrackingNumProcessed.Size = new System.Drawing.Size(66, 20);
            this.tbNtrackingNumProcessed.TabIndex = 3;
            // 
            // tbNpacksProcessed
            // 
            this.tbNpacksProcessed.Location = new System.Drawing.Point(153, 23);
            this.tbNpacksProcessed.Name = "tbNpacksProcessed";
            this.tbNpacksProcessed.Size = new System.Drawing.Size(66, 20);
            this.tbNpacksProcessed.TabIndex = 2;
            // 
            // tbNtrackingNumFile
            // 
            this.tbNtrackingNumFile.Location = new System.Drawing.Point(65, 49);
            this.tbNtrackingNumFile.Name = "tbNtrackingNumFile";
            this.tbNtrackingNumFile.Size = new System.Drawing.Size(66, 20);
            this.tbNtrackingNumFile.TabIndex = 1;
            // 
            // tbNpacksFile
            // 
            this.tbNpacksFile.Location = new System.Drawing.Point(65, 23);
            this.tbNpacksFile.Name = "tbNpacksFile";
            this.tbNpacksFile.Size = new System.Drawing.Size(66, 20);
            this.tbNpacksFile.TabIndex = 0;
            // 
            // tbBGBatch
            // 
            this.tbBGBatch.Location = new System.Drawing.Point(403, 305);
            this.tbBGBatch.Name = "tbBGBatch";
            this.tbBGBatch.Size = new System.Drawing.Size(100, 20);
            this.tbBGBatch.TabIndex = 35;
            // 
            // lbBGBatch
            // 
            this.lbBGBatch.AutoSize = true;
            this.lbBGBatch.Location = new System.Drawing.Point(16, 43);
            this.lbBGBatch.Name = "lbBGBatch";
            this.lbBGBatch.Size = new System.Drawing.Size(57, 13);
            this.lbBGBatch.TabIndex = 36;
            this.lbBGBatch.Text = "Print Prefix";
            // 
            // gbArBatches
            // 
            this.gbArBatches.Controls.Add(this.label1);
            this.gbArBatches.Controls.Add(this.cbLuxPrefix);
            this.gbArBatches.Controls.Add(this.cbPrintPrefix);
            this.gbArBatches.Controls.Add(this.cbMainBatchPrefix);
            this.gbArBatches.Controls.Add(this.lbBGBatch);
            this.gbArBatches.Controls.Add(this.lbInvoiceGroup);
            this.gbArBatches.Location = new System.Drawing.Point(541, 13);
            this.gbArBatches.Name = "gbArBatches";
            this.gbArBatches.Size = new System.Drawing.Size(302, 119);
            this.gbArBatches.TabIndex = 37;
            this.gbArBatches.TabStop = false;
            this.gbArBatches.Text = "AR Batches";
            // 
            // btnBatchCheck
            // 
            this.btnBatchCheck.Location = new System.Drawing.Point(6, 52);
            this.btnBatchCheck.Name = "btnBatchCheck";
            this.btnBatchCheck.Size = new System.Drawing.Size(126, 22);
            this.btnBatchCheck.TabIndex = 29;
            this.btnBatchCheck.Text = "Batch Check";
            this.btnBatchCheck.UseVisualStyleBackColor = true;
            // 
            // cbMainBatchPrefix
            // 
            this.cbMainBatchPrefix.FormattingEnabled = true;
            this.cbMainBatchPrefix.Items.AddRange(new object[] {
            "K",
            "R"});
            this.cbMainBatchPrefix.Location = new System.Drawing.Point(98, 13);
            this.cbMainBatchPrefix.Name = "cbMainBatchPrefix";
            this.cbMainBatchPrefix.Size = new System.Drawing.Size(83, 21);
            this.cbMainBatchPrefix.TabIndex = 37;
            // 
            // cbPrintPrefix
            // 
            this.cbPrintPrefix.FormattingEnabled = true;
            this.cbPrintPrefix.Items.AddRange(new object[] {
            "S",
            "X"});
            this.cbPrintPrefix.Location = new System.Drawing.Point(98, 40);
            this.cbPrintPrefix.Name = "cbPrintPrefix";
            this.cbPrintPrefix.Size = new System.Drawing.Size(83, 21);
            this.cbPrintPrefix.TabIndex = 38;
            // 
            // cbLuxPrefix
            // 
            this.cbLuxPrefix.FormattingEnabled = true;
            this.cbLuxPrefix.Items.AddRange(new object[] {
            "L",
            "U"});
            this.cbLuxPrefix.Location = new System.Drawing.Point(98, 67);
            this.cbLuxPrefix.Name = "cbLuxPrefix";
            this.cbLuxPrefix.Size = new System.Drawing.Size(83, 21);
            this.cbLuxPrefix.TabIndex = 39;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 40;
            this.label1.Text = "Lux UPS Prefix";
            // 
            // PackEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(859, 584);
            this.Controls.Add(this.gbArBatches);
            this.Controls.Add(this.tbARBatch);
            this.Controls.Add(this.gbStatistics);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.tbBGBatch);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grpAction);
            this.Controls.Add(this.gbTrackingNos);
            this.Controls.Add(this.gbShipAdr);
            this.Controls.Add(this.btnLogin);
            this.Name = "PackEntry";
            this.Text = "PackEntry IBill";
            this.gbShipAdr.ResumeLayout(false);
            this.gbShipAdr.PerformLayout();
            this.gbTrackingNos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTracking)).EndInit();
            this.grpAction.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.gbStatistics.ResumeLayout(false);
            this.gbStatistics.PerformLayout();
            this.gbArBatches.ResumeLayout(false);
            this.gbArBatches.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbPackNo;
        private System.Windows.Forms.Label lblPackNo;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.GroupBox gbShipAdr;
        private System.Windows.Forms.TextBox tbAddr1;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblAddr2;
        private System.Windows.Forms.TextBox tbAddr2;
        private System.Windows.Forms.Label lblAddr1;
        private System.Windows.Forms.Label lblCity;
        private System.Windows.Forms.TextBox tbCity;
        private System.Windows.Forms.Label lblPostalCode;
        private System.Windows.Forms.TextBox tbPostalCode;
        private System.Windows.Forms.Label lblState;
        private System.Windows.Forms.TextBox tbState;
        private System.Windows.Forms.TextBox tbOrderNo;
        private System.Windows.Forms.Label lbOrderNo;
        private System.Windows.Forms.TextBox tbShipDate;
        private System.Windows.Forms.Label lbShipDate;
        private System.Windows.Forms.Label lbClassOfService;
        private System.Windows.Forms.Label lbWeight;
        private System.Windows.Forms.Label lbFrtCharge;
        private System.Windows.Forms.TextBox tbClassOfService;
        private System.Windows.Forms.TextBox tbWeight;
        private System.Windows.Forms.TextBox tbCharge;
        private System.Windows.Forms.GroupBox gbTrackingNos;
        private System.Windows.Forms.TextBox tbARBatch;
        private System.Windows.Forms.TextBox tbProcessedInvoices;
        private System.Windows.Forms.Label lbInvoiceGroup;
        private System.Windows.Forms.GroupBox grpAction;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblInvoiceStatus;
        private System.Windows.Forms.DataGridView dgvTracking;
        private System.Windows.Forms.CheckBox cbFreightFreeCust;
        private System.Windows.Forms.GroupBox gbStatistics;
        private System.Windows.Forms.TextBox tbNpacksFile;
        private System.Windows.Forms.TextBox tbNtrackingNumProcessed;
        private System.Windows.Forms.TextBox tbNpacksProcessed;
        private System.Windows.Forms.TextBox tbNtrackingNumFile;
        private System.Windows.Forms.Label lbTracking;
        private System.Windows.Forms.Label lbPacksStats;
        private System.Windows.Forms.TextBox tbBGBatch;
        private System.Windows.Forms.Label lbBGBatch;
        private System.Windows.Forms.GroupBox gbArBatches;
        private System.Windows.Forms.Button btnRunTillDone;
        private System.Windows.Forms.Button btnRunTillChange;
        private System.Windows.Forms.Button btnRunTrackTable;
        private System.Windows.Forms.Button btnBatchCheck;
        private System.Windows.Forms.ComboBox cbMainBatchPrefix;
        private System.Windows.Forms.ComboBox cbPrintPrefix;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbLuxPrefix;
    }
}