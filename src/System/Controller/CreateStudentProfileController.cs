using wsmcbl.src.Utilities;
using wsmcbl.src.View.Secretary.Profiles;

namespace wsmcbl.src.Controller;

public class CreateStudentProfileController
{
    private ApiConsumer Consumer;

    public CreateStudentProfileController(ApiConsumer consumer)
    {
        Consumer = consumer;
    }

    public async Task<string?> CreateNewStudent(NewStudentDto newStudent)
    {
        NewStudentDto Default = new();
        var response = await Consumer
            .PostAsync(Modules.Accounting, "/students", newStudent, Default);

        return response!.student.studentId;
    }
}