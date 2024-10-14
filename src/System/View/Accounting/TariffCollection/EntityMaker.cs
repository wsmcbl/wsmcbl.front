using wsmcbl.src.Model.Accounting;

namespace wsmcbl.src.View.Accounting.TariffCollection;

public static class EntityMaker
{
    private static TariffEntity ToEntity(this TariffDto tariff)
    {
        return new TariffEntity
        {
            TariffId = tariff.TariffId,
            SchoolYear = tariff.SchoolYear,
            Type = tariff.Type,
            Concept = tariff.Concept,
            Amount = tariff.Amount,
            DueDate = tariff.DueDate,
            IsLate = tariff.IsLate
        };
    }

    public static List<TariffEntity> ToEntity(this IEnumerable<TariffDto> list)
        => list.Select(e => e.ToEntity()).ToList();
    
    private static void UpdateAmounts(this TariffEntity tariff, StudentEntity student)
    {
        tariff.SetSubamount(student.discount);
        if (student.HasPayments(tariff.TariffId))
        {
            tariff.SubAmount = student.GetDebt(tariff.TariffId);
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

    public static List<DetailDto> MapToDto(this List<TariffEntity> list, bool isApplyArrears)
    {
        List<DetailDto> result = [];
        
        foreach (var item in list)
        {
            if (!isApplyArrears)
            {
                item.Arrears = 0;
                item.ComputeTotal();
            }
            
            result.Add(new DetailDto
            {
                tariffId = item.TariffId,
                amount = item.Total,
                applyArrears = isApplyArrears
            });
        }

        return result;
    }
}
