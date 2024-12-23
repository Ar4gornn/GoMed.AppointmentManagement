namespace GoMed.AppointmentManagement.Application.Common.Helpers;

public class PhoneFormatter
{
    public static string? ToDigitsOnly(string? input)
    {
        return string.IsNullOrEmpty(input)
            ? input
            : input.Replace(" ", "").Replace("-", "").Replace("+", "").Replace("(", "").Replace(")", "");
    }
}