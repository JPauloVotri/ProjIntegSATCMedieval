
import { useNavigate } from "react-router-dom";
import { useState, type FormEvent } from "react";
import { fazerLogin } from "../auth";
import "../styles/login.css";
import "../styles/index.css";

import Logo from "../assets/logoMedieval.png";

function App() {
  const [login, setLogin] = useState("");
  const [senha, setSenha] = useState("");
  const [erro, setErro] = useState("");
  const navigate = useNavigate();

  function handleSubmit(event: FormEvent<HTMLFormElement>) {
    event.preventDefault();

    if (!login.trim() || !senha) {
      setErro("Preencha o e-mail e a senha.");
      return;
    }

    const autenticado = fazerLogin(login, senha);

    if (!autenticado) {
      setErro("E-mail ou senha incorretos.");
      return;
    }

    setErro("");
    navigate("/home", { replace: true });
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
              <span className="form-text">E-mail</span>
            </label>

            <input
              id="login"
              type="email"
              value={login}
              onChange={(event) => setLogin(event.target.value)}
              autoComplete="username"
              required
            />
          </div>

          <div className="form-field">
            <label htmlFor="senha" className="form-label">
              <span className="form-text">Senha</span>
            </label>

            <input
              id="senha"
              type="password"
              value={senha}
              onChange={(event) => setSenha(event.target.value)}
              autoComplete="current-password"
              required
            />
          </div>

          {erro && (
            <p className="login-error" role="alert">
              {erro}
            </p>
          )}

          <button type="submit">Entrar</button>
        </form>
      </section>
    </main>
  );
}

export default App;
