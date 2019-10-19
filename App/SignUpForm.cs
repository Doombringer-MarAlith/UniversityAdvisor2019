using System;
using System.Windows.Forms;

namespace App
{
    public partial class SignUpForm : Form
    {
        public SignUpForm()
        {
            InitializeComponent();
        }

        private void PasswordTextbox_TextChanged(object sender, EventArgs e)
        {
            passwordTextbox.PasswordChar = '*';
        }

        private void RepeatPasswordTextbox_TextChanged(object sender, EventArgs e)
        {
            repeatPasswordTextbox.PasswordChar = '*';
        }

        private async void CreateUserButton_Click(object sender, EventArgs e)
        {
            int answer = -1;
            if (passwordTextbox.Text.Equals(repeatPasswordTextbox.Text) && passwordTextbox.Text.Length > 4 && !String.IsNullOrWhiteSpace(passwordTextbox.Text))
            {
                answer = await FormManager.CreateUser(usernameTextBox.Text, emailTextBox.Text, passwordTextbox.Text);
                if (answer == 0)
                {
                    MessageBox.Show("User with this email already exists!");
                }
                else if (answer == 1)
                {
                    MessageBox.Show("User with this username already exists!");
                }
                else if (answer == 2)
                {
                    MessageBox.Show("Account created successfully");
                    FormManager.SuccessfulSignup(this);
                }
            }
            else
            {
                MessageBox.Show("Passwords don't match or are shorter than 5 characters. Try again!");
            }
        }
    }
}
