using MedievalApi.DTOs.MovimentacaoEstoque;
using MedievalApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MedievalApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MovimentacoesEstoqueController(IMovimentacaoEstoqueService service) : ControllerBase
{
    private readonly IMovimentacaoEstoqueService service = service;

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<MovimentacaoEstoqueResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<MovimentacaoEstoqueResponse>>> GetAll()
    {
        var movimentacoes = await service.GetAllAsync();
        return Ok(movimentacoes);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(MovimentacaoEstoqueResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MovimentacaoEstoqueResponse>> GetById(int id)
    {
        var movimentacao = await service.GetByIdAsync(id);
        if (movimentacao is null) return NotFound();

        return Ok(movimentacao);
    }

    [HttpPost]
    [ProducesResponseType(typeof(MovimentacaoEstoqueResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<MovimentacaoEstoqueResponse>> Create(MovimentacaoEstoqueCreateRequest request)
    {
        var movimentacao = await service.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = movimentacao.Id }, movimentacao);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(MovimentacaoEstoqueResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<MovimentacaoEstoqueResponse>> Update(int id, MovimentacaoEstoqueUpdateRequest request)
    {
        var movimentacao = await service.UpdateAsync(id, request);
        if (movimentacao is null) return NotFound();

        return Ok(movimentacao);
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
