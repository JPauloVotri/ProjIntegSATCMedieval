import "../styles/estoqueMovimentList.css"

export default function movimentacaoList() {
const movimentacaoLista = [
    { id: 1, grupo: 'Carne', nome: 'Carne de Sol', movimentacao: 5},
    { id: 2, grupo: 'Bebida', nome: 'Cerveja', movimentacao: -7},
    { id: 3, grupo: 'Sobremesa', nome: 'Pudim', movimentacao: 9},
    { id: 4, grupo: 'Carne', nome: 'Carne de Frango', movimentacao: 2},
    { id: 5, grupo: 'Bebida', nome: 'Refrigerante', movimentacao: 0},
    { id: 6, grupo: 'Sobremesa', nome: 'Sorvete', movimentacao: 5},
];
return (
        <div>
            <div className="estoque-moviment-header">
                <div className="estoque-moviment-grupo">Grupo</div>
                <div className="estoque-moviment-nome">Nome</div>
                <div className="estoque-moviment-quantidade">Movimentacao</div>
            </div>
            {movimentacaoLista.map((movimentacao: { id: number, grupo: string; nome: string; movimentacao: number; }) => (
                <div key={movimentacao.id} id={`estoque-moviment-${movimentacao.grupo.toLowerCase()}`} className={`estoque-moviment-list ${`estoque-${movimentacao.grupo.toLowerCase()}`} ${`estoque-moviment-${movimentacao.nome.toLowerCase()}`}`}>
                    <div className="estoque-moviment-grupo">
                        {movimentacao.grupo}
                    </div>
                    <div className="estoque-moviment-nome">
                        {movimentacao.nome}
                    </div>
                    {movimentacao.movimentacao >= 1 ? 
                    <div className={`estoque-moviment-quantidade`}>
                        +{movimentacao.movimentacao}
                    </div> : 
                    <div className={`estoque-moviment-quantidade estoque-moviment-negativo`}>
                        {movimentacao.movimentacao}
                    </div>
                    }
                </div>
            ))}
            <div className="estoque-moviment-list estoque-moviment-list-empty">
                Nenhum item encontrado
            </div>
            <div className="estoque-moviment-linhaFinal"></div>
        </div>
    )
}