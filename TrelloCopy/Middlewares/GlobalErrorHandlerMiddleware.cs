using TrelloCopy.Common.Data.Enums;
using TrelloCopy.Common.Views;

namespace TrelloCopy.Middlewares;

public class GlobalErrorHandlerMiddleware
{
    RequestDelegate _nextAction;
    ILogger<GlobalErrorHandlerMiddleware> _logger;
    public GlobalErrorHandlerMiddleware(RequestDelegate nextAction, 
        ILogger<GlobalErrorHandlerMiddleware> logger)
    {
        _nextAction = nextAction;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _nextAction(context);
        }
        catch (Exception ex)
        {
            var response = EndpointResponse<bool>.Failure(ErrorCode.UnKnownError);
            context.Response.WriteAsJsonAsync(response);
        }

        //return SprintItem.CompletedTask;
    }
}