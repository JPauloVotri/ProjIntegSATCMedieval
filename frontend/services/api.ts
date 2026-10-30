const API_URL = import.meta.env.VITE_API_URL;

export async function apiFetch<T>(
  endpoint: string,
  options: RequestInit = {}
): Promise<T> {
  const response = await fetch(`${API_URL}${endpoint}`, {
    ...options,
    headers: {
      "Content-Type": "application/json",
      ...options.headers,
    },
  });

  if (!response.ok) {
    const texto = await response.text();

    let mensagem = `Erro na API: ${response.status} ${response.statusText}`;

    try {
      const dados = JSON.parse(texto);

      if (dados.errors && typeof dados.errors === "object") {
        const mensagens = Object.values(dados.errors)
          .flat()
          .filter(
            (item): item is string => typeof item === "string"
          );

        if (mensagens.length > 0) {
          mensagem = mensagens.join("\n");
        }
      } else if (dados.detail) {
        mensagem = dados.detail;
      } else if (dados.title) {
        mensagem = dados.title;
      } else if (dados.message) {
        mensagem = dados.message;
      }
    } catch {
      if (texto.trim()) {
        mensagem = texto;
      }
    }

    throw new Error(mensagem);
  }

  if (response.status === 204) {
    return undefined as T;
  }

  return response.json() as Promise<T>;
}