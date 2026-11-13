
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
  const [sigla, setSigla] = useState("");
  const [tipo, setTipo] = useState("");

  const [erros, setErros] = useState<string[]>([]);
  const [sucesso, setSucesso] = useState("");
  const [carregando, setCarregando] = useState(false);

  function fecharModal() {
    setErros([]);
    setSucesso("");
    onFechar();
  }

  async function cadastrarUnidadeMedida(e: FormEvent<HTMLFormElement>) {
    e.preventDefault();

    setErros([]);
    setSucesso("");
    setCarregando(true);

    try {
      await apiFetch("/api/UnidadesMedida", {
        method: "POST",
        body: JSON.stringify({
          nome,
          sigla,
          tipo,
        }),
      });

      setSucesso("Unidade de medida cadastrada com sucesso!");

      setNome("");
      setSigla("");
      setTipo("");

    } catch (error) {
      const mensagem =
        error instanceof Error
          ? error.message
          : "Não foi possível cadastrar a unidade de medida.";

      setErros(mensagem.split("\n"));

    } finally {
      setCarregando(false);
    }
  }

  if (!aberta) return null;

  return (
    <div className="modal-overlay" onClick={fecharModal}>
      <div
        className="modal-caixa modal-usuario modal-unidade-medida"
        onClick={(e) => e.stopPropagation()}
      >
        <div className="modal-usuario-cabecalho">
          <div className="modal-usuario-titulo">
            <div className="modal-titulos">
              <h2>Adicionar Unidade de Medida</h2>
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
          onSubmit={cadastrarUnidadeMedida}
        >
          <h3>Informações da unidade</h3>

          <label>Nome</label>
          <input
            type="text"
            placeholder="Digite o nome da unidade"
            value={nome}
            onChange={(e) => setNome(e.target.value)}
            required
          />

          <label>Sigla</label>
          <input
            type="text"
            placeholder="Ex: KG, UN, L"
            value={sigla}
            onChange={(e) => setSigla(e.target.value)}
            required
          />

          <label>Tipo</label>
          <input
            type="text"
            placeholder="Digite o tipo da unidade"
            value={tipo}
            onChange={(e) => setTipo(e.target.value)}
            required
          />

          <button
            type="submit"
            className="modal-usuario-submit"
            disabled={carregando}
          >
            {carregando ? "Cadastrando..." : "Cadastrar unidade"}
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
