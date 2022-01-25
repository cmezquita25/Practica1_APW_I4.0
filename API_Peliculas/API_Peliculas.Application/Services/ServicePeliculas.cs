using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_Peliculas.Domain.Entities;
using API_Peliculas.Domain.Interfaces;

namespace API_Peliculas.Application.Services
{
    public class ServicePeliculas : services
    {
        public bool ValidarPelicula (Pelicula pelicula)
        {
            if(string.IsNullOrEmpty(pelicula.Titulo))
                return false;

            if(string.IsNullOrEmpty(pelicula.Director))
                return false;

            if(string.IsNullOrEmpty(pelicula.Genero))
                return false;

            if(string.IsNullOrEmpty(pelicula.FechaPublicacion))
                return false;

            return true;
        }

        public bool ActualizarPelicula (Pelicula pelicula)
        {
            if(string.IsNullOrEmpty(pelicula.Titulo))
                return false;

            if(string.IsNullOrEmpty(pelicula.Director))
                return false;

            if(string.IsNullOrEmpty(pelicula.Genero))
                return false;

            if(string.IsNullOrEmpty(pelicula.FechaPublicacion))
                return false;

            return true;
        }
    }
}