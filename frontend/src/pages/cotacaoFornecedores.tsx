import CotacaoFornecedoresAdicionar from "../components/modals/modalCotacaoFornecedorAdicionar";
import CotacaoFornecedorLista from "../components/listas/cotacaoFornecedorList";
import { useState } from "react";
import { useParams } from "react-router-dom";

type TipoModal = "fornecedor" | null;

function App() {
    const [modalAberta, setModalAberta] = useState<TipoModal>(null);

    return (
        <main className="login-page">
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