using MedievalApi.DTOs.PedidoCompraItem;
using MedievalApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MedievalApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PedidosCompraItensController(IPedidoCompraItemService service) : ControllerBase
{
    private readonly IPedidoCompraItemService service = service;

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PedidoCompraItemResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<PedidoCompraItemResponse>>> GetAll()
    {
        var itens = await service.GetAllAsync();
        return Ok(itens);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(PedidoCompraItemResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PedidoCompraItemResponse>> GetById(int id)
    {
        var item = await service.GetByIdAsync(id);
        if (item is null) return NotFound();

        return Ok(item);
    }

    [HttpPost]
    [ProducesResponseType(typeof(PedidoCompraItemResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<PedidoCompraItemResponse>> Create(PedidoCompraItemCreateRequest request)
    {
        var item = await service.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(PedidoCompraItemResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<PedidoCompraItemResponse>> Update(int id, PedidoCompraItemUpdateRequest request)
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
