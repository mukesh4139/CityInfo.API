using CityInfo.API.Entities;
using CityInfo.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using CityInfo.API.AuthorizationRequirements;

namespace CityInfo.API.AuthorizationHandlers
{
    public class PostCrudOperationsAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, Post>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context, 
            OperationAuthorizationRequirement requirement, 
            Post post)
        {           
            switch (requirement.Name)
            {
                case nameof(CrudOperationRequirements.CreateRequirement):
                    {
                        if (context.User.GetCurrentOrganizationId() == post.User.OrganizationId)
                        {
                            context.Succeed(requirement);
                        }
                        break;
                    }
                
                case nameof(CrudOperationRequirements.ReadRequirement):
                    {
                        if (context.User.GetCurrentOrganizationId() == post.User.OrganizationId)
                        {
                            context.Succeed(requirement);
                        }
                        break;
                    }


            }

            return Task.CompletedTask;
        }
    }
}
