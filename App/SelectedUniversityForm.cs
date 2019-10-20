using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace App
{
    public partial class SelectedUniversityForm : Form, ISelectedUniversityForm
    {
        public SelectedUniversityForm()
        {
            InitializeComponent();
        }

        private async void ReadButton_Click(object sender, EventArgs e)
        {
            if (facultiesListBox.SelectedItem == null)
            {
                // new readReviewForm for uni
                // FormManager opens it up, already has selected index
                await FormManager.LoadReviewsOf(-1, this);
            }
            else
            {
                // new readReviewForm to read reviews of selected faculty
                // send index of selected Faculty
                await FormManager.LoadReviewsOf(facultiesListBox.SelectedIndex, this);
            }
        }

        private void WriteReviewButton_Click(object sender, EventArgs e)
        {
            if (facultiesListBox.SelectedItem == null)
            {
                // new reviewForm for uni
                // FormManager opens it up, already has selected index
                FormManager.WriteReview(-1, this);
            }
            else
            {
                // new reviewForm to write reviews of selected faculty
                // send index of selected Faculty
                FormManager.WriteReview(facultiesListBox.SelectedIndex, this);
            }
        }

        private async void SelectedUniversity_Load(object sender, EventArgs e)
        {
            // Request server to get faculties of selected university and add them to listbox
            List<string> faculties = await FormManager.GetFaculties();
            if (faculties.Count != 0)
            {
                facultiesListBox.Items.AddRange(faculties.ToArray());
            }
            else
            {
                MessageBox.Show("No faculties found for this university.");
            }
        }
    }
}
