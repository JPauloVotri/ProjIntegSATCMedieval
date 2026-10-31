
import { useEffect, useState } from "react";
import { apiFetch } from "../../services/api";

interface GrupoProduto {
  id: number;
  nome: string;
}

interface SelectGruposProdutosProps {
  grupoProdutoId: number | "";
  setGrupoProdutoId: (id: number | "") => void;
}

export default function SelectGruposProdutos({
  grupoProdutoId,
  setGrupoProdutoId,
}: SelectGruposProdutosProps) {
  const [grupos, setGrupos] = useState<GrupoProduto[]>([]);

  useEffect(() => {
    async function carregarGrupos() {
      try {
        setGrupos(await apiFetch("/api/GruposProdutos"));
      } catch (error) {
        console.error("Erro ao carregar grupos:", error);
      }
    }

    carregarGrupos();
  }, []);

  return (
    <select
      value={grupoProdutoId}
      onChange={(e) =>
        setGrupoProdutoId(
          e.target.value === "" ? "" : Number(e.target.value)
        )
      }
    >
      <option value="">Selecione um grupo</option>

      {grupos.map((grupo) => (
        <option key={grupo.id} value={grupo.id}>
          {grupo.nome}
        </option>
      ))}
    </select>
  );
}
