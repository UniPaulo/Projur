using System;

namespace Projur.BackEnd.Projur.Domain
{
    public class Validador
    {
        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        public static bool IsDateBiggerThanToday(DateTime date)
        {
            if (date > DateTime.Now)
                return true;
            else
                return false;
        }
    }
}
