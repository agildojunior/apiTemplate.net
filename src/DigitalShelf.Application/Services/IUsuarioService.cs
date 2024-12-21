using DigitalShelf.Application.Dtos;
using DigitalShelf.Domain.Models;

namespace DigitalShelf.Application.Services
{
    public interface IUsuarioService
    {
        Task<IEnumerable<UsuarioDto>> GetAllAsync();
        Task<UsuarioDto> GetByIdAsync(int id);
        Task AddAsync(CreateUsuarioDto createUsuarioDto);
        Task UpdateAsync(int id, UsuarioDto usuarioDto);
        Task DeleteAsync(int id);
        Task<Usuario> GetByUsernameAndPassword(string username, string password);
    }
}