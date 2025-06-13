using Flow.Domain;
using Flow.Dto;

namespace Flow.ServiceInterface
{
    public interface IFileServices
    {
        void UploadFileToDatabase(MusicDto dto, Music music);
        Task<FileToDatabase> RemoveImageFromDatabase(FileToDatabase dto);
    }
}
