import { FaUser } from "react-icons/fa";
import "../styles/userCard.css";

export default function UserCard() { 
const users = [
    { id: 1, name: 'Paulocesar Galdino' },
    { id: 2, name: 'Sr. Medieval' },
    { id: 3, name: 'Sra. Medieval' },
];


 return (
    <>
    {users.map((user: { id: number; name: string }) => (
        <div key={user.id} className="user-card">
            <FaUser className="user-icon" />
                <p className="user-name">
                    {user.name}
                </p>
        </div>
    ))}
    </>
  )
};