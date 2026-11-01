
import { useState } from "react";

interface PesquisaEstMovProps {
  onPesquisar: (nome: string) => void;
}

export default function PesquisaEstMov({ onPesquisar }: PesquisaEstMovProps) {
  const [nome, setNome] = useState("");

  function handleInputChange(valor: string) {
    setNome(valor);
    onPesquisar(valor);
  }

  return (
    <>
      <label htmlFor="nome">Nome: </label>
      <input
        type="text"
        name="nome"
        id="nome"
        value={nome}
        onChange={(e) => handleInputChange(e.target.value)}
        placeholder="Pesquisar produto..."
      />
    </>
  );
}