import { FaUser } from "react-icons/fa";
import "../styles/userCard.css";

import { apiFetch } from "../../services/api";
import { useEffect, useState } from "react";

interface Usuario {
  id: number;
  nome: string;
}

export default function UserCard() { 
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

 return (
    <>
    {usuarios.map((user) => (
        <div key={user.id} className="user-card">
            <FaUser className="user-icon" />
                <p className="user-name" onClick={() => window.location.href = `/usuario/${user.id}`}>
                    {user.nome}
                </p>
        </div>
    ))}
    </>
  )
};