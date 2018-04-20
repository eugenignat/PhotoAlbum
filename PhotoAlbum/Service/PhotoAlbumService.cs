using Microsoft.AspNetCore.Http;
using PhotoAlbum.Interfaces;
using PhotoAlbum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoAlbum.Service
{
    public class PhotoAlbumService : IPhotoAlbumService
    {
        HttpRequest _request;
        string   _draw,
                 _start,
                 _length,
                 _sortColumn,
                 _sortColumnDirection,
                 _searchValue;
        int _pageSize,
            _skip,
            _recordsTotal;

        IEnumerable<object> _data;

        public object LoadAlbumsData(HttpContext httpContext, string apiPath)
        {
            InitializeParametersFromContext(httpContext);

            IApiClient client = new ApiClient();
            Task<List<Album>> albums = client.GetAllAlbums(apiPath);
            var albumResult = albums.Result;

            _data = SortAndSearchResult(albumResult);

            return (new { draw = _draw, recordsFiltered = _recordsTotal, recordsTotal = _recordsTotal, data = _data });
        }

        public object LoadPhotosFromAlbumData(HttpContext httpContext, string apiPath, long albumId)
        {
            InitializeParametersFromContext(httpContext);

            IApiClient client = new ApiClient();
            Task<List<Photo>> photos = client.GetAllPhotosFromAlbum(apiPath, albumId);
            var photosResult = photos.Result;

            _data = SortAndSearchResult(photosResult);

            return (new { draw = _draw, recordsFiltered = _recordsTotal, recordsTotal = _recordsTotal, data = _data });
        }

        private void InitializeParametersFromContext(HttpContext httpContext)
        {
            _request = httpContext.Request;
            _draw = _request.Query["draw"].FirstOrDefault();

            // Skip number of Rows count  
            _start = _request.Query["start"].FirstOrDefault();

            // Paging Length 10,20  
            _length = _request.Query["length"].FirstOrDefault();

            // Sort Column Name  
            _sortColumn = _request.Query["columns[" + _request.Query["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();

            // Sort Column Direction (asc, desc)  
            _sortColumnDirection = _request.Query["order[0][dir]"].FirstOrDefault();

            // Search Value from (Search box)  
            _searchValue = _request.Query["search[value]"].FirstOrDefault();

            //Paging Size (10, 20, 50,100)  
            _pageSize = _length != null ? Convert.ToInt32(_length) : 0;

            _skip = _start != null ? Convert.ToInt32(_start) : 0;

            _recordsTotal = 0;
        }

        private List<T> SortAndSearchResult<T>(List<T> result)
        {
            if (!(string.IsNullOrEmpty(_sortColumn) && string.IsNullOrEmpty(_sortColumnDirection)))
            {
                if (!_sortColumnDirection.Equals("desc"))
                    result = result.OrderBy(a => a.GetType().GetProperty(_sortColumn)).ToList();
                else
                    result = result.OrderByDescending(a => a.GetType().GetProperty(_sortColumn)).ToList();
            }

            if (!string.IsNullOrEmpty(_searchValue))
            {
                if (result.First() is Album)
                {
                    result = (result as List<Album>).Where(m => m.title.Contains(_searchValue)).ToList() as List<T>;
                }
                else if(result.First() is Photo)
                {
                    result = (result as List<Photo>).Where(m => m.title.Contains(_searchValue)).ToList() as List<T>;
                }
                
            }

            //total number of rows count   
            _recordsTotal = result.Count();
            //Paging   
            var data = result.Skip(_skip).Take(_pageSize).ToList();

            return data;
        }
    }
}
