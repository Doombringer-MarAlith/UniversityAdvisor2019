using System;
using System.Windows.Forms;

namespace App
{
    public partial class SignUpForm : Form, ISignUpForm
    {
        private readonly ISignUpFormManager _signUpFormManager;

        public SignUpForm(ISignUpFormManager signUpFormManager)
        {
            _signUpFormManager = signUpFormManager;
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
                if (!_signUpFormManager.IsEmailValid(emailTextBox.Text))
                {
                    MessageBox.Show("Invalid email!");
                    return;
                }

                switch (await _signUpFormManager.CreateUser(usernameTextBox.Text, emailTextBox.Text, passwordTextbox.Text))
                {
                    case 0:
                        MessageBox.Show("User with this email already exists!");
                        break;
                    case 1:
                        MessageBox.Show("User with this username already exists!");
                        break;
                    case 2:
                        MessageBox.Show("Account created successfully.");
                        _signUpFormManager.ChangeForm(this, _signUpFormManager.GetForm(FormType.FormLogin));
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
            _signUpFormManager.ChangeForm(this, _signUpFormManager.GetForm(FormType.FormLogin));
        }
    }
}
