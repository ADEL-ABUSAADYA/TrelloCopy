using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Filters;
using TrelloCopy.Common;
using TrelloCopy.Common.Views;

public class UserInfoFilter : IActionFilter
{
    private readonly UserInfoProvider _userInfoProvider;

    public UserInfoFilter(UserInfoProvider userInfoProvider)
    {
        _userInfoProvider = userInfoProvider;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        var user = context.HttpContext.User;
        if (user.Identity.IsAuthenticated)
        {
            var userId = int.TryParse(user.FindFirst("ID")?.Value, out var id) ? id : -1;
            _userInfoProvider.UserInfo = new UserInfo { ID = userId };
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}