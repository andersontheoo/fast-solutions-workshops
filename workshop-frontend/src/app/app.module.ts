import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';

import { AppComponent } from './app.component';
import { WorkshopListComponent } from './components/workshop-list/workshop-list.component';
import { WorkshopDetailComponent } from './components/workshop-detail/workshop-detail.component';
import { ColaboradorListComponent } from './components/colaborador-list/colaborador-list.component';

@NgModule({
  declarations: [
    AppComponent,
    WorkshopListComponent,
    WorkshopDetailComponent,
    ColaboradorListComponent
  ],
  imports: [
    BrowserModule,
    CommonModule,
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
