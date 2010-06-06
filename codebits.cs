       static void ShowLoginDialogBox()
        {
            Form1 loginDialog = new Form1();

        // Show testDialog as a modal dialog and determine if DialogResult = OK.
            if (loginDialog.ShowDialog(this) == DialogResult.OK)
            {
                // Read the contents of testDialog's TextBox.
                this.txtResult.Text = testDialog.TextBox1.Text;
            }
            else
            {
                this.txtResult.Text = "Cancelled";
            }
            testDialog.Dispose();
        }

btnLogin.Click += new EventHandler(btnLogin_Click);