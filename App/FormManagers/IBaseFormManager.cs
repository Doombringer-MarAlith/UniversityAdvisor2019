using System.Windows.Forms;

namespace App
{
    public interface IBaseFormManager
    {
        void ChangeForm(Form form, Form changeTo);
        Form GetForm(FormType formType);
    }
}