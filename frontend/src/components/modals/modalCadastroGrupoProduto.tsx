
import { IoClose } from "react-icons/io5";

import "../../styles/modalObs.css";
import "../../styles/modalCriacaoMensagem.css";

import { apiFetch } from "../../../services/api";
import { useState, type FormEvent } from "react";

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
  const [carregando, setCarregando] = useState(false);

  function fecharModal() {
    setErros([]);
    setSucesso("");
    onFechar();
  }

  async function cadastrarGrupoProduto(e: FormEvent<HTMLFormElement>) {
    e.preventDefault();

    setErros([]);
    setSucesso("");
    setCarregando(true);

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

    } finally {
      setCarregando(false);
    }
  }

  if (!aberta) return null;

  return (
    <div className="modal-overlay" onClick={fecharModal}>
      <div
        className="modal-caixa modal-usuario modal-grupo-produto"
        onClick={(e) => e.stopPropagation()}
      >
        <div className="modal-usuario-cabecalho">
          <div className="modal-usuario-titulo">
            <div className="modal-titulos-3">
              <h2>Adicionar Grupo de Produto</h2>
            </div>
          </div>

          <button
            type="button"
            className="modal-close"
            onClick={fecharModal}
          >
            <IoClose />
          </button>
        </div>

        <form
          className="modal-usuario-form"
          onSubmit={cadastrarGrupoProduto}
        >
          <h3>Informações do grupo</h3>

          <label>Nome</label>
          <input
            type="text"
            placeholder="Digite o nome do grupo"
            value={nome}
            onChange={(e) => setNome(e.target.value)}
            required
          />

          <label>Descrição</label>
          <input
            type="text"
            placeholder="Digite a descrição do grupo"
            value={descricao}
            onChange={(e) => setDescricao(e.target.value)}
          />

          <button
            type="submit"
            className="modal-usuario-submit"
            disabled={carregando}
          >
            {carregando ? "Cadastrando..." : "Cadastrar grupo"}
          </button>

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
        </form>
      </div>
    </div>
  );
}
