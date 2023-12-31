﻿using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface ICatagoryRepository
    {

        ICollection<Catagory> GetCatagories();
        Catagory GetCatagory(int id);
        ICollection<Pokemon> GetPokemonByCatagory(int catagoryId);
        bool CatagoryExists(int id);
        bool CreateCatagory(Catagory catagory);
        bool UpdateCatagory(Catagory catagory);
        bool DeleteCatagory(Catagory catagory);
        bool Save();

    }
}
