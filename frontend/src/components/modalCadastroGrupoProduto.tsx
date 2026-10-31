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
  const [nome, setNome] = useState("");
  const [descricao, setDescricao] = useState("");

  const [erros, setErros] = useState<string[]>([]);
  const [sucesso, setSucesso] = useState("");

  if (!aberta) return null;

  async function cadastrarGrupoProduto(e: FormEvent<HTMLFormElement>) {
    e.preventDefault();

    setErros([]);
    setSucesso("");

    try {
      await apiFetch("/api/GruposProdutos", {
        method: "POST",
        body: JSON.stringify({
          nome,
          descricao,
        }),
      });

      setSucesso("Grupo de produtos cadastrado com sucesso!");
      setNome("");
      setDescricao("");
    } catch (error) {
      const mensagem =
        error instanceof Error
          ? error.message
          : "Não foi possível cadastrar o grupo de produtos.";

      setErros(mensagem.split("\n"));
    }
  }

  return (
    <div className="modal-overlay" onClick={onFechar}>
      <div
        className="modal-cadastro"
        onClick={(e) => e.stopPropagation()}
      >
        <form onSubmit={cadastrarGrupoProduto}>
          <label htmlFor="nome">Nome:</label>
          <input
            id="nome"
            type="text"
            value={nome}
            onChange={(e) => setNome(e.target.value)}
          />

          <label htmlFor="descricao">Descrição:</label>
          <input
            id="descricao"
            type="text"
            value={descricao}
            onChange={(e) => setDescricao(e.target.value)}
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