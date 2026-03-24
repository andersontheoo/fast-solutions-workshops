import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { WorkshopsComponent } from './pages/workshops/workshops';
import { ColaboradoresComponent } from './pages/colaboradores/colaboradores';

const routes: Routes = [
  { path: 'workshops', component: WorkshopsComponent },
  { path: 'colaboradores', component: ColaboradoresComponent },
  { path: '', redirectTo: 'workshops', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
