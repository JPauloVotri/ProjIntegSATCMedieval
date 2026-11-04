
import { useEffect, useState } from "react";
import { apiFetch } from "../../services/api";

interface produto {
  id: number;
  nome: string;
}

interface SelectUnidadesMedidaProps {
  produtoId: number | "";
  setProdutoId: (id: number | "") => void;
}

export default function SelectUnidadesMedida({
  produtoId,
  setProdutoId,
}: SelectUnidadesMedidaProps) {
  const [produto, setProduto] = useState<produto[]>([]);

  useEffect(() => {
    async function carregarproduto() {
      try {
        setProduto(await apiFetch("/api/Produtos", { method: "GET" }));
      } catch (error) {
        console.error("Erro ao carregar produtos:", error);
      }
    }

    carregarproduto();
  }, []);

  return (
    <select
      value={produtoId}
      onChange={(e) =>
        setProdutoId(
          e.target.value === "" ? "" : Number(e.target.value)
        )
      }
    >
      <option value="">Selecione um Produto</option>

      {produto.map((produto) => (
        <option key={produto.id} value={produto.id}>
          {produto.nome}
        </option>
      ))}
    </select>
  );
}
