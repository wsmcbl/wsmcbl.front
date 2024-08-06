namespace wsmcbl.front.View.Secretary.SchoolYears.Dto;

public class EnrollmentToCreateDto
{
    public string degreeId { get; set; }
    public int quantity { get; set; }

    public EnrollmentToCreateDto(string degreeId, int quantity)
    {
        this.degreeId = degreeId;
        this.quantity = quantity;
    }
}