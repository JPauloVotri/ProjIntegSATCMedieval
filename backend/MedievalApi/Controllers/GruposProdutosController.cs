using MedievalApi.DTOs.GrupoProduto;
using MedievalApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MedievalApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GruposProdutosController(IGrupoProdutoService service) : ControllerBase
{
    private readonly IGrupoProdutoService service = service;

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<GrupoProdutoResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<GrupoProdutoResponse>>> GetAll()
    {
        var grupos = await service.GetAllAsync();
        return Ok(grupos);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(GrupoProdutoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GrupoProdutoResponse>> GetById(int id)
    {
        var grupo = await service.GetByIdAsync(id);
        if (grupo is null) return NotFound();

        return Ok(grupo);
    }

    [HttpPost]
    [ProducesResponseType(typeof(GrupoProdutoResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<GrupoProdutoResponse>> Create(GrupoProdutoCreateRequest request)
    {
        var grupo = await service.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = grupo.Id }, grupo);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(GrupoProdutoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<GrupoProdutoResponse>> Update(int id, GrupoProdutoUpdateRequest request)
    {
        var grupo = await service.UpdateAsync(id, request);
        if (grupo is null) return NotFound();

        return Ok(grupo);
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
