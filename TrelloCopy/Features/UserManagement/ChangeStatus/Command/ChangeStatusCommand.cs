﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using TrelloCopy.Common.BaseHandlers;
using TrelloCopy.Common.Data.Enums;
using TrelloCopy.Common.Views;
using TrelloCopy.Models;

namespace TrelloCopy.Features.UserManagement.BlockUser.Command
{
    public record ChangeStatusCommand(int id) : IRequest<RequestResult<bool>>;

    public class BlockUserCommandHandler : BaseRequestHandler<ChangeStatusCommand, RequestResult<bool>, User>
    {
        public BlockUserCommandHandler(BaseWithoutRepositoryRequestHandlerParameter<User> parameters) : base(parameters)
        {
        }

        public override async Task<RequestResult<bool>> Handle(ChangeStatusCommand request, CancellationToken cancellationToken)
        {
            var checkActivtion = await  _repository 
               .Get(c => c.ID == request.id)
               .Select(c=> new  { ID = c.ID ,  IsActive =c.IsActive })
               .FirstOrDefaultAsync();

            if (checkActivtion == null) return RequestResult<bool>.Failure(ErrorCode.NoUsersFound, "this user not found");

         

            var changeStatus = !checkActivtion.IsActive;   
                 

            var user = new User { ID = checkActivtion.ID  , IsActive = changeStatus };


          await  _repository.SaveIncludeAsync(user , nameof(user.IsActive));

          await  _repository.SaveChangesAsync();

         return RequestResult<bool>.Success(true);  
        }
    }



}
