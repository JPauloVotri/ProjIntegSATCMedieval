import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { apiFetch } from "../../services/api";

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
    const [erro, setErro] = useState("");

    const { id } = useParams<{ id: string }>();

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
    );

    return (
        <>
            {erro && <p>{erro}</p>}

            {cotacoesFiltradas.length === 0 ? (
                <p>Nenhum fornecedor encontrado para este item.</p>
            ) : 
                cotacoesFiltradas.map((cotacao) => {
                    const fornecedor = fornecedores.find(
                        (f) => f.id === cotacao.fornecedorId
                    );
                return (
                    <div key={cotacao.id}>
                        <h3>{fornecedor?.razaoSocial ?? "Fornecedor não encontrado"}</h3>

                        <p>
                            Valor Unitário: R$ {cotacao.valorUnitario.toFixed(2)}
                        </p>

                        <p>
                            Prazo de Entrega: {cotacao.prazoEntregaDias} dias
                        </p>
                    </div>
                );
            })}
        </>
    );
}