using App;
using System.Windows.Forms;

namespace Objektinis.FormManagers
{
    public interface IBaseFormManager
    {
        void ChangeForm(Form form, Form changeTo);

        Form GetForm(FormType formType);
    }
}