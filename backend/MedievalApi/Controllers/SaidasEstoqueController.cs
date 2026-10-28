using MedievalApi.DTOs.SaidaEstoque;
using MedievalApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MedievalApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SaidasEstoqueController(ISaidaEstoqueService service) : ControllerBase
{
    private readonly ISaidaEstoqueService service = service;

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<SaidaEstoqueResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<SaidaEstoqueResponse>>> GetAll()
    {
        var saidas = await service.GetAllAsync();
        return Ok(saidas);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(SaidaEstoqueResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SaidaEstoqueResponse>> GetById(int id)
    {
        var saida = await service.GetByIdAsync(id);
        if (saida is null) return NotFound();

        return Ok(saida);
    }

    [HttpPost]
    [ProducesResponseType(typeof(SaidaEstoqueResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<SaidaEstoqueResponse>> Create(SaidaEstoqueCreateRequest request)
    {
        var saida = await service.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = saida.Id }, saida);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(SaidaEstoqueResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<SaidaEstoqueResponse>> Update(int id, SaidaEstoqueUpdateRequest request)
    {
        var saida = await service.UpdateAsync(id, request);
        if (saida is null) return NotFound();

        return Ok(saida);
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
