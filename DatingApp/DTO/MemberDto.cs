namespace DatingApp.DTO
{
    public class MemberDto
    {
        public string UserName { get; set; }
        // public string? Password { get; set; }
        public int Id { get; set; }
       
        //public string ImageUrl { get; set; }
        public string Age { get; set; }
        public string KnownAs { get; set; }
        public DateTime Created { get; set; } 
        public DateTime LastActive { get; set; } 
        public string Gender { get; set; }
        public string Introduction { get; set; }
        public string LookingFor { get; set; }
        public string Interests { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public ICollection<PhotoDto> Photos { get; set; }

    }

}
