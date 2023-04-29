namespace TheFarmingGame.Domains
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Alias { get; set; }
        public decimal Money { get; set; }
    }
}