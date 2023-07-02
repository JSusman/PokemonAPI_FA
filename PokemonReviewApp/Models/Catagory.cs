namespace PokemonReviewApp.Models
{
    public class Catagory
    {
        public int Id { get; set; } 
        
        public string Name { get; set; }
        
        public ICollection<PokemonCatagory>PokemonCatagories { get; set; }
    }
}
