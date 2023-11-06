using FluentValidation;
using RegistrationServiceAPI.FluentValidation.ErrorMessages;
using RegistrationServiceAPI.Models;

namespace RegistrationServiceAPI.FluentValidation;

public class Validators : AbstractValidator<ClientPath>
{
    public Validators() // Utilizando a lib FluentValidation para fazer algumas validações 
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage(ClientErrorMessages.Empty)
                             .Length(10, 100).WithMessage(ClientErrorMessages.InvalidName);

        RuleFor(x => x.BirthDate).NotEmpty().WithMessage(ClientErrorMessages.Empty)
                                  .Must(LegalAge).WithMessage(ClientErrorMessages.LegalAge);

        RuleFor(x => x.CPF).NotEmpty().WithMessage(ClientErrorMessages.Empty)
                           .Must(IsCPF).WithMessage(ClientErrorMessages.InvalidCPF);

        RuleFor(x => x.Email).NotEmpty().WithMessage(ClientErrorMessages.Empty)
                              .EmailAddress().WithMessage(ClientErrorMessages.InvalidEmail);

        RuleFor(x => x.FinancialData.Income).NotEmpty().WithMessage(ClientErrorMessages.Empty);

        RuleFor(x => x.FinancialData.Patrimony).NotEmpty().WithMessage(ClientErrorMessages.Empty);

        RuleFor(x => x.AddressData.ZipCode).NotEmpty().WithMessage(ClientErrorMessages.Empty);

        RuleFor(x => x.AddressData.Address).NotEmpty().WithMessage(ClientErrorMessages.Empty);

        RuleFor(x => x.AddressData.Number).NotEmpty().WithMessage(ClientErrorMessages.Empty);

        RuleFor(x => x.AddressData.District).NotEmpty().WithMessage(ClientErrorMessages.Empty);

        RuleFor(x => x.AddressData.City).NotEmpty().WithMessage(ClientErrorMessages.Empty);

        RuleFor(x => x.AddressData.State).NotEmpty().WithMessage(ClientErrorMessages.Empty);
    }

    private static bool LegalAge(DateTime birthDate)
    {
        return birthDate <= DateTime.Now.AddYears(-18); // Verifica se o usuário possui 18 anos ou mais
    }

    private static bool IsCPF(string cpf) // Verifica se é um cpf válido
    {
        int[] multiplier1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplier2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        cpf = cpf.Trim().Replace(".", "").Replace("-", "");
        if (cpf.Length != 11)

            return false;

        for (int j = 0; j < 10; j++)
        {
            if (j.ToString().PadLeft(11, char.Parse(j.ToString())) == cpf)

                return false;
        }

        string tempCpf = cpf.Substring(0, 9);

        int sum = 0;

        for (int i = 0; i < 9; i++)
            sum += int.Parse(tempCpf[i].ToString()) * multiplier1[i];

        int remainder = sum % 11;
        if (remainder < 2)
            remainder = 0;
        else
            remainder = 11 - remainder;

        string number = remainder.ToString();

        tempCpf += number;
        sum = 0;

        for (int i = 0; i < 10; i++)
            sum += int.Parse(tempCpf[i].ToString()) * multiplier2[i];

        remainder = sum % 11;
        if (remainder < 2)
            remainder = 0;
        else
            remainder = 11 - remainder;

        number += remainder.ToString();

        return cpf.EndsWith(number);
    }
}
