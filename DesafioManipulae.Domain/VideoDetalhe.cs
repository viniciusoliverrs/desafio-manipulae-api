using System;

namespace DesafioManipulae.Domain
{
    public class VideoDetalhe
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Duracao { get; set; }
        public DateTime? DataPublicado { get; set; }
    }
}