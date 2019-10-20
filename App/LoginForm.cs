using System;
using System.Windows.Forms;

namespace App
{
    public partial class LoginForm : Form, ILoginForm
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private async void LoginButton_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(emailTextBox.Text) && !String.IsNullOrWhiteSpace(passwordTextBox.Text))
            {
                await FormManager.TryToLogIn(emailTextBox.Text, passwordTextBox.Text, this);
            }
            else
            {
                MessageBox.Show("Invalid username or password!");
            }
        }

        private void SignUpButton_Click(object sender, EventArgs e)
        {
            FormManager.ChangeForm(this, FormManager.GetForm(FormType.FORM_SIGN_UP));
        }

        private void PasswordTextBox_TextChanged(object sender, EventArgs e)
        {
            passwordTextBox.PasswordChar = '*';
        }
    }
}
