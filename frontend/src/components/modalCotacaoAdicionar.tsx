import { apiFetch } from "../../services/api";
import { useState, type FormEvent } from "react";

import "../styles/modalCriacaoMensagem.css";
import "../styles/ModalCadastro.css";

import SelectProduto from "./selectProduto";

interface ModalCadastroProps {
  aberta: boolean;
  onFechar: () => void;
}

interface Produto {
  id: number;
  nome: string;
  unidadeMedidaId: number;
}

interface Cotacao {
  id: number;
}

export default function ModalCadastro({
  aberta,
  onFechar,
}: ModalCadastroProps) {
  const [dataSolicitacao, setDataSolicitacao] = useState("");
  const [dataLimiteResposta, setDataLimiteResposta] = useState("");
  const [observacao, setObservacao] = useState("");
  const [produtoId, setProdutoId] = useState<number | "">("");
  const [unidadeMedidaId, setUnidadeMedidaId] = useState<number | "">("");
  const [quantidade, setQuantidade] = useState("");

  const [erros, setErros] = useState<string[]>([]);
  const [sucesso, setSucesso] = useState("");

  let idUsuario = localStorage.getItem("usuario_id");

  if (!aberta) return null;

  async function CotacaoFornecedoresAdicionar(e: FormEvent<HTMLFormElement>) {
    e.preventDefault();

      async function buscarProdutos(): Promise<Produto[]> {
      const resposta = await apiFetch("/api/Produtos", {
        method: "GET",
      });
    
      return resposta as Produto[];
    }

    setErros([]);
    setSucesso("");

    if (dataSolicitacao === "") {
      setErros(["Data de solicitação é obrigatória."]);
      return;
    } else if (dataLimiteResposta === "") {
      setErros(["Data limite de resposta é obrigatória."]);
      return;
    }

    const produtos = await buscarProdutos();

    const produtoCriado = produtos.find(
      (produto) =>
        produto.id === produtoId
      );

    if (!produtoCriado) {
      setErros(["Não foi possível localizar o produto cadastrado."]);
      return;
    }

    console.log("Produto encontrado:", produtoCriado);

    const cotacaoCriada = await apiFetch("/api/Cotacoes", {
      method: "POST",
      body: JSON.stringify({
        usuarioId: Number(idUsuario),
        dataSolicitacao,
        dataLimiteResposta,
        status: 1,
        observacao,
      }),
    }) as Cotacao;

    console.log("ID da cotação:", cotacaoCriada.id);

  try {
      await apiFetch("/api/CotacaoItens", {
        method: "POST",
        body: JSON.stringify({
          cotacaoId: Number(cotacaoCriada.id),
          produtoId: Number(produtoId),
          unidadeMedidaId: Number(produtoCriado.unidadeMedidaId),
          quantidade: Number(quantidade),
          status: 1,
          observacao,
        }),
      });

      setSucesso("Cotacao cadastrada com sucesso!");
      setDataSolicitacao("");
      setDataLimiteResposta("");
      setObservacao("");
      setProdutoId("");
      setUnidadeMedidaId("");
      setQuantidade("");
    } catch (error) {
      const mensagem =
        error instanceof Error
          ? error.message
          : "Não foi possível cadastrar a cotacao.";

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
        <form onSubmit={CotacaoFornecedoresAdicionar}>
          <label htmlFor="dataSolicitacao">Data de Solicitação:</label>
          <input
            id="dataSolicitacao"
            type="date"
            value={dataSolicitacao}
            onChange={(e) => setDataSolicitacao(e.target.value)}
          />

          <label htmlFor="dataLimiteResposta">Data Limite de Resposta:</label>
          <input
            id="dataLimiteResposta"
            type="date"
            value={dataLimiteResposta}
            onChange={(e) => setDataLimiteResposta(e.target.value)}
          />

          <label htmlFor="observacao">Observação:</label>
          <input
            id="observacao"
            type="text"
            value={observacao}
            onChange={(e) => setObservacao(e.target.value)}
          />

          <label htmlFor="produtoId">Produto:</label>
          <SelectProduto
            produtoId={produtoId}
            setProdutoId={setProdutoId}
          />

          <label htmlFor="quantidade">Quantidade:</label>
          <input
            id="quantidade"
            type="number"
            value={quantidade}
            onChange={(e) => setQuantidade(e.target.value)}
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