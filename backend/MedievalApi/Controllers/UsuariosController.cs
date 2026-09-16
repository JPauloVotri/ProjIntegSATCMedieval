using MedievalApi.DTOs.Usuario;
using MedievalApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MedievalApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController(IUsuarioService service) : ControllerBase
{
    private readonly IUsuarioService service = service;

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<UsuarioResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<UsuarioResponse>>> GetAll()
    {
        var usuarios = await service.GetAllAsync();
        return Ok(usuarios);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(UsuarioResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UsuarioResponse>> GetById(Guid id)
    {
        var usuario = await service.GetByIdAsync(id);
        if (usuario is null) return NotFound();

        return Ok(usuario);
    }

    [HttpPost]
    [ProducesResponseType(typeof(UsuarioResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<UsuarioResponse>> Create(UsuarioCreateRequest request)
    {
        var usuario = await service.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = usuario.Id }, usuario);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(UsuarioResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<UsuarioResponse>> Update(Guid id, UsuarioUpdateRequest request)
    {
        var usuario = await service.UpdateAsync(id, request);
        if (usuario is null) return NotFound();

        return Ok(usuario);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await service.DeleteAsync(id);
        if (!deleted) return NotFound();

        return NoContent();
    }
}
