import "../styles/ModalCadastro.css";

interface ModalCadastroProps {
  aberta: boolean;
  onFechar: () => void;
}

export default function ModalCadastro({
  aberta,
  onFechar,
}: ModalCadastroProps) {
  if (!aberta) return null;

  return (
    <div className="modal-overlay" onClick={onFechar}>
      <div
        className="modal-cadastro"
        onClick={(e) => e.stopPropagation()}
      >
        <form>
          <label htmlFor="nome">Nome:</label>
          <input id="nome" type="text" />

          <label htmlFor="descricao">Descricao:</label>
          <input id="descricao" type="text" />

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
        </form>
      </div>
    </div>
  );
}