using MedievalApi.Data;
using MedievalApi.DTOs.Usuario;
using MedievalApi.Exceptions;
using MedievalApi.Models;
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
            throw new ConflictException("E-mail já cadastrado.");

        var usuario = new Usuario(request.Nome, request.Email, request.Senha, request.GrupoUsuario);

        context.Usuarios.Add(usuario);

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateException ex) when (IsUniqueViolation(ex))
        {
            throw new ConflictException("E-mail já cadastrado.");
        }

        return ToResponse(usuario);
    }

    public async Task<bool> DeleteAsync(int id)
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

    public async Task<UsuarioResponse?> GetByIdAsync(int id)
    {
        var usuario = await context.Usuarios
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id);

        return usuario == null ? null : ToResponse(usuario);
    }

    public async Task<UsuarioResponse?> UpdateAsync(int id, UsuarioUpdateRequest request)
    {
        var usuario = await context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
        if (usuario == null) return null;

        var emailNormalizado = Usuario.NormalizeEmail(request.Email);

        if (await context.Usuarios.AnyAsync(u => u.Email == emailNormalizado && u.Id != id))
            throw new ConflictException("E-mail já cadastrado por outro usuário.");

        usuario.Nome = request.Nome;
        usuario.Email = request.Email;
        usuario.GrupoUsuario = request.GrupoUsuario;
        usuario.Status = request.Status;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateException ex) when (IsUniqueViolation(ex))
        {
            throw new ConflictException("E-mail já cadastrado por outro usuário.");
        }

        return ToResponse(usuario);
    }

    private static UsuarioResponse ToResponse(Usuario u) => new(
        u.Id, u.Nome, u.Email, u.GrupoUsuario, u.Status, u.UltimoLogin, u.CriadoEm, u.AtualizadoEm
    );

    private static bool IsUniqueViolation(DbUpdateException ex)
    {
        var inner = ex.InnerException?.Message ?? string.Empty;
        return inner.Contains("UNIQUE", StringComparison.OrdinalIgnoreCase)
            || inner.Contains("duplicate key", StringComparison.OrdinalIgnoreCase)
            || inner.Contains("23505", StringComparison.OrdinalIgnoreCase);
    }
}
