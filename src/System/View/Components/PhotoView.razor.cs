using System.Text;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using wsmcbl.src.Model.Secretary;

namespace wsmcbl.src.View.Components
{
    public partial class PhotoView : ComponentBase
    {
        [Inject] protected IJSRuntime JS { get; set; } = null!;
         [Parameter] public StudentEntity? Student { get; set; }
        protected string ImgSrc { get; set; } = "/img/Placeholder/Man.png"; //Usado unicamente en el componente.
        private StringBuilder imageBase64Builder = new StringBuilder();

        protected override async Task OnParametersSetAsync()
        {
            if (Student.profilePicture != null)
            {
                ImgSrc = await ConvertIFormFileToBase64(Student.profilePicture);
            }
        }
        
        protected async Task OpenFileDialog()
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
                Student.profilePicture = ConvertBase64ToIFormFile(ImgSrc);
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
           var base64Image = imageBase64Builder.ToString();
           ImgSrc = imageBase64Builder.ToString();
           Student.profilePicture = ConvertBase64ToIFormFile(ImgSrc);
           imageBase64Builder.Clear();
           await JS.InvokeVoidAsync("stopCamera");
       }
       
       public IFormFile ConvertBase64ToIFormFile(string captureImage)
       {
           var base64Data = captureImage.Substring(captureImage.IndexOf(',') + 1);
           byte[] bytes = Convert.FromBase64String(base64Data);

           var stream = new MemoryStream(bytes);

           var file = new FormFile(stream, 0, bytes.Length, "file", "profile.png")
           {
               Headers = new HeaderDictionary(),
               ContentType = "image/png"
           };

           return file;
       }
        
       public async Task<string> ConvertIFormFileToBase64(IFormFile? file)
       {
           using (var memoryStream = new MemoryStream())
           {
               await file.CopyToAsync(memoryStream);
               byte[] fileBytes = memoryStream.ToArray();
               string base64String = Convert.ToBase64String(fileBytes);
               return $"data:{file.ContentType};base64,{base64String}";
           }
       }
        
        
        
        
        
        
    }
}