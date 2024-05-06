import { Routes } from '@angular/router';
import {
  ContentsOverviewComponentComponent
} from "./contents-overview-component/contents-overview-component.component";
import {
  StoragecasesOverviewComponentComponent
} from "./storagecases-overview-component/storagecases-overview-component.component";
import {
  ContainersOverviewComponentComponent
} from "./containers-overview-component/containers-overview-component.component";
import {
  StoragecaseContentBuilderComponent
} from "./storagecase-content-builder/storagecase-content-builder.component";
import { CreateContainerComponentComponent } from "./create-container-component/create-container-component.component";
import { CreateContentComponent } from "./create-content/create-content.component";

export const routes: Routes = [
  { path: 'contents', component: ContentsOverviewComponentComponent },
  { path: 'contents/new', component: CreateContentComponent},
  { path: 'containers', component: ContainersOverviewComponentComponent },
  { path: 'containers/new', component: CreateContainerComponentComponent},
  { path: 'cases', component: StoragecasesOverviewComponentComponent },
  { path: 'cases/:id', component: StoragecaseContentBuilderComponent}
];
