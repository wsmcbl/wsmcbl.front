using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Secretary.EnrollmentStudent.Dto;
using wsmcbl.src.View.Secretary.SchoolYears.Dto;

namespace wsmcbl.src.View.Secretary.EnrollmentStudent;

public class EnrollStudent : ComponentBase
{
    [Parameter] 
    public string StudentId { get; set; }
    [Inject] protected IEnrollSudentController Controller { get; set; }
    [Inject] protected Notificator Notificator { get; set; }

    public StudentFullDto Student;
    
    protected DateOnly Birthday;
    protected int Age;
    protected string Address;

    protected Parent MotherInfo;
    protected Parent FatherInfo;
    protected Tutor Tutor;
    
    protected string SelectSex;
    protected string SelectActive;
    
    protected List<string> grade = new() { "Primero", "Segundo", "Tercero" };
    protected List<string> section = new() { "A", "B", "C" };


    
    
    protected override async Task OnParametersSetAsync()
    {
        try
        {
            Student = await Controller.GetInfoStudent(StudentId);
            MotherInfo = new Parent();
            FatherInfo = new Parent();
            Tutor = new Tutor();
            SetStudentData();
        }
        catch (Exception e)
        {
            Notificator.AlertError($"{e}");
        }
    }
    
    protected void SetStudentData()
    {
        SelectSex = Student.Sex ? "true" : "false";
        SelectActive = Student.IsActive ? "true" : "false";
        foreach (var parent in Student.Parents)
        {
            if (parent.Sex)
            {
                FatherInfo = parent;
            }
            else
            {
                MotherInfo = parent;
            }
        }
            
        if (Student.birthday != null)
        {
            Birthday = Student.birthday.ToDateOnly();
            Age = ConverDate(Student.birthday);    
        }
        else
        {
            Birthday = Student.birthday.ToDateNow();
        }
    }
    private int ConverDate(Date birthDate)
    {
        var birthDateTime = new DateTime(birthDate.Year, birthDate.Month, birthDate.Day);
        var today = DateTime.Today;
        var age = today.Year - birthDateTime.Year;

        if (birthDateTime.Date > today.AddYears(-age))
        {
            age--;
        }

        return age;
    }
    protected void GetGrade(ChangeEventArgs e)
    {
        var selectGrade = e.Value.ToString();
        Console.WriteLine("Option selected: " + selectGrade);
    }
   protected void GetSection(ChangeEventArgs e)
    {
        var selectSection = e.Value.ToString();
        Console.WriteLine("Option selected: " + selectSection);
    }
    protected Task Save()
    {
        throw new NotImplementedException();
    }
}