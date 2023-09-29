namespace WebClient.Domain.Entities.Abstractions
{
    public class BaseEntity
    {
        public long Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }
}
