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
                await BaseFormManager.LoadReviewsForSelectedUniversity(this);
            }
            else
            {
                await BaseFormManager.LoadReviewsForSelectedFaculty(facultiesListBox.SelectedIndex, this);
            }
        }

        private void WriteReviewButton_Click(object sender, EventArgs e)
        {
            // If no faculty is selected, write review for current selected university
            if (facultiesListBox.SelectedItem == null)
            {
                BaseFormManager.OpenWriteReviewFormForSelectedUniversity(this);
            }
            else
            {
                BaseFormManager.OpenWriteReviewFormForSelectedFaculty(facultiesListBox.SelectedIndex, this);
            }
        }

        private async void SelectedUniversity_Load(object sender, EventArgs e)
        {
            universityName.Text = BaseFormManager.GetSelectedUniversityName();

            // Request server to get faculties of selected university and add them to listbox
            List<string> faculties = await BaseFormManager.GetFaculties();
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
            BaseFormManager.CloseSelectedUniversityForm(this);
        }
    }
}
