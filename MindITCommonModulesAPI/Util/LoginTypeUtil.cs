using System.Text.RegularExpressions;

namespace MindITCommonModulesAPI.Util
{
    public class UserNameTypeUtils
    {
       
        // Function to Verify Email Format
        public bool? IsValidEmail(string? email)
        {
            if(email == null) { throw new ArgumentNullException("email"); }
            else
            {
                try
                {
                    var addr = new System.Net.Mail.MailAddress(email);
                    if (addr.Address != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch
                {
                    return false;
                }
            }
            
        }


        public string? InputUserName(string? userName)
        {
            bool? ab = IsValidEmail(userName);
            if(userName!= null)
            {
                if (ab == true)
                {
                    //Code for Email - Password verification

                    return TYPEEnum.Email.ToString();
                }


                else if (Regex.IsMatch(userName, @"^(\+91[\-\s]?)?[0]?(91)?[789]\d{9}$"))
                {
                    //Code for Phone Number - Password verification

                    return TYPEEnum.MobileNumber.ToString();

                }
                else
                {

                    //Code for simple Username - Password verification

                    return TYPEEnum.Username.ToString();
                }
            }
           else return null;
        }
    }
}

