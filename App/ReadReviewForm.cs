using System;
using System.Windows.Forms;

namespace App
{
    public partial class ReadReviewForm : Form
    {
        private readonly ReadReviewFormManager _loginFormManager;

        public ReadReviewForm(ReadReviewFormManager loginFormManager)
        {
            _loginFormManager = loginFormManager;

            InitializeComponent();
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            _loginFormManager.LoadNextOrPreviousReview(true, this);
        }

        private void PreviousButton_Click(object sender, EventArgs e)
        {
            _loginFormManager.LoadNextOrPreviousReview(false, this);
        }

        private void LikeButton_Click(object sender, EventArgs e)
        {
            // increment review Value? Points?
        }

        private void ReadReviewForm_Load(object sender, EventArgs e)
        {
            titleOfThing.Text = _loginFormManager.GetNameOfReviewee();
            reviewText.Text = _loginFormManager.GetReviewText();
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            _loginFormManager.ResetSelectedFaculty();
            _loginFormManager.ChangeForm(this, _loginFormManager.GetForm(FormType.FORM_SELECTED_UNIVERSITY));
        }
    }
}
