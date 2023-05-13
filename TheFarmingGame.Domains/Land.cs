namespace TheFarmingGame.Domains
{
    public class Land
    {
        public Land() {
            Alias = null;
            Plant = 0;
            HarvestTime = null;
            IsProtected = false;
        }
        public int Id { get; set; }
        public string? Alias { get; set; }
        public int Plant { get; set; }
        public DateTime? HarvestTime { get; set; }
        public bool IsProtected { get; set; }
        public int? UserId { get; set; }
    }
}
