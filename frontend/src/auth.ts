
const CHAVE_SESSAO = "medieval_auth";

export function estaLogado(): boolean {
  return localStorage.getItem(CHAVE_SESSAO) === "true";
}

export function fazerLogin(email: string, senha: string): boolean {
  // MUDAR ISSO DPS DE CONECTAR O BACKEND!!!
  if (
    email.trim().toLowerCase() === "admin@medieval.com" &&
    senha === "Medieval123!"
  ) {
    localStorage.setItem(CHAVE_SESSAO, "true");
    return true;
  }

  return false;
}

export function fazerLogout(): void {
  localStorage.removeItem(CHAVE_SESSAO);
}
