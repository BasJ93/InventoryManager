import { Component, Input, OnInit } from '@angular/core';
import { NgFor } from '@angular/common';
import { FormsModule, FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButton } from '@angular/material/button';
import { Subject } from 'rxjs';

import {
  ContentTypeDto,
  LabelDefinitionClient, LabelDefinitionDto,
  LabelPrinterConfigurationDto
} from "../../../generated/api.generated.clients";

@Component({
  selector: 'app-label-settings',
  standalone: true,
  imports: [
    MatFormFieldModule,
    FormsModule,
    ReactiveFormsModule,
    MatInputModule,
    MatSelectModule,
    MatButton,
    NgFor
  ],
  templateUrl: './label-settings.component.html',
  styleUrl: './label-settings.component.scss'
})
export class LabelSettingsComponent implements OnInit {

  constructor(private labelClient: LabelDefinitionClient) {
  }

  private labelDefinitionId: string = "";

  availableTypes: ContentTypeDto[] = [];

  /*
   * public string Name { get; set; } = string.Empty;
   * public LabelType Type { get; set; }
   * public string CommandText { get; set; } = string.Empty;
   */

  public formGroup = new FormGroup({
    name: new FormControl(''),
    labelType: new FormControl(-1),
    commandText: new FormControl('')
  });

  @Input({required: true})
  set id(labelDefinitionId: string) {
    this.labelDefinitionId = labelDefinitionId;
  }

  ngOnInit(): void {
    this.labelClient.getStorageLocationTypes()
      .subscribe(x => {
        this.availableTypes = x;

        this.labelClient.getLabelDefinition(this.labelDefinitionId)
          .subscribe(resp => {
            this.formGroup.patchValue({
              name: resp.name,
              labelType: resp.type,
              commandText: resp.commandText
            });
          });
      });
  }

  saveLabel(): void {
    if (this.formGroup.valid) {
      let request: LabelDefinitionDto = new LabelDefinitionDto();
      request.id = this.labelDefinitionId;
      if (this.formGroup.controls["name"].value != null) {
        request.name = this.formGroup.controls["name"].value;
      }
      if (this.formGroup.controls["labelType"].value != null) {
        request.type = this.formGroup.controls["labelType"].value;
      }
      if (this.formGroup.controls["commandText"].value != null) {
        request.commandText = this.formGroup.controls["commandText"].value;
      }
      console.log(request);
      this.labelClient.updateLabelDefinition(this.labelDefinitionId, request)
        .subscribe(x => {
          console.log(x);
        });
    }
  }

  labelTypeCompareFunction(o1: any, o2: any): boolean {
    return (o1 == o2);
  }
}
