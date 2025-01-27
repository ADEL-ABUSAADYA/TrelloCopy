// using MediatR;
// using TrelloCopy.Data.Repositories;
//
// namespace TrelloCopy.Features.userManagement.LogInUser.Queries;
// public record UserLogInQuery(string email, string password) : IRequest<(int, bool)>;
//
// public class UserLogInQueryHandler : IRequestHandler<UserLogInQuery, (int, bool)>
// {
//     IUserRepository _repository;
//
//     public UserLogInQueryHandler(IMediator mediator, IUserRepository repository)
//     {
//         _repository = repository;
//     }
//
//     public async Task<(int, bool)> Handle(UserLogInQuery request, CancellationToken cancellationToken)
//     {
//         var userData = await _repository.LogInUser(request.email, request.password);
//         
//         return userData;
//     }
// }