using MedievalApi.DTOs.CotacaoItemFornecedor;
using MedievalApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MedievalApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CotacaoItemFornecedoresController(ICotacaoItemFornecedorService service) : ControllerBase
{
    private readonly ICotacaoItemFornecedorService service = service;

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CotacaoItemFornecedorResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CotacaoItemFornecedorResponse>>> GetAll()
    {
        var itens = await service.GetAllAsync();
        return Ok(itens);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(CotacaoItemFornecedorResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CotacaoItemFornecedorResponse>> GetById(int id)
    {
        var item = await service.GetByIdAsync(id);
        if (item is null) return NotFound();

        return Ok(item);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CotacaoItemFornecedorResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<CotacaoItemFornecedorResponse>> Create(CotacaoItemFornecedorCreateRequest request)
    {
        var created = await service.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(CotacaoItemFornecedorResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<CotacaoItemFornecedorResponse>> Update(int id, CotacaoItemFornecedorUpdateRequest request)
    {
        var updated = await service.UpdateAsync(id, request);
        if (updated is null) return NotFound();

        return Ok(updated);
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
