
import { NavLink } from "react-router-dom";
import Logo from "../assets/logoMedieval.png";

type SidebarProps = {
  isOpen: boolean;
  onClose: () => void;
};

const pages = [
  { name: "Home", path: "/home" },
  { name: "Administração", path: "/administracao" },
  { name: "Cadastro", path: "/cadastro" },
  { name: "Cotação", path: "/cotacao" },
  { name: "Estoque", path: "/estoque" },
  { name: "Movimentação", path: "/movimentacao" },
];

export default function Sidebar({
  isOpen,
  onClose,
}: SidebarProps) {
  if (!isOpen) return null;

  return (
    <div className="sidebar-overlay" onClick={onClose}>
      <aside
        className="sidebar"
        onClick={(event) => event.stopPropagation()}
      >
        <div className="sidebar-header">
          <img
            src={Logo}
            alt="Logo"
            className="logo-sidebar"
          />
        </div>

        <nav>
          {pages.map((page) => (
            <NavLink
              key={page.path}
              to={page.path}
              end={page.path === "/app"}
              onClick={onClose}
              className={({ isActive }) =>
                `sidebar-link ${isActive ? "active" : ""}`
              }
            >
              {page.name}
            </NavLink>
          ))}
        </nav>
      </aside>
    </div>
  );
}
