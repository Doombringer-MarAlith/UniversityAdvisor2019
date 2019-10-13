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

namespace Objektinis
{
    public partial class SelectedUniversity : System.Windows.Forms.Form
    {
        public SelectedUniversity()
        {
            InitializeComponent();
        }

        private void ReadButton_Click(object sender, EventArgs e)
        {
            if(facultiesListBox.SelectedItem == null)
            {
                // new readReviewForm for uni
                // FormManager opens it up, already has selected index
            }
            else
            {
                // new readReviewForm to read reviews of selected faculty
                // send index of selected Faculty
            }
        }

        private void WriteReviewButton_Click(object sender, EventArgs e)
        {
            if (facultiesListBox.SelectedItem == null)
            {
                // new reviewForm for uni
                // FormManager opens it up, already has selected index
            }
            else
            {
                // new reviewForm to write reviews of selected faculty
                // send index of selected Faculty
            }
        }

        private void SelectedUniversity_Load(object sender, EventArgs e)
        {
            // request server to get faculties of selected university and add them to listbox
            List<string> faculties = FormManager.GetFaculties();
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
