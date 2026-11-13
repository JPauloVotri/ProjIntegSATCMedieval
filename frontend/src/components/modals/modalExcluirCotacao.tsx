
import { IoClose } from "react-icons/io5";

import "../../styles/modalObs.css";

interface ModalExcluirCotacaoProps {
  aberta: boolean;
  onFechar: () => void;
  onConfirmar: () => void;
}

export default function ModalConfirmarExclusaoCotacao({
  aberta,
  onFechar,
  onConfirmar,
}: ModalExcluirCotacaoProps) {

  if (!aberta) return null;

  return (
    <div className="modal-overlay" onClick={onFechar}>
      <div
        className="modal-caixa"
        onClick={(e) => e.stopPropagation()}
      >
        <div className="modal-usuario-cabecalho">
          <div className="modal-titulos-4">
            <h2>Excluir Cotação</h2>
          </div>

          <button
            type="button"
            className="modal-close"
            onClick={onFechar}
          >
            <IoClose />
          </button>
        </div>

        <div className="modal-grupo-conteudo">
          <p>
            Tem certeza de que deseja excluir esta cotação?
          </p>

          <p>
            Essa ação não poderá ser desfeita.
          </p>

          <div className="modal-grupo-botoes">
            <button
              type="button"
              onClick={onFechar}
              className="button-adicionar-grupo-produto-2"
            >
              Cancelar
            </button>

            <button
              type="button"
              onClick={onConfirmar}
              className="button-excluir-grupo-produto-2"
            >
              Excluir
            </button>
          </div>
        </div>
      </div>
    </div>
  );
}
