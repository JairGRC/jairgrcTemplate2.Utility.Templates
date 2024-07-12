namespace TemplateBaseMicroservice.Entities.Filter
{
    public record class EjemploFilter(int ID);
    public record class EjemploCreateDto
    {
        public string Nombre { get; init; }
        public int Edad { get; init; }
        public string Email { get; init; }
    };
    public record class EjemploUpdateDto
    {
        public string Nombre { get; init; }
        public int Edad { get; init; }
        public string Email { get; init; }
    }



}
