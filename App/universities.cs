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
using Models.Models;
using Newtonsoft.Json;



namespace Objektinis
{
    public partial class universities : Form
    {
        string searchFor;
        public universities()
        {
            InitializeComponent();
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            if(searchBar.Text.Length  > 0)
            {
                searchFor = searchBar.Text;
                List<University> result = new List<University>();
                try
                {
                    result =
                    JsonConvert.DeserializeObject<List<University>>(DataManipulations.GetDataFromServer($"university/{searchFor}"));
                }
                catch (ArgumentNullException)
                {
                    MessageBox.Show("NULL");
                }
                
                if(result.Count != 0)   
                {
                    universitiesList.Items.Clear();
                    var range = result.Select(uni => uni.Name).ToArray();
                    universitiesList.Items.AddRange(range);
                    MessageBox.Show(result.ToString());
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
                this.Hide();
                SelectedUniversity selectedUniversity = new SelectedUniversity(universitiesList.SelectedItem.ToString());
                selectedUniversity.Closed += (s, args) => this.Close();
                selectedUniversity.Show();
            }
            else
            {
                MessageBox.Show("Please select a university from the list first");
            }
        }

        private void Universities_Load(object sender, EventArgs e)
        {

        }
    }
}
