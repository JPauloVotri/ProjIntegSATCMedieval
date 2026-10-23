import "../styles/cotacaoList.css";
import "../styles/modalObs.css"

import { FaSearch } from "react-icons/fa";
import { IoClose } from "react-icons/io5";
import { useState } from "react"

type Cotacao = {
    id: number;
    grupo: string;
    nome: string;
    preco: number;
    marca: string;
    observacao: string;
};

export default function cotacaoList() {
        const [cotacaoSelecionada, setCotacaoSelecionada] = useState <Cotacao | null>(null);

const cotacaoLista = [
    { id: 1, grupo: 'Carne', nome: 'Carne de Sol', preco: 20.00, marca: 'Friboi', observacao: 'Carne de sol de primeira qualidade' },
    { id: 2, grupo: 'Bebida', nome: 'Cerveja', preco: 5.00, marca: 'Skol', observacao: 'Cerveja de qualidade' },
    { id: 3, grupo: 'Sobremesa', nome: 'Pudim', preco: 10.00, marca: 'Tio João', observacao: 'Pudim cremoso e saboroso' },
    { id: 4, grupo: 'Carne', nome: 'Carne de Frango', preco: 15.00, marca: 'Seara', observacao: 'Carne de frango de primeira qualidade' },
    { id: 5, grupo: 'Bebida', nome: 'Refrigerante', preco: 3.00, marca: 'Coca-Cola', observacao: 'Refrigerante gelado e refrescante' },
    { id: 6, grupo: 'Sobremesa', nome: 'Sorvete', preco: 7.00, marca: 'Nestlé', observacao: 'Sorvete de vários sabores' },
];
 
    return (
        <div>
            <div className="cotacao-header">
                <div className="cotacao-grupo">Grupo</div>
                <div className="cotacao-nome">Nome</div>
                <div className="cotacao-marca">Marca</div>
                <div className="cotacao-preco">Preço</div>
                <div className="cotacao-observacao"></div>
            </div>
            {cotacaoLista.map((cotacao: { id: number; grupo: string; nome: string; preco: number; marca: string; observacao: string }) => (
                <div key={cotacao.id} id={`cotacao-${cotacao.grupo.toLowerCase()}`} className={`cotacao-list ${`cotacao-${cotacao.grupo.toLowerCase()}`} ${`cotacao-${cotacao.nome.toLowerCase()}`}`}>
                    <div className="cotacao-grupo">
                        {cotacao.grupo}
                    </div>
                    <div className="cotacao-nome">
                        {cotacao.nome}
                    </div>
                    <div className="cotacao-marca">
                        {cotacao.marca}
                    </div>
                    <div className="cotacao-preco">
                        R$ {cotacao.preco.toFixed(2)}
                    </div>
                    <div className="cotacao-observacao cotacao-botao">
                        <FaSearch onClick={() => setCotacaoSelecionada(cotacao)} />
                    </div>
                </div>
            ))}
            <div className="cotacao-list cotacao-list-empty">
                Nenhum item encontrado
            </div>
            <div className="cotacao-linhaFinal"></div>
            {cotacaoSelecionada && (
                <div className="modal-overlay" onClick={() => setCotacaoSelecionada(null)}>
                    <div className="modal-caixa" onClick={(e) => e.stopPropagation()}>
                        <div className="modal-header">
                            <div className="modal-close">
                                <IoClose onClick={() => setCotacaoSelecionada(null)}/>
                            </div>
                            <h2 id="modal-titulo">
                                Observações do produto
                            </h2>
                        </div>
                        <div className="modal-conteudo">
                            <div className="modal-observacao">
                                {cotacaoSelecionada.observacao ||
                                    "Nenhuma observação cadastrada."}
                            </div>
                        </div>
                    </div>
                </div>
            )}
        </div>
    )
}