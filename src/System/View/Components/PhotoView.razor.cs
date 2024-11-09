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
        protected string ImgSrc { get; set; } = "/img/Placeholder/Man.png";

        private StringBuilder imageBase64Builder = new StringBuilder();

        protected override void OnParametersSet()
        {
            if (Student.profilePicture != null)
            {
                ImgSrc = Student.profilePicture;
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
                StateHasChanged();
            }
        }
       ///////////////////////////////////////////////////////
       
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
       public async Task ImageTransferComplete()       {
           var base64Image = imageBase64Builder.ToString();
           ImgSrc = imageBase64Builder.ToString();
           imageBase64Builder.Clear();
           await JS.InvokeVoidAsync("stopCamera");
       }
        
        
        
        
        
        
        
        
    }
}