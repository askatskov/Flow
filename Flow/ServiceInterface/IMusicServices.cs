using Flow.Domain;
using Flow.Dto;

namespace Flow.ServiceInterface
{
    public interface IMusicServices
    {
        Task<Music> Create(MusicDto dto);
        Task<Music> Delete(Guid id);
        Task<Music> DetailsAsync(Guid id);

    }
}
