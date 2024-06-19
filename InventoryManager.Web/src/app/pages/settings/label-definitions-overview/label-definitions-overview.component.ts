import { Component, OnInit, ViewChild } from '@angular/core';
import { NgFor, TitleCasePipe } from '@angular/common'
import { Router } from '@angular/router'
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule, MatTableDataSource } from "@angular/material/table"
import { MatSortModule, MatSort } from '@angular/material/sort'
import {
  ContentTypeDto,
  LabelDefinitionClient, LabelDefinitionDto, LabelTypeDto
} from "../../../generated/api.generated.clients";

@Component({
  selector: 'app-label-definitions-overview',
  standalone: true,
  imports: [
    MatIconModule,
    MatTableModule,
    MatSortModule,
    MatSort,
    TitleCasePipe,
    NgFor
  ],
  templateUrl: './label-definitions-overview.component.html',
  styleUrl: './label-definitions-overview.component.scss'
})
export class LabelDefinitionsOverviewComponent implements OnInit {

  labelTypes:  LabelTypeDto[] = [];

  labelDefinitions: MatTableDataSource<LabelDefinitionDto> = new MatTableDataSource<LabelDefinitionDto>();

  automaticTableColumns: string[] = ['name'];

  tableColumns: string[] = ['name', 'type', 'actions'];

  @ViewChild('labelDefinitionTblSort') contentTblSort = new MatSort();

  constructor(private labelDefinitionClient: LabelDefinitionClient, private router: Router) {
  }

  ngOnInit(): void {
    this.labelDefinitionClient.getLabelTypes()
      .subscribe(x => this.labelTypes = x);

    this.labelDefinitionClient.getLabelDefinitions()
      .subscribe(x => {
        this.labelDefinitions.data = x;
        this.labelDefinitions.sort = this.contentTblSort;
      });
  }

  editDefinition(id: string): void {
    this.router.navigate(['/config', 'labels', id]);
  }
}
