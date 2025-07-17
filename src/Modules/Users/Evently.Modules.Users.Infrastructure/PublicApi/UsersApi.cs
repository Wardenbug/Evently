using Evently.Common.Domain;
using Evently.Modules.Users.Application.Users.GetUser;
using Evently.Modules.Users.PublicApi;
using MediatR;

namespace Evently.Modules.Users.Infrastructure.PublicApi;
internal sealed class UsersApi(ISender sender) : IUsersApi
{
    public async Task<Modules.Users.PublicApi.UserResponse?> GetAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        Result<Application.Users.GetUser.UserResponse> result =
            await sender.Send(new GetUserQuery(userId), cancellationToken);

        if (result.IsFailure)
        {
            return null;
        }

        return new Modules.Users.PublicApi.UserResponse(
            result.Value.Id,
            result.Value.Email,
            result.Value.FirstName,
            result.Value.LastName);
    }
}
