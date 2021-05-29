using System;

namespace DesafioManipulae.API.Dtos
{
    public class VideoListDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string Descricao { get; set; }
        public int Duracao { get; set; }
        public DateTime PublicadoEm { get; set; }
    }
}