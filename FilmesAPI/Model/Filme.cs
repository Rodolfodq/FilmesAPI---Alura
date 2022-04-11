using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FilmesAPI.Model
{
    public class Filme
    {
        [Key]
        [Required]
        public int id { get; set; }
        [Required(ErrorMessage = "O campo Titulo é obrigatório")]
        public string Titulo { get; set; }
        [Required(ErrorMessage = "O campo Diretor é obrigatório")]
        public string Diretor { get; set; }
        [StringLength(30, ErrorMessage ="O gênero não pode passar de 30 caracteres")]
        public string Genero { get; set; }
        [Range(1, 6000, ErrorMessage ="A duração deve ter 1 e no máximo 6000 minutos")]
        public int Duracao { get; set; }

        public int ClassificacaoEtaria { get; set; }
        [JsonIgnore]
        public virtual List<Sessao> Sessoes { get; set; }
    }
}
