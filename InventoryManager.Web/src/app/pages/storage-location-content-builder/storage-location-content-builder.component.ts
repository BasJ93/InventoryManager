import { Component, Input, OnInit } from '@angular/core';
import { NgIf, NgFor, NgStyle } from '@angular/common'
import { CdkDragDrop, CdkDrag, CdkDropList, CdkDropListGroup } from '@angular/cdk/drag-drop'
import { Subject } from 'rxjs';

import {
  ContainerClient,
  ContainerWithLocationResponseDto,
  GetStorageLocationResponseDto,
  StorageLocationClient
} from "../../generated/api.generated.clients";

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
  templateUrl: './storage-location-content-builder.component.html',
  styleUrl: './storage-location-content-builder.component.scss'
})

export class StorageLocationContentBuilderComponent implements OnInit {

  constructor(private storageLocationClient: StorageLocationClient, private containerClient: ContainerClient) {
    this.locationId$.subscribe(id => {
      // TODO: Move to function
      this.storageLocationClient.getStorageLocation(id)
        .subscribe(storageLocation => {
          this.storageLocation = storageLocation;
          for(let i = 0; i < storageLocation.sizeX * storageLocation.sizeY; i++)
          {
            let posX = i % (storageLocation.sizeX);
            let posY = Math.floor(i / storageLocation.sizeX);

            let empty = new ContainerWithLocationResponseDto();
            empty.positionX = posX + 1;
            empty.positionY = posY + 1;

            this.storageLocationGrid.push(empty)
          }

          for(let j = 0; j < this.storageLocation.containers.length; j++)
          {
            let position = this.calculatePosition(this.storageLocation.containers[j]);//(this.storageCase.containers[j].positionX - 1) * (this.storageCase.sizeX - 1) + this.storageCase.containers[j].positionY - 1;
            this.storageLocationGrid[position] = this.storageLocation.containers[j];
          }
        });
    });
  }

  storageLocation: GetStorageLocationResponseDto | null = null;

  storageLocationGrid: ContainerWithLocationResponseDto[] = [];

  availableContainers: ContainerWithLocationResponseDto[] = [];

  locationId$: Subject<string> = new Subject<string>;

  @Input({ required: true })
  set id(storagecase: string) {
    this.locationId$.next(storagecase);
  }

  ngOnInit(): void {
    this.containerClient.getUnplacedContainers()
      .subscribe(x => this.availableContainers = x);
  }

  getGridStyle() {
    if(this.storageLocation !== null) {
      let gridStyle: any = {
        'grid-template-columns': 'repeat(' + this.storageLocation.sizeX + ', 8vw)',
        'grid-template-rows': 'repeat(' + this.storageLocation.sizeY + ', 8vw)',
      };

      return gridStyle;
    }
    return "";
  }

  getAvailableStyle() {
    if(this.storageLocation !== null) {
      let gridStyle: any = {
        'grid-template-rows': 'repeat(' + this.storageLocation.sizeY + ', 8vw)',
      };

      return gridStyle;
    }
    return "";
  }

  dropInCase(event: CdkDragDrop<ContainerWithLocationResponseDto>) {

    if(this.storageLocation !== null) {
      const oldContainer: ContainerWithLocationResponseDto = JSON.parse(JSON.stringify(event.previousContainer.data));
      const newContainer: ContainerWithLocationResponseDto = JSON.parse(JSON.stringify(event.container.data));

      const oldPosition = this.calculatePosition(oldContainer);
      const newPosition = this.calculatePosition(newContainer);

      if (Array.isArray(event.previousContainer.data)) {
        // Add the item to the grid, and remove it from the available list
        this.storageLocationGrid[newPosition] = event.previousContainer.data[event.previousIndex];
        this.storageLocationGrid[newPosition].positionX = newContainer.positionX;
        this.storageLocationGrid[newPosition].positionY = newContainer.positionY;
        this.availableContainers.splice(event.previousIndex, 1);
      } else {
        let empty = new ContainerWithLocationResponseDto();
        empty.positionX = oldContainer.positionX;
        empty.positionY = oldContainer.positionY;
        this.storageLocationGrid[oldPosition] = empty;

        // Move the piece to the new location.
        this.storageLocationGrid[newPosition] = oldContainer;
        this.storageLocationGrid[newPosition].positionX = newContainer.positionX;
        this.storageLocationGrid[newPosition].positionY = newContainer.positionY;
      }

      this.updateContainerPositionInDatabase(this.storageLocationGrid[newPosition]);
    }
  }

  dropInAvailable(event: CdkDragDrop<ContainerWithLocationResponseDto[], ContainerWithLocationResponseDto>) {
    if(this.storageLocation !== null) {
      const oldContainer: ContainerWithLocationResponseDto = JSON.parse(JSON.stringify(event.previousContainer.data));

      const oldPosition = this.calculatePosition(oldContainer);

      this.availableContainers.push(oldContainer);
      this.removeContainerFromCaseInDatabase(oldContainer);

      let empty = new ContainerWithLocationResponseDto();
      empty.positionX = oldContainer.positionX;
      empty.positionY = oldContainer.positionY;

      this.storageLocationGrid[oldPosition] = empty;
    }
  }

  enterCasePredicate = (drag: CdkDrag, drop: CdkDropList) => {
    let position = this.calculatePosition(drop.data);
    return this.storageLocationGrid[position].content === null || this.storageLocationGrid[position].content === undefined;
  };

  updateContainerPositionInDatabase(container: ContainerWithLocationResponseDto): void {
    if(this.storageLocation != null) {
      this.storageLocationClient.putContainerInStorageLocation(this.storageLocation.id, container.positionX, container.positionY, container.id)
        .subscribe(x => console.log(x));
    }
  }

  removeContainerFromCaseInDatabase(container: ContainerWithLocationResponseDto): void {
    if(this.storageLocation != null) {
      this.storageLocationClient.removeContainerFromStorageLocation(this.storageLocation.id, container.positionX, container.positionY)
        .subscribe(x => console.log(x));
    }
  }

  private calculatePosition(container: ContainerWithLocationResponseDto): number {
    if(this.storageLocation != null) {
      return (container.positionX - 1) + (container.positionY - 1) * this.storageLocation.sizeX;
    }
    return -1;
  }
}
