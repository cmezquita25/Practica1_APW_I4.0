using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using  API_Peliculas.Domain.dtos.request;
using FluentValidation;

namespace API_Peliculas.Infraestructure.Validators
{
    public class PeliculasValidators : AbstractValidator<PeliculaRequest>
    {
        public PeliculasValidators()
        {
            RuleFor(p => p.Titulo).NotNull().NotEmpty().Length(5,30);
            RuleFor(p => p.Director).NotNull().NotEmpty();
            RuleFor(p => p.Genero).NotNull().NotEmpty().Length(5,20);
            RuleFor(p => p.Puntuacion).NotNull().NotEmpty();
            RuleFor(p => p.Rating).NotNull().NotEmpty();
            RuleFor(p => p.FechaPublicacion).NotNull().NotEmpty();
        }
    }
}