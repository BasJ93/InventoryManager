import { Routes } from '@angular/router';
import {
  ContentsOverviewComponent
} from "./pages/contents-overview/contents-overview.component";
import {
  StoragecasesOverviewComponent
} from "./pages/storagecases-overview/storagecases-overview.component";
import {
  ContainersOverviewComponent
} from "./pages/containers-overview/containers-overview.component";
import {
  StoragecaseContentBuilderComponent
} from "./pages/storagecase-content-builder/storagecase-content-builder.component";
import { CreateContainerComponent } from "./pages/create-container/create-container.component";
import { CreateContentComponent } from "./pages/create-content/create-content.component";

export const routes: Routes = [
  {path: 'contents', component: ContentsOverviewComponent},
  {path: 'contents/new', component: CreateContentComponent},
  {path: 'containers', component: ContainersOverviewComponent},
  {path: 'containers/new', component: CreateContainerComponent},
  {path: 'cases', component: StoragecasesOverviewComponent},
  {path: 'cases/:id', component: StoragecaseContentBuilderComponent}
];
