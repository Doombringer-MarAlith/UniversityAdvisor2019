using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace App
{
    public partial class UniversitySearchForm : Form, IUniversitySearchForm
    {
        private readonly IUniversitySearchFormManager _universitySearchFormManager;

        public UniversitySearchForm(IUniversitySearchFormManager universitySearchFormManager)
        {
            _universitySearchFormManager = universitySearchFormManager;
            InitializeComponent();
        }

        private async void SearchButton_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(searchBar.Text))
            {
                List<string> result = await _universitySearchFormManager.GetUniversities(searchBar.Text);
                if (result!=null && result.Count != 0)   
                {
                    universitiesList.Items.Clear();
                    universitiesList.Items.AddRange(result.ToArray());
                }
                else
                {
                    MessageBox.Show("No universities with that name.");
                }
            }
            else
            {
                MessageBox.Show("Please enter a university you want to search for.");
            }
        }

        private void SelectUniversityButton_Click(object sender, EventArgs e)
        {
            if (universitiesList.SelectedItem != null)
            {
                _universitySearchFormManager.OpenSelectedUniversityForm(universitiesList.SelectedIndex, this);
            }
            else
            {
                MessageBox.Show("Please select a university from the list first!");
            }
        }
    }
}
