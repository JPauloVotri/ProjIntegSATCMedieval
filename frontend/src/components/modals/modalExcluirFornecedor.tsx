
import { useState } from "react";
import { IoClose } from "react-icons/io5";

import "../../styles/modalObs.css";
import "../../styles/modalCriacaoMensagem.css";

interface ModalConfirmarExclusaoFornecedorProps {
  aberta: boolean;
  onFechar: () => void;
  onConfirmar: () => Promise<void>;
}

export default function ModalConfirmarExclusaoFornecedor({
  aberta,
  onFechar,
  onConfirmar,
}: ModalConfirmarExclusaoFornecedorProps) {
  const [carregando, setCarregando] = useState(false);
  const [erro, setErro] = useState("");

  if (!aberta) return null;

  async function confirmarExclusao() {
    setCarregando(true);
    setErro("");

    try {
      await onConfirmar();
    } catch (error) {
      setErro(
        error instanceof Error
          ? error.message
          : "Não foi possível excluir o fornecedor."
      );
    } finally {
      setCarregando(false);
    }
  }

  return (
    <div className="modal-overlay" onClick={carregando ? undefined : onFechar}>
      <div
        className="modal-caixa modal-usuario modal-confirmar-exclusao"
        onClick={(e) => e.stopPropagation()}
      >
        <div className="modal-usuario-cabecalho">
          <div className="modal-usuario-titulo">
            <div className="modal-titulos-9">
              <h2>Excluir fornecedor</h2>
            </div>
          </div>

          <button
            type="button"
            className="modal-close"
            onClick={onFechar}
            disabled={carregando}
          >
            <IoClose />
          </button>
        </div>

        <div className="modal-usuario-form">
          <div className="confirmar-exclusao-conteudo">
            <h3>Tem certeza que deseja excluir este fornecedor?</h3>

            <p>
              Essa ação não poderá ser desfeita. Os dados do fornecedor
              serão removidos do sistema.
            </p>
          </div>

          {erro && (
            <div className="mensagens-erro" role="alert">
              <p>{erro}</p>
            </div>
          )}

          <div className="fornecedor-botoes-etapas">
            <button
              type="button"
              className="fornecedor-voltar"
              onClick={onFechar}
              disabled={carregando}
            >
              Cancelar
            </button>

            <button
              type="button"
              className="modal-usuario-delete-2"
              onClick={confirmarExclusao}
              disabled={carregando}
            >
              {carregando ? "Excluindo..." : "Excluir"}
            </button>
          </div>
        </div>
      </div>
    </div>
  );
}
