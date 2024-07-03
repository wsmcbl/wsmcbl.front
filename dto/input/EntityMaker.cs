using wsmcbl.front.model.accounting;

namespace wsmcbl.front.dto.input;

public static class EntityMaker
{
    private static TariffModal ToModalItem(this TariffDto tariff, StudentEntity student)
    {
        var tariffModal = new TariffModal
        {
            TariffId = tariff.TariffId,
            Concept = tariff.Concept
        };
        
        if (student.hasPayments(tariff.TariffId))
        {
            tariffModal.Amount = student.getDebt(tariff.TariffId);
            tariffModal.computeTotal();
            return tariffModal;
        }

        tariffModal.Amount = tariff.Amount;
        tariffModal.Discount = tariff.Type == 1 ? tariff.Amount * student.discount : 0;
        tariffModal.Arrear = tariff.IsLate ? tariff.Amount * (1 - student.discount) * 0.1 : 0;
        tariffModal.computeTotal();
        
        return tariffModal;
    }

    public static List<TariffModal> ToModalList(this IEnumerable<TariffDto> list, StudentEntity student)
    {
        var listResult = new List<TariffModal>();
        listResult.AddRange(list.Select(item => item.ToModalItem(student)));
        return listResult;
    }
}