using MitroVehicle.Common;
using System.Text.RegularExpressions;

namespace MitroVehicle.Application.Common.Helpers
{
    public static class Validator
    {
        public static bool LicensePlateIsValid(string licensePlate)
        {
            return Regex.IsMatch(licensePlate.UnMask().ToUpper(), "[A-Z]{3}[0-9][0-9A-Z][0-9]{2}");
        }
        public static bool IsValidDocument(string? document)
        {
            if (string.IsNullOrWhiteSpace(document))
                return false;

            string unmaskedDocument = document.UnMask();
            return unmaskedDocument.Length switch
            {
                11 => CPFIsValid(unmaskedDocument),
                14 => CNPJIsValid(unmaskedDocument),
                _ => false
            };

        }

        public static bool CNPJIsValid(string cnpj)
        {
            if (string.IsNullOrWhiteSpace(cnpj))
                return false;

            cnpj = cnpj.Replace(".", "").Replace("/", "").Replace("-", "").Trim();

            if (cnpj.Length != 14 || !Regex.IsMatch(cnpj, @"^\d{14}$"))
                return false;

            string[] invalidCnpjs = { "00000000000000", "11111111111111", "22222222222222", "33333333333333",
                                   "44444444444444", "55555555555555", "66666666666666", "77777777777777",
                                   "88888888888888", "99999999999999" };
            if (Array.Exists(invalidCnpjs, c => c == cnpj))
                return false;

            int[] multiplicador1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma = 0;
            for (int i = 0; i < 12; i++)
                soma += (cnpj[i] - '0') * multiplicador1[i];

            int resto = soma % 11;
            int digito1 = resto < 2 ? 0 : 11 - resto;

            if (digito1 != (cnpj[12] - '0'))
                return false;

            int[] multiplicador2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += (cnpj[i] - '0') * multiplicador2[i];

            resto = soma % 11;
            int digito2 = resto < 2 ? 0 : 11 - resto;

            return digito2 == (cnpj[13] - '0');
        }

        public static bool CPFIsValid(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                return false;

            cpf = cpf.Replace(".", "").Replace("-", "").Trim();

            if (cpf.Length != 11 || !Regex.IsMatch(cpf, @"^\d{11}$"))
                return false;

            string[] invalidCpfs = { "00000000000", "11111111111", "22222222222", "33333333333", "44444444444",
                                  "55555555555", "66666666666", "77777777777", "88888888888", "99999999999" };
            if (Array.Exists(invalidCpfs, c => c == cpf))
                return false;

            int[] multiplicador1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma = 0;
            for (int i = 0; i < 9; i++)
                soma += (cpf[i] - '0') * multiplicador1[i];

            int resto = soma % 11;
            int digito1 = resto < 2 ? 0 : 11 - resto;

            if (digito1 != (cpf[9] - '0'))
                return false;

            int[] multiplicador2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += (cpf[i] - '0') * multiplicador2[i];

            resto = soma % 11;
            int digito2 = resto < 2 ? 0 : 11 - resto;

            return digito2 == (cpf[10] - '0');
        }

        public static bool IsValidUf(string sigla)
        {
            if (string.IsNullOrWhiteSpace(sigla) || sigla.Length != 2)
                return false;

            return States.Contains(sigla.ToUpper());
        }

        public static readonly HashSet<string> States = new HashSet<string>
        {
            "AC", "AL", "AP", "AM", "BA", "CE", "DF", "ES", "GO", "MA",
            "MT", "MS", "MG", "PA", "PB", "PR", "PE", "PI", "RJ", "RN",
            "RS", "RO", "RR", "SC", "SP", "SE", "TO"
        };

        //private static readonly Regex EmailRegex = new Regex(
        //    @"^[a-zA-Z0-9._%+-]+@a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
        //    RegexOptions.Compiled | RegexOptions.IgnoreCase);

        //public static bool IsValidEmail(string email)
        //{
        //    if (string.IsNullOrWhiteSpace(email))
        //    {
        //        return false;
        //    }

        //    return EmailRegex.IsMatch(email);
        //}
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
        }

        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            if (!phoneNumber.HasValue())
                return false;

            var cleanedPhoneNumber = new string(phoneNumber.UnMask().Where(char.IsDigit).ToArray());
            return cleanedPhoneNumber.Length == 10 || cleanedPhoneNumber.Length == 11;
        }


    }
}
