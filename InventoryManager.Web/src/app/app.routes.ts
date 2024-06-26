import { Routes } from '@angular/router';
import {
  ContentsOverviewComponent
} from "./pages/contents-overview/contents-overview.component";
import {
  StorageLocationsOverviewComponent
} from "./pages/storage-locations-overview/storage-locations-overview.component";
import {
  ContainersOverviewComponent
} from "./pages/containers-overview/containers-overview.component";
import {
  StorageLocationContentBuilderComponent
} from "./pages/storage-location-content-builder/storage-location-content-builder.component";
import { CreateContainerComponent } from "./pages/create-container/create-container.component";
import { CreateContentComponent } from "./pages/create-content/create-content.component";
import { CreateStorageLocationComponent } from "./pages/create-storage-location/create-storage-location.component";

export const routes: Routes = [
  {path: 'contents', component: ContentsOverviewComponent},
  {path: 'contents/new', component: CreateContentComponent},
  {path: 'containers', component: ContainersOverviewComponent},
  {path: 'containers/new', component: CreateContainerComponent},
  {path: 'locations', component: StorageLocationsOverviewComponent},
  {path: 'locations/new', component: CreateStorageLocationComponent},
  {path: 'locations/:id', component: StorageLocationContentBuilderComponent}
];
