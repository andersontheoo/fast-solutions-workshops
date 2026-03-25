import { Colaborador } from './colaborador.model';

export interface Workshop {
  id: number;
  nome: string;
  dataRealizacao: string;
  descricao: string;
  colaboradores?: Colaborador[];
}

export interface WorkshopDTO {
  nome: string;
  dataRealizacao: string;
  descricao: string;
  colaboradoresIds: number[];
}

export interface WorkshopResponseDTO {
  id: number;
  nome: string;
  descricao: string;
  dataRealizacao: string;
  colaboradores: Colaborador[];
}
