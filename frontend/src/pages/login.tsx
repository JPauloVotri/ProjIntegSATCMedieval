
import { useNavigate } from "react-router-dom";
import { useState, type FormEvent } from "react";
import "../styles/login.css";
import "../styles/index.css";

// Imagens
import Logo from "../assets/logoMedieval.png";

function App() {
  const [login, setLogin] = useState("");
  const [senha, setSenha] = useState("");
  const [erro, setErro] = useState("");
  const navigate = useNavigate();

  function handleSubmit(event: FormEvent<HTMLFormElement>) {
    event.preventDefault();

    if (!login.trim() || !senha) {
      setErro("Preencha o login e a senha.");
      return;
    } else

    setErro("");
    navigate("/home");
    console.log("Login:", login);

  }

  return (
    <main>

      <section className="principal-section">
        <img
          src={Logo}
          alt="Logotipo"
          className="login-logo"
        />

        <form className="login-form" onSubmit={handleSubmit}>
          <div className="form-field">
            <label htmlFor="login" className="form-label">
              <a className="form-text">Login</a>
            </label>
            <input
              id="login"
              type="text"
              value={login}
              onChange={(event) => setLogin(event.target.value)}
              autoComplete="username"
            />
          </div>

          <div className="form-field">
            <label htmlFor="senha" className="form-label">
              <a className="form-text">Senha</a>
            </label>
            <input
              id="senha"
              type="password"
              value={senha}
              onChange={(event) => setSenha(event.target.value)}
              autoComplete="current-password"
            />
          </div>

          {erro && <p className="login-error">{erro}</p>}

          <button type="submit">
            Entrar
          </button>
        </form>
      </section>
    </main>
  );
}

export default App;
