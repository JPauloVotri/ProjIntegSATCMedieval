import EstoqueLista from "../components/listas/estoqueList";
import { useState } from "react"
import ModalCadastroProduto from "../components/modals/modalCadastroProduto";

type TipoModal = "produto" | null;

function App() {
    const [modalAberta, setModalAberta] = useState<TipoModal>(null);

    return (
        <main className="login-page">
            <button onClick={() => setModalAberta("produto")}>
                Cadastrar Produto
            </button>
            <EstoqueLista />
            <ModalCadastroProduto
                aberta={modalAberta === "produto"}
                onFechar={() => setModalAberta(null)}
            />
        </main>
    )
}

export default App;