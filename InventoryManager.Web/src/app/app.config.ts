import { ApplicationConfig } from '@angular/core';
import { provideRouter, withComponentInputBinding } from '@angular/router';
import { provideHttpClient } from '@angular/common/http'

import { routes } from './app.routes';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import {
  API_BASE_URL,
  ContainerClient,
  StorageLocationClient,
  ContentClient,
  StandardClient
} from "./generated/api.generated.clients";
import { environment } from "../environments/environment";

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes, withComponentInputBinding()),
    provideAnimationsAsync('noop'),
    provideHttpClient(),
    { provide: API_BASE_URL, useValue: environment.basePath },
    { provide: ContainerClient },
    { provide: StorageLocationClient },
    { provide: ContentClient },
    { provide: StandardClient}
  ]
};
