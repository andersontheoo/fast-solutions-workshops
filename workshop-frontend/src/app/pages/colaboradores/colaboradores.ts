import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ColaboradorService } from '../../services/colaborador.service';
import { Colaborador } from '../../models/colaborador.model';

@Component({
  selector: 'app-colaboradores',
  templateUrl: './colaboradores.html',
  styleUrls: ['./colaboradores.css'],
  standalone: false
})
export class ColaboradoresComponent implements OnInit {
  colaboradores: Colaborador[] = [];
  loading = false;
  error = '';

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
}
