import { Component, OnInit, ViewChild } from '@angular/core';
import { NgFor, TitleCasePipe } from '@angular/common'
import { ContentClient, ContentResponseDto } from "../../generated/api.generated.clients";
import { MatTableModule, MatTableDataSource } from "@angular/material/table"
import { MatSortModule, MatSort, Sort } from '@angular/material/sort'
import { MatToolbarModule } from '@angular/material/toolbar';

@Component({
  selector: 'app-contents-overview-component',
  standalone: true,
  imports: [
    MatToolbarModule,
    MatTableModule,
    MatSortModule,
    MatSort,
    NgFor,
    TitleCasePipe
  ],
  templateUrl: './contents-overview.component.html',
  styleUrl: './contents-overview.component.scss',
})
export class ContentsOverviewComponent implements OnInit{

  contents: MatTableDataSource<ContentResponseDto> = new MatTableDataSource<ContentResponseDto>();

  tableColumns: string[] = ['definition', 'standard', 'description']

  @ViewChild('contentTblSort') contentTblSort = new MatSort();

  constructor(private contentClient: ContentClient) {
  }

  ngOnInit(): void {
    this.contentClient.getContents(false)
      .subscribe(x => {
        this.contents.data = x;
        this.contents.sort = this.contentTblSort;
      });
  }
}
