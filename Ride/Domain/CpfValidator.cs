using System.Text.RegularExpressions;

namespace Ride.Domain;

public class CpfValidator
{
    public bool Validate(string cpf)
    {
        if (string.IsNullOrEmpty(cpf)) return false;
        cpf = this.Clean(cpf); 
        if (this.IsInvalidLength(cpf)) return false;
        if (this.IsAllDigitsTheSame(cpf)) return false;
        var dg1 = this.CalculateDigit(cpf, 10);
        var dg2 = this.CalculateDigit(cpf, 11);
        var checkDigit = this.ExtractDigit(cpf);
        var calculatedDigit = $"{dg1}{dg2}";
        return checkDigit == calculatedDigit;
    }

    private string Clean(string cpf) => Regex.Replace(cpf, @"\D", "");

    private bool IsInvalidLength(string cpf) => (cpf.Length != 11);

    private bool IsAllDigitsTheSame(string cpf) => !(cpf.Any(c => c != cpf[0]));
    
    private object CalculateDigit(string cpf, int factor)
    {
        int total = 0;
        foreach (char digit in cpf)
        {
            if (factor > 1)
                total += int.Parse(digit.ToString()) * factor--;
        }

        int rest = total % 11;
        return (rest < 2) ? 0 : 11 - rest;
    }
    
    private string ExtractDigit(string cpf) 
        => cpf.Substring(cpf.Length - 2);
}