using Flow.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Flow.Data
{
	public class MusicContext : DbContext
	{
		public MusicContext(DbContextOptions<MusicContext> options) : base(options) { }
		public DbSet<Music> Musics { get; set; }
		public DbSet<FileToDatabase> FileToDatabase { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

		}
	}
}
