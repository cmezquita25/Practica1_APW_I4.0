using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Peliculas.Domain.dtos.response
{
    public class PeliculasCreate
    {
        public int Id {get; set;}
        public string InformacionPelicula {get; set;}
        public string RatingPuntuacion {get; set;}
        public string FechaDePublicacion { get; set; }
    }
}
