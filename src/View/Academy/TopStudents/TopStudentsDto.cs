namespace wsmcbl.src.View.Academy.TopStudents;

public class TopStudentsDto
{
    public string studentId { get; set; } = null!;
    public string fullName { get; set; } = null!;
    public List<Average> averageList { get; set; } = [];
    public decimal averageGrade { get; set; } 
}

public class Average()
{
    public int partialId { get; set; }
    public decimal grade { get; set; }
    public string label { get; set; } = "-";
}