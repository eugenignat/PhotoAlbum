using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoAlbum.Interfaces
{
    public interface IPhotoAlbumService
    {
        object LoadAlbumsData(HttpContext httpContext, string apiPath);

        object LoadPhotosFromAlbumData(HttpContext httpContext, string apiPath, long albumId);
    }
}
