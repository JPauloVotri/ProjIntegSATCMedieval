
import { useState } from "react";
import { apiFetch } from "../../../services/api";
import "../../styles/ModalCadastro.css";

interface ModalProps {
  aberta: boolean;
  onFechar: () => void;
  onConfirmar: () => void;
  pedidoSelecionado: number | null;
  produtoId: number | null;
}

interface Estoque {
  id: number;
  produtoId: number;
  quantidadeDisponivel: number;
  valorTotal: number;
  ultimaEntrada: string;
  ultimaSaida: string;
}

interface PedidoItem {
  id: number;
  pedidoCompraId: number;
  produtoId: number;
  quantidadeRecebida: number;
}

interface PedidoCompra {
  id: number;
  fornecedorId: number;
  usuarioId: number;
  cotacaoId: number | null;
  dataPedido: string;
  dataPrevistaEntrega: string;
  dataRecebimento: string | null;
  status: number;
  total: number;
  observacao: string;
}

export default function ModalConfirmarPedido({
  aberta,
  onFechar,
  onConfirmar,
  pedidoSelecionado,
  produtoId,
}: ModalProps) {

    const [erros, setErros] = useState<string[]>([]);
    const [sucesso, setSucesso] = useState("");

    if (!aberta) return null;


async function confirmarPedido(e: React.FormEvent<HTMLFormElement>) {
  e.preventDefault();

  if (pedidoSelecionado === null || produtoId === null) return;

  setErros([]);
  setSucesso("");

  try {
    const [estoques, itensPedido, pedidosCompra] = await Promise.all([
      apiFetch<Estoque[]>("/api/Estoques"),
      apiFetch<PedidoItem[]>("/api/PedidosCompraItens"),
      apiFetch<PedidoCompra[]>("/api/PedidosCompra")
    ]);

    const estoqueEncontrado = estoques.find(
      (e) => e.produtoId === produtoId
    );

    const itemPedido = itensPedido.find(
      (i) =>
        i.pedidoCompraId === pedidoSelecionado &&
        i.produtoId === produtoId
    );

    const pedidoEncontrado = pedidosCompra.find(
      (p) => p.id === pedidoSelecionado
    );

    if (!estoqueEncontrado || !itemPedido || !pedidoEncontrado) {
      throw new Error("Estoque, item do pedido ou pedido não encontrado.");
    }

    const quantidadeNova = estoqueEncontrado.quantidadeDisponivel + itemPedido.quantidadeRecebida;

    await apiFetch(`/api/Estoques/${estoqueEncontrado.id}`, {
      method: "PUT",
      body: JSON.stringify({
        produtoId: estoqueEncontrado.produtoId,
        quantidadeDisponivel: quantidadeNova,
        valorTotal: estoqueEncontrado.valorTotal,
        ultimaEntrada: estoqueEncontrado.ultimaEntrada,
        ultimaSaida: estoqueEncontrado.ultimaSaida
      }),
    });

    await apiFetch(`/api/PedidosCompra/${pedidoSelecionado}`, {
      method: "PUT",
      body: JSON.stringify({
        fornecedorId: pedidoEncontrado.fornecedorId,
        usuarioId: pedidoEncontrado.usuarioId,
        cotacaoId: pedidoEncontrado.cotacaoId,
        dataPedido: pedidoEncontrado.dataPedido,
        dataPrevistaEntrega: pedidoEncontrado.dataPrevistaEntrega,
        dataRecebimento: new Date().toISOString(),
        status: 0,
        total: pedidoEncontrado.total,
        observacao: pedidoEncontrado.observacao
      }),
    });

    setSucesso("Pedido confirmado com sucesso!");

  } catch (error) {
    console.error(error);
    setErros(["Não foi possível confirmar o pedido."]);
  }
}


  return (
    <div className="modal-overlay">
      <div className="modal-cadastro">

        <h2>Confirmar recebimento</h2>

        <p>
          Tem certeza que deseja confirmar a chegada deste pedido?
        </p>

        <p>
          Essa ação atualizará o estoque e finalizará o pedido.
        </p>

        <div className="modal-botoes">
          <button onClick={onFechar}>
            Cancelar
          </button>
          <form onSubmit={confirmarPedido}>
            <button type="submit">
              Confirmar chegada
            </button>
          </form>
        </div>
        <div>
            {erros.length > 0 && (
              <div className="mensagens-erro" role="alert">
                {erros.map((mensagem, index) => (
                  <p key={`${index}-${mensagem}`}>
                    {mensagem}
                  </p>
                ))}
              </div>
            )}

            {sucesso && (
              <p className="mensagem-sucesso" role="status">
                {sucesso}
              </p>
            )}
            </div>

      </div>
    </div>
  );
}
