using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoAlbum.Models
{
    public class Photo
    {
        public long id;

        public long albumId;
        
        public string title;

        public string url;

        public string thumbnailUrl;
    }
}
