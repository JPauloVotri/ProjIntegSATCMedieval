
import { useState } from "react";
import { IoClose } from "react-icons/io5";
import { apiFetch } from "../../../services/api";

import "../../styles/modalObs.css";
import "../../styles/modalCriacaoMensagem.css";

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
  const [carregando, setCarregando] = useState(false);

  if (!aberta) return null;

  function fecharModal() {
    if (carregando) return;

    setErros([]);
    setSucesso("");
    onFechar();
  }

  async function confirmarPedido(e: React.FormEvent<HTMLFormElement>) {
    e.preventDefault();

    if (pedidoSelecionado === null || produtoId === null) {
      setErros(["Não foi possível identificar o pedido ou produto."]);
      return;
    }

    setErros([]);
    setSucesso("");
    setCarregando(true);

    try {
      const [estoques, itensPedido, pedidosCompra] = await Promise.all([
        apiFetch<Estoque[]>("/api/Estoques"),
        apiFetch<PedidoItem[]>("/api/PedidosCompraItens"),
        apiFetch<PedidoCompra[]>("/api/PedidosCompra"),
      ]);

      const estoqueEncontrado = estoques.find(
        (estoque) => estoque.produtoId === produtoId
      );

      const itemPedido = itensPedido.find(
        (item) =>
          item.pedidoCompraId === pedidoSelecionado &&
          item.produtoId === produtoId
      );

      const pedidoEncontrado = pedidosCompra.find(
        (pedido) => pedido.id === pedidoSelecionado
      );

      if (!estoqueEncontrado || !itemPedido || !pedidoEncontrado) {
        throw new Error(
          "Estoque, item do pedido ou pedido não encontrado."
        );
      }

      const quantidadeNova = estoqueEncontrado.quantidadeDisponivel + itemPedido.quantidadeRecebida;

      await apiFetch(`/api/Estoques/${estoqueEncontrado.id}`, {
        method: "PUT",
        body: JSON.stringify({
          produtoId: estoqueEncontrado.produtoId,
          quantidadeDisponivel: quantidadeNova,
          valorTotal: estoqueEncontrado.valorTotal,
          ultimaEntrada: estoqueEncontrado.ultimaEntrada,
          ultimaSaida: estoqueEncontrado.ultimaSaida,
        }),
      });

      await apiFetch(`/api/PedidosCompra/${pedidoSelecionado}`, {
        method: "PUT",
        body: JSON.stringify({
          fornecedorId: pedidoEncontrado.fornecedorId,
          usuarioId: pedidoEncontrado.usuarioId,
          cotacaoId: pedidoEncontrado.cotacaoId,
          dataPedido: new Date(pedidoEncontrado.dataPedido),
          dataPrevistaEntrega: new Date(pedidoEncontrado.dataPrevistaEntrega),
          dataRecebimento: new Date().toISOString(),
          status: 0,
          total: pedidoEncontrado.total,
          observacao: pedidoEncontrado.observacao,
        }),
      });

      setSucesso("Pedido confirmado com sucesso!");

      onConfirmar();
    } catch (error) {
      console.error(error);

      const mensagem =
        error instanceof Error
          ? error.message
          : "Não foi possível confirmar o pedido.";

      setErros(mensagem.split("\n"));
    } finally {
      setCarregando(false);
    }
  }

  return (
    <div className="modal-overlay" onClick={fecharModal}>
      <div
        className="modal-caixa modal-usuario modal-confirmar-pedido"
        onClick={(e) => e.stopPropagation()}
      >
        <div className="modal-usuario-cabecalho">
          <div className="modal-usuario-titulo">
            <div className="modal-titulos-2">
              <h2>Confirmar recebimento</h2>
            </div>
          </div>

          <button
            type="button"
            className="modal-close"
            onClick={fecharModal}
            disabled={carregando}
          >
            <IoClose />
          </button>
        </div>

        <form
          className="modal-usuario-form"
          onSubmit={confirmarPedido}
        >
          <div className="confirmar-pedido-conteudo">
            <h3>Deseja confirmar a chegada deste pedido?</h3>

            <p>
              Essa ação atualizará a quantidade disponível no estoque
              e finalizará o pedido de compra.
            </p>
          </div>

          {erros.length > 0 && (
            <div className="mensagens-erro" role="alert">
              {erros.map((mensagem, index) => (
                <p key={`${index}-${mensagem}`}>{mensagem}</p>
              ))}
            </div>
          )}

          {sucesso && (
            <p className="mensagem-sucesso" role="status">
              {sucesso}
            </p>
          )}

          <div className="fornecedor-botoes-etapas">
            <button
              type="button"
              className="fornecedor-voltar"
              onClick={fecharModal}
              disabled={carregando}
            >
              Cancelar
            </button>

            <button
              type="submit"
              className="modal-usuario-submit"
              disabled={carregando}
            >
              {carregando ? "Confirmando..." : "Confirmar"}
            </button>
          </div>
        </form>
      </div>
    </div>
  );
}
