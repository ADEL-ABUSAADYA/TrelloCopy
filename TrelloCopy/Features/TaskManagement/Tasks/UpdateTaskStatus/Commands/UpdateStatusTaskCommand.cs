using MediatR;
using Microsoft.EntityFrameworkCore;
using TrelloCopy.Common.BaseHandlers;
using TrelloCopy.Common.Data.Enums;
using TrelloCopy.Common.Views;
using TrelloCopy.Models;

namespace TrelloCopy.Features.TaskManagement.Tasks.UpdateTaskStatus.Commands
{
    public record UpdateStatusCommand (int id , TaskStatus Status ) : IRequest<RequestResult<bool>>;

    public class UpdateStatusTaskCommand : BaseRequestHandler<UpdateStatusCommand, RequestResult<bool>, TaskEntity>
    {
        public UpdateStatusTaskCommand(BaseWithoutRepositoryRequestHandlerParameter<TaskEntity> parameters) : base(parameters)
        {
        }

        public override async Task<RequestResult<bool>> Handle(UpdateStatusCommand request, CancellationToken cancellationToken)
        {
          
            var taskStatus = await _repository.Get(c=> c.ID==request.id &&  c.UserId == _userInfo.ID && !c.Deleted).Select(x =>new { x.ID , x.Status}).FirstOrDefaultAsync();


            if (taskStatus is null) return RequestResult<bool>.Failure(ErrorCode.NoTaskAdd, "there is no task with this id or User doesnt have the permission on this task");
            var changestatus = request.Status; 

            var taskupdated = new TaskEntity
            {
                ID = request.id,
                Status = changestatus,
            };

            await _repository.SaveIncludeAsync(taskupdated , nameof(taskupdated.Status));
           await  _repository.SaveChangesAsync(); 

            return RequestResult<bool>.Success(true , "status updated ");
        }
    }




}
