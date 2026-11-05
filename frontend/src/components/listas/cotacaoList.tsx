
import "../../styles/cotacaoList.css";
import "../../styles/modalObs.css";

import { FaSearch } from "react-icons/fa";
import { IoClose } from "react-icons/io5";
import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";

import ModalCadastroCotacao from "../modals/modalCotacaoAdicionar";
import { apiFetch } from "../../../services/api";

type TipoModal = "cotacao" | "cotacaoItem" | null;

type Cotacao = {
    id: number;
    produtoNome: string;
    produtoId: number;
    quantidade: number;
    observacao: string;
    fornecedores: string;
};

export default function cotacaoList() {
    const [cotacaoSelecionada, setCotacaoSelecionada] = useState<Cotacao[]>([]);
    const [cotacaoObservacao, setCotacaoObservacao] = useState<Cotacao | null>(null);
    const [modalAberta, setModalAberta] = useState<TipoModal>(null);
    const [erro, setErro] = useState("");
    const [pesquisa, setPesquisa] = useState("");

    const navigate = useNavigate();

    useEffect(() => {
        async function carregarCotacoes() {
            try {
                const dados = await apiFetch<Cotacao[]>("/api/CotacaoItens");
                setCotacaoSelecionada(dados);
            } catch (error) {
                setErro("Não foi possível carregar as cotações.");
                console.error(error);
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
        <div>
            <button onClick={() => setModalAberta("cotacao")}>
                Cadastrar Cotação
            </button>

            <div className="cotacao-header">
                <div className="cotacao-grupo">Nome</div>
                <div className="cotacao-nome">quantidade</div>
                <div className="cotacao-marca">observacao</div>
                <div className="cotacao-preco">Fornecedores</div>
            </div>

            {cotacoesFiltrados.map((cotacao) => (
                <div
                    key={cotacao.id}
                    id={`cotacao-${cotacao.produtoNome.toLowerCase()}`}
                    className={`cotacao-list cotacao-${cotacao.produtoNome.toLowerCase()} cotacao-${cotacao.quantidade}`}
                >
                    <div className="cotacao-grupo">
                        {cotacao.produtoNome}
                    </div>

                    <div className="cotacao-nome">
                        {cotacao.quantidade}
                    </div>

                    <div className="cotacao-marca cotacao-botao">
                        <FaSearch onClick={() => setCotacaoObservacao(cotacao)} />
                    </div>

                    <div className="cotacao-observacao cotacao-botao">
                        <FaSearch
                            onClick={() =>
                                navigate(
                                    `/cotacaoFornecedores/${cotacao.id}/${cotacao.produtoId}`
                                )
                            }
                        />
                    </div>
                </div>
            ))}

            {cotacoesFiltrados.length === 0 && (
                <div className="cotacao-list cotacao-list-empty">
                    Nenhum item encontrado
                </div>
            )}

            <div className="cotacao-linhaFinal"></div>

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
                                <IoClose onClick={() => setCotacaoObservacao(null)} />
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
        </div>
    );
}
