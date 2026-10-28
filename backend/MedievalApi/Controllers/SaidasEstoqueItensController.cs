using MedievalApi.DTOs.SaidaEstoqueItem;
using MedievalApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MedievalApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SaidasEstoqueItensController(ISaidaEstoqueItemService service) : ControllerBase
{
    private readonly ISaidaEstoqueItemService service = service;

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<SaidaEstoqueItemResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<SaidaEstoqueItemResponse>>> GetAll()
    {
        var itens = await service.GetAllAsync();
        return Ok(itens);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(SaidaEstoqueItemResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SaidaEstoqueItemResponse>> GetById(int id)
    {
        var item = await service.GetByIdAsync(id);
        if (item is null) return NotFound();

        return Ok(item);
    }

    [HttpPost]
    [ProducesResponseType(typeof(SaidaEstoqueItemResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<SaidaEstoqueItemResponse>> Create(SaidaEstoqueItemCreateRequest request)
    {
        var item = await service.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(SaidaEstoqueItemResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<SaidaEstoqueItemResponse>> Update(int id, SaidaEstoqueItemUpdateRequest request)
    {
        var item = await service.UpdateAsync(id, request);
        if (item is null) return NotFound();

        return Ok(item);
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
