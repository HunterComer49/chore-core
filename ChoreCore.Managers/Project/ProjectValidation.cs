using ChoreCore.Models;
using System;
using System.Text.RegularExpressions;

namespace ChoreCore.Managers
{
    public class ProjectValidation : IProjectValidation
    {
        private IAddressValidation _addressValidation;

        public ProjectValidation(IAddressValidation addressValidation = null)
        {
            _addressValidation = addressValidation ?? (IAddressValidation)Splat.Locator.Current.GetService(typeof(IAddressValidation));
        }

        public string ValidateProject(Models.Project project)
        {
            try
            {
                ValidateEmail(project);
                ValidateCategory(project);
                ValidateStatus(project);
                _addressValidation.ValidateAddress(project.Address);

                project.Location = _addressValidation.GetGeopoint(project.Address);

                return string.Empty;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        internal void ValidateEmail(Models.Project project)
        {
            if (!string.IsNullOrEmpty(project.Email) && !Regex.Match(project.Email, RegexValidation.EmailPattern).Success)
            {
                throw new Exception("Invalid user email.");
            }

            if (!string.IsNullOrEmpty(project.CoreMemberEmail) && !Regex.Match(project.CoreMemberEmail, RegexValidation.EmailPattern).Success)
            {
                throw new Exception("Invalid Core Member email.");
            }
        }

        internal void ValidateCategory(Models.Project project)
        {
            if (!string.IsNullOrEmpty(project.Category) && !Enum.IsDefined(typeof(ProjectCategory), project.Category))
            {
                throw new Exception("Invalid Category.");
            }
        }

        internal void ValidateStatus(Models.Project project)
        {
            if (!string.IsNullOrEmpty(project.Status) && !Enum.IsDefined(typeof(ProjectStatus), project.Status))
            {
                throw new Exception("Invalid Status.");
            }
        }
    }
}
