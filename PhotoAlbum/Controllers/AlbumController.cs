using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PhotoAlbum.Config;
using PhotoAlbum.Interfaces;

namespace PhotoAlbum.Controllers
{
    public class AlbumController : Controller
    {
        IPhotoAlbumService _service;
        private readonly IOptions<ApiConfig> _apiConfig;

        public AlbumController(IPhotoAlbumService service, IOptions<ApiConfig> config)
        {
            _service = service;
            _apiConfig = config;
        }

        public async Task<IActionResult> ShowGrid()
        {
            return View();
        }

        public async Task<IActionResult> LoadData()
        {
            try {
                var result = _service.LoadAlbumsData(HttpContext, _apiConfig.Value.ApiAlbumPath);

                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(null);
            }
        }
    }
}