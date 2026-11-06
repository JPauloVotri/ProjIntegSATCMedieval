
import "../../styles/estoqueMovimentList.css";

import { apiFetch } from "../../../services/api";
import { useEffect, useState } from "react";
import PesquisaEstMov from "../pesquisas/pesquisaEstMov";

interface GrupoProduto {
  id: number;
  nome: string;
  descricao: string;
}

export default function GrupoProdutoList() {
  const [gruposProdutos, setGruposProdutos] = useState<GrupoProduto[]>([]);
  const [erro, setErro] = useState("");
  const [pesquisa, setPesquisa] = useState("");

  useEffect(() => {
    async function carregarGruposProdutos() {
      try {
        const dados = await apiFetch<GrupoProduto[]>("/api/GruposProdutos");
        setGruposProdutos(dados);
      } catch (error) {
        setErro("Não foi possível carregar os grupos de produtos.");
        console.error(error);
      }
    }

    carregarGruposProdutos();
  }, []);

  const gruposFiltrados = gruposProdutos.filter((grupo) =>
    grupo.nome
      .toLowerCase()
      .startsWith(pesquisa.trim().toLowerCase())
  );

  return (
    <div>
      <PesquisaEstMov onPesquisar={setPesquisa} />

      {erro && <p>{erro}</p>}

      <div className="estoque-moviment-header">
        <div className="estoque-moviment-nome">Nome</div>
        <div className="estoque-moviment-quantidade">Descrição</div>
      </div>

      {gruposFiltrados.map((grupo) => (
        <div
          key={grupo.id}
          className="estoque-moviment-list"
        >
          <div className="estoque-moviment-nome">
            {grupo.nome}
          </div>

          <div className="estoque-moviment-quantidade">
            {grupo.descricao}
          </div>

        </div>
      ))}

      {gruposFiltrados.length === 0 && (
        <div className="estoque-moviment-list estoque-moviment-list-empty">
          Nenhum item encontrado
        </div>
      )}

      <div className="estoque-moviment-linhaFinal" />
    </div>
  );
}
