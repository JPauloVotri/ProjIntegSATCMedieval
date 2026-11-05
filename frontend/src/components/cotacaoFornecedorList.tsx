import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { apiFetch } from "../../services/api";

import ModalRealizarPedido from "./modalRealizarPedido";

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
    const [cotacoes, setCotacoes] = useState<CotacaoItemFornecedores[]>([]);
    const [fornecedores, setFornecedores] = useState<Fornecedor[]>([]);
    const [fornecedorSelecionado, setFornecedorSelecionado] = useState(0);
    const [erro, setErro] = useState("");

    const [modalAberta, setModalAberta] = useState<TipoModal>(null);

    const { id } = useParams<{ id: string }>();

    const { cotacaoId, produtoId } = useParams<{
        cotacaoId: string;
        produtoId: string;
    }>();

    useEffect(() => {
        async function carregarDados() {
            try {
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
            }
        }

        carregarDados();
    }, []);

    const cotacoesFiltradas = cotacoes.filter(
        (cotacao) => cotacao.cotacaoItemId === Number(id)
    )

    const fornecedoresOrdenados = [...cotacoesFiltradas].sort(
        (a, b) => a.valorUnitario - b.valorUnitario
    );

    return (
        <>
            {erro && <p>{erro}</p>}

            {cotacoesFiltradas.length === 0 ? (
                <p>Nenhum fornecedor encontrado para este item.</p>
            ) : (
                fornecedoresOrdenados.map((cotacao) => {
    const fornecedor = fornecedores.find(
        (f) => f.id === cotacao.fornecedorId
    );

    return (
        <div key={fornecedor?.id}>
            <h3 id={fornecedor?.razaoSocial}>{fornecedor?.id}</h3>
            <h3>{fornecedor?.razaoSocial ?? "Fornecedor não encontrado"}</h3>

            <p>
                Valor Unitário: R$ {cotacao.valorUnitario.toFixed(2)}
            </p>

            <p>
                Prazo de Entrega: {cotacao.prazoEntregaDias} dias
            </p>
            <button onClick={() => {
                setFornecedorSelecionado(cotacao.fornecedorId);
                setModalAberta("pedido");
            }}>
                Realizar Pedido
            </button>
            <ModalRealizarPedido
                aberta={modalAberta === "pedido"}
                onFechar={() => setModalAberta(null)}
                fornecedorId={fornecedorSelecionado}
                cotacaoId={id}
                produtoId={produtoId}
                valorUnitario={cotacao.valorUnitario}
            />
        </div>
    );
})
            )}
        </>
    );
}