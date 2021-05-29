using System;
using System.Threading.Tasks;
using DesafioManipulae.Domain;

namespace DesafioManipulae.Repository
{
    public interface IDesafioManipulaeRepository
    {
        void Add<T>(T entity) where T : class;
        void AddRange<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();
        Task<VideoList> GetVideo(int IdVideo);
        Task<VideoList[]> GetAllVideos();
        Task<VideoList[]> GetYoutubeApiVideos(int Duracao, string q);
        Task<VideoList[]> VideosSearch(string Titulo,int Duracao, string Autor,string q,string PublicadoEm);
    }
}