
import { useEffect, useState, type FormEvent } from "react";

import "../../styles/modalSaidaEstoque.css";
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
  onConfirmar
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
    <div className="modalSaida-overlay">
      <div className="modalSaida-container">

        <div className="modalSaida-header">
          <h2>Registrar saída</h2>
          <button type="button" onClick={onFechar}>
            ×
          </button>
        </div>

        <form onSubmit={handleSubmit}>

          <div className="modalSaida-info">
            <p>Produto selecionado</p>
            <h3>{estoque.produtoNome}</h3>

            <p>Quantidade disponível</p>
            <strong>
              {estoque.quantidadeDisponivel}
            </strong>
          </div>

          <div className="modalSaida-campo">
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
            />
          </div>

          {erros.length > 0 && (
            <div className="modalSaida-erros">
              {erros.map((erro, index) => (
                <p key={index} className="modalSaida-erro">
                  {erro}
                </p>
              ))}
            </div>
          )}

          {sucesso && (
            <p className="modalSaida-sucesso">{sucesso}</p>
          )}

          <div className="modalSaida-botoes">
            <button
              type="button"
              onClick={onFechar}
              disabled={carregando}
            >
              Cancelar
            </button>

            <button
              type="submit"
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
