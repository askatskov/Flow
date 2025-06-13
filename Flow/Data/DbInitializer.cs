using Flow.Data;
using Flow.Domain;

namespace Flow.Data
{
	public class DbInitializer
	{
		public static void Initialize(MusicContext context)
		{
			if (context.Musics.Any())
			{
				return;
			}

			var musics = new Music[]
			{
				new Music()
				{
					ArtistId = Guid.NewGuid(),
					Artist = "YoungBoy Never Broke Again",
					Song = "Top Tingz",
					Rating = 10
				}
			};
		}
	}
}