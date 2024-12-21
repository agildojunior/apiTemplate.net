using DigitalShelf.Application.Dtos;
using DigitalShelf.Application.Services;
using DigitalShelf.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace DigitalShelf.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuariosController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioDto>>> GetUsuarios()
        {
            var usuarios = await _usuarioService.GetAllAsync();
            return Ok(usuarios);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioDto>> GetUsuario(int id)
        {
            var usuario = await _usuarioService.GetByIdAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> PostUsuario(CreateUsuarioDto createUsuarioDto)
        {
            await _usuarioService.AddAsync(createUsuarioDto);

            return Ok("Usuário cadastrado com sucesso!");
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, UsuarioDto usuarioDto)
        {
            await _usuarioService.UpdateAsync(id, usuarioDto);
            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            await _usuarioService.DeleteAsync(id);
            return NoContent();
        }

        [HttpPost("auth")]
        public async Task<IActionResult> Auth(string username, string password)
        {
            Usuario user = await _usuarioService.GetByUsernameAndPassword(username, password);

            if (user != null)
            {
                var token = TokenService.GenerateToken(user); // Passa o usuário autenticado para GenerateToken
                var userInfo = new
                {
                    Token = token,
                    User = new
                    {
                        Id = user.Id,
                        Username = user.User,
                        Status = user.Status,
                    }
                };
                return Ok(userInfo);
            }

            return BadRequest("Usuário ou senha inválidos.");
        }
    }
}