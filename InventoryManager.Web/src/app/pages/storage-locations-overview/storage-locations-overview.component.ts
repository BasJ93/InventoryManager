import { Component, OnInit } from '@angular/core';
import { NgFor, TitleCasePipe } from '@angular/common';
import { RouterLink, Router } from '@angular/router'
import { MatListModule } from '@angular/material/list';
import { MatIconModule } from '@angular/material/icon';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatTableModule, MatTableDataSource } from "@angular/material/table"
import { saveAs } from 'file-saver';
import {
  GetStorageLocationsResponseDto,
  StorageLocationClient
} from "../../generated/api.generated.clients";
import { QuickSettingsService } from "../../services/quick-settings/quick-settings.service";

@Component({
  selector: 'app-storagecases-overview',
  standalone: true,
  imports: [
    NgFor,
    RouterLink,
    MatListModule,
    MatIconModule,
    MatToolbarModule,
    MatTableModule,
    TitleCasePipe
  ],
  templateUrl: './storage-locations-overview.component.html',
  styleUrl: './storage-locations-overview.component.scss'
})
export class StorageLocationsOverviewComponent implements OnInit {
  constructor(private storageLocationClient: StorageLocationClient, private router: Router, private quickSettingsService: QuickSettingsService) {
  }

  storageLocations: MatTableDataSource<GetStorageLocationsResponseDto> = new MatTableDataSource<GetStorageLocationsResponseDto>();

  automaticTableColumns: string[] = ['name', 'type']

  tableColumns: string[] = this.automaticTableColumns.concat(['actions']);

  labelPrinterEnabled: boolean = false;

  ngOnInit(): void {
    this.storageLocationClient.getStorageLocations()
      .subscribe(x => this.storageLocations.data = x);

    this.quickSettingsService.getLabelPrinterEnabledAsObservable().subscribe(x => this.labelPrinterEnabled = x);
  }

  generateLid(locationId: string): void {
    this.storageLocationClient.getLidInsertForStorageLocation(locationId)
      .subscribe(x => {
        saveAs(x.data, x.fileName)
      });
  }

  generateLabels(locationId: string): void {
    if (this.labelPrinterEnabled) {
      this.storageLocationClient.printLabelsForStorageLocation(locationId)
        .subscribe(x => console.log(x));
    } else {
      this.storageLocationClient.getLabelsForStorageLocation(locationId)
        .subscribe(x => {
          saveAs(x.data, x.fileName)
        });
    }
  }

  configureLocation(locationId: string): void {
    this.router.navigate(['/locations', locationId]);
  }
}
