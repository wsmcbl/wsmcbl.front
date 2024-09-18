namespace wsmcbl.src.View.Secretary.EnrollmentStudent.Dto;

public class StudentFullDto
{
    public string studentId { get; set; }
    public string name { get; set; }
    public string secondName { get; set; }
    public string surname { get; set; }
    public string secondSurname { get; set; }
    public bool sex { get; set; }
    public DateTime? birthday { get; set; }
    public string religion { get; set; }
    public string diseases { get; set; }
    
    public string address { get; set; }
    public bool isActive { get; set; }
    public File file { get; set; }
    public Tutor tutor { get; set; }
    public List<Parent> parents { get; set; }
    public Measurements measurements { get; set; }

    public StudentFullDto()
    {
        parents =
        [
            new Parent(),
            new Parent()
        ];
        tutor = new Tutor();
        measurements = new Measurements();
        file = new File();
    }
}
public class Parent
{
    public string parentId { get; set; }
    public bool sex { get; set; }
    public string name { get; set; }
    public string idCard { get; set; }
    public string occupation { get; set; }
}
public class Tutor
{ 
    public string tutorId { get; set; }
    public string name { get; set; }
    public string phone { get; set; }
}
public class Measurements
{ 
    public int measurementId { get; set; }
    public int height { get; set; }
    public int weight { get; set; }
}
public class File
{
    public int fileId { get; set; }
    public bool transferSheet { get; set; }
    public bool birthDocument { get; set; }
    public bool parentIdentifier { get; set; }
    public bool updatedGradeReport { get; set; }
    public bool conductDocument { get; set; }
    public bool financialSolvency { get; set; }
}