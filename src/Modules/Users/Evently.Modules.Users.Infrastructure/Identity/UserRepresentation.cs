namespace Evently.Modules.Users.Infrastructure.Identity;

internal sealed record UserRepresentation(
    string UserName,
    string Email,
    string FirstName,
    string LastName,
    bool EmailVerified,
    bool Enabled,
    CredentianRepresentation[] Credentians);
