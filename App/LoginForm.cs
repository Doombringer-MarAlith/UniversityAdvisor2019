using System;
using System.Windows.Forms;
using App;
using Objektinis.FormManagers;

namespace Objektinis
{
    public partial class LoginForm : Form, ILoginForm
    {
        private readonly ILoginFormManager _loginFormManager;

        public LoginForm(ILoginFormManager loginFormManager)
        {
            _loginFormManager = loginFormManager;

            InitializeComponent();
        }

        private async void LoginButton_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(emailTextBox.Text) && !String.IsNullOrWhiteSpace(passwordTextBox.Text))
            {
                await _loginFormManager.TryToLogIn(emailTextBox.Text, passwordTextBox.Text, this);
            }
            else
            {
                MessageBox.Show("Invalid username or password!");
            }
        }

        private void SignUpButton_Click(object sender, EventArgs e)
        {
            _loginFormManager.ChangeForm(this, _loginFormManager.GetForm(FormType.FormSignUp));
        }

        private void PasswordTextBox_TextChanged(object sender, EventArgs e)
        {
            passwordTextBox.PasswordChar = '*';
        }
    }
}
