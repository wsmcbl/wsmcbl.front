using System.Text.Json;
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

  public async Task<string> CreateNewStudent(NewStudentDto newStudent)
  {
    var resource = "/students";
    NewStudentDto Default = new();
    var studentJson = JsonSerializer.Serialize(newStudent);

    var response = await Consumer.PostAsync(Modules.Accounting, resource, newStudent, Default);
    return response.student.studentId;
  }
  
}