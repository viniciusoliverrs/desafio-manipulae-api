using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using DesafioManipulae.API.Dtos;
using DesafioManipulae.Domain;
using DesafioManipulae.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DesafioManipulae.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VideoController : ControllerBase
    {
        public readonly IDesafioManipulaeRepository _repository;
        public readonly IMapper _mapper;
        private readonly IHttpClientFactory _clienteFactory;
        public VideoController(IDesafioManipulaeRepository repository, IMapper mapper, IHttpClientFactory clientFactory)
        {
            _repository = repository;
            _mapper = mapper;
            _clienteFactory = clientFactory;
        }

        #region  YoutubeApiVideos
        [HttpGet("YoutubeApiVideos")]
        [AllowAnonymous]
        public async Task<IActionResult> YoutubeApiVideos(int Duracao = 0, string q = "")
        {
            try
            {
                var videos = await _repository.GetYoutubeApiVideos(Duracao, q);
                return Ok(videos);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Ocorreu um erro na operação!");
            }
        }
        #endregion

        #region Filter
        [HttpGet("Filter")]
        [AllowAnonymous]
        public async Task<IActionResult> Filter(string Titulo = "", int Duracao = 0, string Autor = "", string q = "", string PublicadoEm = "")
        {
            try
            {
                var videos = await _repository.VideosSearch(Titulo, Duracao, Autor, q, PublicadoEm);
                return Ok(videos);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Ocorreu um erro na operação!");
            }
        }
        #endregion

        #region Get All
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            try
            {
                var videoDetalhe = await _repository.GetAllVideos();
                if (videoDetalhe == null) return NotFound();
                var result = _mapper.Map<IEnumerable<VideoListDto>>(videoDetalhe);
                return Ok(result);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Ocorreu um erro na operação!");
            }
        }
        #endregion

        #region Get By Id
        [HttpGet("{IdVideo:int}")]
        public async Task<IActionResult> Get(int IdVideo)
        {
            try
            {
                var videoDetalhe = await _repository.GetVideo(IdVideo);
                if (videoDetalhe == null) return NotFound();
                var result = _mapper.Map<VideoListDto>(videoDetalhe);
                return Ok(result);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Ocorreu um erro na operação!");
            }
        }
        #endregion

        #region Create
        [HttpPost]
        public async Task<IActionResult> Post(VideoListDto model)
        {
            try
            {
                var videoDetalhe = _mapper.Map<VideoList>(model);
                _repository.Add(videoDetalhe);
                if (await _repository.SaveChangesAsync()) return Created("", videoDetalhe);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Ocorreu um erro na operação!");
            }
            return BadRequest();
        }
        #endregion

        #region Edit
        [HttpPut("{IdVideo:int}")]
        public async Task<IActionResult> Put(int IdVideo, VideoListDto model)
        {
            try
            {
                var videoDetalhe = await _repository.GetVideo(IdVideo);

                if (videoDetalhe == null) return NotFound();

                _mapper.Map(model, videoDetalhe);
                _repository.Update(videoDetalhe);

                if (await _repository.SaveChangesAsync()) return Created("", model);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Ocorreu um erro na operação!");
            }
            return BadRequest();
        }
        #endregion

        #region Delete
        [HttpDelete("{IdVideo:int}")]
        public async Task<IActionResult> Delete(int IdVideo)
        {
            try
            {
                var evento = await _repository.GetVideo(IdVideo);

                if (evento == null) return NotFound();
                _repository.Delete(evento);
                if (await _repository.SaveChangesAsync()) return Ok();
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Ocorreu um erro na operação!");
            }
            return BadRequest();
        }
        #endregion
    }
}