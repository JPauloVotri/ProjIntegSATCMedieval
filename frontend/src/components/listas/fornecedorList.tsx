
import "../../styles/fornecedorLis.css";

import { FaSearch, FaWhatsapp } from "react-icons/fa";
import { IoClose } from "react-icons/io5";

import { apiFetch } from "../../../services/api";
import { useEffect, useState } from "react";
import PesquisaEstMov from "../pesquisas/pesquisaEstMov";

interface Fornecedor {
  id: number;
  razaoSocial: string;
  telefone: string;
  observacao: string;
}

export default function FornecedorList() {
  const [fornecedorSelecionada, setFornecedorSelecionada] =
    useState<Fornecedor | null>(null);

  const [fornecedores, setFornecedores] = useState<Fornecedor[]>([]);
  const [erro, setErro] = useState("");
  const [pesquisa, setPesquisa] = useState("");

  useEffect(() => {
    async function carregarFornecedores() {
      try {
        const dados = await apiFetch<Fornecedor[]>("/api/Fornecedores");
        setFornecedores(dados);
      } catch (error) {
        setErro("Não foi possível carregar os fornecedores.");
        console.error(error);
      }
    }

    carregarFornecedores();
  }, []);

  const fornecedoresFiltrados = fornecedores.filter((fornecedor) =>
    fornecedor.razaoSocial
      .toLowerCase()
      .startsWith(pesquisa.trim().toLowerCase())
  );

  return (
    <div className="fornecedor-container">

      <div className="fornecedor-pesquisa">
        <PesquisaEstMov onPesquisar={setPesquisa} />
      </div>

      {erro && <p className="fornecedor-erro">{erro}</p>}

      <div className="fornecedor-tabela">

        <div className="fornecedor-header">
          <div className="fornecedor-razao">Razão Social</div>
          <div className="fornecedor-observacao">Observação</div>
          <div className="fornecedor-whatsapp">WhatsApp</div>
        </div>

        {fornecedoresFiltrados.map((fornecedor) => (
          <div key={fornecedor.id} className="fornecedor-list">

            <div className="fornecedor-razao">
              {fornecedor.razaoSocial}
            </div>

            <div className="fornecedor-observacao">
              <button
                className="fornecedor-botao-observacao"
                onClick={() => setFornecedorSelecionada(fornecedor)}
                aria-label={`Ver observações de ${fornecedor.razaoSocial}`}
              >
                <FaSearch />
              </button>
            </div>

            <div className="fornecedor-whatsapp">
              <a
                href={`https://wa.me/${fornecedor.telefone.replace(/\D/g, "")}`}
                target="_blank"
                rel="noreferrer"
                aria-label={`WhatsApp de ${fornecedor.razaoSocial}`}
              >
                <FaWhatsapp className="fornecedor-icone-whatsapp" />
                {fornecedor.telefone}
              </a>
            </div>

          </div>
        ))}

        {fornecedoresFiltrados.length === 0 && (
          <div className="fornecedor-list-empty">
            Nenhum fornecedor encontrado.
          </div>
        )}

      </div>

      {fornecedorSelecionada && (
        <div
          className="modal-overlay"
          onClick={() => setFornecedorSelecionada(null)}
        >
          <div
            className="modal-caixa"
            onClick={(e) => e.stopPropagation()}
          >
            <div className="modal-header">

              <div className="modal-close">
                <IoClose
                  onClick={() => setFornecedorSelecionada(null)}
                />
              </div>

              <h2 id="modal-titulo">
                Observações do Fornecedor
              </h2>

            </div>

            <div className="modal-conteudo">
              <div className="modal-observacao">
                {fornecedorSelecionada.observacao}
              </div>
            </div>

          </div>
        </div>
      )}

    </div>
  );
}
