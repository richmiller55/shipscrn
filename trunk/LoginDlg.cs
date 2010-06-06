using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ShipScrn
{
    public partial class LoginDlg : Form
    {
        PackEntry packEntry;
        public LoginDlg(PackEntry pe)
        {
            packEntry = pe;
            InitializeComponent();
            btnLogin.Click += new EventHandler(btnLogin_Click);
            btnOK.Click += new EventHandler(btnOK_Click);
        }

        void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        void btnLogin_Click(object sender, EventArgs e)
        {
            string user = tbUser.Text;
            string password = tbPassword.Text;
            string database = cbVanDatabase.Text;
            VanAccess va = new VanAccess(user, password, database);
            packEntry.SetVanAccess(va);  // maybe do this on the VanAccess class
        }

    }
}