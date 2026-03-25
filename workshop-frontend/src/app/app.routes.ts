import { Routes } from '@angular/router';
import { WorkshopListComponent } from './components/workshop-list/workshop-list.component';
import { WorkshopDetailComponent } from './components/workshop-detail/workshop-detail.component';
import { ColaboradorListComponent } from './components/colaborador-list/colaborador-list.component';

export const routes: Routes = [
  { path: '', redirectTo: '/workshops', pathMatch: 'full' },
  { path: 'workshops', component: WorkshopListComponent },
  { path: 'workshop/:id', component: WorkshopDetailComponent },
  { path: 'workshop/novo', component: WorkshopDetailComponent },
  { path: 'colaboradores', component: ColaboradorListComponent }
];
