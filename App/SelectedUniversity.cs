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
    public partial class SelectedUniversity : Form
    {
        string universitySelected;
        public SelectedUniversity(string uniSelected)
        {
            InitializeComponent();
            universitySelected = uniSelected;
        }

        private void ReadButton_Click(object sender, EventArgs e)
        {
            if(facultiesListBox.SelectedItem == null)
            {
                // new Form to read reviews of a selected university
            }
            else
            {
                // new Form to read reviews of selected faculty
            }
        }

        private void WriteReviewButton_Click(object sender, EventArgs e)
        {
            if (facultiesListBox.SelectedItem == null)
            {
                // new Form to write review of a selected university
            }
            else
            {
                // new Form to write review of selected faculty
                // maybe no need for different forms (this if statement), just set the name to that uni or faculty and send request to add to fac/uni
            }
        }

        private void SelectedUniversity_Load(object sender, EventArgs e)
        {
            // request server to get faculties of selected university and add them to listbox
            List <string> faculties = DataManipulations.GetDataFromServer($"facultiesOfUni/{universitySelected}").Split(',').ToList();
            if(faculties.Count != 0)
            {
                facultiesListBox.Items.AddRange(faculties.ToArray());
            }
            else
            {
                facultiesListBox.Items.Add("No faculties found for this university");
            }
        }
    }
}
