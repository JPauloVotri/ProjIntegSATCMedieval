import { FaUser } from "react-icons/fa";
import "../styles/userCard.css";
import "../styles/modalObs.css"
import "../styles/modalCriacaoMensagem.css"

import { apiFetch } from "../../services/api";
import { useState, type FormEvent } from "react";

export default function UserCardAdicionar() { 
    const [CriarUsuario, setCriarUsuario] = useState(false);

    const [nome, setNome] = useState("");
    const [email, setEmail] = useState("");
    const [senha, setSenha] = useState("");
    const [grupoUsuario, setGrupoUsuario] = useState(1);

    const [erros, setErros] = useState<string[]>([]);
    const [sucesso, setSucesso] = useState("");

    async function cadastrarUsuario(e: FormEvent<HTMLFormElement>) {
        e.preventDefault();

        setErros([]);
        setSucesso("");

        try {
            await apiFetch("/api/Usuarios", {
            method: "POST",
            body: JSON.stringify({ 
                nome,
                email,
                senha,
                grupoUsuario,
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
        }
    }

 return (
    <>
        <div className="user-card">
            <FaUser className="user-icon" />
            <p className="user-name" onClick={() => setCriarUsuario(true)}>
                Adicionar Usuário
            </p>
        </div>
        {CriarUsuario && (
            <div className="modal-overlay" onClick={() => setCriarUsuario(false)}>
                <div className="modal-caixa" onClick={(e) => e.stopPropagation()}>
                    <form onSubmit={cadastrarUsuario}>
                        <input type="text" placeholder="Nome do usuário" value={nome} onChange={(e) => setNome(e.target.value)} />
                        <input type="text" placeholder="E-mail" value={email} onChange={(e) => setEmail(e.target.value)} />
                        <input type="password" placeholder="Senha" value={senha} onChange={(e) => setSenha(e.target.value)} />
                        <select value={grupoUsuario} onChange={(e) => setGrupoUsuario(Number(e.target.value))}>
                            <option value={1}>Grupo 1</option>
                            <option value={2}>Grupo 2</option>
                        </select>
                        <button type="submit">Cadastrar usuário</button>

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
  )
};