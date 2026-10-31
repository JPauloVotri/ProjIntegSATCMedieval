import { apiFetch } from "../../services/api";
import { useState, type FormEvent } from "react";

import "../styles/modalCriacaoMensagem.css";
import "../styles/ModalCadastro.css";

import SelectGruposProdutos from "./selectGrupoprodutos";
import SelectUnidadeMedida from "./selectUnidadeMedida";

interface ModalCadastroProps {
  aberta: boolean;
  onFechar: () => void;
}

export default function ModalCadastro({
  aberta,
  onFechar,
}: ModalCadastroProps) {
  const [grupoProdutoId, setGrupoProdutoId] = useState<number | "">("");
  const [unidadeMedidaId, setUnidadeMedidaId] = useState<number | "">("");
  const [nome, setNome] = useState("");
  const [codigo, setCodigo] = useState("");
  const [descricao, setDescricao] = useState("");
  const [estoqueMinimo, setEstoqueMinimo] = useState("");
  const [estoqueMaximo, setEstoqueMaximo] = useState("");
  const [validadeDias, setValidadeDias] = useState("");

  const [erros, setErros] = useState<string[]>([]);
  const [sucesso, setSucesso] = useState("");

  if (!aberta) return null;

  async function cadastrarProduto(e: FormEvent<HTMLFormElement>) {
    e.preventDefault();

    setErros([]);
    setSucesso("");

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

      setSucesso("Produto cadastrado com sucesso!");
      setGrupoProdutoId("");
      setUnidadeMedidaId("");
      setNome("");
      setCodigo("");
      setDescricao("");
      setEstoqueMinimo("");
      setEstoqueMaximo("");
      setValidadeDias("");
    } catch (error) {
      const mensagem =
        error instanceof Error
          ? error.message
          : "Não foi possível cadastrar o produto.";

      setErros(mensagem.split("\n"));
    }
  }

  return (
    <div className="modal-overlay" onClick={onFechar}>
      <div
        className="modal-cadastro"
        onClick={(e) => e.stopPropagation()}
      >
        <form onSubmit={cadastrarProduto}>

          <label htmlFor="grupoProdutoId">Grupo de Produto:</label>
          <SelectGruposProdutos
            grupoProdutoId={grupoProdutoId}
            setGrupoProdutoId={setGrupoProdutoId}
          />

          <label htmlFor="grupoUnidadeId">Grupo de Produto:</label>
          <SelectUnidadeMedida
            unidadeMedidaId={unidadeMedidaId}
            setUnidadeMedidaId={setUnidadeMedidaId}
          />

          <label htmlFor="nome">Nome:</label>
          <input
            id="nome"
            type="text"
            value={nome}
            onChange={(e) => setNome(e.target.value)}
          />

          <label htmlFor="codigo">Codigo:</label>
          <input
            id="codigo"
            type="text"
            value={codigo}
            onChange={(e) => setCodigo(e.target.value)}
          />

          <label htmlFor="descricao">Descrição:</label>
          <input
            id="descricao"
            type="text"
            value={descricao}
            onChange={(e) => setDescricao(e.target.value)}
          />

          <label htmlFor="estoqueMinimo">Estoque Minimo:</label>
          <input
            id="estoque minimo"
            type="text"
            value={estoqueMinimo}
            onChange={(e) => setEstoqueMinimo(e.target.value)}
          />

          <label htmlFor="estoqueMaximo">Estoque Maximo:</label>
          <input
            id="estoque maximo"
            type="text"
            value={estoqueMaximo}
            onChange={(e) => setEstoqueMaximo(e.target.value)}
          />

          <label htmlFor="validadeDias">Validade de Dias:</label>
          <input
            id="validade dias"
            type="text"
            value={validadeDias}
            onChange={(e) => setValidadeDias(e.target.value)}
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