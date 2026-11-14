
import { IoClose } from "react-icons/io5";

import { apiFetch } from "../../../services/api";
import { useState, type FormEvent } from "react";

import "../../styles/modalObs.css";
import "../../styles/modalCriacaoMensagem.css";

import SelectProduto from "../selects/selectProduto";

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
  const [quantidade, setQuantidade] = useState("");

  const [erros, setErros] = useState<string[]>([]);
  const [sucesso, setSucesso] = useState("");
  const [carregando, setCarregando] = useState(false);

  if (!aberta) return null;

  function fecharModal() {
    if (carregando) return;

    setDataSolicitacao("");
    setDataLimiteResposta("");
    setObservacao("");
    setProdutoId("");
    setQuantidade("");

    setErros([]);
    setSucesso("");

    onFechar();
  }

  async function cadastrarCotacao(e: FormEvent<HTMLFormElement>) {
    e.preventDefault();

    setErros([]);
    setSucesso("");

    const novosErros: string[] = [];

    if (!dataSolicitacao) {
      novosErros.push("Data de solicitação é obrigatória.");
    }

    if (!dataLimiteResposta) {
      novosErros.push("Data limite de resposta é obrigatória.");
    }

    if (!produtoId) {
      novosErros.push("Selecione um produto.");
    }

    if (novosErros.length > 0) {
      setErros(novosErros);
      return;
    }

    const idUsuario = localStorage.getItem("usuario_id");

    if (!idUsuario) {
      setErros(["Não foi possível identificar o usuário logado."]);
      return;
    }

    setCarregando(true);

    try {
      const produtos = await apiFetch("/api/Produtos", {
        method: "GET",
      }) as Produto[];

      const produtoCriado = produtos.find(
        (produto) => produto.id === Number(produtoId)
      );

      if (!produtoCriado) {
        throw new Error("Não foi possível localizar o produto selecionado.");
      }

      const cotacaoCriada = await apiFetch("/api/Cotacoes", {
        method: "POST",
        body: JSON.stringify({
          usuarioId: Number(idUsuario),
          dataSolicitacao: new Date(dataSolicitacao),
          dataLimiteResposta: new Date(dataLimiteResposta),
          status: 1,
          observacao,
        }),
      }) as Cotacao;

      await apiFetch("/api/CotacaoItens", {
        method: "POST",
        body: JSON.stringify({
          cotacaoId: Number(cotacaoCriada.id),
          produtoId: Number(produtoId),
          unidadeMedidaId: Number(produtoCriado.unidadeMedidaId),
          quantidade: 1,
          status: 1,
          observacao,
        }),
      });

      setSucesso("Cotação cadastrada com sucesso!");

      setDataSolicitacao("");
      setDataLimiteResposta("");
      setObservacao("");
      setProdutoId("");
      setQuantidade("");

    } catch (error) {
      const mensagem =
        error instanceof Error
          ? error.message
          : "Não foi possível cadastrar a cotação.";

      setErros(mensagem.split("\n"));

    } finally {
      setCarregando(false);
    }
  }

  return (
    <div className="modal-overlay" onClick={fecharModal}>
      <div
        className="modal-caixa modal-usuario modal-cotacao"
        onClick={(e) => e.stopPropagation()}
      >
        <div className="modal-usuario-cabecalho">
          <div className="modal-usuario-titulo">
            <div className="modal-titulos-4">
              <h2>Adicionar Cotação</h2>
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
          onSubmit={cadastrarCotacao}
        >
          <h3>Informações da cotação</h3>

          <label>Data de Solicitação</label>
          <input
            type="date"
            value={dataSolicitacao}
            onChange={(e) => setDataSolicitacao(e.target.value)}
          />

          <label>Data Limite de Resposta</label>
          <input
            type="date"
            value={dataLimiteResposta}
            onChange={(e) => setDataLimiteResposta(e.target.value)}
          />

          <label>Observação</label>
          <textarea
            placeholder="Digite uma observação"
            value={observacao}
            onChange={(e) => setObservacao(e.target.value)}
          />

          <h3>Produto da cotação</h3>

          <label>Produto</label>
          <SelectProduto
            produtoId={produtoId}
            setProdutoId={setProdutoId}
          />

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
              {carregando ? "Cadastrando..." : "Cadastrar"}
            </button>
          </div>
        </form>
      </div>
    </div>
  );
}
