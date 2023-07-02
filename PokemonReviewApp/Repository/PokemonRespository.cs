using Microsoft.EntityFrameworkCore.ChangeTracking;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class PokemonRespository : IPokemonRepository
    {

        private readonly DataContext _context;

        public PokemonRespository(DataContext context) 
        { 
            _context = context;
        }

        public ICollection<Pokemon> GetPokemons() 
        {
            return _context.Pokemon.OrderBy(p => p.Id).ToList();
        }

    }
}
