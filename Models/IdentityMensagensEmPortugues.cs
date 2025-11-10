using Microsoft.AspNetCore.Identity;

public class IdentityMensagensEmPortugues : IdentityErrorDescriber
{
    public override IdentityError DefaultError()
        => new IdentityError { Code = nameof(DefaultError), Description = "Ocorreu um erro desconhecido." };

    public override IdentityError ConcurrencyFailure()
        => new IdentityError { Code = nameof(ConcurrencyFailure), Description = "Falha de concorrência, tente novamente." };

    public override IdentityError PasswordTooShort(int length)
        => new IdentityError { Code = nameof(PasswordTooShort), Description = $"A senha deve ter pelo menos {length} caracteres." };

    public override IdentityError PasswordRequiresNonAlphanumeric()
        => new IdentityError { Code = nameof(PasswordRequiresNonAlphanumeric), Description = "A senha deve conter pelo menos um caractere especial." };

    public override IdentityError PasswordRequiresDigit()
        => new IdentityError { Code = nameof(PasswordRequiresDigit), Description = "A senha deve conter pelo menos um número." };

    public override IdentityError PasswordRequiresLower()
        => new IdentityError { Code = nameof(PasswordRequiresLower), Description = "A senha deve conter pelo menos uma letra minúscula." };

    public override IdentityError PasswordRequiresUpper()
        => new IdentityError { Code = nameof(PasswordRequiresUpper), Description = "A senha deve conter pelo menos uma letra maiúscula." };

    public override IdentityError DuplicateUserName(string userName)
        => new IdentityError { Code = nameof(DuplicateUserName), Description = $"O nome de usuário '{userName}' já está em uso." };

    public override IdentityError DuplicateEmail(string email)
        => new IdentityError { Code = nameof(DuplicateEmail), Description = $"O email '{email}' já está em uso." };

    public override IdentityError InvalidEmail(string email)
        => new IdentityError { Code = nameof(InvalidEmail), Description = $"O email '{email}' é inválido." };

    public override IdentityError InvalidUserName(string userName)
        => new IdentityError { Code = nameof(InvalidUserName), Description = $"O nome de usuário '{userName}' é inválido." };

    public override IdentityError UserAlreadyHasPassword()
        => new IdentityError { Code = nameof(UserAlreadyHasPassword), Description = "O usuário já possui uma senha definida." };

    public override IdentityError UserLockoutNotEnabled()
        => new IdentityError { Code = nameof(UserLockoutNotEnabled), Description = "O bloqueio de usuário não está habilitado." };
}