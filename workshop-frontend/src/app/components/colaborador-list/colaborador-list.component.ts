import { Component, OnInit } from '@angular/core';
import { ColaboradorService } from '../../services/colaborador.service';
import { Colaborador } from '../../models/colaborador.model';

@Component({
  selector: 'app-colaborador-list',
  templateUrl: './colaborador-list.component.html',
  styleUrls: ['./colaborador-list.component.css'],
  standalone: false
})
export class ColaboradorListComponent implements OnInit {
  colaboradores: Colaborador[] = [];
  loading = false;
  error = '';
  novoNome = '';

  constructor(private colaboradorService: ColaboradorService) { }

  ngOnInit(): void {
    this.loadColaboradores();
  }

  loadColaboradores(): void {
    this.loading = true;
    this.colaboradorService.getColaboradores().subscribe({
      next: (data) => {
        this.colaboradores = data;
        this.loading = false;
      },
      error: (err) => {
        this.error = 'Erro ao carregar colaboradores: ' + err.message;
        this.loading = false;
        console.error(err);
      }
    });
  }

  createColaborador(): void {
    if (!this.novoNome.trim()) {
      alert('Digite um nome para o colaborador');
      return;
    }

    this.colaboradorService.createColaborador({ id: 0, nome: this.novoNome }).subscribe({
      next: () => {
        this.novoNome = '';
        this.loadColaboradores();
      },
      error: (err) => {
        this.error = 'Erro ao criar colaborador: ' + err.message;
        console.error(err);
      }
    });
  }

  deleteColaborador(id: number): void {
    if (confirm('Tem certeza que deseja excluir este colaborador?')) {
      this.colaboradorService.deleteColaborador(id).subscribe({
        next: () => {
          this.loadColaboradores();
        },
        error: (err) => {
          this.error = 'Erro ao excluir colaborador: ' + err.message;
          console.error(err);
        }
      });
    }
  }
}
