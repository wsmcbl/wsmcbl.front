namespace wsmcbl.src.View.Secretary.Schoolyear;

public static class DtoMapper
{
    public static int AgeCompute(this DateOnly? birthday)
    {
        if (birthday == null)
        {
            return 0;
        }

        var today = DateOnly.FromDateTime(DateTime.Today);
        var age = today.Year - birthday.Value.Year;

        if (today < birthday.Value.AddYears(age))
        {
            age--;
        }

        return age;
    }
}