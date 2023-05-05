
namespace TheFarmingGame.Domains
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Alias { get; set; }
        public int Money { get; set; }
        public int StealAmount { get; set; }
        public int ProtectAmount { get; set; }
        public List<Land> Lands { get; set; }
    }
}