using Microsoft.AspNetCore.Authorization;

namespace FilmesAPI.Authorization
{
    public class IdadeMinimaRequirements : IAuthorizationRequirement
    {
        public int IdadeMinima { get; set; }

        public IdadeMinimaRequirements(int idadeMinima)
        {
            IdadeMinima = idadeMinima;
        }
    }
}
