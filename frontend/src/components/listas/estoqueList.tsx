
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
    <div>
      <PesquisaEstMov onPesquisar={setPesquisa} />

      {erro && <p>{erro}</p>}

      <div className="estoque-moviment-header">
        <div className="estoque-moviment-nome">Nome</div>
        <div className="estoque-moviment-quantidade">Valor Total</div>
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

          <div className="estoque-moviment-quantidade">
            {estoque.valorTotal}
          </div>

          <div
            className={`estoque-moviment-quantidade ${
              estoque.quantidadeDisponivel < 1
                ? "estoque-moviment-negativo"
                : ""
            }`}
          >
            {estoque.quantidadeDisponivel}
          </div>
        </div>
      ))}

      {estoquesFiltrados.length === 0 && (
        <div className="estoque-moviment-list estoque-moviment-list-empty">
          Nenhum item encontrado
        </div>
      )}

      <div className="estoque-moviment-linhaFinal" />
    </div>
  );
}
