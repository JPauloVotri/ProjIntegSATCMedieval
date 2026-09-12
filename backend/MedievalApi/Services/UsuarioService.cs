using MedievalApi.Data;
using MedievalApi.DTOs.Usuario;
using MedievalApi.Models;
using MedievalApi.Models.Enums;
using MedievalApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MedievalApi.Services;

public class UsuarioService(AppDbContext appDbContext) : IUsuarioService
{
    private readonly AppDbContext context = appDbContext;

    public async Task<UsuarioResponse> CreateAsync(UsuarioCreateRequest request)
    {
        var emailNormalizado = Usuario.NormalizeEmail(request.Email);

        if (await context.Usuarios.AnyAsync(u => u.Email == emailNormalizado))
            throw new InvalidOperationException("E-mail já cadastrado.");

        var usuario = new Usuario(request.Nome, request.Email, request.Senha, request.GrupoUsuario);

        context.Usuarios.Add(usuario);
        await context.SaveChangesAsync();

        return ToResponse(usuario);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var usuario = await context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
        if (usuario == null) return false;

        context.Usuarios.Remove(usuario);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<UsuarioResponse>> GetAllAsync()
    {
        var usuarios = await context.Usuarios
            .AsNoTracking()
            .OrderBy(u => u.Nome)
            .ToListAsync();

        return usuarios.Select(ToResponse);
    }

    public async Task<UsuarioResponse?> GetByIdAsync(Guid id)
    {
        var usuario = await context.Usuarios.AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id);

        return usuario == null ? null : ToResponse(usuario);
    }

    public async Task<UsuarioResponse?> UpdateAsync(Guid id, UsuarioUpdateRequest request)
    {
        var usuario = await context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
        if (usuario == null) return null;

        var emailNormalizado = Usuario.NormalizeEmail(request.Email);
        if (await context.Usuarios.AnyAsync(u => u.Email == emailNormalizado && u.Id != id))
            throw new InvalidOperationException("E-mail já cadastrado por outro usuário.");

        usuario.Nome = request.Nome;
        usuario.Email = request.Email;
        usuario.GrupoUsuario = request.GrupoUsuario;
        usuario.Status = request.Status;

        await context.SaveChangesAsync();
        return ToResponse(usuario);
    }

    private static UsuarioResponse ToResponse(Usuario u) => new(
        u.Id, u.Nome, u.Email, u.GrupoUsuario, u.Status, u.UltimoLogin, u.CriadoEm, u.AtualizadoEm
    );
}
