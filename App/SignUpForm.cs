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
            if (passwordTextbox.Text.Equals(repeatPasswordTextbox.Text) && passwordTextbox.Text.Length > 4 && !String.IsNullOrWhiteSpace(passwordTextbox.Text))
            {
                switch (await FormManager.CreateUser(usernameTextBox.Text, emailTextBox.Text, passwordTextbox.Text))
                {
                    case 0:
                        MessageBox.Show("User with this email already exists!");
                        break;
                    case 1:
                        MessageBox.Show("User with this username already exists!");
                        break;
                    case 2:
                        MessageBox.Show("Account created successfully.");
                        FormManager.ChangeForm(this, FormManager.GetForm(FormManager.FormType.FORM_LOGIN));
                        break;
                    default:
                        break;
                };
            }
            else
            {
                MessageBox.Show("Passwords don't match or are shorter than 5 characters. Try again!");
            }
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            FormManager.ChangeForm(this, FormManager.GetForm(FormManager.FormType.FORM_LOGIN));
        }
    }
}
