using StudTeacher.DTO;
using System.Threading.Tasks;

namespace StudTeacher.Services
{
   public interface IAuthService
   {
       Task<AuthResponseDto?> RegisterAsync(RegisterDto dto);
       Task<AuthResponseDto?> LoginAsync(LoginDto dto);
   }
}
