
import "../../styles/estoqueMovimentList.css";

import { apiFetch } from "../../../services/api";
import { useEffect, useState } from "react";
import PesquisaEstMov from "../pesquisas/pesquisaEstMov";
import Loading from "../loading";

interface UnidadeMedida {
  id: number;
  nome: string;
  sigla: string;
  tipo: string;
}

export default function UnidadeMedidaList() {
  const [loading, setLoading] = useState(true);
  const [unidadesMedida, setUnidadesMedida] = useState<UnidadeMedida[]>([]);
  const [erro, setErro] = useState("");
  const [pesquisa, setPesquisa] = useState("");

  useEffect(() => {
    async function carregarUnidadesMedida() {
      try {
        setLoading(true);
        const dados = await apiFetch<UnidadeMedida[]>("/api/UnidadesMedida");
        setUnidadesMedida(dados);
      } catch (error) {
        setErro("Não foi possível carregar as unidades de medida.");
        console.error(error);
      } finally {
        setLoading(false);
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
    <div className="estoque-moviment-container">
      {loading ? (
        <Loading />
      ) : (
        <>
          <div className="estoque-moviment-pesquisa">
            <PesquisaEstMov onPesquisar={setPesquisa} />
          </div>

          {erro && <p className="estoque-moviment-erro">{erro}</p>}

          <div className="estoque-moviment-tabela">
            <div className="estoque-moviment-header">
              <div className="estoque-moviment-nome">Nome</div>
              <div className="unidade-medida-sigla">Sigla</div>
              <div className="unidade-medida-tipo">Tipo</div>
            </div>

            {unidadesFiltradas.map((unidade) => (
              <div key={unidade.id} className="estoque-moviment-list">
                <div className="estoque-moviment-nome">{unidade.nome}</div>

                <div className="unidade-medida-sigla">{unidade.sigla}</div>

                <div className="unidade-medida-tipo">{unidade.tipo}</div>
              </div>
            ))}

            {unidadesFiltradas.length === 0 && (
              <div className="estoque-moviment-list-empty">
                Nenhuma unidade de medida encontrada.
              </div>
            )}
          </div>
        </>
      )}
    </div>
  );
}
