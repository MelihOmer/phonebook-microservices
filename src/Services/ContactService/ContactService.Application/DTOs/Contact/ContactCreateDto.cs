namespace ContactService.Application.DTOs.Contact
{
    public record ContactCreateDto
    {
        public string Firstname { get; init; }
        public string Lastname { get; init; }
        public string Company { get; init; }
        public ContactCreateDto(string firstname, string lastname, string company)
        {
            Firstname = firstname;
            Lastname = lastname;
            Company = company;
        }

    }
}
