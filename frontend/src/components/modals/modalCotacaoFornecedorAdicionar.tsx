import { apiFetch } from "../../../services/api";
import { useState, type FormEvent } from "react";
import { useParams } from "react-router-dom";

import SelectFornecedor from "../selects/selectFornecedor";

import "../../styles/modalCriacaoMensagem.css";
import "../../styles/ModalCadastro.css";

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

  if (!aberta) return null;

  async function cadastrarUnidadeMedida(e: FormEvent<HTMLFormElement>) {
    e.preventDefault();

    setErros([]);
    setSucesso("");

    console.log("ID da cotação:", id);

    try {
      await apiFetch("/api/CotacaoItemFornecedores", {
        method: "POST",
        body: JSON.stringify({
          cotacaoItemId: Number(id),
          fornecedorId,
          valorUnitario,
          prazoEntregaDias,
          condicaoPagamento,
          observacao,
        }),
      });

      setSucesso("Unidade de medida cadastrada com sucesso!");
    } catch (error) {
      const mensagem =
        error instanceof Error
          ? error.message
          : "Não foi possível cadastrar a unidade de medida.";

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
        <form onSubmit={cadastrarUnidadeMedida}>
          <label htmlFor="fornecedorId">Fornecedor:</label>
            <SelectFornecedor
              fornecedorId={fornecedorId}
              setFornecedorId={setFornecedorId}
            />

          <label htmlFor="valorUnitario">Valor Unitário:</label>
          <input
            type="number"
            id="valorUnitario"
            value={valorUnitario}
            onChange={(e) => setValorUnitario(e.target.value)}
          />

          <label htmlFor="prazoEntregaDias">Prazo de Entrega (dias):</label>
          <input
            type="number"
            id="prazoEntregaDias"
            value={prazoEntregaDias}
            onChange={(e) => setPrazoEntregaDias(e.target.value)}
          />

          <label htmlFor="condicaoPagamento">Condição de Pagamento:</label>
          <input
            type="text"
            id="condicaoPagamento"
            value={condicaoPagamento}
            onChange={(e) => setCondicaoPagamento(e.target.value)}
          />

          <label htmlFor="observacao">Observação:</label>
          <input
            type="text"
            id="observacao"
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