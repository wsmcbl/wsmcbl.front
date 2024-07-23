using Microsoft.AspNetCore.Components;

namespace wsmcbl.front.View.Academy.EnrollStudent;

public class EnrollStudent : ComponentBase
{
    protected string FirsName;
    protected string SecondName;
    protected string FirsSurName;
    protected string SecondSurName;
    protected string Padecimientos;
    protected string Religion;
    protected int Edad;
    protected DateTime Birthday;
    
    protected string Tutor;
    protected string FatherName;
    protected string MotherName;
    protected string PhoneNumbers;
    protected int Peso;
    protected double Altura;


    protected bool Repite;
    protected List<string> repite = new() { "No", "Si"};
    
    protected bool Sex;
    protected List<string> sexo = new() { "Femenino", "Masculino"};
    
    protected List<string> grade = new() { "Primero", "Segundo", "Tercero" };
    protected List<string> section = new() { "A", "B", "C" };
    
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
    
    protected void GetRepite(ChangeEventArgs e)
    {
        var selectRepite = e.Value.ToString();

        if(selectRepite == "No")
        {
            Sex = false;
        }else{
            Sex = true;
        }
    }
    
    
    protected void GetGrade(ChangeEventArgs e)
    {
        var selectGrade = e.Value.ToString();
        // Aquí puedes hacer lo que necesites con el ítem seleccionado
        Console.WriteLine("Option selected: " + selectGrade);
    }

   protected void GetSection(ChangeEventArgs e)
    {
        var selectSection = e.Value.ToString();
        // Aquí puedes hacer lo que necesites con el ítem seleccionado
        Console.WriteLine("Option selected: " + selectSection);
    }
   

    protected Task Save()
    {
        throw new NotImplementedException();
    }
}