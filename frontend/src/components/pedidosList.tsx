
import "../styles/estoqueMovimentList.css";
import { GiConfirmed } from "react-icons/gi";

import { apiFetch } from "../../services/api";
import { useEffect, useState } from "react";
import PesquisaEstMov from "./pesquisaEstMov";
import ModalConfirmarPedido from "./modalConfirmaPedido";

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
  const [Pedidos, setPedidos] = useState<PedidoItem[]>([]);
  const [PedidosCompra, setPedidosCompra] = useState<PedidoCompra[]>([]);
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
      setErro("Não foi possível carregar os Pedidos.");
      console.error(error);
    }
  }

  carregarPedidos();
}, []);

  const PedidosFiltrados = Pedidos.filter((Pedido) => {
  const pedidoEncontrado = PedidosCompra.find(
    (p) => p.id === Pedido.pedidoCompraId
  );

  return (
    pedidoEncontrado?.status === 1 &&
    Pedido.produtoNome
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
    <div>
      <PesquisaEstMov onPesquisar={setPesquisa} />

      {erro && <p>{erro}</p>}

      <div className="estoque-moviment-header">
        <div className="estoque-moviment-nome">Nome</div>
        <div className="estoque-moviment-quantidade">Data Pedido</div>
        <div className="estoque-moviment-quantidade">Data Chegada</div>
        <div className="estoque-moviment-quantidade">Confimação</div>
      </div>

      {PedidosFiltrados.map((Pedido) => {

  const pedidoEncontrado = PedidosCompra.find(
    (p) => p.id === Pedido.pedidoCompraId
  );

  return (
    <div key={Pedido.id} className="estoque-moviment-list">

      <div className="estoque-moviment-nome">
        {Pedido.produtoNome}
      </div>

      <div className="estoque-moviment-nome">
        {pedidoEncontrado
          ? new Date(pedidoEncontrado.dataPedido).toLocaleDateString("pt-BR")
          : "Não encontrado"}
      </div>

      <div className="estoque-moviment-nome">
        {pedidoEncontrado
          ? new Date(pedidoEncontrado.dataPrevistaEntrega).toLocaleDateString("pt-BR")
          : "Não encontrado"}
      </div>

      <div className="estoque-moviment-quantidade">
        <GiConfirmed
            onClick={() => abrirModal(Pedido.pedidoCompraId, Pedido.produtoId)}
        />
      </div>

    </div>
    );
    })}
    {produtoId !== null && (
  <ModalConfirmarPedido
    aberta={modalAberta}
    onFechar={() => {
      setModalAberta(false);
      setProdutoId(null);
    }}
    onConfirmar={confirmarPedido}
    pedidoSelecionado={pedidoSelecionado}
    produtoId={produtoId}
  />
)}
    </div>
  );
}
