
import "../../styles/estoqueMovimentList.css";

import { apiFetch } from "../../../services/api";
import { useEffect, useState } from "react";
import PesquisaEstMov from "../pesquisas/pesquisaEstMov";
import Loading from "../loading";
import { FaSearch } from "react-icons/fa";
import ModalGrupoProduto from "../modals/modalGrupoProduto";

interface GrupoProduto {
  id: number;
  nome: string;
  descricao: string;
}

export default function GrupoProdutoList() {
  const [loading, setLoading] = useState(true);
  const [gruposProdutos, setGruposProdutos] = useState<GrupoProduto[]>([]);
  const [erro, setErro] = useState("");
  const [pesquisa, setPesquisa] = useState("");
  const [grupoSelecionado, setGrupoSelecionado] = useState<GrupoProduto | null>(null);
  const [modalAberta, setModalAberta] = useState(false);

  useEffect(() => {
    async function carregarGruposProdutos() {
      try {
        const dados = await apiFetch<GrupoProduto[]>("/api/GruposProdutos");
        setGruposProdutos(dados);
      } catch (error) {
        setErro("Não foi possível carregar os grupos de produtos.");
        console.error(error);
      } finally {
        setLoading(false);
      }
    }

    carregarGruposProdutos();
  }, []);

  const gruposFiltrados = gruposProdutos.filter((grupo) =>
    grupo.nome
      .toLowerCase()
      .startsWith(pesquisa.trim().toLowerCase())
  );

  function abrirModal(grupo: GrupoProduto) {
  setGrupoSelecionado(grupo);
  setModalAberta(true);
}

function atualizarGrupo(grupoAtualizado: GrupoProduto) {
  setGruposProdutos((grupos) =>
    grupos.map((grupo) =>
      grupo.id === grupoAtualizado.id ? grupoAtualizado : grupo
    )
  );
}

function excluirGrupo(id: number) {
  setGruposProdutos((grupos) =>
    grupos.filter((grupo) => grupo.id !== id)
  );
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

          {erro && <p className="estoque-moviment-erro">{erro}</p>}

          <div className="estoque-moviment-tabela">
            <div className="estoque-moviment-header">
              <div className="grupo-produto-nome">Nome</div>
              <div className="grupo-produto-descricao">Descrição</div>
            </div>

            {gruposFiltrados.map((grupo) => (
              <div key={grupo.id} className="estoque-moviment-list">
                <div className="grupo-produto-nome">
                  {grupo.nome}
                </div>

                <div className="grupo-produto-observacao">
                  <button
                    type="button"
                    className="grupo-produto-botao-observacao"
                    onClick={() => abrirModal(grupo)}
                    aria-label={`Ver descrição de ${grupo.nome}`}
                  >
                  <FaSearch />
                  </button>
                </div>
              </div>
            ))}

            {gruposFiltrados.length === 0 && (
              <div className="estoque-moviment-list-empty">
                Nenhum grupo encontrado.
              </div>
            )}
          </div>
        </>
      )}
      <ModalGrupoProduto
        aberta={modalAberta}
        grupo={grupoSelecionado}
        onFechar={() => setModalAberta(false)}
        onAtualizar={atualizarGrupo}
        onExcluir={excluirGrupo}
      />
    </div>
  );
}
