using System.Threading.Tasks;
using AutoMapper;
using DesafioManipulae.API.Dtos;
using DesafioManipulae.Domain;
using DesafioManipulae.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DesafioManipulae.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VideoController : ControllerBase
    {
        public readonly IDesafioManipulaeRepository _repository;
        public readonly IMapper _mapper;
        public VideoController(IDesafioManipulaeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            return Ok();
        }

        #region Get By Id
        [HttpGet("{IdVideo:int}")]
        public async Task<IActionResult> Get(int IdVideo)
        {
            try
            {
                var videoDetalhe = await _repository.GetVideoDetalhe(IdVideo);
                if (videoDetalhe == null) return NotFound();
                var result = _mapper.Map<VideoDetalheDto>(videoDetalhe);
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
        public async Task<IActionResult> Post(VideoDetalheDto model)
        {
            try
            {
                var videoDetalhe = _mapper.Map<VideoDetalhe>(model);
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
        public async Task<IActionResult> Put(int IdVideo, VideoDetalheDto model)
        {
            try
            {
                var videoDetalhe = await _repository.GetVideoDetalhe(IdVideo);

                if (videoDetalhe == null) return NotFound();

                _mapper.Map(model, videoDetalhe);
                _repository.Update(videoDetalhe);

                if (await _repository.SaveChangesAsync()) return Created($"", model);
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
                var evento = await _repository.GetVideoDetalhe(IdVideo);

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