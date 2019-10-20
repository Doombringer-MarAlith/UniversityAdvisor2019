using System;
using System.Windows.Forms;

namespace App
{
    public partial class ReadReviewForm : Form
    {
        public ReadReviewForm()
        {
            InitializeComponent();
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            FormManager.LoadNextOrPreviousReview(true, this);
        }

        private void PreviousButton_Click(object sender, EventArgs e)
        {
            FormManager.LoadNextOrPreviousReview(false, this);
        }

        private void LikeButton_Click(object sender, EventArgs e)
        {
            // increment review Value? Points?
        }

        private void ReadReviewForm_Load(object sender, EventArgs e)
        {
            titleOfThing.Text = FormManager.GetNameOfReviewee();
            reviewText.Text = FormManager.GetReviewText();
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            FormManager.ChangeForm(this, FormManager.GetForm(FormManager.FormType.FORM_SELECTED_UNIVERSITY));
        }
    }
}
