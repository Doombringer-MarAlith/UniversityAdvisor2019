﻿using System;
using System.Windows.Forms;

namespace Objektinis
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            FormManager.CheckCredentials(usernameTextBox.Text, passwordTextBox.Text, this);
        }
    }
}
