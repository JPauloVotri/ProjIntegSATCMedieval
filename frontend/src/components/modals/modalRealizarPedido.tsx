import { apiFetch } from "../../../services/api";
import { useState, type FormEvent } from "react";

import "../../styles/modalCriacaoMensagem.css";
import "../../styles/ModalCadastro.css";
import { useParams } from "react-router-dom";

interface ModalCadastroProps {
  aberta: boolean;
  onFechar: () => void;
  fornecedorId: number;
  cotacaoId: string | undefined;
  produtoId: string | undefined;
  valorUnitario: number | undefined;
}

interface Produto {
  id: number;
  nome: string;
  unidadeMedidaId: number;
}

export default function ModalCadastro({
  aberta,
  onFechar,
  fornecedorId,
  cotacaoId,
  produtoId,
  valorUnitario,
}: ModalCadastroProps) {
  let idUsuario = localStorage.getItem("usuario_id");

  const [total, setTotal] = useState("");
  const [observacao, setObservacao] = useState("");
  const [dataPrevisao, setDataPrevisao] = useState("");
  const [erros, setErros] = useState<string[]>([]);
  const [sucesso, setSucesso] = useState("");

  if (!aberta) return null;

    async function pegarUnidadeMedida(produtoId: number): Promise<number | undefined> {
      const produtos = await apiFetch<Produto[]>("/api/Produtos");

      const produtoSelecionado = produtos.find(
        (produto) => produto.id === Number(produtoId)
      );

      const unidadeMedidaId = produtoSelecionado?.unidadeMedidaId;
      console.log("Unidade de Medida ID: " + unidadeMedidaId);
      return unidadeMedidaId;
    }

  async function cadastrarPedido(e: FormEvent<HTMLFormElement>) {
    e.preventDefault();

    setErros([]);
    setSucesso("");

    if (dataPrevisao == "") {
      setErros(["A data de previsão de entrega é obrigatória."]);
      return;
    } else if (total == "") {
      setErros(["O total do pedido é obrigatório."]);
      return;
    }



try {
  const unidadeMedidaId = await pegarUnidadeMedida(Number(produtoId));

    if (unidadeMedidaId === undefined) {
      setErros(["Não foi possível obter a unidade de medida do produto."]);
      return;
    }
  
  const pedidoCriado = await apiFetch<{ id: number }>("/api/PedidosCompra", {
    method: "POST",
    body: JSON.stringify({
      fornecedorId: Number(fornecedorId),
      usuarioId: Number(idUsuario),
      cotacaoId: Number(cotacaoId),
      dataPedido: new Date().toISOString(),
      dataPrevistaEntrega: dataPrevisao,
      dataRecebimento: null,
      status: 1,
      total,
      observacao,
    }),
  });

  setSucesso("Pedido cadastrado com sucesso!");

  const valorTotal = Number(total || 1) * (valorUnitario || 0);

  console.log("Valor Unitário: " + valorUnitario);
  console.log("Valor Total: " + valorTotal);

  await apiFetch("/api/PedidosCompraItens", {
    method: "POST",
    body: JSON.stringify({
      pedidoCompraId: Number(pedidoCriado.id),
      produtoId: Number(produtoId),
      unidadeMedidaId: Number(unidadeMedidaId),
      cotacaoItemId: Number(cotacaoId),
      quantidadeSolicitada: Number(total),
      quantidadeRecebida: 1,
      valorUnitario: valorUnitario,
      total: Number(valorTotal),
      observacao,
    }),
  });

} catch (error) {
  const mensagem =
    error instanceof Error
      ? error.message
      : "Não foi possível cadastrar o pedido.";

  setErros(mensagem.split("\n"));
}

  }

  function fecharModal() {
    setErros([]);
    setSucesso("");
  }

  return (
    <div className="modal-overlay" onClick={() => { fecharModal(); onFechar();}}>
      <div
        className="modal-cadastro"
        onClick={(e) => e.stopPropagation()}
      >
        <form onSubmit={cadastrarPedido}>
          <label htmlFor="dataPrevisao">Data de previsão de entrega:</label>
          <input
            id="dataPrevisao"
            type="date"
            value={dataPrevisao}
            onChange={(e) => setDataPrevisao(e.target.value)}
          />

          <label htmlFor="total">Total do pedido:</label>
          <input
            id="total"
            type="text"
            value={total}
            onChange={(e) => setTotal(e.target.value)}
          />

          <label htmlFor="observacao">Observação:</label>
          <input
            id="observacao"
            type="text"
            value={observacao}
            onChange={(e) => setObservacao(e.target.value)}
          />

          <div className="modal-botoes">
            <button
              type="button"
              onClick={onFechar}
              className="modal-cancelar"
            >
              Cancelar
            </button>

            <button type="submit" className="modal-salvar">
              Salvar
            </button>
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
        </form>
      </div>
    </div>
  );
}