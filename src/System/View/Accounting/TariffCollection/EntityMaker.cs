using wsmcbl.src.Model.Accounting;

namespace wsmcbl.src.View.Accounting.TariffCollection;

public static class EntityMaker
{
    private static void UpdateAmounts(this TariffEntity tariff, StudentEntity student)
    {
        tariff.SetSubamount(student.discount);
        if (student.HasPayments(tariff.tariffId))
        {
            tariff.SubAmount = student.GetDebt(tariff.tariffId);
            tariff.Discount = 0;
            tariff.Arrears = 0;
        }
        
        tariff.ComputeTotal();
    }

    public static void UpdateAmounts(this IEnumerable<TariffEntity> list, StudentEntity student)
    {
        foreach (var item in list)
        {
            item.UpdateAmounts(student);
        }
    }

    public static List<DetailDto> MapToDto(this List<TariffEntity> list, bool applyArrears, decimal amountToPay)
    {
        List<DetailDto> result = [];

        foreach (var item in list)
        {
            if (amountToPay <= 0)
            {
                break;
            }
            
            if (!applyArrears)
            {
                item.Arrears = 0;
                item.ComputeTotal();
            }

            if (amountToPay < item.Total)
            {
                item.Total = amountToPay;
                amountToPay -= item.Total;
            }
            
            result.Add(new DetailDto
            {
                tariffId = item.tariffId,
                amount = item.Total,
                applyArrears = applyArrears
            });
        }

        return result;
    }
}
