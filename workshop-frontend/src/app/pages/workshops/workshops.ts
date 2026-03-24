import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../services/api';
import { CommonModule } from '@angular/common';

@Component({
  standalone: true,
  selector: 'app-workshops',
  imports: [CommonModule],
  templateUrl: './workshops.html'
})
export class WorkshopsComponent implements OnInit {

  workshops: any[] = [];

  constructor(private api: ApiService) {}

  ngOnInit() {
  this.api.getWorkshops().subscribe((data: any) => {
    console.log('WORKSHOPS:', data);
    console.log(data);
    this.workshops = data;
    });
  }
}


