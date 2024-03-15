namespace wsmcbl.front.Models
{
    public class StudentEntity
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? SecondName { get; set; }
        public string? Surname { get; set; }
        public string? SecondSurname { get; set; }
        public bool Sex { get; set; }
        public DateTime Birthday { get; set; }
    }
}