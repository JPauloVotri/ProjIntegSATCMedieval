
import { Navigate, Outlet } from "react-router-dom";
import { estaLogado } from "./auth";

export default function PublicRoute() {
  return estaLogado() ? (
    <Navigate to="/home" replace />
  ) : (
    <Outlet />
  );
}
