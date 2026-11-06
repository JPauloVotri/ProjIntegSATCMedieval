import { Link, useParams } from "react-router-dom";
import { apiFetch } from "../../services/api";
import { useEffect, useState } from "react";
import { FaUser } from "react-icons/fa";

import "../styles/usuarioDetalhes.css";

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
  const [carregando, setCarregando] = useState(true);

  const { id } = useParams<{ id: string }>();

  useEffect(() => {
    async function carregarUsuarios() {
      try {
        const dados = await apiFetch<Usuario[]>("/api/Usuarios");
        setUsuarios(dados);
      } catch (error) {
        setErro("Não foi possível carregar os usuários.");
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

  if (carregando) {
    return (
      <main className="usuario-detalhes">
        <p>Carregando informações...</p>
      </main>
    );
  }

  if (erro || !usuario) {
    return (
      <main className="usuario-detalhes">
        <h2>{erro || "Usuário não encontrado"}</h2>
        <Link to="/administracao" className="button-voltar-2">
          Voltar para a lista
        </Link>
      </main>
    );
  }

  return (
    <main className="usuario-detalhes">

      <div className="usuario-cabecalho">
        <h1>Visualize as informações do usuário cadastrado.</h1>
      </div>

      <section className="usuario-card">

        <div className="usuario-identificacao">
          <div className="usuario-avatar">
            <FaUser />
          </div>

          <div>
            <h2>{usuario.nome}</h2>
            <p>{usuario.email}</p>
          </div>
        </div>

        <div className="usuario-secao">
          <h3>Informações pessoais</h3>

          <div className="usuario-grid">

            <div className="usuario-campo">
              <span>Nome completo</span>
              <strong>{usuario.nome}</strong>
            </div>

            <div className="usuario-campo">
              <span>E-mail</span>
              <strong>{usuario.email}</strong>
            </div>

          </div>
        </div>

      <Link to="/administracao" className="button-voltar">
        Voltar para a lista
      </Link>

      </section>
    </main>
  );
}