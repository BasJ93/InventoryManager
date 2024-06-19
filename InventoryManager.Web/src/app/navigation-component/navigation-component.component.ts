import { Component, inject } from '@angular/core';
import { RouterOutlet, RouterLink } from '@angular/router';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { AsyncPipe } from '@angular/common';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatListModule } from '@angular/material/list';
import { MatIconModule } from '@angular/material/icon';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatMenuModule } from '@angular/material/menu'
import { Observable } from 'rxjs';
import { map, shareReplay } from 'rxjs/operators';

import {MatTreeNestedDataSource, MatTreeFlattener, MatTreeModule} from '@angular/material/tree';
import {NestedTreeControl} from '@angular/cdk/tree';

import { QuickSettingsComponent } from "../quick-settings/quick-settings.component";

interface NavEntry {
  name: string;
  routerArgs?: string[];
  children?: NavEntry[];
}

const TREE_DATA: NavEntry[] = [
  {
    name: 'Locations',
    children: [
      {
        name: "All",
        routerArgs: ['/locations']
      },
      {
        name: "New",
        routerArgs: ['/locations', 'new']
      }
    ]
  },
  {
    name: 'Containers',
    children: [
      {
        name: "All",
        routerArgs: ['/containers']
      },
      {
        name: "New",
        routerArgs: ['/containers', 'new']
      }
    ]
  },
  {
    name: 'Contents',
    children: [
      {
        name: "All",
        routerArgs: ['/contents']
      },
      {
        name: "New",
        routerArgs: ['/contents', 'new']
      }
    ]
  },
  {
    name: 'Configuration',
    children: [
      {
        name: "Labels",
        children: [
          {
            name: "Label definitions",
            routerArgs: ['/config', 'labels']
          }
        ]
      },
      {
        name: "Label printer settings",
        routerArgs: ['/config', 'printers', 'label']
      }
    ]
  }
]

@Component({
  selector: 'app-navigation-component',
  templateUrl: './navigation-component.component.html',
  styleUrl: './navigation-component.component.scss',
  standalone: true,
  imports: [
    MatToolbarModule,
    MatButtonModule,
    MatSidenavModule,
    MatListModule,
    MatIconModule,
    AsyncPipe,
    RouterOutlet,
    RouterLink,
    MatExpansionModule,
    MatMenuModule,
    MatTreeModule,
    QuickSettingsComponent
  ]
})
export class NavigationComponentComponent {
  private breakpointObserver = inject(BreakpointObserver);

  isHandset$: Observable<boolean> = this.breakpointObserver.observe(Breakpoints.Handset)
    .pipe(
      map(result => result.matches),
      shareReplay()
    );

  constructor() {
    this.dataSource.data = TREE_DATA;
  }

  treeControl = new NestedTreeControl<NavEntry>(node => node.children);
  dataSource = new MatTreeNestedDataSource<NavEntry>();

  hasChild = (_: number, node: NavEntry) => {
    console.log(node);
    return !!node.children && node.children.length > 0;
  }}
