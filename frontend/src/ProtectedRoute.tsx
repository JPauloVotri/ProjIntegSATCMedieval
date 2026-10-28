
import { Navigate, Outlet } from "react-router-dom";
import { estaLogado } from "./auth";

export default function ProtectedRoute() {
  return estaLogado() ? (
    <Outlet />
  ) : (
    <Navigate to="/login" replace />
  );
}
