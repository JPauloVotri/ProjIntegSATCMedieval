import { useState } from 'react';
import GrupoProdutoList from '../components/listas/grupoProdutoList';
import ModalCadastroGrupoProduto from "../components/modals/modalCadastroGrupoProduto"

type TipoModal = "grupoproduto" | null;

function App() {
    const [modalAberta, setModalAberta] = useState<TipoModal>(null);

    return (
        <main>
            <button onClick={() => setModalAberta("grupoproduto")}>
                Cadastrar Grupo de Produto
            </button>
            <GrupoProdutoList />
            <ModalCadastroGrupoProduto
                aberta={modalAberta === "grupoproduto"}
                onFechar={() => setModalAberta(null)}
            />
        </main>
    )
}

export default App;