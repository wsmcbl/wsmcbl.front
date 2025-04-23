using Microsoft.JSInterop;
using wsmcbl.src.Controller.Service;
using wsmcbl.src.Model.Accounting;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Accounting.TariffCollection;

namespace wsmcbl.src.Controller;

public class CollectTariffController : BaseController
{
    private readonly JwtClaimsService _jwtClaimsService;
    private readonly IJSRuntime _jsRuntime;

    public CollectTariffController(ApiConsumerFactory apiConsumerFactory, JwtClaimsService jwtClaimsService, IJSRuntime jsRuntime)
        : base(apiConsumerFactory, "students")
    {
        _jwtClaimsService = jwtClaimsService;
        _jsRuntime = jsRuntime;
    }

    public async Task<Paginator<StudentEntity>> GetStudentList(PagedRequest pagedRequest)
    {
        Paginator<StudentEntity> defaultResult = new();
        return await apiFactory.WithNotificator.GetAsync(Modules.Accounting, $"{path}{pagedRequest}",
            defaultResult);
    }

    public async Task<StudentEntity> GetStudentById(string studentId)
    {
        var resource = $"{path}/{studentId}";
        StudentEntity defaultResult = new();
        return await apiFactory.WithNotificator.GetAsync(Modules.Accounting, resource, defaultResult);
    }

    public async Task<List<TariffEntity>> GetTariffListByStudentId(string? studentId)
    {
        var resource = $"{path}/{studentId}/tariffs";

        var result = await apiFactory.WithNotificator.GetAsync(Modules.Accounting, resource, new List<TariffDto>());
        return result.Select(e => e.ToEntity()).ToList();
    }

    public async Task<string> SendPay()
    {
        var defaultResult = new TransactionEntity();
        var result =
            await apiFactory.WithNotificator.PostAsync(Modules.Accounting, "transactions", Transaction, defaultResult);
        return result.transactionId!;
    }

    public async Task<byte[]> GetInvoice(string transactionId)
    {
        var controller = new CancelTransactionController(apiFactory);
        return await controller.GetInvoice(transactionId);
    }

    private TransactionEntity? Transaction { get; set; }

    public async Task BuildTransaction(string studentId, List<DetailDto> transactionDetail)
    {
        Transaction = new TransactionEntity
        {
            studentId = studentId,
            cashierId = await GetCashierId(),
            details = transactionDetail
        };
    }

    private async Task<string> GetCashierId()
    {
        var value = await _jwtClaimsService.GetClaimAsync("roleid");
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InternalException("Este usuario no puede realizar esta acción.");
        }

        return value;
    }
    
    public async Task GetAccountStatement(string studentId)
    {
        var resource = $"{path}/{studentId}/account-statement/export";
        var fileBytes = await apiFactory.WithNotificator.GetByteFileAsync(Modules.Accounting, resource);
        if (fileBytes.Length <= 0)
        {
            throw new InternalException("Error al descargar el archivo.");
        }

        var docname = $"Estado de cuenta de {studentId}.pdf";
        var base64 = Convert.ToBase64String(fileBytes);
        var url = $"data:application/pdf;base64,{base64}";

        await _jsRuntime.InvokeVoidAsync("downloadFile", docname, url);
    }
}