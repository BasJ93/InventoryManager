import { Component, OnInit } from '@angular/core';
import { NgFor, NgIf } from '@angular/common';
import { FormsModule, FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import { MatButton } from '@angular/material/button';
import {
  CreateStorageLocationRequestDto,
  StorageLocationClient,
  StorageLocationTypeDto
} from "../../generated/api.generated.clients";

@Component({
  selector: 'app-create-storage-location',
  standalone: true,
  imports: [
    MatFormFieldModule,
    MatSelectModule,
    NgFor,
    NgIf,
    MatButton,
    FormsModule,
    ReactiveFormsModule,
    MatInputModule
  ],
  templateUrl: './create-storage-location.component.html',
  styleUrl: './create-storage-location.component.scss'
})
export class CreateStorageLocationComponent implements OnInit {
  constructor(private storageLocationClient: StorageLocationClient) {
  }

  // property with the available location types
  availableTypes: StorageLocationTypeDto[] = [];

  // required properties for the location
  public formGroup = new FormGroup({
    selectedType: new FormControl(-1),
    name: new FormControl(''),
    sizeWidth: new FormControl(0),
    sizeHeight: new FormControl(0)
  });

  ngOnInit(): void {
    this.storageLocationClient.getStorageLocationTypes()
      .subscribe(x => this.availableTypes = x);
  }

  createLocation(): void {
    if(this.formGroup.valid) {
      let request: CreateStorageLocationRequestDto = new CreateStorageLocationRequestDto();
      if (this.formGroup.controls["selectedType"].value != null) {
        request.type = Number(this.formGroup.controls["selectedType"].value);
      }
      if (this.formGroup.controls["name"].value != null) {
        request.name = this.formGroup.controls["name"].value;
      }
      if (this.formGroup.controls["sizeWidth"].value != null) {
        request.sizeX = Number(this.formGroup.controls["sizeWidth"].value);
      }
      if (this.formGroup.controls["sizeHeight"].value != null) {
        request.sizeY = Number(this.formGroup.controls["sizeHeight"].value);
      }
      console.log(request);
      this.storageLocationClient.createStorageLocation(request)
        .subscribe(x => {
          console.log(x);
          this.formGroup.reset({
            selectedType: -1,
            name: '',
            sizeWidth: 0,
            sizeHeight: 0
          });
        });
    }
  }
}
