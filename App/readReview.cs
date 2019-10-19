﻿using System;
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
            FormManager.LoadReview(true, this);
        }

        private void PreviousButton_Click(object sender, EventArgs e)
        {
            FormManager.LoadReview(false, this);
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
    }
}
