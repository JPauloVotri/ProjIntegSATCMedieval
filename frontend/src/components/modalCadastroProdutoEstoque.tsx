// Esse modal serviu para testes
// Vou deixar ele aqui para caso precise no futuro
// ass: kaio

import { apiFetch } from "../../services/api";
import { useState, type FormEvent } from "react";

import "../styles/modalCriacaoMensagem.css";
import "../styles/ModalCadastro.css";

interface ModalCadastroProps {
  aberta: boolean;
  onFechar: () => void;
}

export default function ModalCadastro({
  aberta,
  onFechar,
}: ModalCadastroProps) {
  const [produtoId, setProdutoId] = useState("");
  const [quantidadeDisponivel, setQuantidadeDisponivel] = useState("");
  const [valorTotal, setValorTotal] = useState("");
  const [ultimaEntrada, setUltimaEntrada] = useState("");
  const [ultimaSaida, setUltimaSaida] = useState("");

  const [erros, setErros] = useState<string[]>([]);
  const [sucesso, setSucesso] = useState("");

  if (!aberta) return null;

  async function cadastrarGrupoProduto(e: FormEvent<HTMLFormElement>) {
    e.preventDefault();

    setErros([]);
    setSucesso("");

    try {
      await apiFetch("/api/Estoques", {
        method: "POST",
        body: JSON.stringify({
          produtoId,
          quantidadeDisponivel,
          valorTotal,
          ultimaEntrada,
          ultimaSaida,
        }),
      });

      setSucesso("Grupo de produtos cadastrado com sucesso!");
      setProdutoId("");
      setQuantidadeDisponivel("")
      setValorTotal("")
      setUltimaEntrada("")
      setUltimaSaida("")
    } catch (error) {
      const mensagem =
        error instanceof Error
          ? error.message
          : "Não foi possível cadastrar o grupo de produtos.";

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
        <form onSubmit={cadastrarGrupoProduto}>
        <label htmlFor="nome">Nome:</label>
          <input
            id="nome"
            type="text"
            value={produtoId}
            onChange={(e) => setProdutoId(e.target.value)}
          />

        <label htmlFor="quantidadeDisponivel">Quantidade Disponivel:</label>
          <input
            id="quantidade disponivel"
            type="number"
            value={quantidadeDisponivel}
            onChange={(e) => setQuantidadeDisponivel(e.target.value)}
          />

        <label htmlFor="ValorTotal">Valor Total:</label>
          <input
            id="Valor total"
            type="number"
            value={valorTotal}
            onChange={(e) => setValorTotal(e.target.value)}
          />

        <label htmlFor="ultimaEntrada">Ultima Entrada:</label>
          <input
            id="ultima entrada"
            type="date"
            value={ultimaEntrada}
            onChange={(e) => setUltimaEntrada(e.target.value)}
          />

        <label htmlFor="ultimaSaida">Ultima Saida:</label>
          <input
            id="ultima saida"
            type="date"
            value={ultimaSaida}
            onChange={(e) => setUltimaSaida(e.target.value)}
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