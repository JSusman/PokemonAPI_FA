using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;
using System.Net.WebSockets;

namespace PokemonReviewApp.Repository
{
    public class CatagoryRepository : ICatagoryRepository
    {
        private DataContext _context;
        public CatagoryRepository(DataContext context)
        {
            _context = context;
        }

        public bool CatagoryExists(int id)
        {
            return _context.Catagories.Any(c => c.Id == id);
        }
        public bool CreateCatagory(Catagory catagory)
        {
            _context.Add(catagory);
            return Save();
        }

        public ICollection<Catagory> GetCatagories()
        {
            return _context.Catagories.ToList();
        }

        public Catagory GetCatagory(int id)
        {
            return _context.Catagories.Where(e => e.Id == id).FirstOrDefault();
        }

        public ICollection<Pokemon> GetPokemonByCatagory(int catagoryId)
        {
            return _context.PokemonCatagories.Where(e => e.CatagoryId == catagoryId).Select(c => c.Pokemon).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0? true: false;
        }
    }
}
