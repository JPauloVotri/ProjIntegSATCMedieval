
import { IoClose } from "react-icons/io5";

import "../../styles/modalObs.css";
import "../../styles/modalGrupoProduto.css";
import { useEffect, useState } from "react";
import { apiFetch } from "../../../services/api";

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

interface GrupoProduto {
  id: number;
  nome: string;
}

interface UnidadeMedida {
  id: number;
  nome: string;
}

interface ModalProdutoProps {
  aberta: boolean;
  produto: Produto | null;
  onFechar: () => void;
}

export default function ModalProduto({
  aberta,
  produto,
  onFechar,
}: ModalProdutoProps) {
  const [gruposProdutos, setGruposProdutos] = useState<GrupoProduto[]>([]);
  const [unidadesMedida, setUnidadesMedida] = useState<UnidadeMedida[]>([]);

  useEffect(() => {
    async function carregarInformacoes() {
      try {
        const [grupos, unidades] = await Promise.all([
          apiFetch<GrupoProduto[]>("/api/GruposProdutos"),
          apiFetch<UnidadeMedida[]>("/api/UnidadesMedida"),
        ]);

        setGruposProdutos(grupos);
        setUnidadesMedida(unidades);
      } catch (error) {
        console.error("Erro ao carregar grupos e unidades:", error);
      }
    }

    carregarInformacoes();
  }, []);

  if (!aberta || !produto) return null;

  return (
    <div className="modal-overlay" onClick={onFechar}>
      <div
        className="modal-caixa modal-grupo-produto"
        onClick={(e) => e.stopPropagation()}
      >

        <div className="modal-usuario-cabecalho">
          <div className="modal-titulos-2">
            <h2>Informações do Produto</h2>
          </div>

          <button
            type="button"
            className="modal-close"
            onClick={onFechar}
          >
            <IoClose />
          </button>
        </div>

        <div className="modal-grupo-conteudo">

          <h3>{produto.nome}</h3>

          <label>Grupo de Produto</label>
          <div className="modal-grupo-campo">
            {gruposProdutos.find(
              (grupo) => grupo.id === produto.grupoProdutoId
            )?.nome || "Não encontrado"}
          </div>

          <label>Unidade de Medida</label>
          <div className="modal-grupo-campo">
            {unidadesMedida.find(
              (unidade) => unidade.id === produto.unidadeMedidaId
            )?.nome || "Não encontrada"}
          </div>

          <label>Nome</label>
          <div className="modal-grupo-campo">
            {produto.nome || "Não informado"}
          </div>

          <label>Código</label>
          <div className="modal-grupo-campo">
            {produto.codigo || "Não informado"}
          </div>

          <label>Estoque Mínimo</label>
          <div className="modal-grupo-campo">
            {produto.estoqueMinimo ?? "Não informado"}
          </div>

          <label>Estoque Máximo</label>
          <div className="modal-grupo-campo">
            {produto.estoqueMaximo ?? "Não informado"}
          </div>

          <label>Validade (dias)</label>
          <div className="modal-grupo-campo">
            {produto.validadeDias ?? "Não informado"}
          </div>

          <label>Descrição</label>
          <div className="modal-grupo-campo-textarea">
            {produto.descricao || "Nenhuma descrição cadastrada."}
          </div>

        </div>
      </div>
    </div>
  );
}
