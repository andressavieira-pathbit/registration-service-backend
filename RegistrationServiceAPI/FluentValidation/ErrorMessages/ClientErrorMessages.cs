namespace RegistrationServiceAPI.FluentValidation.ErrorMessages
{
    public class ClientErrorMessages
    {
        public static string Empty = "Campo obrigatório.";

        public static string InvalidName = "O nome deve ter entre 10 e 100 caracteres.";

        public static string LegalAge = "O usuário deve ter no mínimo 18 anos.";

        public static string InvalidCPF = "CPF inválido, tente novamente.";

        public static string InvalidEmail = "E-mail inválido, tente novamente.";

        public static string DifferentPasswords = "As senhas informadas são diferentes.";
    }
}
