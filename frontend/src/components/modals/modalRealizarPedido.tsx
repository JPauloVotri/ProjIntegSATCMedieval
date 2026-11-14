
import { apiFetch } from "../../../services/api";
import { useState, type FormEvent } from "react";
import { IoClose } from "react-icons/io5";

import "../../styles/modalObs.css";
import "../../styles/modalCriacaoMensagem.css";

interface ModalCadastroProps {
  aberta: boolean;
  onFechar: () => void;
  fornecedorId: number;
  cotacaoId: string | undefined;
  produtoId: string | undefined;
  unidadeMedidaId?: number;
  valorUnitario: number | undefined;
}

export default function ModalCadastro({
  aberta,
  onFechar,
  fornecedorId,
  cotacaoId,
  produtoId,
  unidadeMedidaId,
  valorUnitario,
}: ModalCadastroProps) {
  const idUsuario = localStorage.getItem("usuario_id");

  const [total, setTotal] = useState("");
  const [observacao, setObservacao] = useState("");
  const [dataPrevisao, setDataPrevisao] = useState("");
  const [erros, setErros] = useState<string[]>([]);
  const [sucesso, setSucesso] = useState("");
  const [carregando, setCarregando] = useState(false);

  if (!aberta) return null;

  function limparCampos() {
    setTotal("");
    setObservacao("");
    setDataPrevisao("");
    setErros([]);
    setSucesso("");
  }

  function fecharModal() {
    if (carregando) return;

    limparCampos();
    onFechar();
  }

  async function cadastrarPedido(e: FormEvent<HTMLFormElement>) {
    e.preventDefault();

    setErros([]);
    setSucesso("");

    if (!dataPrevisao) {
      setErros(["A data de previsão de entrega é obrigatória."]);
      return;
    }

    if (!total || Number(total) <= 0) {
      setErros(["Informe um total válido para o pedido."]);
      return;
    }

    if (!cotacaoId || !produtoId || Number.isNaN(Number(produtoId))) {
      setErros(["Não foi possível identificar a cotação ou o produto."]);
      return;
    }

    if (!idUsuario) {
      setErros(["Não foi possível identificar o usuário."]);
      return;
    }

    setCarregando(true);

    try {
      const unidadeMedida = unidadeMedidaId ?? 1;

      const quantidade = Number(total);
      const valorTotal = quantidade * (valorUnitario || 0);

      const pedidoCriado = await apiFetch<{ id: number }>(
        "/api/PedidosCompra",
        {
          method: "POST",
          body: JSON.stringify({
            fornecedorId: Number(fornecedorId),
            usuarioId: Number(idUsuario),
            cotacaoId: Number(cotacaoId),
            dataPedido: new Date().toISOString(),
            dataPrevistaEntrega: new Date(dataPrevisao),
            dataRecebimento: null,
            status: 1,
            total: quantidade,
            observacao,
          }),
        }
      );

      await apiFetch("/api/PedidosCompraItens", {
        method: "POST",
        body: JSON.stringify({
          pedidoCompraId: Number(pedidoCriado.id),
          produtoId: Number(produtoId),
          unidadeMedidaId: Number(unidadeMedida),
          cotacaoItemId: Number(cotacaoId),
          quantidadeSolicitada: quantidade,
          quantidadeRecebida: 1,
          valorUnitario: valorUnitario,
          total: valorTotal,
          observacao,
        }),
      });

      setSucesso("Pedido cadastrado com sucesso!");
      limparCampos();
      setSucesso("Pedido cadastrado com sucesso!");
    } catch (error) {
      const mensagem =
        error instanceof Error
          ? error.message
          : "Não foi possível cadastrar o pedido.";

      setErros(mensagem.split("\n"));
    } finally {
      setCarregando(false);
    }
  }

  return (
    <div className="modal-overlay" onClick={fecharModal}>
      <div
        className="modal-caixa modal-usuario modal-pedido"
        onClick={(e) => e.stopPropagation()}
      >
        <div className="modal-usuario-cabecalho">
          <div className="modal-usuario-titulo">
            <div className="modal-titulos-6">
              <h2>Realizar Pedido</h2>
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

        <form className="modal-usuario-form" onSubmit={cadastrarPedido}>
          <h3>Informações do pedido</h3>

          <label htmlFor="dataPrevisao">
            Data de previsão de entrega
          </label>
          <input
            id="dataPrevisao"
            type="date"
            value={dataPrevisao}
            onChange={(e) => setDataPrevisao(e.target.value)}
            required
          />

          <label htmlFor="total">Quantidade solicitada</label>
          <input
            id="total"
            type="number"
            min="1"
            step="1"
            value={total}
            onChange={(e) => setTotal(e.target.value)}
            placeholder="Digite a quantidade"
            required
          />

          <label htmlFor="observacao">Observação</label>
          <textarea
            id="observacao"
            value={observacao}
            onChange={(e) => setObservacao(e.target.value)}
            placeholder="Digite uma observação (opcional)"
            rows={3}
          />

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
              {carregando ? "Cadastrando..." : "Salvar Pedido"}
            </button>
          </div>
        </form>
      </div>
    </div>
  );
}
