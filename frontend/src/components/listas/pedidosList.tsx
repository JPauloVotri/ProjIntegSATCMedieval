
import "../../styles/estoqueMovimentList.css";
import "../../styles/pedidoList.css";

import { GiConfirmed } from "react-icons/gi";
import { apiFetch } from "../../../services/api";
import { useEffect, useState } from "react";
import PesquisaEstMov from "../pesquisas/pesquisaEstMov";
import ModalConfirmarPedido from "../modals/modalConfirmaPedido";

interface PedidoItem {
    id: number;
    produtoId: number;
    produtoNome: string;
    quantidade: number;
    pedidoCompraId: number;
}

interface PedidoCompra {
    id: number;
    dataPedido: string;
    dataPrevistaEntrega: string;
    status: number;
}

export default function PedidoList() {
    const [pedidos, setPedidos] = useState<PedidoItem[]>([]);
    const [pedidosCompra, setPedidosCompra] = useState<PedidoCompra[]>([]);
    const [erro, setErro] = useState("");
    const [pesquisa, setPesquisa] = useState("");

    const [modalAberta, setModalAberta] = useState(false);
    const [pedidoSelecionado, setPedidoSelecionado] = useState<number | null>(null);
    const [produtoId, setProdutoId] = useState<number | null>(null);

    useEffect(() => {
        async function carregarPedidos() {
            try {
                const [itens, pedidos] = await Promise.all([
                    apiFetch<PedidoItem[]>("/api/PedidosCompraItens"),
                    apiFetch<PedidoCompra[]>("/api/PedidosCompra")
                ]);

                setPedidos(itens);
                setPedidosCompra(pedidos);
            } catch (error) {
                setErro("Não foi possível carregar os pedidos.");
                console.error(error);
            }
        }

        carregarPedidos();
    }, []);

    const pedidosFiltrados = pedidos.filter((pedido) => {
        const pedidoEncontrado = pedidosCompra.find(
            (p) => p.id === pedido.pedidoCompraId
        );

        return (
            pedidoEncontrado?.status === 1 &&
            pedido.produtoNome
                .toLowerCase()
                .startsWith(pesquisa.trim().toLowerCase())
        );
    });

    function abrirModal(pedidoId: number, produtoId: number) {
        setPedidoSelecionado(pedidoId);
        setProdutoId(produtoId);
        setModalAberta(true);
    }

    function confirmarPedido() {
        setModalAberta(false);
        setPedidoSelecionado(null);
        setProdutoId(null);
    }

    return (
        <div className="pedido-container">

            <div className="pedido-pesquisa">
                <PesquisaEstMov onPesquisar={setPesquisa} />
            </div>

            {erro && <p className="pedido-erro">{erro}</p>}

            <div className="pedido-tabela">

                <div className="estoque-moviment-header pedido-header">
                    <div className="pedido-nome">Produto</div>
                    <div className="pedido-data">Data do Pedido</div>
                    <div className="pedido-data">Data de Chegada</div>
                    <div className="pedido-confirmacao">Confirmação</div>
                </div>

                {pedidosFiltrados.length === 0 ? (
                    <div className="pedido-vazio">
                        Nenhum pedido encontrado.
                    </div>
                ) : (
                    pedidosFiltrados.map((pedido) => {
                        const pedidoEncontrado = pedidosCompra.find(
                            (p) => p.id === pedido.pedidoCompraId
                        );

                        return (
                            <div key={pedido.id} className="estoque-moviment-list pedido-list">

                                <div className="pedido-nome">
                                    {pedido.produtoNome}
                                </div>

                                <div className="pedido-data">
                                    {pedidoEncontrado
                                        ? new Date(pedidoEncontrado.dataPedido).toLocaleDateString("pt-BR")
                                        : "Não encontrado"}
                                </div>

                                <div className="pedido-data">
                                    {pedidoEncontrado
                                        ? new Date(pedidoEncontrado.dataPrevistaEntrega).toLocaleDateString("pt-BR")
                                        : "Não encontrado"}
                                </div>

                                <div className="pedido-confirmacao">
                                    <button
                                        className="pedido-botao-confirmar"
                                        title="Confirmar chegada do pedido"
                                        onClick={() => abrirModal(
                                            pedido.pedidoCompraId,
                                            pedido.produtoId
                                        )}
                                    >
                                        <GiConfirmed />
                                    </button>
                                </div>

                            </div>
                        );
                    })
                )}

            </div>

            {produtoId !== null && (
                <ModalConfirmarPedido
                    aberta={modalAberta}
                    onFechar={() => {
                        setModalAberta(false);
                        setProdutoId(null);
                        setPedidoSelecionado(null);
                    }}
                    onConfirmar={confirmarPedido}
                    pedidoSelecionado={pedidoSelecionado}
                    produtoId={produtoId}
                />
            )}

        </div>
    );
}
