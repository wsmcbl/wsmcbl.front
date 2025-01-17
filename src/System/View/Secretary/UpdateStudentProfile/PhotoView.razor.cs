using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using wsmcbl.src.Controller.Service;
using wsmcbl.src.Model.Secretary;
using wsmcbl.src.Utilities;

namespace wsmcbl.src.View.Secretary.UpdateStudentProfile;

public partial class PhotoView : ComponentBase
{
    [Inject] protected IJSRuntime JS { get; set; } = null!;
    [Inject] protected ApiConsumer Consumer { get; set; } = null!;
    [Inject] public Notificator? Notificator { get; set; }
    [Parameter] public StudentEntity Student { get; set; } = null!;
    protected string ImgSrc { get; set; } = "/img/Placeholder/Man.png";
    private StringBuilder imageBase64Builder { get; set; } = new();
    private bool IsCamaraOpen { get; set; } = false;

    protected override Task OnParametersSetAsync()
    {
        if (Student.profilePicture != null)
        {
            ImgSrc = $"data:image/png;base64,{Student.profilePicture}";
        }

        return Task.CompletedTask;
    }

    private async Task SaveProfilePicture()
    {
        var base64Data = ImgSrc.Contains(',') ? ImgSrc.Split(',')[1] : ImgSrc;

        var imageBytes = Convert.FromBase64String(base64Data);

        using var content = new MultipartFormDataContent();
        var imageContent = new ByteArrayContent(imageBytes);
        imageContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");

        content.Add(imageContent, "profilePicture", "photo.jpg");

        var response = await Consumer.PutPhotoAsync(Modules.Secretary, $"students/{Student.studentId}", content);
        if (response)
        {
            await Notificator!.ShowSuccess("Se ha actualizado la imagen correctamente.");
        }
    }

    private async Task OpenFileDialog()
    {
        await JS.InvokeVoidAsync("openFileDialog");
    }

    protected async Task HandleFileChangeAsync(InputFileChangeEventArgs e)
    {
        var files = e.GetMultipleFiles();
        if (files.Count == 0)
        {
            await Notificator!.ShowError("No ha seleccionado ningún archivo.");
            return;
        }

        var file = files[0];

        var validImageTypes = new[] { "image/jpeg", "image/png", "image/jpg", "image/gif" };
        if (!validImageTypes.Contains(file.ContentType))
        {
            await Notificator!.ShowError("El archivo seleccionado no es una imagen válida.");
            return;
        }

        const long maxFileSize = 5 * 1024 * 1024;
        if (file.Size > maxFileSize)
        {
            await Notificator!.ShowError("La imagen seleccionada es demasiado grande. Máximo permitido: 5 MB.");
            return;
        }

        await using var stream = file.OpenReadStream(maxFileSize);
        using var memoryStream = new MemoryStream();
        await stream.CopyToAsync(memoryStream);
        var base64 = Convert.ToBase64String(memoryStream.ToArray());

        ImgSrc = $"data:{file.ContentType};base64,{base64}";
        Student.profilePicture = ImgSrc;
        StateHasChanged();
    }

    private async Task StartCamera()
    {
        await JS.InvokeVoidAsync("startCamera");
        IsCamaraOpen = true;
    }

    private async Task CaptureImage()
    {
        await JS.InvokeVoidAsync("captureAndSendImage", DotNetObjectReference.Create(this));
        await SaveProfilePicture();
        IsCamaraOpen = false;
    }

    [JSInvokable("ReceiveImageChunk")]
    public Task ReceiveImageChunk(string chunk)
    {
        imageBase64Builder.Append(chunk);
        return Task.CompletedTask;
    }

    [JSInvokable("ImageTransferComplete")]
    public async Task ImageTransferComplete()
    {
        ImgSrc = imageBase64Builder.ToString();
        Student.profilePicture = ImgSrc;
        imageBase64Builder.Clear();
        await JS.InvokeVoidAsync("stopCamera");
    }
}