using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PhotoAlbum.Config;
using PhotoAlbum.Interfaces;


namespace PhotoAlbum.Controllers
{
    public class PhotosController : Controller
    {
        IPhotoAlbumService _service;
        private readonly IOptions<ApiConfig> _apiConfig;

        public PhotosController(IPhotoAlbumService service, IOptions<ApiConfig> config)
        {
            _service = service;
            _apiConfig = config;
        }

        public IActionResult ShowGrid(long id)
        {
            ViewData["albumId"] = id;
            ViewBag.albumId = id;
            return View();
        }

        public async Task<IActionResult> LoadData()
        {
            try
            {
                var albumId = long.Parse(Request.Query["albumId"].FirstOrDefault());
                var result = _service.LoadPhotosFromAlbumData(HttpContext, _apiConfig.Value.ApiPhotosForAlbumPath, albumId);

                return Json(result);
            }
            catch(Exception ex)
            {
                return Json(null);
            }
        }
    }
}