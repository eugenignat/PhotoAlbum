using Newtonsoft.Json;
using PhotoAlbum.Interfaces;
using PhotoAlbum.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAlbum.Service
{
    public class ApiClient : IApiClient
    {
        public async Task<List<Album>> GetAllAlbums(string path)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage respose = await client.GetAsync(path);
            string jsonResponse = string.Empty;

            using (StreamReader reader = new StreamReader(await respose.Content.ReadAsStreamAsync()))
            {
                jsonResponse = await reader.ReadToEndAsync();
            }

            List<Album> albums = JsonConvert.DeserializeObject<List<Album>>(jsonResponse);

            return albums;
        }

        public async Task<List<Photo>> GetAllPhotosFromAlbum(string path, long albumId)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage respose = await client.GetAsync(path + albumId.ToString());
            
            string jsonResponse = string.Empty;

            using (StreamReader reader = new StreamReader(await respose.Content.ReadAsStreamAsync()))
            {
                jsonResponse = await reader.ReadToEndAsync();
            }

            List<Photo> photos = JsonConvert.DeserializeObject<List<Photo>>(jsonResponse);

            return photos;
        }
    }
}
