namespace ChoreCore.Models
{
    public class RegexValidation
    {
        public const string PhoneNumberPattern = @"^((\+){0,1}91(\s){0,1}(\-){0,1}(\s){0,1}){0,1}9[0-9](\s){0,1}(\-){0,1}(\s){0,1}[1-9]{1}[0-9]{7}$";

        public const string EmailPattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

        /// <summary>
        /// (Minimum 8 and Maximum 16 characters at least 1 Uppercase Alphabet, 
        /// 1 Lowercase Alphabet, 1 Number and 1 Special Character)
        /// </summary>
        public const string PasswordPattern = @"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$";

        public const string ZipPattern = @"^\d{5}$";
    }
}
