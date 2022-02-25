import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { StatisticsModel } from './stats.model';
import { DefaultStatisticsModel } from './default.model';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  url = environment.statsUrl + '/stats/getall';
  data: StatisticsModel;
  constructor(private httpClient: HttpClient) {
    this.data = new DefaultStatisticsModel();
   }

  ngOnInit(): void {
    this.httpClient.get(this.url).subscribe(res => {
      console.log(res);
      console.log(this.data);
      this.data = (res as StatisticsModel);
      console.log(this.data);
    });
  }

}
