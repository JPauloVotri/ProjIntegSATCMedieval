
import { useState } from "react";

interface PesquisaEstMovProps {
  onPesquisar: (razaoSocial: string) => void;
}

export default function PesquisaEstMov({ onPesquisar }: PesquisaEstMovProps) {
  const [razaoSocial, setRazaoSocial] = useState("");

  function handleInputChange(valor: string) {
    setRazaoSocial(valor);
    onPesquisar(valor);
  }

  return (
    <>
      <label htmlFor="razaoSocial">Razão Social: </label>
      <input
        type="text"
        name="razao social"
        id="razaoSocial"
        value={razaoSocial}
        onChange={(e) => handleInputChange(e.target.value)}
        placeholder="Pesquisar fornecedor..."
      />
    </>
  );
}