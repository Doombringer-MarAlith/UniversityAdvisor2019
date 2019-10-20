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
            // If no faculty is selected, read reviews for current selected university
            if (facultiesListBox.SelectedItem == null)
            {
                await FormManager.LoadReviewsForSelectedUniversity(this);
            }
            else
            {
                await FormManager.LoadReviewsForSelectedFaculty(facultiesListBox.SelectedIndex, this);
            }
        }

        private void WriteReviewButton_Click(object sender, EventArgs e)
        {
            // If no faculty is selected, write review for current selected university
            if (facultiesListBox.SelectedItem == null)
            {
                FormManager.WriteReviewForSelectedUniversity(this);
            }
            else
            {
                FormManager.WriteReviewForSelectedFaculty(facultiesListBox.SelectedIndex, this);
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

        private void BackButton_Click(object sender, EventArgs e)
        {
            FormManager.CloseSelectedUniversity(this);
        }
    }
}
