namespace wsmcbl.src.View.Secretary.EnrollStudent.Dto;

public class EnrollmentsBasicDto
{
    public string enrollmentId { get; set; }
    public string label { get; set; }
    public string section { get; set; }
    public int capacity { get; set; }
    public int quantity { get; set; }

    public  EnrollmentsBasicDto()
    {
        enrollmentId = "";
        label = "Sin sección"; 
        section = "Sin asignar";
        capacity = 0;
        quantity = 0;
    }
}