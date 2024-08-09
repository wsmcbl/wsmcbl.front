using wsmcbl.src.Model.Accounting;

namespace wsmcbl.src.View.Accounting.TariffCollection;

public static class EntityMaker
{
    private static TariffModalDto ToModalItem(this TariffDto tariff, StudentEntity student)
    {
        var tariffModal = new TariffModalDto
        {
            TariffId = tariff.TariffId,
            Concept = tariff.Concept
        };
        
        if (student.HasPayments(tariff.TariffId))
        {
            tariffModal.Amount = student.GetDebt(tariff.TariffId);
            tariffModal.computeTotal();
            return tariffModal;
        }

        tariffModal.Amount = tariff.Amount;
        tariffModal.Discount = tariff.Type == 1 ? tariff.Amount * student.discount : 0;
        tariffModal.Arrear = tariff.IsLate ? tariff.Amount * (1 - student.discount) * 0.1 : 0;
        tariffModal.computeTotal();
        
        return tariffModal;
    }

    public static List<TariffModalDto> ToModalList(this IEnumerable<TariffDto> list, StudentEntity student)
    {
        var listResult = new List<TariffModalDto>();
        listResult.AddRange(list.Select(item => item.ToModalItem(student)));
        return listResult;
    }
}