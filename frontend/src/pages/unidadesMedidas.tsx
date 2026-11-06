import { useState } from 'react';
import UnidadeMedidaList from '../components/listas/unidadeMedidaList';
import ModalCadastroUnidadeMedida from "../components/modals/modalCadastroUnidadeMedida"

type TipoModal = "unidademedida" | null;

function App() {
    const [modalAberta, setModalAberta] = useState<TipoModal>(null);

    return (
        <main>
            <button onClick={() => setModalAberta("unidademedida")}>
                Cadastrar Unidade de Medida
            </button>
            <UnidadeMedidaList />
            <ModalCadastroUnidadeMedida
                aberta={modalAberta === "unidademedida"}
                onFechar={() => setModalAberta(null)}
            />
        </main>
    )
}

export default App;