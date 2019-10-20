using System;
using System.Windows.Forms;

namespace App
{
    public partial class WriteReviewForm : Form, IReviewForm
    {
        public WriteReviewForm()
        {
            InitializeComponent();
        }

        private async void SubmitButton_Click(object sender, EventArgs e)
        {
            if (numericReview.SelectedItem != null)
            {
                if (!String.IsNullOrEmpty(reviewTextBox.Text) && reviewTextBox.Text.Length < 300)
                {
                    await BaseFormManager.SubmitReview(reviewTextBox.Text, numericReview.SelectedIndex + 1, this);
                    MessageBox.Show("Review submitted successfully.");
                }
                else
                {
                    MessageBox.Show("Your text has to be not empty and less than 300 characters.");
                }
            }
            else
            {
                MessageBox.Show("Please select the rating you want to give from the dropdown menu.");
            }
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            BaseFormManager.ResetSelectedFaculty();
            BaseFormManager.ChangeForm(this, BaseFormManager.GetForm(FormType.FORM_SELECTED_UNIVERSITY));
        }
    }
}
