
import "../../styles/cotacaoList.css";
import "../../styles/modalObs.css";

import { FaSearch } from "react-icons/fa";
import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";

import ModalCadastroCotacao from "../modals/modalCotacaoAdicionar";
import ModalCotacao from "../modals/modalObservacaoCotacao";

import { apiFetch } from "../../../services/api";
import PesquisaEstMov from "../pesquisas/pesquisaEstMov";
import Loading from "../loading";

type TipoModal = "cotacao" | null;

interface Cotacao {
    id: number;
    usuarioId: number;
    produtoId?: number;
    dataSolicitacao: string;
    dataLimiteResposta: string | null;
    status: number;
    observacao: string | null;
}

export default function CotacaoList() {
    const navigate = useNavigate();
    const [loading, setLoading] = useState(true);

    const [cotacoes, setCotacoes] = useState<Cotacao[]>([]);
    const [cotacaoSelecionada, setCotacaoSelecionada] = useState<Cotacao | null>(null);

    const [modalAberta, setModalAberta] = useState<TipoModal>(null);
    const [erro, setErro] = useState("");
    const [pesquisa, setPesquisa] = useState("");

    useEffect(() => {
        carregarCotacoes();
    }, []);

    async function carregarCotacoes() {
        try {
            setLoading(true);

            const dados = await apiFetch<Cotacao[]>("/api/Cotacoes");

            setCotacoes(dados);
        } catch (error) {
            setErro("Não foi possível carregar as cotações.");
            console.error(error);
        } finally {
            setLoading(false);
        }
    }

    function formatarData(data: string | null) {
        if (!data) return "Não informada";

        return new Date(data.split("T")[0] + "T12:00:00")
            .toLocaleDateString("pt-BR");
    }

    const cotacoesFiltradas = cotacoes.filter((cotacao) =>
        (
            cotacao.observacao || ""
        ).toLowerCase().includes(pesquisa.trim().toLowerCase())
        ||
        String(cotacao.id).includes(pesquisa.trim())
    );

    function atualizarCotacao(cotacaoAtualizada: Cotacao) {
        setCotacoes((anteriores) =>
            anteriores.map((cotacao) =>
                cotacao.id === cotacaoAtualizada.id
                    ? cotacaoAtualizada
                    : cotacao
            )
        );

        setCotacaoSelecionada(cotacaoAtualizada);
    }

    function excluirCotacao(id: number) {
        setCotacoes((anteriores) =>
            anteriores.filter((cotacao) => cotacao.id !== id)
        );

        setCotacaoSelecionada(null);
    }

    return (
        <div className="cotacao-container">

            {loading ? (
                <Loading />
            ) : (
                <>
                    <div className="cotacao-acoes">
                        <button
                            className="cotacao-cadastrar"
                            onClick={() => setModalAberta("cotacao")}
                        >
                            Cadastrar Cotação
                        </button>
                    </div>

                    <div className="cotacao-pesquisa">
                        <PesquisaEstMov onPesquisar={setPesquisa} />
                    </div>

                    {erro && <p className="cotacao-erro">{erro}</p>}

                    <div className="cotacao-tabela">

                        <div className="cotacao-header">
                            <div className="cotacao-nome">
                                Data de Solicitação
                            </div>

                            <div className="cotacao-observacao">
                                Data Limite
                            </div>

                            <div className="cotacao-fornecedores">
                                Observação
                            </div>

                            <div className="cotacao-fornecedores">
                                Fornecedores
                            </div>
                        </div>

                        {cotacoesFiltradas.map((cotacao) => (
                            <div key={cotacao.id} className="cotacao-list">

                                <div className="cotacao-nome">
                                    {formatarData(cotacao.dataSolicitacao)}
                                </div>

                                <div className="cotacao-observacao">
                                    {formatarData(cotacao.dataLimiteResposta)}
                                </div>

                                <div className="cotacao-fornecedores">
                                    <button
                                        className="cotacao-botao"
                                        onClick={() => setCotacaoSelecionada(cotacao)}
                                        aria-label="Ver observação e detalhes"
                                    >
                                        <FaSearch />
                                    </button>
                                </div>

                                <div className="cotacao-fornecedores">
                                <button
                                    className="cotacao-botao"
                                    onClick={() =>
                                        navigate(`/cotacaoFornecedores/${cotacao.id}/${cotacao.produtoId}`)
                                    }
                                    aria-label="Ver fornecedores"
                                >
                                    <FaSearch />
                                </button>
                            </div>

                            </div>
                        ))}

                        {cotacoesFiltradas.length === 0 && (
                            <div className="cotacao-list-empty">
                                Nenhuma cotação encontrada.
                            </div>
                        )}

                    </div>

                    <ModalCadastroCotacao
                        aberta={modalAberta === "cotacao"}
                        onFechar={() => setModalAberta(null)}
                    />

                    <ModalCotacao
                        aberta={cotacaoSelecionada !== null}
                        cotacao={cotacaoSelecionada}
                        onFechar={() => setCotacaoSelecionada(null)}
                        onAtualizar={atualizarCotacao}
                        onExcluir={excluirCotacao}
                    />
                </>
            )}

        </div>
    );
}
