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
    
    private static void UpdateState(this TariffEntity tariff, StudentEntity student)
    {
        tariff.UpdateDiscount(student.discount);
        if (student.HasPayments(tariff.TariffId))
        {
            tariff.Amount = student.GetDebt(tariff.TariffId);
            tariff.Discount = 0;
            tariff.Arrears = 0;
            tariff.ComputeTotal();
        }
    }

    // mejorar el nombre
    public static void ApplyDiscount(this IEnumerable<TariffEntity> list, StudentEntity student)
    {
        foreach (var item in list)
        {
            item.UpdateState(student);
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