
import { useState, useEffect } from "react";
import { IoClose } from "react-icons/io5";

import { apiFetch } from "../../../services/api";

import "../../styles/modalObs.css";
import "../../styles/modalGrupoProduto.css";

import ModalConfirmarExclusaoCotacao from "./modalExcluirCotacao";

interface Cotacao {
  id: number;
  usuarioId: number;
  dataSolicitacao: string;
  dataLimiteResposta: string | null;
  status: number;
  observacao: string | null;
}

interface ModalCotacaoProps {
  aberta: boolean;
  cotacao: Cotacao | null;
  onFechar: () => void;
  onAtualizar: (cotacao: Cotacao) => void;
  onExcluir: (id: number) => void;
}

export default function ModalCotacao({
  aberta,
  cotacao,
  onFechar,
  onAtualizar,
  onExcluir,
}: ModalCotacaoProps) {
  const [modalExcluirAberta, setModalExcluirAberta] = useState(false);
  const [editando, setEditando] = useState(false);

  const [dataSolicitacao, setDataSolicitacao] = useState("");
  const [dataLimiteResposta, setDataLimiteResposta] = useState("");
  const [observacao, setObservacao] = useState("");

  const [erros, setErros] = useState<string[]>([]);
  const [carregando, setCarregando] = useState(false);

  useEffect(() => {
    if (cotacao) {
      setDataSolicitacao(cotacao.dataSolicitacao?.split("T")[0] || "");
      setDataLimiteResposta(cotacao.dataLimiteResposta?.split("T")[0] || "");
      setObservacao(cotacao.observacao || "");

      setEditando(false);
      setModalExcluirAberta(false);
      setErros([]);
    }
  }, [cotacao]);

  if (!aberta || !cotacao) return null;

  async function salvarCotacao() {
    if (!cotacao) return;

    setErros([]);
    setCarregando(true);

    try {
      await apiFetch(`/api/Cotacoes/${cotacao.id}`, {
        method: "PUT",
        body: JSON.stringify({
          usuarioId: cotacao.usuarioId,
          dataSolicitacao: dataSolicitacao || "",
          dataLimiteResposta: dataLimiteResposta || null,
          status: cotacao.status,
          observacao: observacao.trim() || null,
        }),
      });

      onAtualizar({
        ...cotacao,
        dataSolicitacao,
        dataLimiteResposta,
        observacao,
      });

      setEditando(false);
    } catch (error) {
      const mensagem =
        error instanceof Error
          ? error.message
          : "Não foi possível atualizar a cotação.";

      setErros(mensagem.split("\n"));
    } finally {
      setCarregando(false);
    }
  }

  async function excluirCotacao() {
    if (!cotacao) return;

    setModalExcluirAberta(false);
    onExcluir(cotacao.id);
    onFechar();
  }

  function fecharModal() {
    setEditando(false);
    setModalExcluirAberta(false);
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
          <div className="modal-titulos-2">
            <h2>Observações da Cotação</h2>
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

          <label>Data de Solicitação</label>

          {editando ? (
            <input
              type="date"
              value={dataSolicitacao}
              onChange={(e) => setDataSolicitacao(e.target.value)}
            />
          ) : (
            <div className="modal-grupo-data">
              {dataSolicitacao
                ? new Date(dataSolicitacao + "T12:00:00").toLocaleDateString("pt-BR")
                : "Não informada"}
            </div>
          )}

          <label>Data Limite de Resposta</label>

          {editando ? (
            <input
              type="date"
              value={dataLimiteResposta}
              onChange={(e) => setDataLimiteResposta(e.target.value)}
            />
          ) : (
            <div className="modal-grupo-data">
              {dataLimiteResposta
                ? new Date(dataLimiteResposta + "T12:00:00").toLocaleDateString("pt-BR")
                : "Não informada"}
            </div>
          )}

          <label>Observação</label>

          {editando ? (
            <textarea
              value={observacao}
              onChange={(e) => setObservacao(e.target.value)}
              placeholder="Digite uma observação..."
              rows={5}
            />
          ) : (
            <div className="modal-grupo-descricao">
              {observacao || "Nenhuma observação cadastrada."}
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
                    setDataSolicitacao(cotacao.dataSolicitacao?.split("T")[0] || "");
                    setDataLimiteResposta(cotacao.dataLimiteResposta?.split("T")[0] || "");
                    setObservacao(cotacao.observacao || "");
                    setEditando(false);
                  }}
                  disabled={carregando}
                  className="button-excluir-grupo-produto"
                >
                  Cancelar
                </button>

                <button
                  type="button"
                  onClick={salvarCotacao}
                  disabled={carregando}
                  className="button-adicionar-grupo-produto"
                >
                  {carregando ? "Salvando..." : "Salvar"}
                </button>
              </>
            ) : (
              <>
                <button
                  type="button"
                  onClick={() => setModalExcluirAberta(true)}
                  className="button-excluir-grupo-produto"
                >
                  Excluir
                </button>

                <button
                  type="button"
                  onClick={() => setEditando(true)}
                  className="button-adicionar-grupo-produto"
                >
                  Editar
                </button>
              </>
            )}
          </div>
        </div>
      </div>

      <ModalConfirmarExclusaoCotacao
        aberta={modalExcluirAberta}
        onFechar={() => setModalExcluirAberta(false)}
        onConfirmar={excluirCotacao}
      />
    </div>
  );
}
