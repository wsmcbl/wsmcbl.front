using wsmcbl.src.Controller.Service;
using wsmcbl.src.View.Secretary.Profiles;

namespace wsmcbl.src.Controller;

public class CreateStudentProfileController
{
    private readonly ApiConsumerWithNotificator _apiConsumer;

    public CreateStudentProfileController(ApiConsumerWithNotificator apiConsumer)
    {
        _apiConsumer = apiConsumer;
    }

    public async Task<string?> CreateNewStudent(StudentToCreateDto studentToCreate)
    {
        StudentToCreateDto Default = new();
        Default.SetAsDefault();

        studentToCreate.checkData();
        var response = await _apiConsumer
            .PostAsync(Modules.Accounting, "/students", studentToCreate, Default);

        return response.student.studentId;
    }
}