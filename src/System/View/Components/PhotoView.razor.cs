using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using wsmcbl.src.Model.Secretary;
using wsmcbl.src.Utilities;

namespace wsmcbl.src.View.Components
{
    public partial class PhotoView : ComponentBase
    {
        [Inject] protected IJSRuntime JS { get; set; } = null!;
        [Inject] protected ApiConsumer Consumer { get; set; } = null!;
        [Parameter] public StudentEntity? Student { get; set; }
        protected string ImgSrc { get; set; } = "/img/Placeholder/Man.png"; //Usado unicamente en el componente.
        private StringBuilder imageBase64Builder = new StringBuilder();
        
        ///////////////////////////////////////////////////////////
        protected override async Task OnParametersSetAsync()
        {
            if (Student.profilePicture != null)
            {
                ImgSrc = $"data:image/png;base64,{Student.profilePicture}";
            }
        }
        private async Task OpenFileDialog()
        {
            await JS.InvokeVoidAsync("openFileDialog");
        }
        protected async Task HandleFileChangeAsync(InputFileChangeEventArgs e)
        {
            var files = e.GetMultipleFiles();
            if (files.Count > 0)
            {
                using var stream = files[0].OpenReadStream();
                using var memoryStream = new MemoryStream();
                await stream.CopyToAsync(memoryStream);
                var base64 = Convert.ToBase64String(memoryStream.ToArray());
                ImgSrc = $"data:image/png;base64,{base64}";
                Student.profilePicture = ImgSrc;
                StateHasChanged();
            }
        }
       private async Task StartCamera()
       {
           await JS.InvokeVoidAsync("startCamera");
       }
       protected async Task CaptureImage()
       {
           await JS.InvokeVoidAsync("captureAndSendImage", DotNetObjectReference.Create(this));
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
       
       /////////////////////////////////////////////////////////
       private async Task SendProfilePictureToApi()
       {
           if (Student?.studentId != null)
           {
               var base64Data = ImgSrc.Substring(ImgSrc.IndexOf(",") + 1);
               var imageBytes = Convert.FromBase64String(base64Data);

               using var content = new MultipartFormDataContent();
               using var imageContent = new ByteArrayContent(imageBytes);
               imageContent.Headers.ContentType = new MediaTypeHeaderValue("image/png");
               content.Add(imageContent, "profilePicture", "profile.png");

               bool response = false;
               response = await Consumer.PutAsync(Modules.Secretary,$"students/{Student.studentId}", content);
               
               if (response)
               {
                   Console.WriteLine("Imagen subida exitosamente.");
               }
               
           }
       }
       
       
       
       
    }
}