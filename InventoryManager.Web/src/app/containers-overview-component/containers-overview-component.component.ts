import { Component, OnInit, ViewChild } from '@angular/core';
import { NgFor, NgIf, TitleCasePipe } from '@angular/common'
import {
  ContainerClient, ContainerOverviewResponseDto,
} from "../generated/api.generated.clients";
import { MatTableModule, MatTableDataSource } from "@angular/material/table"
import { MatSortModule, MatSort, Sort } from '@angular/material/sort'

@Component({
  selector: 'app-containers-overview-component',
  standalone: true,
  imports: [
    MatTableModule,
    MatSortModule,
    MatSort,
    NgFor,
    NgIf,
    TitleCasePipe
  ],
  templateUrl: './containers-overview-component.component.html',
  styleUrl: './containers-overview-component.component.scss'
})
export class ContainersOverviewComponentComponent implements OnInit{

  containers: MatTableDataSource<ContainerOverviewResponseDto> = new MatTableDataSource<ContainerOverviewResponseDto>();

  automaticTableColumns: string[] = ['size'] //'id',

  tableColumns: string[] = ['size', 'content', 'location'] //'id',

  // , 'content', 'location'

  @ViewChild('contairnerTblSort') contentTblSort = new MatSort();

  constructor(private containerClient: ContainerClient) {
  }

  ngOnInit(): void {
    this.containerClient.getContainers()
      .subscribe(x => {
        this.containers.data = x;
        this.containers.sort = this.contentTblSort;
      });
  }
}
