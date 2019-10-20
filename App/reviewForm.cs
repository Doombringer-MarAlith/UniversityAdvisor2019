using System;
using System.Windows.Forms;

namespace App
{
    public partial class ReviewForm : Form, IReviewForm
    {
        public ReviewForm()
        {
            InitializeComponent();
        }

        private async void SubmitButton_Click(object sender, EventArgs e)
        {
            if(numericReview.SelectedItem != null)
            {
                if (!String.IsNullOrEmpty(reviewTextBox.Text) && reviewTextBox.Text.Length < 300)
                {
                    await FormManager.SubmitReview(reviewTextBox.Text, numericReview.SelectedIndex+1, this);
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
    }
}
