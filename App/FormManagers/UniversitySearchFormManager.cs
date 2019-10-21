using Models.Models;
using Newtonsoft.Json;
using Objektinis;
using ServerCallFromApp;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App
{
    public class UniversitySearchFormManager : BaseFormManager, IUniversitySearchFormManager
    {
        public UniversitySearchFormManager(IDataManipulations dataManipulations, FormManagerData formManagerData) : base(dataManipulations, formManagerData)
        {
        }

        // Returns a list of universities that match the search phrase or null if there are none
        public async Task<List<string>> GetUniversities(string name)
        {
            string data = await DataManipulations.GetDataFromServer($"university/{name}");
            if (data != null)
            {
                FormManagerData.FoundUniversities = JsonConvert.DeserializeObject<List<University>>(data);
            }
            else
            {
                return new List<string>();
            }

            return FormManagerData.FoundUniversities.Select(uni => uni.Name).ToList();
        }

        public void OpenSelectedUniversityForm(int selectedUniversityIndex, Form form)
        {
            FormManagerData.SelectedUniversity = FormManagerData.FoundUniversities[selectedUniversityIndex];
            ChangeForm(form, GetForm(FormType.FormSelectedUniversity));
        }
    }
}