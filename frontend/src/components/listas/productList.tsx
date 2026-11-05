export default function UserCard() { 
const produtos = [
    { id: 1, name: 'Paulocesar Galdino' },
    { id: 2, name: 'Sr. Medieval' },
    { id: 3, name: 'Sra. Medieval' },
];


 return (
    <>
    {produtos.map((produto: { id: number; name: string }) => (
        <div key={produto.id} className="user-card">
                <p className="user-name">
                    {produto.name}
                </p>
        </div>
    ))}
    </>
  )
};