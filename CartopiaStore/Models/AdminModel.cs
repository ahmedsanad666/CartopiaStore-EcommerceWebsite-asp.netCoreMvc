namespace CartopiaStore.Models
{
    public class AdminModel
    {
        public Category Categories { get; set; }
        public List<Product> Products { get; set; }
        public List<User>   Users { get; set; }
    }
}
