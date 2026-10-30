import { Link, useParams } from "react-router-dom";
import { apiFetch } from "../../services/api";
import { useEffect, useState } from "react";

interface Usuario {
  id: number;
  nome: string;
  email: string;
  grupoUsuario: string;
  status: string;
  ultimoLogin: string | null;
}

export default function UsuarioDetalhes() {
    const [usuarios, setUsuarios] = useState<Usuario[]>([]);
    const [erro, setErro] = useState("");
  
    useEffect(() => {
        async function carregarUsuarios() {
            try {
                const dados = await apiFetch<Usuario[]>("/api/Usuarios");
                setUsuarios(dados);
            } catch (error) {
                setErro("Não foi possível carregar os usuários.");
                console.error(error);
            }
          }
  
    carregarUsuarios();
  }, []);

  const { id } = useParams<{ id: string }>();

  const usuario = usuarios.find(
    (usuario) => usuario.id === Number(id)
  );

  if (!usuario) {
    return (
      <div>
        <h1>Usuário não encontrado</h1>
        <Link to="/administracao">Voltar para a lista</Link>
      </div>
    );
  }

  return (
    <div>
      <Link to="/administracao">← Voltar</Link>
      <p>
        <strong>ID:</strong> {usuario.id}
      </p>
      <p>
        <strong>Nome:</strong> {usuario.nome}
      </p>
      <p>
        <strong>E-mail:</strong> {usuario.email}
      </p>
      <p>
        <strong>Grupo do Usuário:</strong> {usuario.grupoUsuario}
      </p>
      <p>
        <strong>Status:</strong> {usuario.status}
      </p>
      <p>
        <strong>Último Login:</strong> {usuario.ultimoLogin}
      </p>
    </div>
  );
}