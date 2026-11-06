
import React from "react";
import ReactDOM from "react-dom/client";
import {
  BrowserRouter,
  Routes,
  Route,
  Navigate,
} from "react-router-dom";

import Login from "./pages/login";
import AppPage from "./App";
import Home from "./pages/home";
import UnidadeMedida from "./pages/unidadesMedidas";
import Cotacao from "./pages/cotacao";
import Administrador from "./pages/administracao";
import Estoque from "./pages/estoque";
import Saida from "./pages/saida";
import Usuario from "./pages/usuario";
import Fornecedores from "./pages/fornecedores";
import CotacaoFornecedores from "./pages/cotacaoFornecedores";
import Pedidos from "./pages/pedidos";
import GrupoProduto from "./pages/grupoProduto";

import Layout from "./components/layout";

import ProtectedRoute from "./ProtectedRoute";
import PublicRoute from "./PublicRoute";

import "./styles/index.css";

ReactDOM.createRoot(
  document.getElementById("root")!
).render(
  <React.StrictMode>
    <BrowserRouter>
      <Routes>
        <Route element={<PublicRoute />}>
            <Route path="/login" element={<Login />} />
        </Route>

        <Route element={<ProtectedRoute />}>
          <Route element={<Layout />}>
            <Route path="/app" element={<AppPage />} />
            <Route path="/home" element={<Home />} />
            <Route path="/unidadeMedida" element={<UnidadeMedida />} />
            <Route path="/administracao" element={<Administrador />} />
            <Route path="/estoque" element={<Estoque />} />
            <Route path="/saida" element={<Saida />} />
            <Route path="/cotacao" element={<Cotacao />} />
            <Route path="/Usuario/:id" element={<Usuario />} />
            <Route path="/fornecedores" element={<Fornecedores />} />
            <Route path="/cotacaoFornecedores/:id/:produtoId" element={<CotacaoFornecedores />} />
            <Route path="/pedidos" element={<Pedidos />} />
            <Route path="/grupoProduto" element={<GrupoProduto />} />
          </Route>
        </Route>

        <Route path="*" element={<Navigate to="/home" replace />} />
      </Routes>
    </BrowserRouter>
  </React.StrictMode>
);
