import { bootstrapApplication } from '@angular/platform-browser';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { AppComponent } from './app/app.component';
import { includeAntiforgeryToken } from './app/interceptors/include-antiforgery-token';

bootstrapApplication(AppComponent, {
  providers: [provideHttpClient(withInterceptors([includeAntiforgeryToken]))],
});
