import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../services/api';

@Component({
  selector: 'app-colaboradores',
  templateUrl: './colaboradores.html'
})
export class ColaboradoresComponent implements OnInit {

  colaboradores: any[] = [];

  constructor(private api: ApiService) {}

  ngOnInit() {
    this.api.getColaboradores().subscribe((data: any) => {
      this.colaboradores = data;
    });
  }
}
