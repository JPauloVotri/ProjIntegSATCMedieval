import "../styles/home.css";

function App() {

    return (
        <main>
            <section className="principal-section">
                <p className="welcome-text">Bem-Vindo de volta <a className="name-text">Jucemar</a>!</p>
                <section className="button-section">
                    <button onClick={() => window.location.href = "/administracao"}>
                        Administração
                    </button>
                    <button onClick={() => window.location.href = "/cadastro"}>
                        Cadastro
                    </button>
                    <button onClick={() => window.location.href = "/cotacao"}>
                        Cotação
                    </button>
                    <button onClick={() => window.location.href = "/estoque"}>
                        Estoque
                    </button>
                    <button onClick={() => window.location.href = "/movimentacao"}>
                        Movimentação
                    </button>
                </section>
            </section>
        </main>
    )
}

export default App;