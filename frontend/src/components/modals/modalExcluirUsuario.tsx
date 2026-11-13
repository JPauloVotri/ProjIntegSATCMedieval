
import { IoClose } from "react-icons/io5";

import "../../styles/userCard.css";
import "../../styles/modalObs.css";
import "../../styles/modalCriacaoMensagem.css";

import { apiFetch } from "../../../services/api";
import { useState, type FormEvent, useEffect } from "react";
import { useParams } from "react-router-dom";
import { useNavigate } from "react-router-dom";

interface Usuario {
  id: number;
  nome: string;
  email: string;
  grupoUsuario: string;
  status: string;
  ultimoLogin: string | null;
}

export default function ModalExcluirUsuario() {
    const [excluirUsuario, setExcluirUsuario] = useState(false);
    const [usuarios, setUsuarios] = useState<Usuario[]>([]);

    const [erros, setErros] = useState<string[]>([]);
    const [sucesso, setSucesso] = useState("");
    const [carregando, setCarregando] = useState(false);

    const navigate = useNavigate();

    function fecharModal() {
        setExcluirUsuario(false);
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

    async function cadastrarUsuario(e: FormEvent<HTMLFormElement>) {
        e.preventDefault();

        setCarregando(true);

        try {
            await apiFetch("/api/Usuarios/" + id, {
                method: "DELETE",
            });
        } catch (error) {
            const mensagem =
                error instanceof Error
                    ? error.message
                    : "Não foi possível deletar o usuário.";

            setErros(mensagem.split("\n"));
        } finally {
          navigate("/administracao");
        }
    }

    return (
        <>
            <div>
<button
    className="usuario-excluir-button"
    onClick={() => setExcluirUsuario(true)}
>
    Excluir
</button>
            </div>

            {excluirUsuario && (
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
                                    <h2>Excluir Usuário</h2>
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
                            
                            <label>Você tem certeza que quer</label>
                            <label>excluir este usuário?</label>

                            <button
                                type="submit"
                                className="modal-usuario-submit modal-usuario-submit-2"
                                disabled={carregando}
                            >
                                {carregando ? "Excluindo..." : "Excluir usuário"}
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
