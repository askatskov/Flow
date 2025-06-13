using System.ComponentModel.DataAnnotations;

namespace Flow.Domain
{
	public class Music
	{
		[Key]
		public Guid ArtistId { get; set; }
		public string Artist { get; set; }
		public string Song { get; set; }
		public float Rating { get; set; }
	}
}

