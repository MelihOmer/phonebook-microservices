namespace ContactService.Application.DTOs.Contact
{
    public record ContactUpdateDto
    {
        public Guid Id { get; init; }
        public string Firstname { get; init; }
        public string Lastname { get; init; }
        public string Company { get; init; }
        public ContactUpdateDto(Guid id, string firstname, string lastname, string company)
        {
            Id = id;
            Firstname = firstname;
            Lastname = lastname;
            Company = company;
        }

    }
}
