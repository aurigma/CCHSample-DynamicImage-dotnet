using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Aurigma.AssetProcessor;
using System.IO;

namespace DesignBrowserMVC.Controllers
{
    public class ImagesPreviewController : Controller
    {
        private readonly IImageProcessorApiClient _imageProcessorApiClient;
        public ImagesPreviewController(IImageProcessorApiClient imageProcessorApiClient)
        {
            _imageProcessorApiClient = imageProcessorApiClient;
        }

        public async Task<Stream> Index(string id)
        {
            const string previewNamespace = "cchub-di-demoapp";
            const string previewName = "thumbnail";
            const int previewWidth = 600;
            const int previewHeight = 600;

            var previewBody = await _imageProcessorApiClient.PreparePreviewAsync(
                id,
                previewNamespace,
                previewName,
                previewWidth,
                previewHeight);

            return previewBody.Stream;
        }
    }
}
