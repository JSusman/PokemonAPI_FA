﻿using Microsoft.EntityFrameworkCore.ChangeTracking;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;
using System.Net.WebSockets;

namespace PokemonReviewApp.Repository
{
    public class PokemonRepository : IPokemonRepository
    {

        private readonly DataContext _context;

        public PokemonRepository(DataContext context) 
        { 
            _context = context;
        }

        public bool CreatePokemon(int ownerId, int catagoryId, Pokemon pokemon)
        {
            var pokemonOwnerEntity = _context.Owners.Where(a => a.Id == ownerId).FirstOrDefault();
            var catagory = _context.Catagories.Where(a => a.Id == catagoryId).FirstOrDefault();

            var pokemonOwner = new PokemonOwner()
            {
                Owner = pokemonOwnerEntity,
                Pokemon = pokemon,
            };

            _context.Add(pokemonOwner);

            var pokemonCatagory = new PokemonCatagory()
            {
                Catagory = catagory,
                Pokemon = pokemon,
            };

            _context.Add(pokemonCatagory);

            _context.Add(pokemon);

            return Save();
        }

        public Pokemon GetPokemon(int id)
        {
            return _context.Pokemon.Where(p => p.Id == id).FirstOrDefault();
        }

        public Pokemon GetPokemon(string name)
        {
            return _context.Pokemon.Where(p => p.Name == name).FirstOrDefault();
        }

        public decimal GetPokemonRating(int pokeId)
        {
            var review = _context.Reviews.Where(p => p.Pokemon.Id == pokeId);

            if(review.Count() <= 0)
                return 0;

            return ((decimal)review.Sum(r => r.Rating) / review.Count());
        }

        public ICollection<Pokemon> GetPokemons() 
        {
            return _context.Pokemon.OrderBy(p => p.Id).ToList();
        }

        public bool PokemonExists(int pokeId)
        {
            return _context.Pokemon.Any(p => p.Id == pokeId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdatePokemon(int ownerId, int catagoryId, Pokemon pokemon)
        {
            _context.Update(pokemon);
            return Save();

        }
    }
}
