using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace App
{
    public partial class SelectedUniversityForm : Form, ISelectedUniversityForm
    {
        private readonly ISelectedUniversityFormManager _selectedUniversityFormManager;

        public SelectedUniversityForm(ISelectedUniversityFormManager selectedUniversityFormManager)
        {
            _selectedUniversityFormManager = selectedUniversityFormManager;
            InitializeComponent();
        }

        private async void ReadButton_Click(object sender, EventArgs e)
        {
            // If no faculty is selected, read reviews for current selected university
            if (facultiesListBox.SelectedItem == null)
            {
                await _selectedUniversityFormManager.LoadReviewsForSelectedUniversity(this);
            }
            else
            {
                await _selectedUniversityFormManager.LoadReviewsForSelectedFaculty(facultiesListBox.SelectedIndex, this);
            }
        }

        private void WriteReviewButton_Click(object sender, EventArgs e)
        {
            // If no faculty is selected, write review for current selected university
            if (facultiesListBox.SelectedItem == null)
            {
                _selectedUniversityFormManager.OpenWriteReviewFormForSelectedUniversity(this);
            }
            else
            {
                _selectedUniversityFormManager.OpenWriteReviewFormForSelectedFaculty(facultiesListBox.SelectedIndex, this);
            }
        }

        private async void SelectedUniversity_Load(object sender, EventArgs e)
        {
            universityName.Text = _selectedUniversityFormManager.GetSelectedUniversityName();

            // Request server to get faculties of selected university and add them to listbox
            List<string> faculties = await _selectedUniversityFormManager.GetFaculties();
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
            _selectedUniversityFormManager.CloseSelectedUniversityForm(this);
        }
    }
}
