
import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { apiFetch } from "../../../services/api";

import "../../styles/cotacaoItemFornecedorList.css";
import ModalRealizarPedido from "../modals/modalRealizarPedido";
import Loading from "../loading";

type TipoModal = "pedido" | null;

type CotacaoItemFornecedores = {
    id: number;
    fornecedorId: number;
    cotacaoItemId: number;
    valorUnitario: number;
    prazoEntregaDias: number;
};

type Fornecedor = {
    id: number;
    razaoSocial: string;
};

export default function CotacaoItemFornecedorList() {
    const [loading, setLoading] = useState(true);
    const [cotacoes, setCotacoes] = useState<CotacaoItemFornecedores[]>([]);
    const [fornecedores, setFornecedores] = useState<Fornecedor[]>([]);
    const [fornecedorSelecionado, setFornecedorSelecionado] = useState(0);
    const [cotacaoSelecionada, setCotacaoSelecionada] =
        useState<CotacaoItemFornecedores | null>(null);
    const [erro, setErro] = useState("");

    const [modalAberta, setModalAberta] = useState<TipoModal>(null);

    const { id, produtoId } = useParams<{
        id: string;
        produtoId: string;
    }>();

    useEffect(() => {
        async function carregarDados() {
            try {
                setLoading(true);
                const dadosCotacoes = await apiFetch<CotacaoItemFornecedores[]>(
                    "/api/CotacaoItemFornecedores"
                );

                const dadosFornecedores = await apiFetch<Fornecedor[]>(
                    "/api/Fornecedores"
                );

                setCotacoes(dadosCotacoes);
                setFornecedores(dadosFornecedores);
            } catch (error) {
                setErro("Não foi possível carregar os dados.");
                console.error(error);
            } finally {
                setLoading(false);
            }
        }

        carregarDados();
    }, []);

    const cotacoesFiltradas = cotacoes.filter(
        (cotacao) => cotacao.cotacaoItemId === Number(id)
    );

    const fornecedoresOrdenados = [...cotacoesFiltradas].sort(
        (a, b) => a.valorUnitario - b.valorUnitario
    );

    return (
        <div className="cotacao-fornecedor-container">
            {loading ? (
                <Loading />
            ) : (
                <>
                    {erro && <p className="cotacao-fornecedor-erro">{erro}</p>}

                    {cotacoesFiltradas.length === 0 ? (
                        <div className="cotacao-fornecedor-vazio">
                            Nenhum fornecedor encontrado para este item.
                        </div>
                    ) : (
                        <div className="cotacao-fornecedor-lista">
                            {fornecedoresOrdenados.map((cotacao) => {
                                const fornecedor = fornecedores.find(
                                    (f) => f.id === cotacao.fornecedorId
                                );

                                return (
                                    <div className="cotacao-fornecedor-card" key={cotacao.id}>
                                        <div className="cotacao-fornecedor-info">
                                            <h3>
                                                {fornecedor?.razaoSocial ??
                                                    "Fornecedor não encontrado"}
                                            </h3>

                                            <div className="cotacao-fornecedor-detalhes">
                                                <div>
                                                    <span>Valor Unitário</span>
                                                    <strong>
                                                        R$ {cotacao.valorUnitario.toFixed(2)}
                                                    </strong>
                                                </div>

                                                <div>
                                                    <span>Prazo de Entrega</span>
                                                    <strong>
                                                        {cotacao.prazoEntregaDias} dias
                                                    </strong>
                                                </div>
                                            </div>
                                        </div>

                                        <button
                                            className="cotacao-fornecedor-botao"
                                            onClick={() => {
                                                setFornecedorSelecionado(cotacao.fornecedorId);
                                                setCotacaoSelecionada(cotacao);
                                                setModalAberta("pedido");
                                            }}
                                        >
                                            Realizar Pedido
                                        </button>
                                    </div>
                                );
                            })}
                        </div>
                    )}

                    {cotacaoSelecionada && (
                        <ModalRealizarPedido
                            aberta={modalAberta === "pedido"}
                            onFechar={() => setModalAberta(null)}
                            fornecedorId={fornecedorSelecionado}
                            cotacaoId={id}
                            produtoId={produtoId}
                            valorUnitario={cotacaoSelecionada.valorUnitario}
                        />
                    )}
                </>
            )}
        </div>
    );
}

