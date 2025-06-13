using Flow.Models.Music;
using Microsoft.AspNetCore.Mvc;

namespace Flow.Models.Musics
{
    public class MusicIndexViewModel
    {
		public Guid ArtistId { get; set; }
		public string Artist { get; set; }
		public string Song { get; set; }
		public float Rating { get; set; }
		public List<IFormFile> Files { get; set; }
        public List<MusicImageViewModel> Image { get; set; } = new List<MusicImageViewModel>();
    }
}
