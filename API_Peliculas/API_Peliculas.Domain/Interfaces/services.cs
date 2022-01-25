using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_Peliculas.Domain.Entities;

namespace API_Peliculas.Domain.Interfaces
{
    public interface services
    {
        bool ValidarPelicula(Pelicula movie);
        bool ActualizarPelicula(Pelicula movie);
    }
}