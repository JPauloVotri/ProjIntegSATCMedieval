import CotacaoFornecedoresAdicionar from "../components/modalCotacaoFornecedorAdicionar";
import CotacaoFornecedorLista from "../components/cotacaoFornecedorList";
import { useState } from "react";
import { useParams } from "react-router-dom";

type TipoModal = "fornecedor" | null;

function App() {
    const [modalAberta, setModalAberta] = useState<TipoModal>(null);

    const { cotacaoId, produtoId } = useParams<{
        cotacaoId: string;
        produtoId: string;
    }>();

    return (
        <main className="login-page">
            <p>Produto ID: {produtoId}</p>
            <button onClick={() => setModalAberta("fornecedor")}>
                Cadastrar Fornecedor
            </button>
            <CotacaoFornecedorLista />
            <CotacaoFornecedoresAdicionar
                aberta={modalAberta === "fornecedor"}
                onFechar={() => setModalAberta(null)}
            />
        </main>
    )
}

export default App;