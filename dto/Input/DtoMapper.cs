using wsmcbl.front.Models.Accounting;

namespace wsmcbl.front.dto.Input;

public static class DtoMapper
{
    public static TariffModal ToModalItem(this Tariff tariff, StudentEntity student)
    {
        var tariffModal = new TariffModal
        {
            TariffId = tariff.TariffId,
            Concept = tariff.Concept
        };

        var debt = student.getDebBalanceByTariff(tariff.TariffId);
        
        if (debt > 0)
        {
            tariffModal.Amount = debt;
            tariffModal.computeTotal();
            return tariffModal;
        }

        tariffModal.Amount = tariff.Amount;
        tariffModal.Discount = tariffModal.Amount * student.discount;
        tariffModal.Arrear = tariff.IsLate ? tariffModal.Amount * (1 - student.discount) * 0.1 : 0;
        tariffModal.computeTotal();
        
        return tariffModal;
    }
}