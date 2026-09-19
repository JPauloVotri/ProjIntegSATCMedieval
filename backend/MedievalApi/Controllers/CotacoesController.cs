using MedievalApi.DTOs.Cotacao;
using MedievalApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MedievalApi.Controllers;

[ApiController]
[Route("api/[controller]")]

public class CotacoesController(ICotacaoService service) : ControllerBase
{
    private readonly ICotacaoService service = service;
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CotacaoResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CotacaoResponse>>> GetAll()
    {
        var cotacaos = await service.GetAllAsync();
        return Ok(cotacaos);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(CotacaoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CotacaoResponse>> GetById(int id)
    {
        var cotacao = await service.GetByIdAsync(id);
        if (cotacao is null) return NotFound();

        return Ok(cotacao);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CotacaoResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<CotacaoResponse>> Create(CotacaoCreateRequest request)
    {
        var cotacao = await service.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = cotacao.Id }, cotacao);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(CotacaoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<CotacaoResponse>> Update(int id, CotacaoUpdateRequest request)
    {
        var cotacao = await service.UpdateAsync(id, request);
        if (cotacao is null) return NotFound();

        return Ok(cotacao);
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
