
import { IoClose } from "react-icons/io5";

import "../../styles/modalObs.css";
import "../../styles/modalCriacaoMensagem.css";
import "../../styles/modalCriacaoFornecedor.css";

import { apiFetch } from "../../../services/api";
import { useState, type FormEvent } from "react";

import SelectGruposProdutos from "../selects/selectGrupoProdutos";
import SelectUnidadeMedida from "../selects/selectUnidadeMedida";

interface ModalCadastroProps {
  aberta: boolean;
  onFechar: () => void;
}

interface Produto {
  id: number;
  nome: string;
  codigo: number;
}

export default function ModalCadastroProduto({
  aberta,
  onFechar,
}: ModalCadastroProps) {
  const [etapa, setEtapa] = useState(1);

  const [grupoProdutoId, setGrupoProdutoId] = useState<number | "">("");
  const [unidadeMedidaId, setUnidadeMedidaId] = useState<number | "">("");

  const [nome, setNome] = useState("");
  const [codigo, setCodigo] = useState("");
  const [descricao, setDescricao] = useState("");

  const [estoqueMinimo, setEstoqueMinimo] = useState("");
  const [estoqueMaximo, setEstoqueMaximo] = useState("");
  const [validadeDias, setValidadeDias] = useState("");

  const [quantidadeDisponivel, setQuantidadeDisponivel] = useState("");
  const [valorTotal, setValorTotal] = useState("");
  const [ultimaEntrada, setUltimaEntrada] = useState("");
  const [ultimaSaida, setUltimaSaida] = useState("");

  const [erros, setErros] = useState<string[]>([]);
  const [sucesso, setSucesso] = useState("");
  const [carregando, setCarregando] = useState(false);

  function limparCampos() {
    setGrupoProdutoId("");
    setUnidadeMedidaId("");
    setNome("");
    setCodigo("");
    setDescricao("");

    setEstoqueMinimo("");
    setEstoqueMaximo("");
    setValidadeDias("");

    setQuantidadeDisponivel("");
    setValorTotal("");
    setUltimaEntrada("");
    setUltimaSaida("");
  }

  function fecharModal() {
    if (carregando) return;

    setEtapa(1);
    setErros([]);
    setSucesso("");
    onFechar();
  }

  function validarEtapa(): boolean {
    const novosErros: string[] = [];

    if (etapa === 1) {
      if (!grupoProdutoId) {
        novosErros.push("Selecione um grupo de produto.");
      }

      if (!unidadeMedidaId) {
        novosErros.push("Selecione uma unidade de medida.");
      }

      if (!nome.trim()) {
        novosErros.push("Nome do produto é obrigatório.");
      }
    }

    if (etapa === 2) {
      if (!estoqueMinimo.trim()) {
        novosErros.push("Estoque mínimo é obrigatório.");
      }

      if (!estoqueMaximo.trim()) {
        novosErros.push("Estoque máximo é obrigatório.");
      }

      if (!validadeDias.trim()) {
        novosErros.push("Validade em dias é obrigatório.");
      }
    }

    if (etapa === 3) {
      if (!quantidadeDisponivel.trim()) {
        novosErros.push("Quantidade disponível é obrigatório.");
      }

      if (!valorTotal.trim()) {
        novosErros.push("Valor total é obrigatório.");
      }

      if (!ultimaEntrada.trim()) {
        novosErros.push("Última entrada é obrigatório.");
      }

      if (!ultimaSaida.trim()) {
        novosErros.push("Última saída é obrigatório.");
      }
    }

    setErros(novosErros);

    return novosErros.length === 0;
  }

  function avancarEtapa() {
    if (!validarEtapa()) return;

    setErros([]);

    if (etapa < 3) {
      setEtapa(etapa + 1);
    }
  }

  function voltarEtapa() {
    setErros([]);

    if (etapa > 1) {
      setEtapa(etapa - 1);
    }
  }

  async function cadastrarProduto() {

    if (etapa !== 3) {
      avancarEtapa();
      return;
    }

    if (!validarEtapa()) return;

    setErros([]);
    setSucesso("");
    setCarregando(true);

    try {
      await apiFetch("/api/Produtos", {
        method: "POST",
        body: JSON.stringify({
          grupoProdutoId,
          unidadeMedidaId,
          nome,
          codigo,
          descricao,
          estoqueMinimo,
          estoqueMaximo,
          validadeDias,
        }),
      });

      const produtos = await apiFetch("/api/Produtos") as Produto[];

      const produtoCriado = produtos.find(
        (produto) =>
          produto.nome.trim() === nome.trim() &&
          String(produto.codigo) === String(codigo)
      );

      if (!produtoCriado) {
        throw new Error(
          "Produto cadastrado, mas não foi possível localizar seu registro."
        );
      }

      await apiFetch("/api/Estoques", {
        method: "POST",
        body: JSON.stringify({
          produtoId: produtoCriado.id,
          quantidadeDisponivel,
          valorTotal,
          ultimaEntrada: new Date(ultimaEntrada),
          ultimaSaida: new Date(ultimaSaida),
        }),
      });

      setSucesso("Produto e estoque cadastrados com sucesso!");

      limparCampos();
      setEtapa(1);

    } catch (error) {
      const mensagem =
        error instanceof Error
          ? error.message
          : "Não foi possível cadastrar o produto.";

      setErros(mensagem.split("\n"));

    } finally {
      setCarregando(false);
    }
  }

  if (!aberta) return null;

  return (
    <div className="modal-overlay" onClick={fecharModal}>
      <div
        className="modal-caixa modal-usuario modal-fornecedor"
        onClick={(e) => e.stopPropagation()}
      >
        <div className="modal-usuario-cabecalho">
          <div className="modal-usuario-titulo">
            <div className="modal-titulos-9">
              <h2>Adicionar Produto</h2>
            </div>
          </div>

          <button
            type="button"
            className="modal-close"
            onClick={fecharModal}
            disabled={carregando}
          >
            <IoClose />
          </button>
        </div>

        <div className="fornecedor-progresso">
          <div className="fornecedor-progresso-info">
            <span>Etapa {etapa} de 3</span>

            <span>
              {etapa === 1
                ? "Informações do produto"
                : etapa === 2
                  ? "Controle de estoque"
                  : "Estoque inicial"}
            </span>
          </div>

          <div className="fornecedor-progresso-barra">
            <div
              className="fornecedor-progresso-preenchimento"
              style={{ width: `${(etapa / 3) * 100}%` }}
            />
          </div>

          <div className="fornecedor-etapas">
            <span className={etapa >= 1 ? "ativa" : ""}>
              1. Produto
            </span>

            <span className={etapa >= 2 ? "ativa" : ""}>
              2. Controle
            </span>

            <span className={etapa >= 3 ? "ativa" : ""}>
              3. Estoque
            </span>
          </div>
        </div>

        <form
          className="modal-usuario-form"
          onSubmit={(e) => e.preventDefault()}
        >
          {etapa === 1 && (
            <>
              <h3>Informações do produto</h3>

              <label>Grupo de Produto</label>
              <SelectGruposProdutos
                grupoProdutoId={grupoProdutoId}
                setGrupoProdutoId={setGrupoProdutoId}
              />

              <label>Unidade de Medida</label>
              <SelectUnidadeMedida
                unidadeMedidaId={unidadeMedidaId}
                setUnidadeMedidaId={setUnidadeMedidaId}
              />

              <label>Nome</label>
              <input
                type="text"
                placeholder="Nome do produto"
                value={nome}
                onChange={(e) => setNome(e.target.value)}
              />

              <label>Código</label>
              <input
                type="text"
                placeholder="Código do produto"
                value={codigo}
                onChange={(e) => setCodigo(e.target.value)}
              />

              <label>Descrição</label>
              <textarea
                placeholder="Descrição do produto"
                value={descricao}
                onChange={(e) => setDescricao(e.target.value)}
              />
            </>
          )}

          {etapa === 2 && (
            <>
              <h3>Controle de estoque</h3>

              <label>Estoque mínimo</label>
              <input
                type="number"
                placeholder="Quantidade mínima"
                value={estoqueMinimo}
                onChange={(e) => setEstoqueMinimo(e.target.value)}
              />

              <label>Estoque máximo</label>
              <input
                type="number"
                placeholder="Quantidade máxima"
                value={estoqueMaximo}
                onChange={(e) => setEstoqueMaximo(e.target.value)}
              />

              <label>Validade (dias)</label>
              <input
                type="number"
                placeholder="Validade em dias"
                value={validadeDias}
                onChange={(e) => setValidadeDias(e.target.value)}
              />
            </>
          )}

          {etapa === 3 && (
            <>
              <h3>Estoque inicial</h3>

              <label>Quantidade disponível</label>
              <input
                type="number"
                placeholder="Quantidade inicial"
                value={quantidadeDisponivel}
                onChange={(e) =>
                  setQuantidadeDisponivel(e.target.value)
                }
              />

              <label>Valor total</label>
              <input
                type="number"
                step="0.01"
                placeholder="Valor total"
                value={valorTotal}
                onChange={(e) => setValorTotal(e.target.value)}
              />

              <label>Última entrada</label>
              <input
                type="date"
                value={ultimaEntrada}
                onChange={(e) => setUltimaEntrada(e.target.value)}
              />

              <label>Última saída</label>
              <input
                type="date"
                value={ultimaSaida}
                onChange={(e) => setUltimaSaida(e.target.value)}
              />
            </>
          )}

          <div className="fornecedor-botoes-etapas">
            {etapa > 1 && (
              <button
                type="button"
                className="fornecedor-voltar"
                onClick={voltarEtapa}
                disabled={carregando}
              >
                Voltar
              </button>
            )}

            {etapa < 3 ? (
              <button
                type="button"
                className="modal-usuario-submit"
                onClick={avancarEtapa}
              >
                Próximo
              </button>
            ) : (
              <button
                type="button"
                className="modal-usuario-submit"
                disabled={carregando}
                onClick={cadastrarProduto}
              >
                {carregando ? "Cadastrando..." : "Cadastrar"}
              </button>
            )}
          </div>

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
