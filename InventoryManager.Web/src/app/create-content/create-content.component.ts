import { Component } from '@angular/core';
import { NgFor, NgIf } from '@angular/common';
import { FormsModule, FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import { MatButton } from '@angular/material/button';
import { ContentClient, ContentTypeDto, CreateContentRequestDto } from "../generated/api.generated.clients";

@Component({
  selector: 'app-create-content',
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
  templateUrl: './create-content.component.html',
  styleUrl: './create-content.component.scss'
})
export class CreateContentComponent {
  constructor(private contentClient: ContentClient) {
    this.contentClient.getContentTypes()
      .subscribe(x => this.availableTypes = x);
  }

  // property with the available content types
  availableTypes: ContentTypeDto[] = [];

  // required properties for the content
  public formGroup = new FormGroup({
    selectedType: new FormControl(-1),
    standard: new FormControl(''),
    size: new FormControl(''),
    length: new FormControl('')
  });

  createContent(): void {
    if(this.formGroup.valid) {
      let request: CreateContentRequestDto = new CreateContentRequestDto();
      if (this.formGroup.controls["selectedType"].value != null) {
        request.type = Number(this.formGroup.controls["selectedType"].value);
      }
      if (this.formGroup.controls["standard"].value != null) {
        request.standard = this.formGroup.controls["standard"].value;
      }
      if (this.formGroup.controls["size"].value != null) {
        request.size = this.formGroup.controls["size"].value;
      }
      if (this.formGroup.controls["length"].value != null) {
        request.length = this.formGroup.controls["length"].value;
      }
      console.log(request);
      this.contentClient.createContent(request)
        .subscribe(x => {
          console.log(x);
          this.formGroup.reset({
            selectedType: -1,
            standard: '',
            size: '',
            length: ''
          });
        });
    }
  }
}
