using System;

namespace DesafioManipulae.API.Dtos
{
    public class VideoDetalheDto
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Duracao { get; set; }
        public DateTime? DataPublicado { get; set; }
    }
}