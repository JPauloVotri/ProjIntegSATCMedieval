export default function pesquisaEstMov() {

const EstoqueMoviLista = [
    { id: 1, grupo: 'Carne', nome: 'Carne de Sol', quantidade: 1},
    { id: 2, grupo: 'Bebida', nome: 'Cerveja', quantidade: 1},
    { id: 3, grupo: 'Sobremesa', nome: 'Pudim', quantidade: 1},
    { id: 4, grupo: 'Carne', nome: 'Carne de Frango', quantidade: 1},
    { id: 5, grupo: 'Bebida', nome: 'Refrigerante', quantidade: 0},
    { id: 6, grupo: 'Sobremesa', nome: 'Sorvete', quantidade: 1},
];

function handleInputChange() {
    const grupo = (document.getElementById("grupo") as HTMLInputElement).value.toLowerCase();
    const nome = (document.getElementById("nome") as HTMLInputElement).value.toLowerCase();
    const resultados = EstoqueMoviLista.filter((item) => {return (item.grupo.toLowerCase().startsWith(grupo) && item.nome.toLowerCase().startsWith(nome));});

    document.querySelectorAll(".estoque-moviment-list").forEach((item) => {(item as HTMLElement).style.display = "none";});
    resultados.forEach((valor) => {
        const elementos = document.getElementsByClassName(`estoque-moviment-${valor.nome.toLowerCase()}`);
        for (let i = 0; i < elementos.length; i++) {
            (elementos[i] as HTMLElement).style.display = "flex";
        }
    });
    const mensagem = document.querySelector(".estoque-moviment-list-empty") as HTMLElement;
   
    mensagem.style.display = resultados.length === 0 ? "block" : "none";
}

    return (
        <>
            grupo: <input type="text" name="grupo" id="grupo" onInput={() => handleInputChange()} />
            nome: <input type="text" name="nome" id="nome" onInput={() => handleInputChange()} />
        </>
    )
}