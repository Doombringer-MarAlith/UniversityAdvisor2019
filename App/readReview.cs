using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Objektinis
{
    public partial class readReviewForm : Form
    {
        public readReviewForm()
        {
            InitializeComponent();
            titleOfThing.Text = FormManager.GetNameOfReviewee();
            reviewText.Text = FormManager.GetReviewText();

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
    }
}
