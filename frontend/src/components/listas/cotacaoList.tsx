
import "../../styles/cotacaoList.css";
import "../../styles/modalObs.css";

import { FaSearch } from "react-icons/fa";
import { IoClose } from "react-icons/io5";
import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";

import ModalCadastroCotacao from "../modals/modalCotacaoAdicionar";
import { apiFetch } from "../../../services/api";
import PesquisaEstMov from "../pesquisas/pesquisaEstMov";
import Loading from "../loading";

type TipoModal = "cotacao" | "cotacaoItem" | null;

type Cotacao = {
    id: number;
    produtoNome: string;
    produtoId: number;
    quantidade: number;
    observacao: string;
    fornecedores: string;
};

export default function CotacaoList() {
    const [loading, setLoading] = useState(true);
    const [cotacaoSelecionada, setCotacaoSelecionada] = useState<Cotacao[]>([]);
    const [cotacaoObservacao, setCotacaoObservacao] = useState<Cotacao | null>(null);
    const [modalAberta, setModalAberta] = useState<TipoModal>(null);
    const [erro, setErro] = useState("");
    const [pesquisa, setPesquisa] = useState("");

    const navigate = useNavigate();

    useEffect(() => {
        async function carregarCotacoes() {
            try {
                setLoading(true);
                const dados = await apiFetch<Cotacao[]>("/api/CotacaoItens");
                setCotacaoSelecionada(dados);
            } catch (error) {
                setErro("Não foi possível carregar as cotações.");
                console.error(error);
            } finally {
                setLoading(false);
            }
        }

        carregarCotacoes();
    }, []);

    const cotacoesFiltrados = cotacaoSelecionada.filter((cotacao) =>
        cotacao.produtoNome
            .toLowerCase()
            .startsWith(pesquisa.trim().toLowerCase())
    );


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
                        <div className="cotacao-nome">Produto</div>
                        <div className="cotacao-quantidade">Quantidade</div>
                        <div className="cotacao-observacao">Observação</div>
                        <div className="cotacao-fornecedores">Fornecedores</div>
                    </div>

                    {cotacoesFiltrados.map((cotacao) => (
                        <div key={cotacao.id} className="cotacao-list">

                            <div className="cotacao-nome">
                                {cotacao.produtoNome}
                            </div>

                            <div className="cotacao-quantidade">
                                {cotacao.quantidade}
                            </div>

                            <div className="cotacao-observacao">
                                <button
                                    className="cotacao-botao"
                                    onClick={() => setCotacaoObservacao(cotacao)}
                                    aria-label="Ver observação"
                                >
                                    <FaSearch />
                                </button>
                            </div>

                            <div className="cotacao-fornecedores">
                                <button
                                    className="cotacao-botao"
                                    onClick={() =>
                                        navigate(
                                            `/cotacaoFornecedores/${cotacao.id}/${cotacao.produtoId}`
                                        )
                                    }
                                    aria-label="Ver fornecedores"
                                >
                                    <FaSearch />
                                </button>
                            </div>

                        </div>
                    ))}

                    {cotacoesFiltrados.length === 0 && (
                        <div className="cotacao-list-empty">
                            Nenhuma cotação encontrada.
                        </div>
                    )}

                </div>

                {cotacaoObservacao && (
                    <div
                        className="modal-overlay"
                        onClick={() => setCotacaoObservacao(null)}
                    >
                        <div
                            className="modal-caixa"
                            onClick={(e) => e.stopPropagation()}
                        >
                            <div className="modal-header">

                                <div className="modal-close">
                                    <IoClose
                                        onClick={() => setCotacaoObservacao(null)}
                                    />
                                </div>

                                <h2 id="modal-titulo">
                                    Observações do produto
                                </h2>

                            </div>

                            <div className="modal-conteudo">
                                <div className="modal-observacao">
                                    {cotacaoObservacao.observacao ||
                                        "Nenhuma observação cadastrada."}
                                </div>
                            </div>
                        </div>
                    </div>
                )}

                <ModalCadastroCotacao
                    aberta={modalAberta === "cotacao"}
                    onFechar={() => setModalAberta(null)}
                />
            </>
        )}

    </div>
)};
