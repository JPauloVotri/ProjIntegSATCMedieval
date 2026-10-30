
import { useNavigate } from "react-router-dom";
import { useState, type FormEvent } from "react";
import { fazerLogin } from "../auth";
import { FaRegEye } from "react-icons/fa";
import { FaRegEyeSlash } from "react-icons/fa";
import "../styles/login.css";
import "../styles/index.css";

import Logo from "../assets/logoMedieval.png";

function App() {
  const [login, setLogin] = useState("");
  const [senha, setSenha] = useState("");
  const [erro, setErro] = useState("");
  const navigate = useNavigate();

  const campoSenha = document.getElementById("senha") as HTMLInputElement | null;
  const [verSenha, setVerSenha] = useState(false);

  if (verSenha === true) {
    if (campoSenha) {
      campoSenha.type = "text";
    }

  } else {
    if (campoSenha) campoSenha.type = "password";
  }

  async function handleSubmit(event: FormEvent<HTMLFormElement>) {
    event.preventDefault();

    if (!login.trim() || !senha) {
      setErro("Preencha o e-mail e a senha.");
      return;
    }

    const autenticado = await fazerLogin(login, senha);

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
            <input id="login" type="email" value={login} onChange={(event) => setLogin(event.target.value)} autoComplete="username" required />
          </div>
          <div className="form-field">
            <label htmlFor="senha" className="form-label">
              <span className="form-text">Senha</span>
            </label>
              <button type="button" className="olho-button" onClick={() => setVerSenha((visivel) => !visivel)} aria-label={verSenha ? "Esconder senha" : "Mostrar senha"} aria-pressed={verSenha} >
                {verSenha ? <FaRegEyeSlash /> : <FaRegEye />}
              </button>
            <input id="senha" type={verSenha ? "text" : "password"} value={senha} onChange={(event) => setSenha(event.target.value)} autoComplete="current-password" className="senha-input" required />
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
