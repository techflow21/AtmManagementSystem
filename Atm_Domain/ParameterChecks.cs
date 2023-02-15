
using System.Text.RegularExpressions;

namespace AtmManagementSystem;

internal class ParameterChecks
{
    public bool ValidateName(string name)
    {
        if (!Regex.IsMatch(name, "^[a-zA-Z]{2,15}$"))
        {
            return false;
        }

        return true;
    }

    public string CapitalizeFirstLetter(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return input;
        }

        input = input.ToLower();
        string firstChar = input.Substring(0, 1).ToUpper();
        return firstChar + input.Substring(1);
    }


    public bool ValidateATMPin(string atmPin)
    {
        if (!Regex.IsMatch(atmPin, "^[0-9]{4}$"))
        {
            return false;
        }

        return true;
    }


    public bool ValidatePhoneNumber(string phoneNumber)
    {
        if (!Regex.IsMatch(phoneNumber, "^[0-9]{10,15}$") )
        {
            return false;
        }

        return true;
    }
}
