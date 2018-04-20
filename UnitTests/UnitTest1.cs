using Microsoft.AspNetCore.Http;
using PhotoAlbum.Interfaces;
using PhotoAlbum.Service;
using System;
using Xunit;
using Moq;

namespace UnitTests
{
    public class UnitTest1
    {
        private readonly IApiClient _service;
        private const string albumsApiPath = "http://jsonplaceholder.typicode.com/albums";
        private const string photosApiPath = "http://jsonplaceholder.typicode.com/photos?albumId=";


        public UnitTest1()
        {
            _service = new ApiClient();
        }

        [Fact]
        public async void ThereAre100AlbumsWhichAreRetrieved()
        {
            var result = await _service.GetAllAlbums(albumsApiPath);

            Assert.Equal(100, result.Count);
        }

        [Fact]
        public async void ThereAre50PhotosWhichAreRetrieved()
        {
            var result = await _service.GetAllPhotosFromAlbum(photosApiPath, 1);

            Assert.Equal(50, result.Count);
        }
    }
}
