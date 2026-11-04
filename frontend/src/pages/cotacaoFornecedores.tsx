import CotacaoFornecedoresAdicionar from "../components/modalCotacaoFornecedorAdicionar";
import CotacaoFornecedorLista from "../components/cotacaoFornecedorList";
import { useState } from "react";

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