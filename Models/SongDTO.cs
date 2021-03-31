using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicLibraryWebAPI.Models
{
    public class SongDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Album { get; set; }
        public string Artist { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Likes { get; set; }
    }
}
