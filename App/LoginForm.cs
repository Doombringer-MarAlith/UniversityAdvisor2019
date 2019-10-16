using System;
using System.Windows.Forms;
using Objektinis;

namespace App
{
    public partial class LoginForm : Form, ILoginForm
    {
        public LoginForm(bool displayMsg)
        {
            InitializeComponent();
            if (displayMsg)
            {
                MessageBox.Show("Wrong username or password");
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private async void LoginButton_Click(object sender, EventArgs e)
        {
            if (usernameTextBox.Text.Length != 0 && passwordTextBox.Text.Length != 0)
            {
                await FormManager.CheckCredentials(usernameTextBox.Text, passwordTextBox.Text, this);
            }
            else
            {
                MessageBox.Show("You have not entered username or password");
            }
        }
    }
}
