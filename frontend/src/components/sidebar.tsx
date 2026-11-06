
import { NavLink } from "react-router-dom";
import Logo from "../assets/logoMedieval.png";

type SidebarProps = {
  isOpen: boolean;
  onClose: () => void;
};

const pages = [
  { name: "Home", path: "/home" },
  { name: "Administração", path: "/administracao" },
  { name: "Fornecedores", path: "/fornecedores" },
  { name: "Grupo de Produtos", path: "/grupoProduto" },
  { name: "Unidades de Medida", path: "/unidadeMedida" },
  { name: "Estoque", path: "/estoque" },
  { name: "Cotação", path: "/cotacao" },
  { name: "Pedidos", path: "/pedidos" },
  { name: "Saída", path: "/saida" },
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
        <div>
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
