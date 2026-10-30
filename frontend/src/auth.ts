const CHAVE_SESSAO = "medieval_auth";
const API_URL = import.meta.env.VITE_API_URL;

export interface UsuarioLogado {
  id: number;
  nome: string;
  email: string;
  grupoUsuario: string;
  status: string;
  ultimoLogin: string | null;
}

export function estaLogado(): boolean {
  return localStorage.getItem(CHAVE_SESSAO) === "true";
}

export async function fazerLogin(
  email: string,
  senha: string
): Promise<boolean> {
  try {
    const resposta = await fetch(`${API_URL}/api/Usuarios/login`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        email: email.trim(),
        senha,
      }),
    });

    if (!resposta.ok) {
      return false;
    }

    const usuario: UsuarioLogado = await resposta.json();

    localStorage.setItem(CHAVE_SESSAO, "true");
    localStorage.setItem("medieval_usuario", JSON.stringify(usuario));
    localStorage.setItem("usuario_nome", JSON.stringify(usuario.nome));

    return true;
  } catch (erro) {
    console.error("Erro ao realizar login:", erro);
    return false;
  }
}

export function fazerLogout(): void {
  localStorage.removeItem(CHAVE_SESSAO);
  localStorage.removeItem("medieval_usuario");
  localStorage.removeItem("usuario_nome");
}