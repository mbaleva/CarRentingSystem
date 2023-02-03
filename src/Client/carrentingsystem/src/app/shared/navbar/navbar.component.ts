import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { environment } from '../../../environments/environment';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  authToken: string | null;
  isDealer: Boolean | undefined;
  dealersUrl: string;
  constructor(private httpClient: HttpClient) {
      this.authToken = null;
      this.dealersUrl = environment.dealersUrl;
   }
  ngOnInit() {
      this.getAuthToken();
      this.checkIsDealer();
  }
  getAuthToken(){
      this.authToken = localStorage.getItem('authToken');
  }
  checkIsDealer() {
    this.httpClient.get(this.dealersUrl + `/api/dealers/isdealer?userId=${localStorage.getItem('userId')}`)
            .subscribe(res => {
              this.isDealer = res as Boolean;
            });
  }
  logout(): void {
      localStorage.removeItem('authToken');
      this.authToken = null;
  }

}
