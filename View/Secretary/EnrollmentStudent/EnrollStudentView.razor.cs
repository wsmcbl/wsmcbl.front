using Microsoft.AspNetCore.Components;
using wsmcbl.front.Controller;
using wsmcbl.front.View.Secretary.EnrollmentStudent.Dto;
using wsmcbl.front.View.Secretary.SchoolYears.Dto;
using wsmcbl.front.View.Shared;

namespace wsmcbl.front.View.Secretary.EnrollmentStudent;

public class EnrollStudent : ComponentBase
{
    [Parameter] 
    public string StudentId { get; set; }
    [Inject] protected IEnrollSudentController Controller { get; set; }
    [Inject] protected AlertService AlertService { get; set; }

    public StudentFullDto Student;
    
    protected string Padecimientos;
    protected string Religion;
    protected int Age;
    protected DateOnly Birthday;
    
    protected string Tutor;
    protected string FatherName;
    protected string MotherName;
    protected string PhoneNumbers;
    protected int Peso;
    protected double Altura;


    protected List<string> IsActive = new() { "Si", "No" };
    
    protected bool Sex;
    protected List<string> sexo = new() { "Femenino", "Masculino"};
    
    protected List<string> grade = new() { "Primero", "Segundo", "Tercero" };
    protected List<string> section = new() { "A", "B", "C" };
    
    protected override async Task OnParametersSetAsync()
    {
        try
        {
            Student = await Controller.GetInfoStudent(StudentId);
            Birthday = Student.birthday.ToDateOnly();
            Age = ConverDate(Student.birthday);
        }
        catch (Exception e)
        {
            AlertService.AlertError("Error", $"{e}");
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
    
    protected void GetSexo(ChangeEventArgs e)
    {
        var selectSexo = e.Value.ToString();

        if(selectSexo == "Femenino")
        {
            Sex = false;
        }else{
            Sex = true;
        }
    }
    protected void GetIsActive(ChangeEventArgs e)
    {
        var isActive = e.Value.ToString();

        if(isActive == "No")
        {
            Student.IsActive = false;
        }else{
            Student.IsActive = true;
        }
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