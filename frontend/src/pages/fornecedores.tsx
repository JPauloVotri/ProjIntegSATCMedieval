import { useState } from 'react';
import FornecedorList from '../components/listas/fornecedorList';
import ModalCadastroFornecedor from '../components/modals/modalCadastroFornecedor';

type TipoModal = "fornecedor" | null;

function App() {
    const [modalAberta, setModalAberta] = useState<TipoModal>(null);

    return (
        <main>
            <button onClick={() => setModalAberta("fornecedor")}>
                Cadastrar Fornecedor
            </button>
            <FornecedorList />
            <ModalCadastroFornecedor
                aberta={modalAberta === "fornecedor"}
                onFechar={() => setModalAberta(null)}
            />
        </main>
    )
}

export default App;