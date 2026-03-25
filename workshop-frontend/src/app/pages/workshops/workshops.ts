import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common'; // 👈 IMPORTANTE: Adicionar isso
import { WorkshopService } from '../../services/workshop.service';
import { WorkshopResponseDTO } from '../../models/workshop.model';

@Component({
  selector: 'app-workshops',
  templateUrl: './workshops.html',
  styleUrls: ['./workshops.css'],
  standalone: false // Mantenha isso se estiver usando NgModule
})
export class WorkshopsComponent implements OnInit {
  workshops: WorkshopResponseDTO[] = [];
  loading = false;
  error = '';

  constructor(private workshopService: WorkshopService) { }

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
}
