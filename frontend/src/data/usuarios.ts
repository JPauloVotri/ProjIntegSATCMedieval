export interface Usuario {
  id: number;
  nome: string;
  email: string;
}

export const usuarios: Usuario[] = [
  {
    id: 1,
    nome: "Paulocesar Galdino",
    email: "Paulocesar@gmail.com",
  },
  {
    id: 2,
    nome: "Sr. Medieval",
    email: "SrMedieval@gmail.com",
  },
  {
    id: 3,
    nome: "Sra. Medieval",
    email: "SraMedieval@gmail.com"
  },
];