using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Academy;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Secretary.Degrees.Dto;

namespace wsmcbl.src.View.Secretary.Degrees;

public class ConfigureGrade : BaseView
{
    [Parameter] public string EnrollmentNumber { get; set; }
    [Parameter] public string GradeId { get; set; }
    
    [Inject] protected Notificator Notificator { get; set; } = null!;
    [Inject] protected CreateOfficialEnrollmentController Controller { get; set; }

    protected int NumberEnrollment;
    protected int counter;
    protected int counter2;
    protected List<TeacherEntity> TeacherList;
    protected List<TeacherEntity> TeacherAvailable;
    protected DegreeEntity DegreeEntity; 

    
    protected override async Task OnParametersSetAsync()
    {
        counter = 0;
        counter2 = 0;
        DegreeEntity = await Controller.GetConfigureEnrollment(GradeId);
        
        TeacherList = await Controller.GetTeacherList();
        TeacherAvailable = TeacherList.Where(t => t.isGuide == false).ToList();
        
        NumberEnrollment = Convert.ToInt32(EnrollmentNumber);
    }

    protected void ViewSubjectTeacherAssigment()
    {
        foreach (var enrollment in DegreeEntity.EnrollmentList)
        {
            foreach (var item in enrollment.SubjectTeacherList)
            {
                Console.WriteLine(enrollment.EnrollmentId);
                Console.WriteLine(item.subject.Name);
                Console.WriteLine(item.teacher.teacherId);
                Console.WriteLine(item.teacher.fullName);
            }
        }
    }
    
    protected void OnTeacherChanged(EnrollmentEntity enrollment, SubjectEntity subject, string selectedTeacherId)
    {
        // Encuentra el índice del SubjectEntity dentro de SubjectTeacherList
        for (int i = 0; i < enrollment.SubjectTeacherList.Count; i++)
        {
            var tuple = enrollment.SubjectTeacherList[i];

            // Verifica si el SubjectEntity coincide
            if (tuple.subject.SubjectId == subject.SubjectId)
            {
                // Busca el TeacherEntity en la lista de maestros
                var selectedTeacher = TeacherList.FirstOrDefault(t => t.teacherId == selectedTeacherId);
            
                if (selectedTeacher != null)
                {
                    // Actualiza el elemento en la lista
                    enrollment.SubjectTeacherList[i] = (tuple.subject, selectedTeacher);

                    // Imprime los valores para verificar
                    Console.WriteLine(enrollment.EnrollmentId);
                    Console.WriteLine(tuple.subject.Name);
                    Console.WriteLine(selectedTeacher.teacherId);
                    Console.WriteLine(selectedTeacher.fullName);
                }
                break; // Sale del bucle una vez que encuentres la tupla correcta
            }
        }
    }
    
    protected override bool IsLoad()
    {
        return NumberEnrollment > 0 && DegreeEntity.EnrollmentList != null && TeacherList.Count != 0;
    }
    
}