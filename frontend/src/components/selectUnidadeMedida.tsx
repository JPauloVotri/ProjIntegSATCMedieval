
import { useEffect, useState } from "react";
import { apiFetch } from "../../services/api";

interface UnidadeMedida {
  id: number;
  nome: string;
}

interface SelectUnidadesMedidaProps {
  unidadeMedidaId: number | "";
  setUnidadeMedidaId: (id: number | "") => void;
}

export default function SelectUnidadesMedida({
  unidadeMedidaId,
  setUnidadeMedidaId,
}: SelectUnidadesMedidaProps) {
  const [unidade, setUnidade] = useState<UnidadeMedida[]>([]);

  useEffect(() => {
    async function carregarUnidade() {
      try {
        setUnidade(await apiFetch("/api/UnidadesMedida"));
      } catch (error) {
        console.error("Erro ao unidades de medida:", error);
      }
    }

    carregarUnidade();
  }, []);

  return (
    <select
      value={unidadeMedidaId}
      onChange={(e) =>
        setUnidadeMedidaId(
          e.target.value === "" ? "" : Number(e.target.value)
        )
      }
    >
      <option value="">Selecione uma Unidade de Medida</option>

      {unidade.map((medida) => (
        <option key={medida.id} value={medida.id}>
          {medida.nome}
        </option>
      ))}
    </select>
  );
}
