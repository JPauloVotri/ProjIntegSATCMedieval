
import { IoClose } from "react-icons/io5";

import { apiFetch } from "../../../services/api";
import { useState, type FormEvent } from "react";
import { useParams } from "react-router-dom";

import SelectFornecedor from "../selects/selectFornecedor";

import "../../styles/modalObs.css";
import "../../styles/modalCriacaoMensagem.css";

interface ModalCadastroProps {
  aberta: boolean;
  onFechar: () => void;
}

export default function ModalCadastro({
  aberta,
  onFechar,
}: ModalCadastroProps) {
  const { id } = useParams<{ id: string }>();

  const [fornecedorId, setFornecedorId] = useState<number | "">("");
  const [valorUnitario, setValorUnitario] = useState("");
  const [prazoEntregaDias, setPrazoEntregaDias] = useState("");
  const [condicaoPagamento, setCondicaoPagamento] = useState("");
  const [observacao, setObservacao] = useState("");

  const [erros, setErros] = useState<string[]>([]);
  const [sucesso, setSucesso] = useState("");
  const [carregando, setCarregando] = useState(false);

  if (!aberta) return null;

  function limparCampos() {
    setFornecedorId("");
    setValorUnitario("");
    setPrazoEntregaDias("");
    setCondicaoPagamento("");
    setObservacao("");
  }

  function fecharModal() {
    if (carregando) return;

    setErros([]);
    setSucesso("");
    limparCampos();
    onFechar();
  }

  async function cadastrarFornecedor(e: FormEvent<HTMLFormElement>) {
    e.preventDefault();

    setErros([]);
    setSucesso("");

    setCarregando(true);

    try {
      await apiFetch("/api/CotacaoItemFornecedores", {
        method: "POST",
        body: JSON.stringify({
          cotacaoItemId: Number(id),
          fornecedorId: Number(fornecedorId),
          valorUnitario: Number(valorUnitario),
          prazoEntregaDias: Number(prazoEntregaDias),
          condicaoPagamento,
          observacao,
        }),
      });

      setSucesso("Fornecedor adicionado à cotação com sucesso!");

      limparCampos();

    } catch (error) {
      const mensagem =
        error instanceof Error
          ? error.message
          : "Não foi possível adicionar o fornecedor à cotação.";

      setErros(mensagem.split("\n"));

    } finally {
      setCarregando(false);
    }
  }

  return (
    <div className="modal-overlay" onClick={fecharModal}>
      <div
        className="modal-caixa modal-usuario modal-cotacao-fornecedor"
        onClick={(e) => e.stopPropagation()}
      >
        <div className="modal-usuario-cabecalho">
          <div className="modal-usuario-titulo">
            <div className="modal-titulos-5">
              <h2>Adicionar Fornecedor à Cotação</h2>
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
          onSubmit={cadastrarFornecedor}
        >
          <h3>Informações do fornecedor</h3>

          <label>Fornecedor</label>
          <SelectFornecedor
            fornecedorId={fornecedorId}
            setFornecedorId={setFornecedorId}
          />

          <h3>Informações da oferta</h3>

          <label>Valor Unitário</label>
          <input
            type="number"
            step="0.01"
            placeholder="Digite o valor unitário"
            value={valorUnitario}
            onChange={(e) => setValorUnitario(e.target.value)}
          />

          <label>Prazo de Entrega (dias)</label>
          <input
            type="number"
            min="0"
            placeholder="Ex: 3"
            value={prazoEntregaDias}
            onChange={(e) => setPrazoEntregaDias(e.target.value)}
          />

          <label>Condição de Pagamento</label>
          <input
            type="text"
            placeholder="Ex: 30 dias"
            value={condicaoPagamento}
            onChange={(e) => setCondicaoPagamento(e.target.value)}
          />

          <label>Observação</label>
          <textarea
            placeholder="Digite uma observação (opcional)"
            value={observacao}
            onChange={(e) => setObservacao(e.target.value)}
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
