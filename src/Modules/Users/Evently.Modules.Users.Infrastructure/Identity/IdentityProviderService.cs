﻿using System.Net;
using Evently.Common.Domain;
using Evently.Modules.Users.Application.Abstractions.Identity;
using Microsoft.Extensions.Logging;

namespace Evently.Modules.Users.Infrastructure.Identity;
internal sealed class IdentityProviderService(
    KeyCloakClient keyCloakClient,
    ILogger<IIdentityProviderService> logger) : IIdentityProviderService
{
    private const string PasswordCredentialType = "Password";

    // POST /admin/realms/{realm}/users
    public async Task<Result<string>> RegisterUserAsync(UserModel user, CancellationToken cancellationToken = default)
    {
        var userRepresentation = new UserRepresentation(
            user.Email,
            user.Email,
            user.FirstName,
            user.LastName,
            true,
            true,
            [new CredentianRepresentation(PasswordCredentialType, user.Password, false)]);

        try
        {
            string identityId = await keyCloakClient.RegisterUserAsync(userRepresentation, cancellationToken);

            return identityId;
        }
        catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.Conflict)
        {
            logger.LogError(ex, "User registration failed");

            return Result.Failure<string>(IdentityProviderErrors.EmailIsNotUnique);
        }
    }
}
