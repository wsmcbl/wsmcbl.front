using wsmcbl.src.Controller.Service;
using wsmcbl.src.Model.Accounting;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Accounting.TariffCollection;

namespace wsmcbl.src.Controller;

public class CollectTariffController : BaseController
{
    private readonly JwtClaimsService _JwtClaimsService;

    public CollectTariffController(ApiConsumerFactory apiConsumerFactory, JwtClaimsService jwtClaimsService)
        : base(apiConsumerFactory, "students")
    {
        _JwtClaimsService = jwtClaimsService;
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

    public async Task<bool> EditDiscount(EditDiscountDto editDiscountDto)
    {
        return await apiFactory.WithNotificator.PutAsync(Modules.Accounting, path, editDiscountDto);
    }

    private TransactionEntity? Transaction { get; set; }

    public async Task BuildTransaction(string StudentId, List<DetailDto> transactionDetail)
    {
        Transaction = new TransactionEntity
        {
            studentId = StudentId,
            cashierId = await GetCashierId(),
            details = transactionDetail
        };
    }

    private async Task<string> GetCashierId()
    {
        var value = await _JwtClaimsService.GetClaimAsync("roleid");
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InternalException("Este usuario no puede realizar esta acción.");
        }

        return value;
    }
}