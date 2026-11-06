
import "../../styles/estoqueMovimentList.css";
import { IoMdExit } from "react-icons/io";

import { apiFetch } from "../../../services/api";
import { useEffect, useState } from "react";
import PesquisaEstMov from "../pesquisas/pesquisaEstMov";
import modalRemoverQuantidade from "../modals/modalRemoverQuantidade";
import ModalSaidaEstoque from "../modals/modalRemoverQuantidade";

interface Estoque {
    id: number;
    produtoId: number;
    produtoNome: string;
    quantidadeDisponivel: number;
    valorTotal: number;
    ultimaEntrada: string | null;
    ultimaSaida: string | null;
}

export default function EstoqueList() {
  const [estoques, setEstoques] = useState<Estoque[]>([]);
  const [erro, setErro] = useState("");
  const [pesquisa, setPesquisa] = useState("");
  const [modalAberta, setModalAberta] = useState(false);
  const [estoqueSelecionado, setEstoqueSelecionado] = useState<Estoque | null>(null);

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
        <div className="estoque-moviment-quantidade">Saída</div>
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
            <IoMdExit onClick={() => {
                setEstoqueSelecionado(estoque);
                setModalAberta(true);
            }} />
          </div>

        </div>
      ))}

      {estoquesFiltrados.length === 0 && (
        <div className="estoque-moviment-list estoque-moviment-list-empty">
          Nenhum item encontrado
        </div>
      )}

      <div className="estoque-moviment-linhaFinal" />

    {estoqueSelecionado && (
      <ModalSaidaEstoque
        aberta={modalAberta}
        onFechar={() => setModalAberta(false)}
        estoque={estoqueSelecionado}
        onConfirmar={async (estoqueId, quantidade) => {
        }}
      />
)}
    </div>
  );
}
