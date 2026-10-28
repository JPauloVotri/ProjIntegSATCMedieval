using MedievalApi.DTOs.Fornecedor;
using MedievalApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MedievalApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FornecedoresController(IFornecedorService service) : ControllerBase
{
    private readonly IFornecedorService service = service;

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<FornecedorResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<FornecedorResponse>>> GetAll()
    {
        var fornecedores = await service.GetAllAsync();
        return Ok(fornecedores);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(FornecedorResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<FornecedorResponse>> GetById(int id)
    {
        var fornecedor = await service.GetByIdAsync(id);
        if (fornecedor is null) return NotFound();

        return Ok(fornecedor);
    }

    [HttpPost]
    [ProducesResponseType(typeof(FornecedorResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<FornecedorResponse>> Create(FornecedorCreateRequest request)
    {
        var fornecedor = await service.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = fornecedor.Id }, fornecedor);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(FornecedorResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<FornecedorResponse>> Update(int id, FornecedorUpdateRequest request)
    {
        var fornecedor = await service.UpdateAsync(id, request);
        if (fornecedor is null) return NotFound();

        return Ok(fornecedor);
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
