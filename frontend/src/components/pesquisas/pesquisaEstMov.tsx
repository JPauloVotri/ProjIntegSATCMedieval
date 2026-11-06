import { useState } from "react";
import { FaSearch } from "react-icons/fa";

import "../../styles/pesquisaEstMov.css";

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
    <div className="pesquisa-estmov">
      <FaSearch className="pesquisa-estmov-icon" />

      <input
        type="text"
        name="nome"
        id="nome"
        value={nome}
        onChange={(e) => handleInputChange(e.target.value)}
        placeholder="Pesquisar..."
      />

    </div>
  );
}