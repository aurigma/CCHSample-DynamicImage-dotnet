using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DesignBrowserMVC.Models;
using Aurigma.AssetStorage;
using Microsoft.Extensions.Options;
using Aurigma.AssetProcessor;

namespace DesignBrowserMVC.Controllers
{
    public class ImagesController : Controller
    {
        private readonly ILogger<ImagesController> _logger;
        private readonly IImagesApiClient _imagesApiClient;
        private readonly IImageProcessorApiClient _imageProcessorApiClient;

        private const string _path = "/DynamicImage";
        private readonly CustomersCanvasOptions _ccoptions;

        public ImagesController(
            ILogger<ImagesController> logger,
            IImagesApiClient imagesApiClient,
            IImageProcessorApiClient imageProcessorApiClient,
            IOptions<CustomersCanvasOptions> customersCanvasOptions)
        {
            _logger = logger;
            _imagesApiClient = imagesApiClient;
            _imageProcessorApiClient = imageProcessorApiClient;
            _ccoptions = customersCanvasOptions.Value;
        }

        public async Task<IActionResult> Index()
        {
            var path = _path;
            // get list of images from folder
            var folder = await _imagesApiClient.GetFolderAsync(path);
            return View("FolderView", new DesignsFolderModel(path, folder));
        }
        public IActionResult Edit(string id, string name)
        {
            ViewBag.TenantId = _ccoptions.TenantId;
            ViewBag.DynamicImageVersion = _ccoptions.DynamicImageVersion;
            return View("Edit", new ItemModel(id, name));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
