import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { WorkshopService } from '../../services/workshop.service';
import { WorkshopResponseDTO } from '../../models/workshop.model';

@Component({
  selector: 'app-workshop-list',
  templateUrl: './workshop-list.component.html',
  styleUrls: ['./workshop-list.component.css'],
})
export class WorkshopListComponent implements OnInit {
  workshops: WorkshopResponseDTO[] = [];
  loading = false;
  error = '';

  constructor(
    private workshopService: WorkshopService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.loadWorkshops();
  }

  loadWorkshops(): void {
    this.loading = true;
    this.workshopService.getWorkshops().subscribe({
      next: (data) => {
        this.workshops = data;
        this.loading = false;
      },
      error: (err) => {
        this.error = 'Erro ao carregar workshops: ' + err.message;
        this.loading = false;
        console.error(err);
      }
    });
  }

  viewWorkshopDetails(id: number): void {
    this.router.navigate(['/workshop', id]);
  }

  deleteWorkshop(id: number, event: Event): void {
    event.stopPropagation();
    if (confirm('Tem certeza que deseja excluir este workshop?')) {
      this.workshopService.deleteWorkshop(id).subscribe({
        next: () => {
          this.loadWorkshops(); // Recarrega a lista
        },
        error: (err) => {
          this.error = 'Erro ao excluir workshop: ' + err.message;
          console.error(err);
        }
      });
    }
  }
}
