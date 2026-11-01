import { apiFetch } from "../../services/api";
import { useState, useEffect, type FormEvent } from "react";

import "../styles/modalCriacaoMensagem.css";
import "../styles/ModalCadastro.css";

import SelectGruposProdutos from "./selectGrupoProdutos";
import SelectUnidadeMedida from "./selectUnidadeMedida";

interface ModalCadastroProps {
  aberta: boolean;
  onFechar: () => void;
}

interface Produto {
  id: number;
  nome: string;
  codigo: number;
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
  const [quantidadeDisponivel, setQuantidadeDisponivel] = useState("");
  const [valorTotal, setValorTotal] = useState("");
  const [ultimaEntrada, setUltimaEntrada] = useState("");
  const [ultimaSaida, setUltimaSaida] = useState("");

  const [erros, setErros] = useState<string[]>([]);
  const [sucesso, setSucesso] = useState("");

  if (!aberta) return null;

  async function buscarProdutos(): Promise<Produto[]> {
  const resposta = await apiFetch("/api/Produtos", {
    method: "GET",
  });

  return resposta as Produto[];
}

  async function cadastrarProduto(e: FormEvent<HTMLFormElement>) {
    e.preventDefault();

    const nomeProduto = nome;
    const codigoProduto = Number(codigo);

    setErros([]);
    setSucesso("");
    if (nome === "") {
      setErros(["Nome é obrigatório."]);
      return;
    } else if (estoqueMinimo == "") {
      setErros(["Estoque minimo é obrigatório."]);
      return;
    } else if (estoqueMaximo == "") {
      setErros(["Estoque máximo é obrigatório."]);
      return;
    } else if (estoqueMinimo > estoqueMaximo) {
      setErros(["O estoque minimo não pode ser maior que o estoque maximo."]);
      return;
    } else if (grupoProdutoId == "") {
      setErros(["Grupo do Produto é Obrigatório"]);
      return;
    } else if (unidadeMedidaId == "") {
      setErros(["Unidade de Medida é Obrigatório"]);
      return;
    } else if (validadeDias == "") {
      setErros(["Validade de Dias é Obrigatório"]);
      return;
    } else if (quantidadeDisponivel == "") {
      setErros(["Quantidade Disponivel é Obrigatório"]);
      return;
    } else if (valorTotal == "") {
      setErros(["Valor Total é Obrigatório"]);
      return;
    } else if (quantidadeDisponivel == "") {
      setErros(["Última Entrada é Obrigatório"]);
      return;
    } else if (ultimaSaida == "") {
      setErros(["Última Saida é Obrigatório"]);
      return;
    }
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

    } catch (error) {
      const mensagem =
        error instanceof Error
          ? error.message
          : "Não foi possível cadastrar o produto.";

      setErros(mensagem.split("\n"));
      return
    }

    setErros([]);
    setSucesso("");

    const produtos = await buscarProdutos();

    const produtoCriado = produtos.find(
      (produto) =>
        produto.nome?.trim() === nomeProduto.trim() &&
        String(produto.codigo) === String(codigoProduto)
      );

    if (!produtoCriado) {
      setErros(["Não foi possível localizar o produto cadastrado."]);
      return;
    }

    try {
      await apiFetch("/api/Estoques", {
        method: "POST",
        body: JSON.stringify({
          produtoId: produtoCriado.id,
          quantidadeDisponivel,
          valorTotal,
          ultimaEntrada,
          ultimaSaida,
        }),
      });

      setSucesso("Grupo de produtos cadastrado com sucesso!");
      setGrupoProdutoId("");
      setUnidadeMedidaId("");
      setNome("");
      setCodigo("");
      setDescricao("");
      setEstoqueMinimo("");
      setEstoqueMaximo("");
      setValidadeDias("");
      setQuantidadeDisponivel("")
      setValorTotal("")
      setUltimaEntrada("")
      setUltimaSaida("")
    } catch (error) {
      const mensagem =
        error instanceof Error
          ? error.message
          : "Não foi possível cadastrar o grupo de produtos.";

      setErros(mensagem.split("\n"));
    }
  }

  function fecharModal() {
    setErros([]);
    setSucesso("");
  }

  return (
    <div className="modal-overlay" onClick={() => { fecharModal(); onFechar();}}>
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
            type="number"
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
            type="number"
            value={estoqueMinimo}
            onChange={(e) => setEstoqueMinimo(e.target.value)}
          />

          <label htmlFor="estoqueMaximo">Estoque Maximo:</label>
          <input
            id="estoque maximo"
            type="number"
            value={estoqueMaximo}
            onChange={(e) => setEstoqueMaximo(e.target.value)}
          />

          <label htmlFor="validadeDias">Validade de Dias:</label>
          <input
            id="validade dias"
            type="number"
            value={validadeDias}
            onChange={(e) => setValidadeDias(e.target.value)}
          />

          <label htmlFor="quantidadeDisponivel">Quantidade Disponivel:</label>
          <input
            id="quantidade disponivel"
            type="number"
            value={quantidadeDisponivel}
            onChange={(e) => setQuantidadeDisponivel(e.target.value)}
          />

        <label htmlFor="ValorTotal">Valor Total:</label>
          <input
            id="Valor total"
            type="number"
            value={valorTotal}
            onChange={(e) => setValorTotal(e.target.value)}
          />

        <label htmlFor="ultimaEntrada">Ultima Entrada:</label>
          <input
            id="ultima entrada"
            type="date"
            value={ultimaEntrada}
            onChange={(e) => setUltimaEntrada(e.target.value)}
          />

        <label htmlFor="ultimaSaida">Ultima Saida:</label>
          <input
            id="ultima saida"
            type="date"
            value={ultimaSaida}
            onChange={(e) => setUltimaSaida(e.target.value)}
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