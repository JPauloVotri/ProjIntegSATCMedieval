import { apiFetch } from "../../services/api";
import { useState, type FormEvent } from "react";
import SelectEstados from "./selectEstados";

import "../styles/modalCriacaoMensagem.css";
import "../styles/ModalCadastro.css";

interface ModalCadastroProps {
  aberta: boolean;
  onFechar: () => void;
}

export default function ModalCadastro({
  aberta,
  onFechar,
}: ModalCadastroProps) {
  const [razaoSocial, setRazaoSocial] = useState("");
  const [nomeFantasia, setNomeFantasia] = useState("");
  const [documento, setDocumento] = useState("");
  const [inscricaoEstadual, setInscricaoEstadual] = useState("");
  const [email, setEmail] = useState("");
  const [telefone, setTelefone] = useState("");
  const [cep, setCep] = useState("");
  const [logradouro, setLogradouro] = useState("");
  const [numero, setNumero] = useState("");
  const [complemento, setComplemento] = useState("");
  const [bairro, setBairro] = useState("");
  const [cidade, setCidade] = useState("");
  const [estado, setEstado] = useState("");
  const [diasEntrega, setDiasEntrega] = useState("");
  const [condicaoPagamento, setCondicaoPagamento] = useState("");

  const [erros, setErros] = useState<string[]>([]);
  const [sucesso, setSucesso] = useState("");

  if (!aberta) return null;

  async function cadastrarFornecedor(e: FormEvent<HTMLFormElement>) {
    e.preventDefault();

    setErros([]);
    setSucesso("");

    try {
      await apiFetch("/api/Fornecedores", {
        method: "POST",
        body: JSON.stringify({
          razaoSocial,
          nomeFantasia,
          documento,
          inscricaoEstadual,
          email,
          telefone,
          cep,
          logradouro,
          numero,
          complemento,
          bairro,
          cidade,
          estado,
          diasEntrega,
          condicaoPagamento,
        }),
      });

      setSucesso("Fornecedor cadastrado com sucesso!");
      setRazaoSocial("");
      setNomeFantasia("");
      setDocumento("");
      setInscricaoEstadual("");
      setEmail("");
      setTelefone("");
      setCep("");
      setLogradouro("");
      setNumero("");
      setComplemento("");
      setBairro("");
      setCidade("");
      setEstado("");
      setDiasEntrega("");
      setCondicaoPagamento("");
    } catch (error) {
      const mensagem =
        error instanceof Error
          ? error.message
          : "Não foi possível cadastrar o fornecedor.";

      setErros(mensagem.split("\n"));
    }
  }

  return (
    <div className="modal-overlay" onClick={onFechar}>
      <div
        className="modal-cadastro"
        onClick={(e) => e.stopPropagation()}
      >
        <form onSubmit={cadastrarFornecedor}>
          <label htmlFor="razaoSocial">Razão Social:</label>
          <input
            id="razao social"
            type="text"
            value={razaoSocial}
            onChange={(e) => setRazaoSocial(e.target.value)}
          />

          <label htmlFor="nomeFantasia">Nome Fantasia:</label>
          <input
            id="nome fantasia"
            type="text"
            value={nomeFantasia}
            onChange={(e) => setNomeFantasia(e.target.value)}
          />

          <label htmlFor="documento">Documento:</label>
          <input
            id="documento"
            type="text"
            value={documento}
            onChange={(e) => setDocumento(e.target.value)}
          />

          <label htmlFor="inscricaoEstadual">Inscricao Estadual:</label>
          <input
            id="inscricao estadual"
            type="text"
            value={inscricaoEstadual}
            onChange={(e) => setInscricaoEstadual(e.target.value)}
          />

          <label htmlFor="email">Email:</label>
          <input
            id="email"
            type="text"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
          />
          
          <label htmlFor="telefone">Telefone:</label>
          <input
            id="telefone"
            type="text"
            value={telefone}
            onChange={(e) => setTelefone(e.target.value)}
          />

          - Endereço -

          <label htmlFor="cep">Cep:</label>
          <input
            id="cep"
            type="text"
            value={cep}
            onChange={(e) => setCep(e.target.value)}
          />

          <label htmlFor="logradouro">Logradouro:</label>
          <input
            id="logradouro"
            type="text"
            value={logradouro}
            onChange={(e) => setLogradouro(e.target.value)}
          />

          <label htmlFor="numero">Numero:</label>
          <input
            id="numero"
            type="text"
            value={numero}
            onChange={(e) => setNumero(e.target.value)}
          />

          <label htmlFor="complemento">Complemento:</label>
          <input
            id="complemento"
            type="text"
            value={complemento}
            onChange={(e) => setComplemento(e.target.value)}
          />

          <label htmlFor="bairro">Bairro:</label>
          <input
            id="bairro"
            type="text"
            value={bairro}
            onChange={(e) => setBairro(e.target.value)}
          />

          <label htmlFor="cidade">Cidade:</label>
          <input
            id="cidade"
            type="text"
            value={cidade}
            onChange={(e) => setCidade(e.target.value)}
          />

          <label htmlFor="estado">Estado:</label>
          <SelectEstados
            estado={estado}
            setEstado={setEstado}
          />

          - Outros -

          <label htmlFor="dias Entregas">Dias de Entregas:</label>
          <input
            id="dias entregas"
            type="text"
            value={diasEntrega}
            onChange={(e) => setDiasEntrega(e.target.value)}
          />

          <label htmlFor="condicao Pagamento">Condicao de Pagamento:</label>
          <input
            id="condicao pagamento"
            type="text"
            value={condicaoPagamento}
            onChange={(e) => setCondicaoPagamento(e.target.value)}
          />

          <div className="modal-botoes">
            <button
              type="button"
              onClick={onFechar}
              className="modal-cancelar"
            >
              Cancelar
            </button>

            <button type="submit" className="modal-salvar">
              Salvar
            </button>
            </div>
            
            <div>
            {erros.length > 0 && (
              <div className="mensagens-erro" role="alert">
                {erros.map((mensagem, index) => (
                  <p key={`${index}-${mensagem}`}>
                    {mensagem}
                  </p>
                ))}
              </div>
            )}

            {sucesso && (
              <p className="mensagem-sucesso" role="status">
                {sucesso}
              </p>
            )}
            </div>
        </form>
      </div>
    </div>
  );
}