using MedievalApi.DTOs.CotacaoItem;
using MedievalApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MedievalApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CotacaoItensController(ICotacaoItemService service) : ControllerBase
{
    private readonly ICotacaoItemService service = service;

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CotacaoItemResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CotacaoItemResponse>>> GetAll()
    {
        var cotacaoItens = await service.GetAllAsync();
        return Ok(cotacaoItens);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(CotacaoItemResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CotacaoItemResponse>> GetById(int id)
    {
        var cotacaoItem = await service.GetByIdAsync(id);
        if (cotacaoItem is null) return NotFound();

        return Ok(cotacaoItem);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CotacaoItemResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<CotacaoItemResponse>> Create(CotacaoItemCreateRequest request)
    {
        var cotacaoItem = await service.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = cotacaoItem.Id }, cotacaoItem);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(CotacaoItemResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<CotacaoItemResponse>> Update(int id, CotacaoItemUpdateRequest request)
    {
        var cotacaoItem = await service.UpdateAsync(id, request);
        if (cotacaoItem is null) return NotFound();

        return Ok(cotacaoItem);
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
