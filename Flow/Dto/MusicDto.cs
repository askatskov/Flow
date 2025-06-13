namespace Flow.Dto
{
    public class MusicDto
    {
		public Guid ArtistId { get; set; }
		public string Artist { get; set; }
		public string Song { get; set; }
		public float Rating { get; set; }
		public List<IFormFile> Files { get; set; }
        public IEnumerable<FileToDatabaseDto> Image { get; set; } = new List<FileToDatabaseDto>();


    }
}
