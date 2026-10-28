using MedievalApi.DTOs.UnidadeMedida;
using MedievalApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MedievalApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UnidadesMedidaController(IUnidadeMedidaService service) : ControllerBase
{
    private readonly IUnidadeMedidaService service = service;

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<UnidadeMedidaResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<UnidadeMedidaResponse>>> GetAll()
    {
        var unidades = await service.GetAllAsync();
        return Ok(unidades);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(UnidadeMedidaResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UnidadeMedidaResponse>> GetById(int id)
    {
        var unidade = await service.GetByIdAsync(id);
        if (unidade is null) return NotFound();

        return Ok(unidade);
    }

    [HttpPost]
    [ProducesResponseType(typeof(UnidadeMedidaResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<UnidadeMedidaResponse>> Create(UnidadeMedidaCreateRequest request)
    {
        var unidade = await service.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = unidade.Id }, unidade);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(UnidadeMedidaResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<UnidadeMedidaResponse>> Update(int id, UnidadeMedidaUpdateRequest request)
    {
        var unidade = await service.UpdateAsync(id, request);
        if (unidade is null) return NotFound();

        return Ok(unidade);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await service.DeleteAsync(id);
        if (!deleted) return NotFound();

        return NoContent();
    }
}
