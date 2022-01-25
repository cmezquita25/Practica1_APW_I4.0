//cSpell:disable
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using API_Peliculas.Infraestructure.Data;
using API_Peliculas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using API_Peliculas.Domain.Interfaces;

namespace API_Peliculas.Infrastructure.repositories
{
    public class RepositoriesSQL : repository
    {
        private readonly PeliculasmezqContext _Mcontext;
        public RepositoriesSQL(PeliculasmezqContext Mcontext)
        {
            _Mcontext = Mcontext;
        }
        #region Peticiones GET
        public async Task<IEnumerable<Pelicula>> TodasPeliculas()
        {
            var movies = _Mcontext.Peliculas.Select(m => m);
       
            return await movies.ToListAsync();
        }
        public async Task<Pelicula> BuscarID(int id)
        {
            var movies = await _Mcontext.Peliculas.FirstOrDefaultAsync(m => m.Id == id);
       
            return movies;
        }
        #endregion
        //Registrar una pelicula
        public async Task<int> CrearPelicula(Pelicula movie)
        {
            var entity = movie;
            await _Mcontext.Peliculas.AddAsync(entity);
            var rows = await _Mcontext.SaveChangesAsync();
            if(rows <= 0)
           
                throw new Exception("¡ERROR!: No se pudo registrar la pelicula...Verifique su información.");
           
           
            return entity.Id;
        }
        //Actualizar pelicula
        public async Task<bool> ActualizarPelicula(int id, Pelicula movie)
        {
            if(id <= 0 || movie == null)
                throw new ArgumentException("Falta informacion para poder realizar la modificacion");
            var entity = await BuscarID(id);
            entity.Titulo = movie.Titulo;
            entity.Director = movie.Director;
            entity.Genero = movie.Genero;
            entity.Puntuacion = movie.Puntuacion;
            entity.Rating = movie.Rating;
            entity.FechaPublicacion = movie.FechaPublicacion;
            _Mcontext.Update(entity);
            var rows = await _Mcontext.SaveChangesAsync();
            return rows > 0;
        }
        //Actualizar bloque de peliculas
    }
}