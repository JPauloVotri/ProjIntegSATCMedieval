
import "../../styles/estoqueMovimentList.css";

import { apiFetch } from "../../../services/api";
import { useEffect, useState } from "react";
import PesquisaEstMov from "../pesquisas/pesquisaEstMov";

interface Estoque {
  id: number;
  produtoNome: string;
  quantidadeDisponivel: number;
  valorTotal: number;
}

export default function EstoqueList() {
  const [estoques, setEstoques] = useState<Estoque[]>([]);
  const [erro, setErro] = useState("");
  const [pesquisa, setPesquisa] = useState("");

  useEffect(() => {
    async function carregarEstoques() {
      try {
        const dados = await apiFetch<Estoque[]>("/api/Estoques");
        setEstoques(dados);
      } catch (error) {
        setErro("Não foi possível carregar os estoques.");
        console.error(error);
      }
    }

    carregarEstoques();
  }, []);

  const estoquesFiltrados = estoques.filter((estoque) =>
    estoque.produtoNome
      .toLowerCase()
      .startsWith(pesquisa.trim().toLowerCase())
  );

  return (
    <div className="estoque-moviment-container">

      <div className="estoque-moviment-pesquisa">
        <PesquisaEstMov onPesquisar={setPesquisa} />
      </div>

      {erro && <p className="estoque-moviment-erro">{erro}</p>}

      <div className="estoque-moviment-tabela">

        <div className="estoque-moviment-header">
          <div className="estoque-moviment-nome">Produto</div>
          <div className="estoque-moviment-valor-total">Valor Total</div>
          <div className="estoque-moviment-quantidade">Quantidade</div>
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
          </div>
        ))}

        {estoquesFiltrados.length === 0 && (
          <div className="estoque-moviment-list-empty">
            Nenhum produto encontrado.
          </div>
        )}

      </div>
    </div>
  );
}
