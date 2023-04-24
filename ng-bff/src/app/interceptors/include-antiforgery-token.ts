import type { HttpHandlerFn, HttpRequest } from '@angular/common/http';

export function includeAntiforgeryToken(
  request: HttpRequest<unknown>,
  next: HttpHandlerFn
) {
  const clonedRequest = request.clone({
    setHeaders: {
      'x-csrf': '1',
    },
  });
  return next(clonedRequest);
}
