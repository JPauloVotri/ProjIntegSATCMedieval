
import { IoClose } from "react-icons/io5";

import "../../styles/modalObs.css";
import "../../styles/modalCriacaoMensagem.css";

import { apiFetch } from "../../../services/api";
import { useState, type FormEvent } from "react";
import SelectEstados from "../selects/selectEstados";

interface ModalCadastroProps {
  aberta: boolean;
  onFechar: () => void;
}

export default function ModalCadastro({
  aberta,
  onFechar,
}: ModalCadastroProps) {
  const [etapa, setEtapa] = useState(1);

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
  const [carregando, setCarregando] = useState(false);

  function fecharModal() {
    setEtapa(1);
    setErros([]);
    setSucesso("");
    onFechar();
  }

  function avancarEtapa() {
    setErros([]);

    if (etapa < 3) {
      setEtapa(etapa + 1);
    }
  }

  function voltarEtapa() {
    setErros([]);

    if (etapa > 1) {
      setEtapa(etapa - 1);
    }
  }

  async function cadastrarFornecedor(e: FormEvent<HTMLFormElement>) {
    e.preventDefault();

    if (etapa !== 3) {
      avancarEtapa();
      return;
    }

    setErros([]);
    setSucesso("");
    setCarregando(true);

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
      setEtapa(1);

    } catch (error) {
      const mensagem =
        error instanceof Error
          ? error.message
          : "Não foi possível cadastrar o fornecedor.";

      setErros(mensagem.split("\n"));

    } finally {
      setCarregando(false);
    }
  }

  if (!aberta) return null;

  return (
    <div className="modal-overlay" onClick={fecharModal}>
      <div
        className="modal-caixa modal-usuario modal-fornecedor"
        onClick={(e) => e.stopPropagation()}
      >
        <div className="modal-usuario-cabecalho">
          <div className="modal-usuario-titulo">
            <div className="modal-titulos">
              <h2>Adicionar Fornecedor</h2>
            </div>
          </div>

          <button
            type="button"
            className="modal-close"
            onClick={fecharModal}
          >
            <IoClose />
          </button>
        </div>

        {/* Progresso das etapas */}
        <div className="fornecedor-progresso">
          <div className="fornecedor-progresso-info">
            <span>Etapa {etapa} de 3</span>
            <span>
              {etapa === 1
                ? "Dados do fornecedor"
                : etapa === 2
                  ? "Endereço"
                  : "Outras informações"}
            </span>
          </div>

          <div className="fornecedor-progresso-barra">
            <div
              className="fornecedor-progresso-preenchimento"
              style={{ width: `${(etapa / 3) * 100}%` }}
            />
          </div>

          <div className="fornecedor-etapas">
            <span className={etapa >= 1 ? "ativa" : ""}>1. Dados</span>
            <span className={etapa >= 2 ? "ativa" : ""}>2. Endereço</span>
            <span className={etapa >= 3 ? "ativa" : ""}>3. Outros</span>
          </div>
        </div>

        <form
          className="modal-usuario-form"
          onSubmit={cadastrarFornecedor}
        >
          {/* ETAPA 1 */}
          {etapa === 1 && (
            <>
              <h3>Dados do fornecedor</h3>

              <label>Razão Social</label>
              <input
                type="text"
                placeholder="Digite a razão social"
                value={razaoSocial}
                onChange={(e) => setRazaoSocial(e.target.value)}
                required
              />

              <label>Nome Fantasia</label>
              <input
                type="text"
                placeholder="Digite o nome fantasia"
                value={nomeFantasia}
                onChange={(e) => setNomeFantasia(e.target.value)}
              />

              <label>Documento</label>
              <input
                type="text"
                placeholder="Digite o CNPJ ou CPF"
                value={documento}
                onChange={(e) => setDocumento(e.target.value)}
              />

              <label>Inscrição Estadual</label>
              <input
                type="text"
                placeholder="Digite a inscrição estadual"
                value={inscricaoEstadual}
                onChange={(e) => setInscricaoEstadual(e.target.value)}
              />

              <label>E-mail</label>
              <input
                type="email"
                placeholder="Digite o e-mail"
                value={email}
                onChange={(e) => setEmail(e.target.value)}
              />

              <label>Telefone</label>
              <input
                type="text"
                placeholder="Digite o telefone"
                value={telefone}
                onChange={(e) => setTelefone(e.target.value)}
              />
            </>
          )}

          {/* ETAPA 2 */}
          {etapa === 2 && (
            <>
              <h3>Endereço do fornecedor</h3>

              <label>CEP</label>
              <input
                type="text"
                placeholder="Digite o CEP"
                value={cep}
                onChange={(e) => setCep(e.target.value)}
              />

              <label>Logradouro</label>
              <input
                type="text"
                placeholder="Digite o logradouro"
                value={logradouro}
                onChange={(e) => setLogradouro(e.target.value)}
              />

              <label>Número</label>
              <input
                type="text"
                placeholder="Digite o número"
                value={numero}
                onChange={(e) => setNumero(e.target.value)}
              />

              <label>Complemento</label>
              <input
                type="text"
                placeholder="Digite o complemento"
                value={complemento}
                onChange={(e) => setComplemento(e.target.value)}
              />

              <label>Bairro</label>
              <input
                type="text"
                placeholder="Digite o bairro"
                value={bairro}
                onChange={(e) => setBairro(e.target.value)}
              />

              <label>Cidade</label>
              <input
                type="text"
                placeholder="Digite a cidade"
                value={cidade}
                onChange={(e) => setCidade(e.target.value)}
              />

              <label>Estado</label>
              <SelectEstados
                estado={estado}
                setEstado={setEstado}
              />
            </>
          )}

          {/* ETAPA 3 */}
          {etapa === 3 && (
            <>
              <h3>Outras informações</h3>

              <label>Dias de Entrega</label>
              <input
                type="text"
                placeholder="Ex: 3 dias úteis"
                value={diasEntrega}
                onChange={(e) => setDiasEntrega(e.target.value)}
              />

              <label>Condição de Pagamento</label>
              <input
                type="text"
                placeholder="Ex: 30 dias"
                value={condicaoPagamento}
                onChange={(e) => setCondicaoPagamento(e.target.value)}
              />
            </>
          )}

          {/* NAVEGAÇÃO */}
          <div className="fornecedor-botoes-etapas">
            {etapa > 1 && (
              <button
                type="button"
                className="fornecedor-voltar"
                onClick={voltarEtapa}
              >
                Voltar
              </button>
            )}

            {etapa < 3 ? (
              <button
                type="button"
                className="modal-usuario-submit"
                onClick={avancarEtapa}
              >
                Próximo
              </button>
            ) : (
              <button
                type="submit"
                className="modal-usuario-submit"
                disabled={carregando}
              >
                {carregando ? "Cadastrando..." : "Cadastrar"}
              </button>
            )}
          </div>

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
        </form>
      </div>
    </div>
  );
}
