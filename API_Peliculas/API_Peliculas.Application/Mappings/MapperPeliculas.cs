using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using API_Peliculas.Domain.Entities;
using API_Peliculas.Domain.dtos.response;
using API_Peliculas.Domain.dtos.request;

namespace Peliculas.Application.Mappings
{
    public class MapperPeliculas : Profile
    {
        public MapperPeliculas()
        {
            CreateMap<Pelicula, PeliculasCreate>()

            .ForMember(Inf => Inf.InformacionPelicula, opt => opt.MapFrom(src => $"Título: {src.Titulo} Director: {src.Director}"))
            .ForMember(Inf => Inf.RatingPuntuacion, opt => opt.MapFrom(src => $"Puntuacion: {src.Puntuacion} Rating:  {src.Rating}"));

            CreateMap<PeliculaRequest, Pelicula>();
        }
    }
}