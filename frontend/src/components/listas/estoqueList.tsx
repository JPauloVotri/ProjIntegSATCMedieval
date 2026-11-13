
import "../../styles/estoqueMovimentList.css";

import { apiFetch } from "../../../services/api";
import { useEffect, useState } from "react";
import { FaSearch } from "react-icons/fa";

import PesquisaEstMov from "../pesquisas/pesquisaEstMov";
import Loading from "../loading";
import ObservaocaoProduto from "../observacoes/observacaoProduto";

interface Estoque {
  id: number;
  produtoId: number;
  produtoNome: string;
  quantidadeDisponivel: number;
  valorTotal: number;
}

interface Produto {
  id: number;
  grupoProdutoId: number;
  unidadeMedidaId: number;
  nome: string;
  codigo: string;
  descricao: string | null;
  estoqueMinimo: number;
  estoqueMaximo: number;
  validadeDias: number;
}

export default function EstoqueList() {
  const [loading, setLoading] = useState(true);

  const [estoques, setEstoques] = useState<Estoque[]>([]);
  const [produtos, setProdutos] = useState<Produto[]>([]);

  const [produtoSelecionado, setProdutoSelecionado] = useState<Produto | null>(null);

  const [erro, setErro] = useState("");
  const [pesquisa, setPesquisa] = useState("");

  useEffect(() => {
    async function carregarEstoques() {
      try {
        setLoading(true);

        const [dadosEstoque, dadosProdutos] = await Promise.all([
          apiFetch<Estoque[]>("/api/Estoques"),
          apiFetch<Produto[]>("/api/Produtos"),
        ]);

        setEstoques(dadosEstoque);
        setProdutos(dadosProdutos);

      } catch (error) {
        setErro("Não foi possível carregar os estoques.");
        console.error(error);
      } finally {
        setLoading(false);
      }
    }

    carregarEstoques();
  }, []);

  const estoquesFiltrados = estoques.filter((estoque) =>
    estoque.produtoNome
      .toLowerCase()
      .startsWith(pesquisa.trim().toLowerCase())
  );

  function abrirProduto(estoque: Estoque) {
    const produto = produtos.find(
      (p) => p.id === estoque.produtoId
    ) || produtos.find(
      (p) => p.nome === estoque.produtoNome
    );

    if (!produto) {
      setErro("Não foi possível encontrar as informações deste produto.");
      return;
    }

    setErro("");
    setProdutoSelecionado(produto);
  }

  return (
    <div className="estoque-moviment-container">

      {loading ? (
        <Loading />
      ) : (
        <>
          <div className="estoque-moviment-pesquisa">
            <PesquisaEstMov onPesquisar={setPesquisa} />
          </div>

          {erro && (
            <p className="estoque-moviment-erro">{erro}</p>
          )}

          <div className="estoque-moviment-tabela">

            <div className="estoque-moviment-header">
              <div className="estoque-moviment-nome">Produto</div>
              <div className="estoque-moviment-valor-total">Valor Total</div>
              <div className="estoque-moviment-quantidade">Quantidade</div>
              <div className="estoque-moviment-quantidade">Detalhes</div>
            </div>

            {estoquesFiltrados.map((estoque) => (
              <div
                key={estoque.id}
                className="estoque-moviment-list"
              >
                <div className="estoque-moviment-nome">
                  {estoque.produtoNome}
                </div>

                <div className="estoque-moviment-valor-total">
                  {estoque.valorTotal.toLocaleString("pt-BR", {
                    style: "currency",
                    currency: "BRL",
                  })}
                </div>

                <div className="estoque-moviment-quantidade">
                  <span
                    className={`estoque-moviment-status ${
                      estoque.quantidadeDisponivel < 1
                        ? "estoque-moviment-negativo"
                        : ""
                    }`}
                  >
                    {estoque.quantidadeDisponivel}
                  </span>
                </div>

                <div className="estoque-moviment-quantidade">
                  <button
                    type="button"
                    className="estoque-moviment-botao"
                    onClick={() => abrirProduto(estoque)}
                    aria-label="Ver informações do produto"
                  >
                    <FaSearch />
                  </button>
                </div>
              </div>
            ))}

            {estoquesFiltrados.length === 0 && (
              <div className="estoque-moviment-list-empty">
                Nenhum produto encontrado.
              </div>
            )}

          </div>

          <ObservaocaoProduto
            aberta={produtoSelecionado !== null}
            produto={produtoSelecionado}
            onFechar={() => setProdutoSelecionado(null)}
          />
        </>
      )}

    </div>
  );
}
