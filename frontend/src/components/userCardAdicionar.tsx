
import { FaUser } from "react-icons/fa";
import { IoClose } from "react-icons/io5";

import "../styles/userCard.css";
import "../styles/modalObs.css";
import "../styles/modalCriacaoMensagem.css";

import { apiFetch } from "../../services/api";
import { useState, type FormEvent } from "react";

export default function UserCardAdicionar() {
    const [criarUsuario, setCriarUsuario] = useState(false);

    const [nome, setNome] = useState("");
    const [email, setEmail] = useState("");
    const [senha, setSenha] = useState("");
    const [grupoUsuario, setGrupoUsuario] = useState(1);

    const [erros, setErros] = useState<string[]>([]);
    const [sucesso, setSucesso] = useState("");
    const [carregando, setCarregando] = useState(false);

    function fecharModal() {
        setCriarUsuario(false);
        setErros([]);
        setSucesso("");
    }

    async function cadastrarUsuario(e: FormEvent<HTMLFormElement>) {
        e.preventDefault();

        setErros([]);
        setSucesso("");
        setCarregando(true);

        try {
            await apiFetch("/api/Usuarios", {
                method: "POST",
                body: JSON.stringify({
                    nome,
                    email,
                    senha,
                    grupoUsuario: 1,
                }),
            });

            setSucesso("Usuário cadastrado com sucesso!");

            setNome("");
            setEmail("");
            setSenha("");
            setGrupoUsuario(1);

        } catch (error) {
            const mensagem =
                error instanceof Error
                    ? error.message
                    : "Não foi possível cadastrar o usuário.";

            setErros(mensagem.split("\n"));

        } finally {
            setCarregando(false);
        }
    }

    return (
        <>
            <div className="user-card">
                <FaUser className="user-icon" />

                <p
                    className="user-name"
                    onClick={() => setCriarUsuario(true)}
                >
                    Adicionar Usuário
                </p>
            </div>

            {criarUsuario && (
                <div
                    className="modal-overlay"
                    onClick={fecharModal}
                >
                    <div
                        className="modal-caixa modal-usuario"
                        onClick={(e) => e.stopPropagation()}
                    >
                        <div className="modal-usuario-cabecalho">
                            <div className="modal-usuario-titulo">
                                <div className="modal-titulos">
                                    <h2>Adicionar Usuário</h2>
                                </div>
                            </div>

                            <button
                                type="button"
                                className="modal-close"
                                onClick={fecharModal}
                            >
                                <IoClose />
                            </button>
                        </div>

                        <form
                            className="modal-usuario-form"
                            onSubmit={cadastrarUsuario}
                        >
                            <label>Nome do usuário</label>
                            <input
                                type="text"
                                placeholder="Digite o nome completo"
                                value={nome}
                                onChange={(e) => setNome(e.target.value)}
                            />

                            <label>E-mail</label>
                            <input
                                type="email"
                                placeholder="Digite o e-mail"
                                value={email}
                                onChange={(e) => setEmail(e.target.value)}
                            />

                            <label>Senha</label>
                            <input
                                type="password"
                                placeholder="Digite a senha"
                                value={senha}
                                onChange={(e) => setSenha(e.target.value)}
                            />

                            <button
                                type="submit"
                                className="modal-usuario-submit"
                                disabled={carregando}
                            >
                                {carregando ? "Cadastrando..." : "Cadastrar usuário"}
                            </button>

                            {erros.length > 0 && (
                                <div className="mensagens-erro" role="alert">
                                    {erros.map((mensagem, index) => (
                                        <p key={`${index}-${mensagem}`}>
                                            {mensagem}
                                        </p>
                                    ))}
                                </div>
                            )}

                            {sucesso && (
                                <p className="mensagem-sucesso" role="status">
                                    {sucesso}
                                </p>
                            )}
                        </form>
                    </div>
                </div>
            )}
        </>
    );
}
