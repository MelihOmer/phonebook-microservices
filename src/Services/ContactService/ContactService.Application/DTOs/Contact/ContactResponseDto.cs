namespace ContactService.Domain.DTOs.Contact
{
    public record ContactResponseDto
    {
        public Guid Id { get; init; }
        public string Firstname { get; init; }
        public string Lastname { get; init; }
        public string Company { get; init; }
        public ContactResponseDto(Guid ıd, string firstname, string lastname, string company)
        {
            Id = ıd;
            Firstname = firstname;
            Lastname = lastname;
            Company = company;
        }

        

    }
}
