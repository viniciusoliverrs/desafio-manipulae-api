using System;

namespace DesafioManipulae.API.Dtos
{
    public class VideoListDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Duracao { get; set; }
        public DateTime? PublicadoEm { get; set; }
    }
}