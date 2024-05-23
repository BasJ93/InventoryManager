import { Component, OnInit } from '@angular/core';
import { NgFor } from '@angular/common';
import { RouterLink } from '@angular/router'
import { MatListModule } from '@angular/material/list';
import { MatIconModule } from '@angular/material/icon';
import { MatToolbarModule } from '@angular/material/toolbar';
import { saveAs } from 'file-saver';
import { GetStorageLocationsResponseDto, StorageLocationClient } from "../../generated/api.generated.clients";

@Component({
  selector: 'app-storagecases-overview',
  standalone: true,
  imports: [
    NgFor,
    RouterLink,
    MatListModule,
    MatIconModule,
    MatToolbarModule
  ],
  templateUrl: './storage-locations-overview.component.html',
  styleUrl: './storage-locations-overview.component.scss'
})
export class StorageLocationsOverviewComponent implements OnInit {
  constructor(private storageLocationClient: StorageLocationClient) {
  }

  storageCases: GetStorageLocationsResponseDto[] = [];

  ngOnInit(): void {
    this.storageLocationClient.getStorageLocations()
      .subscribe(x => this.storageCases = x);
  }

  generateLid(caseId: string): void {
    this.storageLocationClient.getLidInsertForStorageLocation(caseId)
      .subscribe(x => {
        saveAs(x.data, x.fileName)
      });
  }

  generateLabels(caseId: string): void {
    this.storageLocationClient.getLabelsForStorageLocation(caseId)
      .subscribe(x => {
        saveAs(x.data, x.fileName)
      });
  }
}
