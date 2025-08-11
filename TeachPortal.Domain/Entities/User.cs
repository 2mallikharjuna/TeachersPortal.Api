namespace TeachersPortal.Api.Domain.Entities
{
    public class User : BaseEntity
    {
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string UserEmail { get; set; }
        public bool Administrator { get; set; }
    }
}
