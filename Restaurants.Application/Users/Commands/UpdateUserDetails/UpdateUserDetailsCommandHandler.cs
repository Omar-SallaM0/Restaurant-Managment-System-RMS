using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Resturants.Domain.Entities;
using Resturants.Domain.Exceptions;

namespace Restaurants.Application.Users.Commands.UpdateUserDetails
{
    internal class UpdateUserDetailsCommandHandler(ILogger<UpdateUserDetailsCommandHandler> logger
        ,IUserContext userContext,IUserStore<User> userStore) : IRequestHandler<UpdateUserDetailsCommand>
    {
        public async Task Handle(UpdateUserDetailsCommand request, CancellationToken cancellationToken)
        {
            var user = userContext.GetCurrentUser();
            logger.LogInformation("Updating user : {@Userid} With {@request}",user?.Id,request);
            var dbUser = await userStore.FindByIdAsync(user!.Id.ToString(),cancellationToken);
            if (dbUser == null)
            {
                throw new NotFoundException(nameof(user),user!.Id.ToString());
            }
            dbUser.Nationality = request.Nationality;
            dbUser.DateOfBirth = request.DateOfBirth;

            await userStore.UpdateAsync(dbUser,cancellationToken);

        }
    }
}
