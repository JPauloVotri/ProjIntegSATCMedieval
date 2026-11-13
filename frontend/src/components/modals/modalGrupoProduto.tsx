
import { useState, useEffect } from "react";
import { IoClose } from "react-icons/io5";

import { apiFetch } from "../../../services/api";

import "../../styles/modalObs.css";
import "../../styles/modalGrupoProduto.css"
import ModalConfirmarExclusaoGrupoProduto from "./modalExcluirGrupoProduto";

interface GrupoProduto {
  id: number;
  nome: string;
  descricao: string;
}

interface ModalGrupoProdutoProps {
  aberta: boolean;
  grupo: GrupoProduto | null;
  onFechar: () => void;
  onAtualizar: (grupo: GrupoProduto) => void;
  onExcluir: (id: number) => void;
}

export default function ModalGrupoProduto({
  aberta,
  grupo,
  onFechar,
  onAtualizar,
  onExcluir,
}: ModalGrupoProdutoProps) {
  const [modalExcluirAberta, setModalExcluirAberta] = useState(false);
  const [editando, setEditando] = useState(false);
  const [confirmarExclusao, setConfirmarExclusao] = useState(false);
  const [descricao, setDescricao] = useState("");
  const [erros, setErros] = useState<string[]>([]);
  const [carregando, setCarregando] = useState(false);

  useEffect(() => {
    if (grupo) {
      setDescricao(grupo.descricao || "");
      setEditando(false);
      setConfirmarExclusao(false);
      setErros([]);
    }
  }, [grupo]);

  if (!aberta || !grupo) return null;

  async function salvarDescricao() {
    if (!grupo) return;

    setErros([]);
    setCarregando(true);

    try {
      await apiFetch(`/api/GruposProdutos/${grupo.id}`, {
        method: "PUT",
        body: JSON.stringify({
          nome: grupo.nome,
          descricao,
          ativo: true,
        }),
      });

      onAtualizar({
        ...grupo,
        descricao,
      });

      setEditando(false);
    } catch (error) {
      const mensagem =
        error instanceof Error
          ? error.message
          : "Não foi possível atualizar a descrição.";

      setErros(mensagem.split("\n"));
    } finally {
      setCarregando(false);
    }
  }

  async function excluirGrupo() {
    if (!grupo) return;

    setErros([]);
    setCarregando(true);

    try {
      await apiFetch(`/api/GruposProdutos/${grupo.id}`, {
        method: "DELETE",
      });

      onExcluir(grupo.id);
      onFechar();
    } catch (error) {
      const mensagem =
        error instanceof Error
          ? error.message
          : "Não foi possível excluir o grupo.";

      setErros(mensagem.split("\n"));
    } finally {
      setCarregando(false);
    }
  }

  function fecharModal() {
    setEditando(false);
    setConfirmarExclusao(false);
    setErros([]);
    onFechar();
  }

  return (
    <div className="modal-overlay" onClick={fecharModal}>
      <div
        className="modal-caixa modal-grupo-produto"
        onClick={(e) => e.stopPropagation()}
      >
        <div className="modal-usuario-cabecalho">
          <div className="modal-titulos-10">
            <h2>Descrição</h2>
          </div>

          <button
            type="button"
            className="modal-close"
            onClick={fecharModal}
          >
            <IoClose />
          </button>
        </div>

        <div className="modal-grupo-conteudo">
          <h3>{grupo.nome}</h3>

          <label>Descrição</label>

          {editando ? (
            <textarea
              value={descricao}
              onChange={(e) => setDescricao(e.target.value)}
              placeholder="Digite a descrição do grupo..."
              rows={5}
            />
          ) : (
            <div className="modal-grupo-descricao">
              {grupo.descricao || "Nenhuma descrição cadastrada."}
            </div>
          )}

          {erros.length > 0 && (
            <div className="mensagens-erro" role="alert">
              {erros.map((mensagem, index) => (
                <p key={index}>{mensagem}</p>
              ))}
            </div>
          )}

            <div className="modal-grupo-botoes">
              {editando ? (
                <>
                  <button
                    type="button"
                    onClick={() => {
                      setDescricao(grupo.descricao || "");
                      setEditando(false);
                    }}
                    disabled={carregando}
                    className = "button-excluir-grupo-produto"
                  >
                    Cancelar
                  </button>

                  <button
                    type="button"
                    onClick={salvarDescricao}
                    disabled={carregando}
                    className = "button-adicionar-grupo-produto"
                  >
                    {carregando ? "Salvando..." : "Salvar"}
                  </button>
                </>
              ) : (
                <>
                <button
                    type="button"
                    onClick={() => setModalExcluirAberta(true)}
                    className = "button-excluir-grupo-produto"
                >
                    Excluir
                </button>

                  <button
                    type="button"
                    onClick={() => setEditando(true)}
                    className = "button-adicionar-grupo-produto"
                  >
                    Editar
                  </button>
                </>
              )}
            </div>
        </div>
      </div>
    <ModalConfirmarExclusaoGrupoProduto
        aberta={modalExcluirAberta}
        onFechar={() => setModalExcluirAberta(false)}
        onConfirmar={excluirGrupo}
    />
    </div>
  );
}
