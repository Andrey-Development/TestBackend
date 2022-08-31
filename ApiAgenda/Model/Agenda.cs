namespace ApiAgenda.Model
{
    public class Agenda
    {
        public int? Id { get; set; }
        public string? Titulo { get; set; }
        public string? Descricao { get; set; }
        public string? Status { get; set; }
        public DateTime dataPrazo { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataConclusao { get; set; }
        public string? Responsavel { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime? Updated_at { get; set; }
    }
}