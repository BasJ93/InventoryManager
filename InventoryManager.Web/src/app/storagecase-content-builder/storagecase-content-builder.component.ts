import { Component, Input, OnInit } from '@angular/core';
import { NgIf, NgFor, NgStyle } from '@angular/common'
import { CdkDragDrop, CdkDrag, CdkDropList, CdkDropListGroup } from '@angular/cdk/drag-drop'
import { Subject } from 'rxjs';

import {
  ContainerClient,
  ContainerWithLocationResponseDto,
  GetStorageCaseResponseDto,
  StorageCaseClient
} from "../generated/api.generated.clients";

@Component({
  selector: 'app-storagecase-content-builder',
  standalone: true,
  imports: [
    NgStyle,
    NgFor,
    NgIf,
    CdkDrag,
    CdkDropList,
    CdkDropListGroup,
  ],
  templateUrl: './storagecase-content-builder.component.html',
  styleUrl: './storagecase-content-builder.component.scss'
})

// TODO: Update the position of a container in the backend when it has been moved.

export class StoragecaseContentBuilderComponent implements OnInit {

  constructor(private storageCaseClient: StorageCaseClient, private containerClient: ContainerClient) {
    this.caseId$.subscribe(id => {
      this.storageCaseClient.getCase(id)
        .subscribe(storageCase => {
          this.storageCase = storageCase;
          for(let i = 0; i < storageCase.sizeX * storageCase.sizeY; i++)
          {
            let posX = i % (storageCase.sizeX);
            let posY = Math.floor(i / storageCase.sizeX);

            let empty = new ContainerWithLocationResponseDto();
            empty.positionX = posX + 1;
            empty.positionY = posY + 1;

            this.storageCaseGrid.push(empty)
          }

          for(let j = 0; j < this.storageCase.containers.length; j++)
          {
            let position = this.calculatePosition(this.storageCase.containers[j]);//(this.storageCase.containers[j].positionX - 1) * (this.storageCase.sizeX - 1) + this.storageCase.containers[j].positionY - 1;
            this.storageCaseGrid[position] = this.storageCase.containers[j];
          }
        });
    });
  }

  storageCase: GetStorageCaseResponseDto | null = null;

  storageCaseGrid: ContainerWithLocationResponseDto[] = [];

  availableContainers: ContainerWithLocationResponseDto[] = [];

  caseId$: Subject<string> = new Subject<string>;

  @Input({ required: true })
  set id(storagecase: string) {
    this.caseId$.next(storagecase);
  }

  ngOnInit(): void {
    this.containerClient.getUnplacedContainers()
      .subscribe(x => this.availableContainers = x);
  }

  getGridStyle() {
    if(this.storageCase !== null) {
      let gridStyle: any = {
        'grid-template-columns': 'repeat(' + this.storageCase.sizeX + ', 8vw)',
        'grid-template-rows': 'repeat(' + this.storageCase.sizeY + ', 8vw)',
      };

      return gridStyle;
    }
    return "";
  }

  getAvailableStyle() {
    if(this.storageCase !== null) {
      let gridStyle: any = {
        'grid-template-rows': 'repeat(' + this.storageCase.sizeY + ', 8vw)',
      };

      return gridStyle;
    }
    return "";
  }

  dropInCase(event: CdkDragDrop<ContainerWithLocationResponseDto>) {

    if(this.storageCase !== null) {
      const oldContainer: ContainerWithLocationResponseDto = JSON.parse(JSON.stringify(event.previousContainer.data));
      const newContainer: ContainerWithLocationResponseDto = JSON.parse(JSON.stringify(event.container.data));

      const oldPosition = this.calculatePosition(oldContainer);
      const newPosition = this.calculatePosition(newContainer);

      if (Array.isArray(event.previousContainer.data)) {
        // Add the item to the grid, and remove it from the available list
        this.storageCaseGrid[newPosition] = event.previousContainer.data[event.previousIndex];
        this.storageCaseGrid[newPosition].positionX = newContainer.positionX;
        this.storageCaseGrid[newPosition].positionY = newContainer.positionY;
        this.availableContainers.splice(event.previousIndex, 1);
      } else {
        let empty = new ContainerWithLocationResponseDto();
        empty.positionX = oldContainer.positionX;
        empty.positionY = oldContainer.positionY;
        this.storageCaseGrid[oldPosition] = empty;

        // Move the piece to the new location.
        this.storageCaseGrid[newPosition] = oldContainer;
        this.storageCaseGrid[newPosition].positionX = newContainer.positionX;
        this.storageCaseGrid[newPosition].positionY = newContainer.positionY;
      }

      this.updateContainerPositionInDatabase(this.storageCaseGrid[newPosition]);
    }
  }

  dropInAvailable(event: CdkDragDrop<ContainerWithLocationResponseDto[], ContainerWithLocationResponseDto>) {
    if(this.storageCase !== null) {
      const oldContainer: ContainerWithLocationResponseDto = JSON.parse(JSON.stringify(event.previousContainer.data));

      const oldPosition = this.calculatePosition(oldContainer);

      this.availableContainers.push(oldContainer);
      this.removeContainerFromCaseInDatabase(oldContainer);

      let empty = new ContainerWithLocationResponseDto();
      empty.positionX = oldContainer.positionX;
      empty.positionY = oldContainer.positionY;

      this.storageCaseGrid[oldPosition] = empty;
    }
  }

  enterCasePredicate = (drag: CdkDrag, drop: CdkDropList) => {
    let position = this.calculatePosition(drop.data);
    return this.storageCaseGrid[position].content === null || this.storageCaseGrid[position].content === undefined;
  };

  updateContainerPositionInDatabase(container: ContainerWithLocationResponseDto): void {
    if(this.storageCase != null) {
      this.storageCaseClient.putContainerInCase(this.storageCase.id, container.positionX, container.positionY, container.id)
        .subscribe(x => console.log(x));
    }
  }

  removeContainerFromCaseInDatabase(container: ContainerWithLocationResponseDto): void {
    if(this.storageCase != null) {
      this.storageCaseClient.removeContainerFromCase(this.storageCase.id, container.positionX, container.positionY)
        .subscribe(x => console.log(x));
    }
  }

  private calculatePosition(container: ContainerWithLocationResponseDto): number {
    if(this.storageCase != null) {
      return (container.positionX - 1) + (container.positionY - 1) * this.storageCase.sizeX;
    }
    return -1;
  }
}
