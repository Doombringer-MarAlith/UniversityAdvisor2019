using Models.Models;
using ServerCallFromApp;
using System.Collections.Generic;
using System.Windows.Forms;

namespace App
{
    public abstract class BaseFormManager
    {
        private readonly IDataManipulations _dataManipulations;

        protected BaseFormManager(IDataManipulations dataManipulations)
        {
            _dataManipulations = dataManipulations;
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
                case FormType.FORM_LOGIN:
                    return new LoginForm();

                case FormType.FORM_SIGN_UP:
                    return new SignUpForm();

                case FormType.FORM_UNIVERSITIES:
                    return new UniversitySearchForm();

                case FormType.FORM_SELECTED_UNIVERSITY:
                    return new SelectedUniversityForm();

                case FormType.FORM_WRITE_REVIEW:
                    return new WriteReviewForm();

                case FormType.FORM_READ_REVIEW:
                    return new ReadReviewForm();

                default:
                    return null;
            }
        }
    }

    public enum FormType
    {
        FORM_LOGIN,
        FORM_SIGN_UP,
        FORM_UNIVERSITIES,
        FORM_SELECTED_UNIVERSITY,
        FORM_WRITE_REVIEW,
        FORM_READ_REVIEW
    }

    internal enum GuidType
    {
        UNIVERSITY_GUID,
        FACULTY_GUID,
        LECTURER_GUID,
        UNIVERSITY_PROGRAMME_GUID
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