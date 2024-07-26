export interface EscalaDTO {
    escalaId: number;
    dataHoraEntrada: string;
    dataHoraSaida: string;
  }
  
  export interface LocalDTO {
    localId: number;
    nome: string;
    descricao: string;
  }
  
  export interface MarcacaoEscalaDTO {
    marcacaoEscalaId: number;
    policialId: number;
    escalaId: number;
    localId: number;
  }
  
  export interface PolicialDTO {
    policialId: number;
    cpf: string;
    nome: string;
    telefone: string;
  }
  