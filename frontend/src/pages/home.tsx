import { useNavigate } from "react-router-dom";
import { fazerLogout } from "../auth";

import "../styles/home.css";

function App() {
    const navigate = useNavigate();

    function sair() {
        fazerLogout();
        navigate("/login", { replace: true });
    }

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
                    <button onClick={sair}>
                        Sair da conta
                    </button>
                </section>
            </section>
        </main>
    )
}

export default App;