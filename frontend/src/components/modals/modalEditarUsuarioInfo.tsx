
import { IoClose } from "react-icons/io5";

import "../../styles/userCard.css";
import "../../styles/modalObs.css";
import "../../styles/modalCriacaoMensagem.css";
import "../../styles/usuarioDetalhes.css"

import { apiFetch } from "../../../services/api";
import { useState, type FormEvent, useEffect } from "react";
import { useParams } from "react-router-dom";

interface Usuario {
  id: number;
  nome: string;
  email: string;
  grupoUsuario: string;
  status: string;
  ultimoLogin: string | null;
}

export default function ModalEditarUsuario() {
    const [alterarUsuario, setAlterarUsuario] = useState(false);
    const [usuarios, setUsuarios] = useState<Usuario[]>([]);

    const [nome, setNome] = useState("");
    const [email, setEmail] = useState("");

    const [erros, setErros] = useState<string[]>([]);
    const [sucesso, setSucesso] = useState("");
    const [carregando, setCarregando] = useState(false);

    function fecharModal() {
        setAlterarUsuario(false);
        setErros([]);
        setSucesso("");
    }

    const { id } = useParams<{ id: string }>();

    useEffect(() => {
        async function carregarUsuarios() {
            try {
                const dados = await apiFetch<Usuario[]>("/api/Usuarios");
                setUsuarios(dados);
            } catch (error) {
                setErros(["Não foi possível carregar os usuários."]);
                console.error(error);
            } finally {
                setCarregando(false);
            }
        }

        carregarUsuarios();
    }, []);

    const usuario = usuarios.find(
      (usuario) => usuario.id === Number(id)
    );

    useEffect(() => {
        if (!usuario) return;

        setNome(usuario.nome ?? "");
        setEmail(usuario.email ?? "");
    }, [usuario]);

    async function cadastrarUsuario(e: FormEvent<HTMLFormElement>) {
        e.preventDefault();

        setErros([]);
        setSucesso("");
        setCarregando(true);

        try {
            await apiFetch("/api/Usuarios/" + id, {
                method: "PUT",
                body: JSON.stringify({
                    nome,
                    email,
                    grupoUsuario: 1,
                }),
            });

            setSucesso("Usuário alterado com sucesso!");
            setNome("");
            setEmail("");
        } catch (error) {
            const mensagem =
                error instanceof Error
                    ? error.message
                    : "Não foi possível alterar o usuário.";

            setErros(mensagem.split("\n"));
        }
    }

    return (
        <>
            <div>
                <button className="adicionar-button button-editar" onClick={() => setAlterarUsuario(true)}>
                    Editar
                </button>
            </div>

            {alterarUsuario && (
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
                                <div className="modal-titulos-9">
                                    <h2>Editar Usuário</h2>
                                </div>
                            </div>

                            <button
                                type="button"
                                className="modal-close modal-close-2"
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

                            <button
                                type="submit"
                                className="modal-usuario-submit modal-usuario-submit-2"
                                disabled={carregando}
                            >
                                {carregando ? "Alterando..." : "Alterar usuário"}
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
