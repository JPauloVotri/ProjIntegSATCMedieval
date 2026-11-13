
import { useEffect, useState, type FormEvent } from "react";
import { IoClose } from "react-icons/io5";

import "../../styles/modalObs.css";
import "../../styles/modalCriacaoMensagem.css";
import { apiFetch } from "../../../services/api";

interface Estoque {
  id: number;
  produtoId: number;
  produtoNome: string;
  quantidadeDisponivel: number;
  valorTotal: number;
  ultimaEntrada: string | null;
  ultimaSaida: string | null;
}

interface ModalSaidaEstoqueProps {
  aberta: boolean;
  onFechar: () => void;
  estoque: Estoque;
  onConfirmar: (estoqueId: number, quantidade: number) => Promise<void>;
}

export default function ModalSaidaEstoque({
  aberta,
  onFechar,
  estoque,
  onConfirmar,
}: ModalSaidaEstoqueProps) {
  const [quantidade, setQuantidade] = useState("");
  const [erros, setErros] = useState<string[]>([]);
  const [sucesso, setSucesso] = useState("");
  const [carregando, setCarregando] = useState(false);

  useEffect(() => {
    if (aberta) {
      setQuantidade("");
      setErros([]);
      setSucesso("");
    }
  }, [aberta, estoque.id]);

  if (!aberta) return null;

  function fecharModal() {
    if (carregando) return;

    setQuantidade("");
    setErros([]);
    setSucesso("");
    onFechar();
  }

  async function handleSubmit(e: FormEvent<HTMLFormElement>) {
        e.preventDefault();
    setErros([]);
    setSucesso("");

    const valor = Number(quantidade);

    if (!quantidade || !Number.isFinite(valor) || valor <= 0) {
      setErros(["Informe uma quantidade válida."]);
      return;
    }

    if (valor > estoque.quantidadeDisponivel) {
      setErros(["A quantidade informada é maior que o estoque disponível."]);
      return;
    }

    try {
      await apiFetch(`/api/Estoques/${estoque.produtoId}`, {
            method: "PUT",
            body: JSON.stringify({
              produtoId: estoque.produtoId,
              quantidadeDisponivel: estoque.quantidadeDisponivel - valor,
              valorTotal: estoque.valorTotal,
              ultimaEntrada: estoque.ultimaEntrada,
              ultimaSaida: estoque.ultimaSaida
            }),
          });

        setSucesso("Saida registrada com sucesso!");

    } catch (error) {
      setErros([
        error instanceof Error
          ? error.message
          : "Não foi possível registrar a saída do produto."
      ]);
    } finally {
      setCarregando(false);
    }
  }

  return (
    <div className="modal-overlay" onClick={fecharModal}>
      <div
        className="modal-caixa modal-usuario modal-saida"
        onClick={(e) => e.stopPropagation()}
      >
        <div className="modal-usuario-cabecalho">
          <div className="modal-usuario-titulo">
            <div className="modal-titulos-7">
              <h2>Registrar saída de estoque</h2>
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

        <form className="modal-usuario-form" onSubmit={handleSubmit}>
          <h3>Informações do produto</h3>

          <div className="saida-info">
            <label>Produto selecionado</label>
            <strong>{estoque.produtoNome}</strong>

            <label>Quantidade disponível</label>
            <strong>{estoque.quantidadeDisponivel}</strong>
          </div>

          <h3>Quantidade da saída</h3>

          <label htmlFor="quantidadeSaida">
            Quantidade a retirar
          </label>

          <input
            id="quantidadeSaida"
            type="number"
            min="0.01"
            max={estoque.quantidadeDisponivel}
            step="any"
            placeholder="Digite a quantidade"
            value={quantidade}
            onChange={(e) => setQuantidade(e.target.value)}
            required
          />

          {erros.length > 0 && (
            <div className="mensagens-erro" role="alert">
              {erros.map((erro, index) => (
                <p key={`${index}-${erro}`}>{erro}</p>
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
              {carregando ? "Registrando..." : "Confirmar saída"}
            </button>
          </div>
        </form>
      </div>
    </div>
  );
}
