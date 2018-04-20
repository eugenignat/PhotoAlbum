using PhotoAlbum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoAlbum.Interfaces
{
    public interface IApiClient
    {
        Task<List<Album>> GetAllAlbums(string path);

        Task<List<Photo>> GetAllPhotosFromAlbum(string path, long albumId);
    }
}
