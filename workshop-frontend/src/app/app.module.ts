import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { WorkshopListComponent } from './components/workshop-list/workshop-list.component';
import { WorkshopDetailComponent } from './components/workshop-detail/workshop-detail.component';
import { ColaboradorListComponent } from './components/colaborador-list/colaborador-list.component';

// NÃO importe os componentes da pasta pages
// import { WorkshopsComponent } from './pages/workshops/workshops';
// import { ColaboradoresComponent } from './pages/colaboradores/colaboradores';

@NgModule({
  declarations: [
    AppComponent,
    WorkshopListComponent,      // Usando o da pasta components
    WorkshopDetailComponent,     // Usando o da pasta components
    ColaboradorListComponent     // Usando o da pasta components
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', redirectTo: '/workshops', pathMatch: 'full' },
      { path: 'workshops', component: WorkshopListComponent },
      { path: 'workshop/:id', component: WorkshopDetailComponent },
      { path: 'workshop/novo', component: WorkshopDetailComponent },
      { path: 'colaboradores', component: ColaboradorListComponent }
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
