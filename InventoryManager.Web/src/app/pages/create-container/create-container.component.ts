import { Component } from '@angular/core';
import { NgFor, AsyncPipe } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatButton } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { Subject } from "rxjs";
import {
  ContainerClient,
  ContainerSizeDto,
  ContentClient,
  ContentResponseDto, CreateContainerRequestDto
} from "../../generated/api.generated.clients";


@Component({
  selector: 'app-create-container-component',
  standalone: true,
  imports: [
    MatFormFieldModule,
    MatSelectModule,
    NgFor,
    MatButton,
    FormsModule,
    MatCheckboxModule,
    AsyncPipe
  ],
  templateUrl: './create-container.component.html',
  styleUrl: './create-container.component.scss'
})
export class CreateContainerComponent {
  constructor(private containerClient: ContainerClient, private contentClient: ContentClient) {
    this.loadContents(true);

    this.containerClient.getContainerSizes()
      .subscribe(x => this.containerSizes = x);
  }

  withoutContainer: boolean = true;

  containerSizes: ContainerSizeDto[] = [];

  availableContents$: Subject<ContentResponseDto[]> = new Subject<ContentResponseDto[]>();

  selectedContent: string = '';

  selectedSize: number = 0;

  createContainer(): void {
    if (this.selectedContent === '') {
      return;
    }
    if (this.selectedSize === 0) {
      return;
    }

    let request = new CreateContainerRequestDto();
    request.contentId = this.selectedContent;
    request.size = Number(this.selectedSize);

    console.log(request);

    this.containerClient.createContainer(request)
      .subscribe(x => {
        console.log(x);
        this.loadContents(this.withoutContainer);
      });
  }

  loadContents(without: boolean): void {
    this.withoutContainer = without;
    this.contentClient.getContents(this.withoutContainer)
      .subscribe(x => this.availableContents$.next(x));
  }
}
