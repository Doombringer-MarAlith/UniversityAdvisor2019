using System;
using System.Windows.Forms;
using App.FormManagers;

namespace App
{
    public partial class ReadReviewForm : Form, IReadReviewForm
    {
        private readonly IReadReviewFormManager _readReviewFormManager;

        public ReadReviewForm(IReadReviewFormManager readReviewFormManager)
        {
            _readReviewFormManager = readReviewFormManager;

            InitializeComponent();
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            _readReviewFormManager.LoadNextOrPreviousReview(true, this);
        }

        private void PreviousButton_Click(object sender, EventArgs e)
        {
            _readReviewFormManager.LoadNextOrPreviousReview(false, this);
        }

        private void LikeButton_Click(object sender, EventArgs e)
        {
            // increment review Value? Points?
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            _readReviewFormManager.ResetReviewIndex();
            _readReviewFormManager.ResetSelectedFaculty();
            _readReviewFormManager.ChangeForm(this, _readReviewFormManager.GetForm(FormType.FormSelectedUniversity));
        }

        private void ReadReviewForm_VisibleChanged(object sender, EventArgs e)
        {
            Form form = sender as Form;
            if (!form.Visible)
            {
                return;
            }

            titleOfThing.Text = _readReviewFormManager.GetNameOfReview();
            reviewText.Text = _readReviewFormManager.GetReviewText();
        }
    }
}
