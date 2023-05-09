namespace TheFarmingGame.Domains
{
    public class Land
    {
        public Land() {
            Alias = null;
            Level = 0;
            Plant = 0;
            HarvestTime = null;
            IsProtected = false;
            BidTime = null;
        }
        public int Id { get; set; }
        public string? Alias { get; set; }
        public int Level { get; set; }
        public int Plant { get; set; }
        public DateTime? HarvestTime { get; set; }
        public bool IsProtected { get; set; }
        public int? UserId { get; set; }
        public string? UserAlias { get; set; }
        public DateTime? BidTime { get; set; }
    }
}
