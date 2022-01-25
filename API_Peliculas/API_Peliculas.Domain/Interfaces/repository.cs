using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_Peliculas.Domain.Entities;

namespace API_Peliculas.Domain.Interfaces
{
    public interface repository
    {
        Task<IEnumerable<Pelicula>> TodasPeliculas();
        Task<Pelicula> BuscarID(int id);
        Task<int> CrearPelicula(Pelicula movie);
        Task<bool> ActualizarPelicula(int id, Pelicula movie);
    }
}