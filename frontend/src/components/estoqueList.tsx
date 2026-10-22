import "../styles/estoqueMovimentList.css"

export default function estoqueList() {
const estoqueLista = [
    { id: 1, grupo: 'Carne', nome: 'Carne de Sol', quantidade: 1},
    { id: 2, grupo: 'Bebida', nome: 'Cerveja', quantidade: 1},
    { id: 3, grupo: 'Sobremesa', nome: 'Pudim', quantidade: 1},
    { id: 4, grupo: 'Carne', nome: 'Carne de Frango', quantidade: 1},
    { id: 5, grupo: 'Bebida', nome: 'Refrigerante', quantidade: 0},
    { id: 6, grupo: 'Sobremesa', nome: 'Sorvete', quantidade: 1},
];
return (
        <div>
            <div className="estoque-moviment-header">
                <div className="estoque-moviment-grupo">Grupo</div>
                <div className="estoque-moviment-nome">Nome</div>
                <div className="estoque-moviment-quantidade">Quantidade</div>
            </div>
            {estoqueLista.map((estoque: { id: number, grupo: string; nome: string; quantidade: number; }) => (
                <div key={estoque.id} id={`estoque-moviment-${estoque.grupo.toLowerCase()}`} className={`estoque-moviment-list ${`estoque-${estoque.grupo.toLowerCase()}`} ${`estoque-moviment-${estoque.nome.toLowerCase()}`}`}>
                    <div className="estoque-moviment-grupo">
                        {estoque.grupo}
                    </div>
                    <div className="estoque-moviment-nome">
                        {estoque.nome}
                    </div>
                    {estoque.quantidade >= 1 ? 
                    <div className={`estoque-moviment-quantidade`}>
                        {estoque.quantidade}
                    </div> : 
                    <div className={`estoque-moviment-quantidade estoque-moviment-negativo`}>
                        {estoque.quantidade}
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