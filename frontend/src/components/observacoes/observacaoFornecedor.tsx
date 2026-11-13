
import { IoClose } from "react-icons/io5";
import { useEffect, useState, type FormEvent } from "react";

import { apiFetch } from "../../../services/api";

import "../../styles/modalObs.css";
import "../../styles/modalCriacaoMensagem.css";
import "../../styles/observacaoFornecedor.css";

import ModalConfirmarExclusaoFornecedor from "../modals/modalExcluirFornecedor";

interface ModalObservacoesFornecedorProps {
  aberta: boolean;
  onFechar: () => void;
  fornecedorId: number;
  observacao: string | null;
  onDeletar?: () => void;
}

interface Fornecedor {
  id: number;
  razaoSocial: string;
  nomeFantasia: string | null;
  documento: string | null;
  inscricaoEstadual: string | null;
  email: string | null;
  telefone: string | null;
  nomeContato: string | null;
  cep: string | null;
  logradouro: string | null;
  numero: string | null;
  complemento: string | null;
  bairro: string | null;
  cidade: string | null;
  estado: string | null;
  diasEntrega: number | null;
  condicaoPagamento: string | null;
  observacao: string | null;
}

const fornecedorVazio: Fornecedor = {
  id: 0,
  razaoSocial: "",
  nomeFantasia: "",
  documento: "",
  inscricaoEstadual: "",
  email: "",
  telefone: "",
  nomeContato: "",
  cep: "",
  logradouro: "",
  numero: "",
  complemento: "",
  bairro: "",
  cidade: "",
  estado: "",
  diasEntrega: null,
  condicaoPagamento: "",
  observacao: "",
};

export default function ModalObservacoesFornecedor({
  aberta,
  onFechar,
  fornecedorId,
  observacao,
  onDeletar,
}: ModalObservacoesFornecedorProps) {
  const [fornecedor, setFornecedor] = useState<Fornecedor>(fornecedorVazio);
  const [carregando, setCarregando] = useState(false);
  const [salvando, setSalvando] = useState(false);
  const [erros, setErros] = useState<string[]>([]);
  const [sucesso, setSucesso] = useState("");
  const [modalExcluirAberta, setModalExcluirAberta] = useState(false);
  const [excluindo, setExcluindo] = useState(false);

  useEffect(() => {
    if (!aberta) return;

    async function carregarFornecedor() {
      setCarregando(true);
      setErros([]);
      setSucesso("");

      try {
        const dados = await apiFetch<Fornecedor>(
          `/api/Fornecedores/${fornecedorId}`
        );

        setFornecedor({
          ...fornecedorVazio,
          ...dados,
          observacao: dados.observacao ?? observacao ?? "",
        });
      } catch (error) {
        console.error(error);
        setErros(["Não foi possível carregar os dados do fornecedor."]);
      } finally {
        setCarregando(false);
      }
    }

    carregarFornecedor();
  }, [aberta, fornecedorId, observacao]);

  if (!aberta) return null;

  function atualizarCampo(
    campo: keyof Fornecedor,
    valor: string | number | null
  ) {
    setFornecedor((anterior) => ({
      ...anterior,
      [campo]: valor,
    }));

    setSucesso("");
  }

  async function salvarAlteracoes(e: FormEvent<HTMLFormElement>) {
    e.preventDefault();

    setErros([]);
    setSucesso("");

    if (!fornecedor.razaoSocial.trim()) {
      setErros(["A razão social é obrigatória."]);
      return;
    }

    setSalvando(true);

    try {
      await apiFetch(`/api/Fornecedores/${fornecedorId}`, {
        method: "PUT",
        body: JSON.stringify({
          id: fornecedorId,
          razaoSocial: fornecedor.razaoSocial,
          nomeFantasia: fornecedor.nomeFantasia,
          documento: fornecedor.documento,
          inscricaoEstadual: fornecedor.inscricaoEstadual,
          email: fornecedor.email,
          telefone: fornecedor.telefone,
          nomeContato: fornecedor.nomeContato,
          cep: fornecedor.cep,
          logradouro: fornecedor.logradouro,
          numero: fornecedor.numero,
          complemento: fornecedor.complemento,
          bairro: fornecedor.bairro,
          cidade: fornecedor.cidade,
          estado: fornecedor.estado,
          diasEntrega:
            fornecedor.diasEntrega === null
              ? null
              : Number(fornecedor.diasEntrega),
          condicaoPagamento: fornecedor.condicaoPagamento,
          observacao: fornecedor.observacao,
        }),
      });

      setSucesso("Dados do fornecedor atualizados com sucesso!");
    } catch (error) {
      const mensagem =
        error instanceof Error
          ? error.message
          : "Não foi possível atualizar o fornecedor.";

      setErros(mensagem.split("\n"));
    } finally {
      setSalvando(false);
    }
  }

  async function excluirFornecedor() {
  setExcluindo(true);

  try {
    await apiFetch(`/api/Fornecedores/${fornecedorId}`, {
      method: "DELETE",
    });

    setModalExcluirAberta(false);
    onFechar();
    onDeletar?.();
  } catch (error) {
    throw error;
  } finally {
    setExcluindo(false);
  }
}

  return (
    <div className="modal-overlay" onClick={onFechar}>
      <div
        className="modal-caixa modal-usuario modal-observacao-fornecedor"
        onClick={(e) => e.stopPropagation()}
      >
        <div className="modal-usuario-cabecalho">
          <div className="modal-usuario-titulo">
            <div className="modal-titulos-8">
              <h2>Editar fornecedor</h2>
            </div>
          </div>

          <button
            type="button"
            className="modal-close"
            onClick={onFechar}
            disabled={salvando}
          >
            <IoClose />
          </button>
        </div>

        {carregando ? (
          <p className="fornecedor-carregando">
            Carregando informações do fornecedor...
          </p>
        ) : (
          <form
            className="modal-usuario-form"
            onSubmit={salvarAlteracoes}
          >
            <h3>Dados cadastrais</h3>

            <div className="fornecedor-info-grid">
              <div>
                <label>Razão Social</label>
                <input
                  type="text"
                  value={fornecedor.razaoSocial}
                  onChange={(e) =>
                    atualizarCampo("razaoSocial", e.target.value)
                  }
                  required
                />
              </div>

              <div>
                <label>Nome Fantasia</label>
                <input
                  type="text"
                  value={fornecedor.nomeFantasia ?? ""}
                  onChange={(e) =>
                    atualizarCampo("nomeFantasia", e.target.value)
                  }
                />
              </div>

              <div>
                <label>Documento</label>
                <input
                  type="text"
                  value={fornecedor.documento ?? ""}
                  onChange={(e) =>
                    atualizarCampo("documento", e.target.value)
                  }
                />
              </div>

              <div>
                <label>Inscrição Estadual</label>
                <input
                  type="text"
                  value={fornecedor.inscricaoEstadual ?? ""}
                  onChange={(e) =>
                    atualizarCampo("inscricaoEstadual", e.target.value)
                  }
                />
              </div>
            </div>

            <h3>Informações de contato</h3>

            <div className="fornecedor-info-grid">
              <div>
                <label>E-mail</label>
                <input
                  type="email"
                  value={fornecedor.email ?? ""}
                  onChange={(e) =>
                    atualizarCampo("email", e.target.value)
                  }
                />
              </div>

              <div>
                <label>Telefone</label>
                <input
                  type="tel"
                  value={fornecedor.telefone ?? ""}
                  onChange={(e) =>
                    atualizarCampo("telefone", e.target.value)
                  }
                />
              </div>

              <div>
                <label>Nome do contato</label>
                <input
                  type="text"
                  value={fornecedor.nomeContato ?? ""}
                  onChange={(e) =>
                    atualizarCampo("nomeContato", e.target.value)
                  }
                />
              </div>
            </div>

            <h3>Endereço</h3>

            <div className="fornecedor-info-grid">
              <div>
                <label>CEP</label>
                <input
                  type="text"
                  value={fornecedor.cep ?? ""}
                  onChange={(e) =>
                    atualizarCampo("cep", e.target.value)
                  }
                />
              </div>

              <div>
                <label>Logradouro</label>
                <input
                  type="text"
                  value={fornecedor.logradouro ?? ""}
                  onChange={(e) =>
                    atualizarCampo("logradouro", e.target.value)
                  }
                />
              </div>

              <div>
                <label>Número</label>
                <input
                  type="text"
                  value={fornecedor.numero ?? ""}
                  onChange={(e) =>
                    atualizarCampo("numero", e.target.value)
                  }
                />
              </div>

              <div>
                <label>Complemento</label>
                <input
                  type="text"
                  value={fornecedor.complemento ?? ""}
                  onChange={(e) =>
                    atualizarCampo("complemento", e.target.value)
                  }
                />
              </div>

              <div>
                <label>Bairro</label>
                <input
                  type="text"
                  value={fornecedor.bairro ?? ""}
                  onChange={(e) =>
                    atualizarCampo("bairro", e.target.value)
                  }
                />
              </div>

              <div>
                <label>Cidade</label>
                <input
                  type="text"
                  value={fornecedor.cidade ?? ""}
                  onChange={(e) =>
                    atualizarCampo("cidade", e.target.value)
                  }
                />
              </div>

              <div>
                <label>Estado</label>
                <input
                  type="text"
                  value={fornecedor.estado ?? ""}
                  onChange={(e) =>
                    atualizarCampo("estado", e.target.value)
                  }
                />
              </div>
            </div>

            <h3>Entrega e pagamento</h3>

            <div className="fornecedor-info-grid">
              <div>
                <label>Dias para entrega</label>
                <input
                  type="number"
                  min="0"
                  value={fornecedor.diasEntrega ?? ""}
                  onChange={(e) =>
                    atualizarCampo(
                      "diasEntrega",
                      e.target.value === "" ? null : Number(e.target.value)
                    )
                  }
                />
              </div>

              <div>
                <label>Condição de pagamento</label>
                <input
                  type="text"
                  value={fornecedor.condicaoPagamento ?? ""}
                  onChange={(e) =>
                    atualizarCampo("condicaoPagamento", e.target.value)
                  }
                />
              </div>
            </div>

            <h3>Observações</h3>

            <textarea
              className="observacao-fornecedor-textarea"
              rows={4}
              value={fornecedor.observacao ?? ""}
              onChange={(e) =>
                atualizarCampo("observacao", e.target.value)
              }
              placeholder="Digite as observações do fornecedor..."
            />

            {erros.length > 0 && (
              <div className="mensagens-erro" role="alert">
                {erros.map((mensagem, index) => (
                  <p key={`${index}-${mensagem}`}>{mensagem}</p>
                ))}
              </div>
            )}

            {sucesso && (
              <p className="mensagem-sucesso" role="status">
                {sucesso}
              </p>
            )}

            <div className="fornecedor-botoes-etapas">
            <button
                type="button"
                className="modal-usuario-delete"
                onClick={() => setModalExcluirAberta(true)}
                disabled={salvando || excluindo}
            >
                Excluir fornecedor
            </button>
            <button
                type="submit"
                className="modal-usuario-submit"
                disabled={salvando || excluindo}
            >
                {salvando ? "Salvando..." : "Salvar alterações"}
            </button>
            </div>
          </form>
        )}
      </div>
      <ModalConfirmarExclusaoFornecedor
        aberta={modalExcluirAberta}
        onFechar={() => setModalExcluirAberta(false)}
        onConfirmar={excluirFornecedor}
      />
    </div>
  );
}
