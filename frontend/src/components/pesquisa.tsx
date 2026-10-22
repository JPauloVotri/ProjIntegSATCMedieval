export default function pesquisa() {
const cotacaoLista = [
    { grupo: 'Carne', nome: 'Carne de Sol'},
    { grupo: 'Bebida', nome: 'Cerveja'},
    { grupo: 'Sobremesa', nome: 'Pudim'},
    { grupo: 'Carne', nome: 'Carne de Frango'},
    { grupo: 'Bebida', nome: 'Refrigerante'},
    { grupo: 'Sobremesa', nome: 'Sorvete'},
];
    
function handleInputChange() {
    const input = (document.getElementById("grupo") as HTMLInputElement).value.toLowerCase();
    const inputName = (document.getElementById("nome") as HTMLInputElement).value.toLowerCase();

    const resultados = cotacaoLista.filter((cotacao) => {return (cotacao.grupo.toLowerCase().startsWith(input) && cotacao.nome.toLowerCase().startsWith(inputName));});

    document.querySelectorAll(".cotacao-list").forEach((item) => {(item as HTMLElement).style.display = "none";});
    resultados.forEach((cotacao) => {
        const elementos = document.getElementsByClassName(`cotacao-${cotacao.nome.toLowerCase()}`);
        for (let i = 0; i < elementos.length; i++) {
            (elementos[i] as HTMLElement).style.display = "flex";
        }
    });
    const mensagem = document.querySelector(".cotacao-list-empty") as HTMLElement;
   
    mensagem.style.display = resultados.length === 0 ? "block" : "none";
}
    return (
        <>
            grupo: <input type="text" name="grupo" id="grupo" onInput={() => handleInputChange()} />
            nome: <input type="text" name="nome" id="nome" onInput={() => handleInputChange()} />
        </>
    )
}