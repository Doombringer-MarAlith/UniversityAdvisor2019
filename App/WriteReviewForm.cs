﻿using System;
using System.Windows.Forms;
using Objektinis.FormManagers;

namespace App
{
    public partial class WriteReviewForm : Form, IWriteReviewForm
    {
        private readonly IWriteReviewFormManager _writeReviewFormManager;

        public WriteReviewForm(IWriteReviewFormManager writeReviewFormManager)
        {
            _writeReviewFormManager = writeReviewFormManager;
            InitializeComponent();
        }

        private async void SubmitButton_Click(object sender, EventArgs e)
        {
            if (numericReview.SelectedItem != null)
            {
                if (!String.IsNullOrEmpty(reviewTextBox.Text) && reviewTextBox.Text.Length < 300)
                {
                    await _writeReviewFormManager.SubmitReview(reviewTextBox.Text, numericReview.SelectedIndex + 1, this);
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
            _writeReviewFormManager.ResetSelectedFaculty();
            _writeReviewFormManager.ChangeForm(this, _writeReviewFormManager.GetForm(FormType.FormSelectedUniversity));
        }
    }
}
