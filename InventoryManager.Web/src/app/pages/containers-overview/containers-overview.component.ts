import { Component, OnInit, ViewChild } from '@angular/core';
import { NgFor, NgIf, TitleCasePipe } from '@angular/common'
import { ContainerClient, ContainerOverviewResponseDto } from "../../generated/api.generated.clients";
import { MatTableModule, MatTableDataSource } from "@angular/material/table"
import { MatSortModule, MatSort } from '@angular/material/sort'
import { MatMenuModule } from '@angular/material/menu'
import { MatIconModule } from '@angular/material/icon';
import { MatToolbarModule } from '@angular/material/toolbar';
import { saveAs } from 'file-saver';

@Component({
  selector: 'app-containers-overview-component',
  standalone: true,
  imports: [
    MatToolbarModule,
    MatIconModule,
    MatMenuModule,
    MatTableModule,
    MatSortModule,
    MatSort,
    NgFor,
    NgIf,
    TitleCasePipe
  ],
  templateUrl: './containers-overview.component.html',
  styleUrl: './containers-overview.component.scss'
})
export class ContainersOverviewComponent implements OnInit {

  containers: MatTableDataSource<ContainerOverviewResponseDto> = new MatTableDataSource<ContainerOverviewResponseDto>();

  automaticTableColumns: string[] = ['size'] //'id',

  tableColumns: string[] = ['size', 'content', 'location'] //'id',

  // , 'content', 'location'

  @ViewChild('containerTblSort') contentTblSort = new MatSort();

  constructor(private containerClient: ContainerClient) {
  }

  ngOnInit(): void {
    this.containerClient.getContainers()
      .subscribe(x => {
        this.containers.data = x;
        this.containers.sort = this.contentTblSort;
      });
  }

  generateAllLabels(): void {
    this.containerClient.getLabelsForContainers()
      .subscribe(x => {
        saveAs(x.data, x.fileName)
      });
  }
}
