namespace Inventofree.Shared.Core.Helpers
{
    public static class UtilityHelper
    {
        public static bool IsValidEmail(string email)
        {
            try {
                var mail = new System.Net.Mail.MailAddress(email);
                return mail.Address == email;
            }
            catch {
                return false;
            }
        }
    }
}