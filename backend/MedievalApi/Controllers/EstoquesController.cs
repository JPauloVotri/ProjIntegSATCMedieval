using MedievalApi.DTOs.Estoque;
using MedievalApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MedievalApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EstoquesController(IEstoqueService service) : ControllerBase
{
    private readonly IEstoqueService service = service;

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<EstoqueResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<EstoqueResponse>>> GetAll()
    {
        var estoques = await service.GetAllAsync();
        return Ok(estoques);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(EstoqueResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EstoqueResponse>> GetById(int id)
    {
        var estoque = await service.GetByIdAsync(id);
        if (estoque is null) return NotFound();

        return Ok(estoque);
    }

    [HttpPost]
    [ProducesResponseType(typeof(EstoqueResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<EstoqueResponse>> Create(EstoqueCreateRequest request)
    {
        var estoque = await service.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = estoque.Id }, estoque);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(EstoqueResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<EstoqueResponse>> Update(int id, EstoqueUpdateRequest request)
    {
        var estoque = await service.UpdateAsync(id, request);
        if (estoque is null) return NotFound();

        return Ok(estoque);
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
