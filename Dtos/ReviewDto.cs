namespace CLH_Final_Project.Dtos
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Text { get; set; }
        public bool Seen { get; set; } = false;
    }
}
