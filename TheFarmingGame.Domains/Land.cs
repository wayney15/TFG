namespace TheFarmingGame.Domains
{
    public class Land
    {
        public Land() { }
        public int Id { get; set; }
        public string Alias { get; set; }
        public int level { get; set; }
        public string UserId { get; set; }
        public int Plant { get; set; }
        public DateTime HarvestTime { get; set; }
        public bool IsAutoHarvested { get; set; }
        public User User { get; set; }
    }
}
