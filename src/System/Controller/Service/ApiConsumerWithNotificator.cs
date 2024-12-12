using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using wsmcbl.src.Utilities;

namespace wsmcbl.src.Controller.Service;

public class ApiConsumerWithNotificator : ApiConsumer
{
    private readonly Notificator _notificator;

    public ApiConsumerWithNotificator(Notificator notificator, HttpClient httpClient,
        ProtectedLocalStorage localStorage) :
        base(httpClient, localStorage)
    {
        _notificator = notificator;
    }

    protected override async Task<T> GenericHttpResponse<T>(Func<Task<T?>> httpRequest, T defaultResult, HttpResponseMessage response) where T : default
    {
        try
        {
            if (response.IsSuccessStatusCode)
            {
                return (await httpRequest())!;
            }

            var problem = await response.Content.ReadFromJsonAsync<ApiProblemDetails>();

            throw new InternalException("Hubo un problema con la solicitud.",
                $"({problem!.Status}) {problem.Detail}", problem.GetValidationErrors());
        }
        catch (InternalException ex)
        {
            await _notificator.ShowError(ex.Title, ex.Content, ex.Details);
        }
        catch (Exception ex)
        {
            await _notificator.ShowError("Error interno.", $"{ex.Message} Trace: {ex.StackTrace}");
        }

        return defaultResult;
    }
}