using System.Security.Claims;
using BackEnd.Repository.Interfaces;

namespace BackEnd.Service
{
    public class AuthorizationService
    {
        private readonly IAsignaturaRepository asignaturaRepository;
        private readonly ITareaRepository tareaRepository;
        private readonly IPeriodoRepository periodoRepository;

        public AuthorizationService(
            IAsignaturaRepository asignaturaRepository,
            ITareaRepository tareaRepository,
            IPeriodoRepository periodoRepository)
        {
            this.asignaturaRepository = asignaturaRepository;
            this.tareaRepository = tareaRepository;
            this.periodoRepository = periodoRepository;
        }

        /// <summary>
        /// Obtiene el UsuarioId del usuario autenticado desde el token JWT
        /// </summary>
        public int GetAuthenticatedUserId(ClaimsPrincipal user)
        {
            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);
            
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
                return -1;

            return userId;
        }

        /// <summary>
        /// Valida que el usuario autenticado sea el propietario del Periodo
        /// </summary>
        public async Task<bool> IsUserPeriodoOwnerAsync(int periodoId, int authenticatedUserId)
        {
            var periodo = await periodoRepository.GetPeriodoByIdAsync(periodoId);
            return periodo != null && periodo.UsuarioId == authenticatedUserId;
        }

        /// <summary>
        /// Valida que el usuario autenticado sea el propietario de la Asignatura (a través del Periodo)
        /// </summary>
        public async Task<bool> IsUserAsignaturaOwnerAsync(int asignaturaId, int authenticatedUserId)
        {
            var asignatura = await asignaturaRepository.GetAsignaturaByIdAsync(asignaturaId);
            if (asignatura == null)
                return false;

            return await IsUserPeriodoOwnerAsync(asignatura.PeriodoId, authenticatedUserId);
        }

        /// <summary>
        /// Valida que el usuario autenticado sea el propietario de la Tarea (a través de la Asignatura y Periodo)
        /// </summary>
        public async Task<bool> IsUserTareaOwnerAsync(Guid tareaId, int authenticatedUserId)
        {
            var tarea = await tareaRepository.GetTareaByIdAsync(tareaId);
            if (tarea == null)
                return false;

            return await IsUserAsignaturaOwnerAsync(tarea.AsignaturaId, authenticatedUserId);
        }
    }
}
