using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Trial.Infrastructure.Authentication
{
    internal class PermissionManager
    {

        private readonly IServiceScopeFactory _serviceScopeFactory;

        public PermissionManager(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }
        public Task<HashSet<string>> GetForUserIdAsync(Guid userId)
        {
            // TODO: Here you'll implement your logic to fetch permissions.
            HashSet<string> permissionsSet = [];

            return Task.FromResult(permissionsSet);
        }

        public async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionRequirement requirement)
        {
            // TODO: You definitely want to reject unauthenticated users here.
            if (context.User is { Identity.IsAuthenticated: true })
            {
                // TODO: Remove this call when you implement the PermissionProvider.GetForUserIdAsync
                context.Succeed(requirement);

                return;
            }

            using IServiceScope scope = _serviceScopeFactory.CreateScope();

            Guid userId = context.User.GetUserId();

            HashSet<string> permissions = await GetForUserIdAsync(userId);

            if (permissions.Contains(requirement.Permission))
            {
                context.Succeed(requirement);
            }
        }
    }
}
