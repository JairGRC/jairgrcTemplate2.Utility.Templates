namespace TemplateBaseMicroservice.Entities.Filter
{
    public record class BaseNameFilter(int ID);
    public record class BaseNameCreateDto
    {
        public string Nombre { get; init; }
        public int Edad { get; init; }
        public string Email { get; init; }
    };
    public record class BaseNameUpdateDto
    {
        public string Nombre { get; init; }
        public int Edad { get; init; }
        public string Email { get; init; }
    }



}
