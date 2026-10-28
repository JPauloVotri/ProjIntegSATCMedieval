
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
import Cadastro from "./pages/cadastro";
import Cotacao from "./pages/cotacao";
import Administrador from "./pages/administracao";
import Estoque from "./pages/estoque";
import Movimento from "./pages/movimentacao";
import Usuario from "./pages/usuario";

import Layout from "./components/layout";

import "./styles/index.css";

ReactDOM.createRoot(
  document.getElementById("root")!
).render(
  <React.StrictMode>
    <BrowserRouter>
      <Routes>
        <Route path="/login" element={<Login />} />

        <Route element={<Layout />}>
          <Route path="/app" element={<AppPage />} />
          <Route path="/home" element={<Home />} />
          <Route path="/cadastro" element={<Cadastro />} />
          <Route path="/administracao" element={<Administrador />} />
          <Route path="/estoque" element={<Estoque />} />
          <Route path="/movimentacao" element={<Movimento />} />
          <Route path="/cotacao" element={<Cotacao />} />
          <Route path="/Usuario/:id" element={<Usuario />} />
        </Route>

        <Route path="*" element={<Navigate to="/home" replace />} />
      </Routes>
    </BrowserRouter>
  </React.StrictMode>
);
