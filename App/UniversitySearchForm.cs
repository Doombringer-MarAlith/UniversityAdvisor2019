using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ServerCallFromApp;

namespace Objektinis
{
    public partial class UniversitySearchForm : Form
    {
        private string _textToSearch;

        public UniversitySearchForm()
        {
            InitializeComponent();
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(searchBar.Text))
            {
                _textToSearch = searchBar.Text;

                List<string> result = new List<string>();
                // = DataManipulations.GetDataFromServer($"uniDbSearch/{searchFor}").Split(',').ToList(); // Need controller for getting uni search results
                if (result.Count != 0)
                {
                    universitiesList.Items.Clear();
                    universitiesList.Items.AddRange(result.ToArray());
                }
            }
            else
            {
                MessageBox.Show("Please enter a university you want to search for!");
            }
        }

        private void SearchBar_Click(object sender, EventArgs e)
        {

        }

        private void SelectUniButton_Click(object sender, EventArgs e)
        {
            if (universitiesList.SelectedItem != null)
            {
                this.Hide();
                SelectedUniversityForm selectedUniversity = new SelectedUniversityForm(universitiesList.SelectedItem.ToString());
                selectedUniversity.Closed += (s, args) => this.Close();
                selectedUniversity.Show();
            }
            else
            {
                MessageBox.Show("Please select a university from the list first!");
            }
        }
    }
}
