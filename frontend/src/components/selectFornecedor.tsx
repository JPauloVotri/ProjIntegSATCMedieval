import { useEffect, useState } from "react";
import { apiFetch } from "../../services/api";

interface Fornecedor {
  id: number;
  razaoSocial: string;
}

interface SelectFornecedoresProps {
  fornecedorId: number | "";
  setFornecedorId: (id: number | "") => void;
}

export default function SelectFornecedores({
  fornecedorId,
  setFornecedorId,
}: SelectFornecedoresProps) {
  const [fornecedores, setFornecedores] = useState<Fornecedor[]>([]);

  useEffect(() => {
    async function carregarFornecedores() {
      try {
        setFornecedores(await apiFetch("/api/Fornecedores"));
      } catch (error) {
        console.error("Erro ao carregar fornecedores:", error);
      }
    }

    carregarFornecedores();
  }, []);

  return (
    <select
      value={fornecedorId}
      onChange={(e) =>
        setFornecedorId(
          e.target.value === "" ? "" : Number(e.target.value)
        )
      }
    >
      <option value="">Selecione um fornecedor</option>

      {fornecedores.map((fornecedor) => (
        <option id={fornecedor.id.toString()} value={fornecedor.id}>
          {fornecedor.razaoSocial}
        </option>
      ))}
    </select>
  );
}
