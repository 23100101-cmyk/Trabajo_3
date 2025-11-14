using FluentValidation;
using Practica03.Core.DTOs;

namespace Practica03.Core.Validators
{
    // Validaciones básicas para ColaboradorDTO
    public class ColaboradorValidator : AbstractValidator<ColaboradorDTO>
    {
        public ColaboradorValidator()
        {
            RuleFor(x => x.NombreCompleto)
                .NotEmpty()
                .WithMessage("El nombre completo es obligatorio.");

            RuleForEach(x => x.Skills).ChildRules(skill =>
            {
                skill.RuleFor(s => s.SkillId).GreaterThan(0).WithMessage("SkillId inválido.");
                skill.RuleFor(s => s.NivelDominio).NotEmpty().WithMessage("NivelDominio es obligatorio.");
            });
        }
    }
}
