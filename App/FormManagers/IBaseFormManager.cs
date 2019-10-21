using System.Windows.Forms;
using App;

namespace Objektinis.FormManagers
{
    public interface IBaseFormManager
    {
        void ChangeForm(Form form, Form changeTo);
        Form GetForm(FormType formType);
    }
}