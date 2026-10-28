import { Link, useParams } from "react-router-dom";
import { usuarios } from "../data/usuarios";

export default function UsuarioDetalhes() {
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
      altero aqui dps
    </div>
  );
}