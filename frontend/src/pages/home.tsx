
import { useNavigate } from "react-router-dom";
import { fazerLogout } from "../auth";

import "../styles/home.css";

function App() {
    const navigate = useNavigate();
    let nomeUsuario = localStorage.getItem("usuario_nome") ?? "Usuário";
    nomeUsuario = nomeUsuario.replaceAll('"', '');

    function sair() {
        fazerLogout();
        navigate("/login", { replace: true });
    }

    return (
        <main>
            <section className="principal-section">
                <p className="welcome-text">
                    Bem-Vindo de volta <a className="name-text" id="nomeUsuario">{nomeUsuario}</a>!
                </p>
                <section className="button-section">
                    <div className="listas-container">
                        <div className="lista-administracao">
                            <h2>Listas</h2>
                            <button onClick={() => navigate("/administracao")}>
                                Administração
                            </button>
                            <button onClick={() => navigate("/fornecedores")}>
                                Fornecedores
                            </button>
                            <button onClick={() => navigate("/grupoProduto")}>
                                Grupo de Produtos
                            </button>
                            <button onClick={() => navigate("/unidadeMedida")}>
                                Unidades de Medida
                            </button>
                        </div>
                        <div className="lista-estoque">
                            <h2>Estoque</h2>
                            <button onClick={() => navigate("/estoque")}>
                                Estoque
                            </button>
                            <button onClick={() => navigate("/cotacao")}>
                                Cotação
                            </button>
                            <button onClick={() => navigate("/pedidos")}>
                                Pedidos
                            </button>
                            <button onClick={() => navigate("/saida")}>
                                Saída
                            </button>
                        </div>
                    </div>
                </section>
                <button onClick={sair} className="logout-button">
                    Sair da conta
                </button>
            </section>
        </main>
    );
}

export default App;
