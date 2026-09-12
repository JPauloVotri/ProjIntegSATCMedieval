using MedievalApi.Data;
using MedievalApi.DTOs.Usuario;
using MedievalApi.Models;
using MedievalApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MedievalApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController(IUsuarioService service) : ControllerBase
{
    private readonly IUsuarioService service = service;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UsuarioResponse>>> GetAll()
    {
        return Ok(await service.GetAllAsync());
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<UsuarioResponse>> GetById(Guid id)
    {
        var usuario = await service.GetByIdAsync(id);
        if (usuario == null) return NotFound();

        return Ok(usuario);
    }

    [HttpPost]
    public async Task<ActionResult<UsuarioResponse>> Create(UsuarioCreateRequest request)
    {
        try
        {
            var usuario = await service.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = usuario.Id }, usuario);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<UsuarioResponse>> Update(Guid id, UsuarioUpdateRequest request)
    {
        try
        {
            var usuario = await service.UpdateAsync(id, request);
            if (usuario == null) return NotFound();

            return Ok(usuario);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await service.DeleteAsync(id);
        if (!deleted) return NotFound();

        return NoContent();
    }
}
