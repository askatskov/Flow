using Flow.Data;
using Flow.Domain;
using Flow.Dto;
using Flow.ServiceInterface;
using Microsoft.EntityFrameworkCore;

namespace Flow.Services
{
    public class MusicServices : IMusicServices
    {
        private readonly MusicContext _context;
        private readonly IFileServices _fileServices;

        public MusicServices(MusicContext context, IFileServices fileServices)
        {
            _context = context;
            _fileServices = fileServices;
        }
        public async Task<Music> DetailsAsync(Guid id)
        {
            var result = await _context.Musics
                .FirstOrDefaultAsync(x => x.ArtistId == id);
            return result;
        }

        public async Task<Music> Create(MusicDto dto)
        {
            Music music = new();

            music.ArtistId = Guid.NewGuid();
            music.Artist = dto.Artist;
            music.Song = dto.Song;
            music.Rating = dto.Rating;

            if (dto.Files != null)
            {
                _fileServices.UploadFileToDatabase(dto, music);
            }
            await _context.Musics.AddAsync(music);
            await _context.SaveChangesAsync();

            return music;
        }
        public async Task<Music> Delete(Guid id)
        {
            var result = await _context.Musics
                .FirstOrDefaultAsync(x => x.ArtistId == id);
            _context.Musics.Remove(result);
            await _context.SaveChangesAsync();

            return result;
        }
    }
}
