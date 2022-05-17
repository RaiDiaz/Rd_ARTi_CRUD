namespace Rd_ARTi_CRUD.Models
{
    public class postsModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Content { get; set; }
        public Status Status { get; set; }
        public string? Author { get; set; }

    }

    public enum Status
    {
        draft, published
    }
}