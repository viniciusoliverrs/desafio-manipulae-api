using System.Collections.Generic;
using System.Linq;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using System.Threading.Tasks;
using DesafioManipulae.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;
using Newtonsoft.Json;

namespace DesafioManipulae.Repository
{
    public class DesafioManipulaeRepository : IDesafioManipulaeRepository
    {
        private readonly DesafioManipulaeContext _context;
        private List<VideoList> videos;
        private WebClient webClient;
        private string API_KEY = "AIzaSyAu7lH35RqFq0uiI6KVzRrZXzH7aKy4tkk";
        public DesafioManipulaeRepository(DesafioManipulaeContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }
        public void AddRange<T>(T entity) where T : class
        {
            _context.AddRange(entity);
        }
        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }
        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }

        public async Task<VideoList> GetVideo(int IdVideo)
        {
            IQueryable<VideoList> query = _context.VideosLists;
            query = query.OrderBy(c => c.Titulo).Where(c => c.Id == IdVideo);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<VideoList[]> GetAllVideos()
        {
            IQueryable<VideoList> query = _context.VideosLists;
            query = query.OrderBy(c => c.Titulo);
            return await query.ToArrayAsync();
        }

        public async Task<VideoList[]> GetYoutubeApiVideos(int Duracao, string q)
        {
            videos = new List<VideoList>();
            webClient = new WebClient();
            int VideoId = 1;
            webClient.Encoding = System.Text.Encoding.UTF8;
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = API_KEY,
                ApplicationName = this.GetType().ToString()
            });
            var searchListRequest = youtubeService.Search.List("snippet");
            searchListRequest.Q = "Manipulação de remédio"; // Replace with your search term.
            searchListRequest.MaxResults = 50;
            searchListRequest.PublishedAfter = Convert.ToDateTime("2020-01-01T00:00:00Z");

            // Call the search.list method to retrieve results matching the specified query term.
            var searchListResponse = await searchListRequest.ExecuteAsync();

            // Add each result to the appropriate list, and then display the lists of
            // matching videos.

            foreach (var searchResult in searchListResponse.Items)
            {
                if (searchResult.Id.Kind == "youtube#video")
                {
                    var jsonResponse = webClient.DownloadString($"https://www.googleapis.com/youtube/v3/videos?id={String.Format(searchResult.Id.VideoId)}&key={API_KEY}&part=contentDetails");
                    dynamic dynamicObject = JsonConvert.DeserializeObject(jsonResponse);
                    string tmp = dynamicObject.items[0].contentDetails.duration;
                    var duration = Convert.ToInt32(System.Xml.XmlConvert.ToTimeSpan(tmp).TotalMinutes);

                    var video = new VideoList()
                    {
                        Id = VideoId,
                        Titulo = searchResult.Snippet.Title,
                        Descricao = searchResult.Snippet.Description,
                        Duracao = duration,
                        Autor = searchResult.Snippet.ChannelTitle,
                        PublicadoEm = Convert.ToDateTime(searchResult.Snippet.PublishedAt)
                    };
                    VideoId++;
                    //Add(video);
                    //await SaveChangesAsync();
                    videos.Add(video);
                }
            }
            if (!string.IsNullOrEmpty(q))
                videos = videos.Where(v => v.Autor.ToLower().Contains(q.ToLower())).ToList();
            videos = videos.Where(v => v.Descricao.ToLower().Contains(q.ToLower())).ToList();
            videos = videos.Where(v => v.Titulo.ToLower().Contains(q.ToLower())).ToList();
            if (Duracao > 0)
                videos = videos.Where(v => v.Duracao >= Duracao).ToList();

            return videos.OrderBy(v => v.Id).ToArray();
        }
        public async Task<VideoList[]> VideosSearch(string Titulo, int Duracao, string Autor, string PublicadoEm, string q)
        {
            IQueryable<VideoList> query = _context.VideosLists;
            if (Duracao > 0)
                query = query.OrderBy(v => v.Duracao).Where(v => v.Duracao == Duracao);

            if (!string.IsNullOrEmpty(Autor))
                query = query.OrderBy(v => v.Autor).Where(v => v.Autor.ToLower().Contains(Autor.ToLower()));

            if (!string.IsNullOrEmpty(Titulo))
                query = query.OrderBy(v => v.Titulo).Where(v => v.Titulo.ToLower().Contains(Titulo.ToLower()));

            if (!string.IsNullOrEmpty(q))
                query = query.Where(v => v.Autor.ToLower().Contains(q.ToLower()) ||
                v.Descricao.ToLower().Contains(q.ToLower()) ||
                v.Titulo.ToLower().Contains(q.ToLower()));
            if (!string.IsNullOrEmpty(PublicadoEm))
                query = query.OrderBy(v => v.PublicadoEm).Where(v => DateTime.Compare(Convert.ToDateTime(PublicadoEm), Convert.ToDateTime(v.PublicadoEm)) < 0);

            return await query.ToArrayAsync();
        }
    }
}