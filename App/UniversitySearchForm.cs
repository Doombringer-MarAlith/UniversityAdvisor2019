using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace App
{
    public partial class UniversitySearchForm : Form, IUniversitySearchForm
    {
        public UniversitySearchForm()
        {
            InitializeComponent();
        }

        private async void SearchButton_Click(object sender, EventArgs e)
        {
            if(!String.IsNullOrWhiteSpace(searchBar.Text))
            {
                List<string> result = await FormManager.GetUniversity(searchBar.Text);
                if(result.Count != 0)   
                {
                    universitiesList.Items.Clear();
                    universitiesList.Items.AddRange(result.ToArray());
                }
                else
                {
                    MessageBox.Show("No universities with that name");
                }

            }
            else
            {
                MessageBox.Show("Please enter a university you want to search for");
            }
        }

        private void SearchBar_Click(object sender, EventArgs e)
        {

        }

        private void SelectUniButton_Click(object sender, EventArgs e)
        {
            if(universitiesList.SelectedItem != null)
            {
                FormManager.OpenSelected(universitiesList.SelectedIndex, this);
            }
            else
            {
                MessageBox.Show("Please select a university from the list first!");
            }
        }

        private void Universities_Load(object sender, EventArgs e)
        {

        }
    }
}
