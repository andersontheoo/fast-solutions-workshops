import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { WorkshopService } from '../../services/workshop.service';
import { ColaboradorService } from '../../services/colaborador.service';
import { WorkshopResponseDTO } from '../../models/workshop.model';
import { Colaborador } from '../../models/colaborador.model';

@Component({
  selector: 'app-workshop-detail',
  templateUrl: './workshop-detail.component.html',
  styleUrls: ['./workshop-detail.component.css'],
  standalone: false
})
export class WorkshopDetailComponent implements OnInit {
  workshop: WorkshopResponseDTO | null = null;
  colaboradoresDisponiveis: Colaborador[] = [];
  loading = false;
  error = '';
  isEditing = false;
  editNome = '';
  editDescricao = '';
  editDataRealizacao = '';
  editColaboradoresIds: number[] = [];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private workshopService: WorkshopService,
    private colaboradorService: ColaboradorService
  ) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id && id !== 'novo') {
      this.loadWorkshop(+id);
    } else {
      this.isEditing = true;
      this.initNewWorkshop();
    }
    this.loadColaboradores();
  }

  loadWorkshop(id: number): void {
    this.loading = true;
    this.workshopService.getWorkshopById(id).subscribe({
      next: (data) => {
        this.workshop = data;
        this.loading = false;
      },
      error: (err) => {
        this.error = 'Erro ao carregar workshop: ' + err.message;
        this.loading = false;
        console.error(err);
      }
    });
  }

  loadColaboradores(): void {
    this.colaboradorService.getColaboradores().subscribe({
      next: (data) => {
        this.colaboradoresDisponiveis = data;
      },
      error: (err) => {
        console.error('Erro ao carregar colaboradores:', err);
      }
    });
  }

  initNewWorkshop(): void {
    this.workshop = {
      id: 0,
      nome: '',
      descricao: '',
      dataRealizacao: '',
      colaboradores: []
    };
    this.editNome = '';
    this.editDescricao = '';
    this.editDataRealizacao = '';
    this.editColaboradoresIds = [];
  }

  startEdit(): void {
    if (this.workshop) {
      this.isEditing = true;
      this.editNome = this.workshop.nome;
      this.editDescricao = this.workshop.descricao;
      this.editDataRealizacao = this.workshop.dataRealizacao;
      this.editColaboradoresIds = this.workshop.colaboradores?.map(c => c.id) || [];
    }
  }

  saveWorkshop(): void {
    const workshopData = {
      nome: this.editNome,
      descricao: this.editDescricao,
      dataRealizacao: this.editDataRealizacao,
      colaboradoresIds: this.editColaboradoresIds
    };

    if (this.workshop && this.workshop.id === 0) {
      // Criar novo
      this.workshopService.createWorkshop(workshopData).subscribe({
        next: () => {
          this.router.navigate(['/workshops']);
        },
        error: (err) => {
          this.error = 'Erro ao criar workshop: ' + err.message;
          console.error(err);
        }
      });
    } else if (this.workshop) {
      // Atualizar existente
      this.workshopService.updateWorkshop(this.workshop.id, workshopData).subscribe({
        next: () => {
          this.isEditing = false;
          this.loadWorkshop(this.workshop!.id);
        },
        error: (err) => {
          this.error = 'Erro ao atualizar workshop: ' + err.message;
          console.error(err);
        }
      });
    }
  }

  cancelEdit(): void {
    this.isEditing = false;
    if (this.workshop && this.workshop.id === 0) {
      this.router.navigate(['/workshops']);
    }
  }

  goBack(): void {
    this.router.navigate(['/workshops']);
  }
}
