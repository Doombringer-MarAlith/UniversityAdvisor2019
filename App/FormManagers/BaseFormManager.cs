using Microsoft.Extensions.DependencyInjection;
using ServerCallFromApp;
using System;
using System.Windows.Forms;

namespace App.FormManagers
{
    public class BaseFormManager : IBaseFormManager
    {
        internal readonly IDataManipulations DataManipulations;
        internal FormManagerData FormManagerData;

        protected BaseFormManager(IDataManipulations dataManipulations, FormManagerData formManagerData)
        {
            DataManipulations = dataManipulations;
            FormManagerData = formManagerData;
        }

        public void ChangeForm(Form form, Form changeTo)
        {
            form.Hide();
            changeTo.Closed += (s, args) => form.Close();
            changeTo.Show();
        }

        public Form GetForm(FormType formType)
        {
            switch (formType)
            {
                case FormType.FormLogin:
                    return (Form)Program.Container.GetService<ILoginForm>();

                case FormType.FormSignUp:
                    return (Form)Program.Container.GetService<ISignUpForm>();

                case FormType.FormUniversities:
                    return (Form)Program.Container.GetService<IUniversitySearchForm>();

                case FormType.FormSelectedUniversity:
                    return (Form)Program.Container.GetService<ISelectedUniversityForm>();

                case FormType.FormWriteReview:
                    return (Form)Program.Container.GetService<IWriteReviewForm>();

                case FormType.FormReadReview:
                    return (Form)Program.Container.GetService<IReadReviewForm>();

                default:
                    throw new ArgumentException("Could not create form");
            }
        }
    }

    public enum FormType
    {
        FormLogin,
        FormSignUp,
        FormUniversities,
        FormSelectedUniversity,
        FormWriteReview,
        FormReadReview
    }

    internal enum GuidType
    {
        UniversityGuid,
        FacultyGuid,
        LecturerGuid,
        UniversityProgrammeGuid
    }

    internal enum CreateUserReturn
    {
        EMAIL_TAKEN,
        USERNAME_TAKEN,
        SUCCESS
    }

    internal enum ReviewType
    {
        REVIEW_UNIVERSITY,
        REVIEW_FACULTY,
        REVIEW_NONE
    }
}