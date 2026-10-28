import { FaUser } from "react-icons/fa";
import "../styles/userCard.css";
import { usuarios } from "../data/usuarios";

export default function UserCard() { 

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