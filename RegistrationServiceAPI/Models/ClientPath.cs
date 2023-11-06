using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace RegistrationServiceAPI.Models;

public class ClientPath
{
    //Dados básicos
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string Name { get; set; } = null!;

    [DisplayFormat(DataFormatString = "dd/mm/yyyy")]
    public DateTime BirthDate { get; set; }
    public string CPF { get; set; } = null!;
    public string Email { get; set; } = null!;
    [Required(ErrorMessage = "Campo obrigatório.")]
    [RegularExpression("^\\(?[1-9]{2}\\)? ?(?:[2-8]|9[0-9])[0-9]{3}\\-?[0-9]{4}$", ErrorMessage = "Telefone inválido.")]
    public string PhoneNumber { get; set; } = null!;
    public FinancialData FinancialData { get; set; } = null!;

    public AddressData AddressData { get; set; } = null!;

    public SecurityData SecurityData { get; set; } = null!;
}

// Dados financeiros
public class FinancialData
{
    public decimal Income { get; set; }
    public decimal Patrimony { get; set; }

}

// Dados de endereço
public class AddressData
{
    public string Address { get; set; } = null!;
    public int Number { get; set; }
    public string District { get; set; } = null!;
    public string City { get; set; } = null!;
    public string State { get; set; } = null!;
    public string ZipCode { get; set; } = null!;

}

// Dados de segurança
public class SecurityData
{
    [Required (ErrorMessage = "Campo obrigatório.")]
    [RegularExpression("^(?=.*[0-9])(?=.*[A-Za-z])(?=.*[@!$#%^&*])[A-Za-z0-9@!$#%^&*]{8,}$", ErrorMessage = "A senha deve conter, no mínimo, 8 caracteres, incluindo pelo menos um número, uma letra e um símbolo (por exemplo: @, !, $).")]
    public string Password { get; set; } = null!;
    [Required(ErrorMessage = "Campo obrigatório.")]
    [Compare("Password", ErrorMessage = "A senha e a confirmação de senha não coincidem.")]
    public string PasswordConfirmation { get; set; } = null!;
}