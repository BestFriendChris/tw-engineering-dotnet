using System;
using VideoWorld.Models;

namespace VideoWorld.ViewModels
{
    public class NewCustomerViewModel
    {
        public string DisplayName { get; set; }
        public string Username { get; set; }
        public string Password1 { get; set; }
        public string Password2 { get; set; }
        public string ErrorMessage { get; set; }

        public bool PasswordsMatch()
        {
            return Password1 == Password2;
        }

        public void PopulateWithError(string errorMessage)
        {
            ClearPasswordFields();
            ErrorMessage = errorMessage;
        }

        private string ClearPasswordFields()
        {
            return Password1 = Password2 = string.Empty;
        }
    }
}