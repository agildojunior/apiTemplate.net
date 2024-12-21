using Microsoft.EntityFrameworkCore;
using DigitalShelf.Application.Dtos;
using DigitalShelf.Domain.Models;
using DigitalShelf.Infrastructure.Data;

namespace DigitalShelf.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly ApplicationDbContext _context;

        public UsuarioService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UsuarioDto>> GetAllAsync()
        {
            return await _context.Usuarios
                .Where(u => u.Status) 
                .Select(u => new UsuarioDto
                {
                    Id = u.Id,
                    User = u.User,
                    Status = u.Status,
                }).ToListAsync();
        }

        public async Task<UsuarioDto> GetByIdAsync(int id)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id && u.Status); 

            if (usuario == null)
            {
                return null;
            }

            return new UsuarioDto
            {
                Id = usuario.Id,
                User = usuario.User,
                Status = usuario.Status,
            };
        }

        public async Task AddAsync(CreateUsuarioDto createUsuarioDto)
        {
            var usuario = new Usuario
            {
                User = createUsuarioDto.User,
                Password = createUsuarioDto.Password,
                Status = createUsuarioDto.Status,
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, UsuarioDto usuarioDto)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) return;

            usuario.User = usuarioDto.User;
            usuario.Status = usuarioDto.Status;

            _context.Entry(usuario).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) return;

            usuario.Status = false; // Desativar o usuário
            _context.Entry(usuario).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<Usuario> GetByUsernameAndPassword(string username, string password)
        {
            Usuario user = await _context.Usuarios.FirstOrDefaultAsync(x => x.User == username && x.Password == password && x.Status); 

            if (user == null)
            {
                throw new Exception("Usuário não encontrado.");
            }

            return user;
        }
    }
}
