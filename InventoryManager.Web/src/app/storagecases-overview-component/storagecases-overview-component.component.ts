import { Component, OnInit } from '@angular/core';
import { NgFor } from '@angular/common';
import { RouterLink } from '@angular/router'
import { MatListModule } from '@angular/material/list';
import { MatIconModule } from '@angular/material/icon';
import { saveAs } from 'file-saver';
import { GetStorageCasesResponseDto, StorageCaseClient } from "../generated/api.generated.clients";

@Component({
  selector: 'app-storagecases-overview-component',
  standalone: true,
  imports: [
    NgFor,
    RouterLink,
    MatListModule,
    MatIconModule
  ],
  templateUrl: './storagecases-overview-component.component.html',
  styleUrl: './storagecases-overview-component.component.scss'
})
export class StoragecasesOverviewComponentComponent implements OnInit {
  constructor(private storageCaseClient: StorageCaseClient) {
  }

  storageCases: GetStorageCasesResponseDto[] = [];

  ngOnInit(): void {
    this.storageCaseClient.getCases()
      .subscribe(x => this.storageCases = x);
  }

  generateLid(caseId: string): void {
    this.storageCaseClient.getLidInsertForCase(caseId)
      .subscribe(x => {
        saveAs(x.data, x.fileName)
      });
  }

  generateLabels(caseId: string): void {
    this.storageCaseClient.getLabelsForCase(caseId)
      .subscribe(x => {
        saveAs(x.data, x.fileName)
      });
  }
}
