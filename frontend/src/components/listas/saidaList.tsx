
import "../../styles/estoqueMovimentList.css";
import "../../styles/saidaEstoqueList.css";

import { IoMdExit } from "react-icons/io";
import { apiFetch } from "../../../services/api";
import { useEffect, useState } from "react";
import PesquisaEstMov from "../pesquisas/pesquisaEstMov";
import ModalSaidaEstoque from "../modals/modalRemoverQuantidade";
import Loading from "../loading";

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
    const [loading, setLoading] = useState(true);
    const [estoques, setEstoques] = useState<Estoque[]>([]);
    const [erro, setErro] = useState("");
    const [pesquisa, setPesquisa] = useState("");
    const [modalAberta, setModalAberta] = useState(false);
    const [estoqueSelecionado, setEstoqueSelecionado] = useState<Estoque | null>(null);

    useEffect(() => {
        async function carregarEstoques() {
            try {
                setLoading(true);
                const dados = await apiFetch<Estoque[]>("/api/Estoques");
                setEstoques(dados);
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

    return (
        <div className="saida-container">
            {loading ? (
                <Loading />
            ) : (
                <>
                    <div className="saida-pesquisa">
                        <PesquisaEstMov onPesquisar={setPesquisa} />
                    </div>

                    {erro && <p className="saida-erro">{erro}</p>}

                    <div className="saida-tabela">
                        <div className="estoque-moviment-header saida-header">
                            <div className="saida-nome">Produto</div>
                            <div className="saida-acao">Saída</div>
                        </div>

                        {estoquesFiltrados.length === 0 ? (
                            <div className="saida-vazio">
                                Nenhum item encontrado.
                            </div>
                        ) : (
                            estoquesFiltrados.map((estoque) => (
                                <div
                                    key={estoque.id}
                                    className="estoque-moviment-list saida-list"
                                >
                                    <div className="saida-nome">
                                        {estoque.produtoNome}
                                    </div>

                                    <div className="saida-acao">
                                        <button
                                            className="saida-botao"
                                            title="Retirar quantidade do estoque"
                                            onClick={() => {
                                                setEstoqueSelecionado(estoque);
                                                setModalAberta(true);
                                            }}
                                        >
                                            <IoMdExit />
                                        </button>
                                    </div>
                                </div>
                            ))
                        )}
                    </div>

                    {estoqueSelecionado && (
                        <ModalSaidaEstoque
                            aberta={modalAberta}
                            onFechar={() => {
                                setModalAberta(false);
                                setEstoqueSelecionado(null);
                            }}
                            estoque={estoqueSelecionado}
                            onConfirmar={async (estoqueId, quantidade) => {
                            }}
                        />
                    )}
                </>
            )}
        </div>
    );
}
