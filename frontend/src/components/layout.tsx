
import { useState } from "react";
import { Outlet, useLocation } from "react-router-dom";
import Header from "./header";
import Sidebar from "./sidebar";
import "../styles/layout.css";

const pageTitles: Record<string, string> = {
  "/app": "Home",
  "/administracao": "Administração",
  "/cadastro": "Cadastro",
  "/cotacao": "Cotação",
  "/estoque": "Estoque",
  "/movimentacao": "Movimentação",
  "/fornecedores": "Fornecedores",
};

export default function Layout() {
  const [menuOpen, setMenuOpen] = useState(false);
  const location = useLocation();

  const title = location.pathname.startsWith("/usuario/")
    ? "Detalhes do Usuário" 
    : location.pathname.startsWith("/cotacaoFornecedores/")
    ? "Fornecedores da Cotação" 
    : pageTitles[location.pathname] ?? "Home";

  return (
    <div className="app-layout">
      <Header
        title={title}
        onMenuClick={() => setMenuOpen(true)}
      />

      <Sidebar
        isOpen={menuOpen}
        onClose={() => setMenuOpen(false)}
      />

      <main className="page-content">
        <Outlet />
      </main>
    </div>
  );
}
