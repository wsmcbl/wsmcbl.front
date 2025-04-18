using wsmcbl.src.Model.Academy;

namespace wsmcbl.src.View.Secretary.Degrees.Dto;

public class UpdateEnrollmentDto
{
    public string label { get; set; }
    public int capacity { get; set; }
    public int quantity { get; set; }
    public string section { get; set; }
    
    public UpdateEnrollmentDto(EnrollmentEntity value)
    {
        label = value.label;
        capacity = value.capacity;
        quantity = value.quantity;
        section = value.section!;
    }
}