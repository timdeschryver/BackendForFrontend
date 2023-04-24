import { Component } from '@angular/core';
import { AsyncPipe, JsonPipe } from '@angular/common';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  standalone: true,
  template: `
    <a href="/bff/login">Login</a>
    <button (click)="user()">User</button>
    <button (click)="requireAuthorization()">RequireAuthorization</button>
    <button (click)="requireAuthorizationWithBff()">
      RequireAuthorizationWithBff
    </button>
    <button (click)="public()">Public</button>
    <button (click)="publicWithBff()">PublicWithBff</button>

    <pre>{{ results | json }}</pre>
  `,
  imports: [AsyncPipe, JsonPipe],
})
export class AppComponent {
  results: any[] = [];
  constructor(private http: HttpClient) {}

  user() {
    this.http.get('/bff/user').subscribe((res) => {
      this.results.push(res);
    });
  }

  requireAuthorization() {
    this.http.get('/api/private-endpoint').subscribe((res) => {
      this.results.push(res);
    });
  }

  requireAuthorizationWithBff() {
    this.http.get('/api/private-bff-endpoint').subscribe((res) => {
      this.results.push(res);
    });
  }

  public() {
    this.http.get('/api/public-endpoint').subscribe((res) => {
      this.results.push(res);
    });
  }

  publicWithBff() {
    this.http.get('/api/public-bff-endpoint').subscribe((res) => {
      this.results.push(res);
    });
  }
}
