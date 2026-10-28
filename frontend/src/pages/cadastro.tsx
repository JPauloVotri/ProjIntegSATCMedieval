import { useState } from "react"

import ModalCadastroProduto from "../components/modalCadastroProduto"
import ModalCadastroGrupoProduto from "../components/modalCadastroGrupoProduto"
import ModalCadastroUnidadeMedida from "../components/modalCadastroUnidadeMedida"
import ModalCadastroFornecedor from "../components/modalCadastroFornecedor"

type TipoModal = "produto" | "grupoproduto" | "unidademedida" | "fornecedor" | null;

function App() {
    const [modalAberta, setModalAberta] = useState<TipoModal>(null);
    
    return (
        <main>
            <section className="principal-section">
                <section className="button-section">
                    <button onClick={() => setModalAberta("produto")}>
                        Cadastrar Produto
                    </button>
                    <button onClick={() => setModalAberta("grupoproduto")}>
                        Cadastrar Grupo de Produto
                    </button>
                    <button onClick={() => setModalAberta("unidademedida")}>
                        Cadastrar Unidade de Medida
                    </button>
                    <button onClick={() => setModalAberta("fornecedor")}>
                        Cadastrar Fornecedor
                    </button>
                    <ModalCadastroProduto
                        aberta={modalAberta === "produto"}
                        onFechar={() => setModalAberta(null)}
                    />
                    <ModalCadastroGrupoProduto
                        aberta={modalAberta === "grupoproduto"}
                        onFechar={() => setModalAberta(null)}
                    />
                    <ModalCadastroUnidadeMedida
                        aberta={modalAberta === "unidademedida"}
                        onFechar={() => setModalAberta(null)}
                    />
                    <ModalCadastroFornecedor
                        aberta={modalAberta === "fornecedor"}
                        onFechar={() => setModalAberta(null)}
                    />
                </section>
            </section>
        </main>
    )
}

export default App;