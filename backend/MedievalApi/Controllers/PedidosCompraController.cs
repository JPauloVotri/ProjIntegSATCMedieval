using MedievalApi.DTOs.PedidoCompra;
using MedievalApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MedievalApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PedidosCompraController(IPedidoCompraService service) : ControllerBase
{
    private readonly IPedidoCompraService service = service;

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PedidoCompraResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<PedidoCompraResponse>>> GetAll()
    {
        var pedidos = await service.GetAllAsync();
        return Ok(pedidos);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(PedidoCompraResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PedidoCompraResponse>> GetById(int id)
    {
        var pedido = await service.GetByIdAsync(id);
        if (pedido is null) return NotFound();

        return Ok(pedido);
    }

    [HttpPost]
    [ProducesResponseType(typeof(PedidoCompraResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<PedidoCompraResponse>> Create(PedidoCompraCreateRequest request)
    {
        var pedido = await service.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = pedido.Id }, pedido);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(PedidoCompraResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<PedidoCompraResponse>> Update(int id, PedidoCompraUpdateRequest request)
    {
        var pedido = await service.UpdateAsync(id, request);
        if (pedido is null) return NotFound();

        return Ok(pedido);
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
