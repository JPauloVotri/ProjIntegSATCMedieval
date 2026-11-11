import { FaUser } from "react-icons/fa";
import "../styles/userCard.css";

import { apiFetch } from "../../services/api";
import { useEffect, useState } from "react";
import Loading from "./loading";

interface Usuario {
  id: number;
  nome: string;
}

export default function UserCard() { 
    const [loading, setLoading] = useState(true);
    const [usuarios, setUsuarios] = useState<Usuario[]>([]);
    const [erro, setErro] = useState("");

    useEffect(() => {
        async function carregarUsuarios() {
            try {
                setLoading(true);
                const dados = await apiFetch<Usuario[]>("/api/Usuarios");
                setUsuarios(dados);
            } catch (error) {
                setErro("Não foi possível carregar os usuários.");
                console.error(error);
            } finally {
                setLoading(false);
            }
          }

    carregarUsuarios();
  }, []);

 return (
    <>
      {loading ? (
        <Loading />
      ) : (
        usuarios.map((user: Usuario) => (
          <div
            key={user.id}
            className="user-card"
            onClick={() => {
              window.location.href = `/usuario/${user.id}`;
            }}
          >
            <FaUser className="user-icon" />
            <p className="user-name">{user.nome}</p>
          </div>
        ))
      )}
    </>
  );
};