import { Routes } from '@angular/router';
import { WorkshopsComponent } from './pages/workshops/workshops';
import { ColaboradoresComponent } from './pages/colaboradores/colaboradores';

export const routes: Routes = [
  { path: 'workshops', component: WorkshopsComponent },
  { path: 'colaboradores', component: ColaboradoresComponent },
  { path: '', redirectTo: 'workshops', pathMatch: 'full' }
];
