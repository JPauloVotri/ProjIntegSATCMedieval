
import "../../styles/estoqueMovimentList.css";

import { apiFetch } from "../../../services/api";
import { useEffect, useState } from "react";
import PesquisaEstMov from "../pesquisas/pesquisaEstMov";

interface UnidadeMedida {
  id: number;
  nome: string;
  sigla: string;
  tipo: string;
}

export default function UnidadeMedidaList() {
  const [unidadesMedida, setUnidadesMedida] = useState<UnidadeMedida[]>([]);
  const [erro, setErro] = useState("");
  const [pesquisa, setPesquisa] = useState("");

  useEffect(() => {
    async function carregarUnidadesMedida() {
      try {
        const dados = await apiFetch<UnidadeMedida[]>("/api/UnidadesMedida");
        setUnidadesMedida(dados);
      } catch (error) {
        setErro("Não foi possível carregar as unidades de medida.");
        console.error(error);
      }
    }

    carregarUnidadesMedida();
  }, []);

  const unidadesFiltradas = unidadesMedida.filter((unidade) =>
    unidade.nome
      .toLowerCase()
      .startsWith(pesquisa.trim().toLowerCase())
  );

  return (
    <div>
      <PesquisaEstMov onPesquisar={setPesquisa} />

      {erro && <p>{erro}</p>}

      <div className="estoque-moviment-header">
        <div className="estoque-moviment-nome">Nome</div>
        <div className="estoque-moviment-quantidade">Sigla</div>
        <div className="estoque-moviment-quantidade">Tipo</div>
      </div>

      {unidadesFiltradas.map((unidade) => (
        <div
          key={unidade.id}
          className="estoque-moviment-list"
        >
          <div className="estoque-moviment-nome">
            {unidade.nome}
          </div>

          <div className="estoque-moviment-quantidade">
            {unidade.sigla}
          </div>

          <div className="estoque-moviment-quantidade">
            {unidade.tipo}
          </div>
        </div>
      ))}

      {unidadesFiltradas.length === 0 && (
        <div className="estoque-moviment-list estoque-moviment-list-empty">
          Nenhum item encontrado
        </div>
      )}

      <div className="estoque-moviment-linhaFinal" />
    </div>
  );
}
