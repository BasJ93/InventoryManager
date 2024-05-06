import { Component, OnInit } from '@angular/core';
import { NgFor } from '@angular/common';
import { ContentClient, ContentReponseDto } from "../generated/api.generated.clients";

@Component({
  selector: 'app-contents-overview-component',
  standalone: true,
  imports: [
    NgFor
  ],
  templateUrl: './contents-overview-component.component.html',
  styleUrl: './contents-overview-component.component.scss',
})
export class ContentsOverviewComponentComponent implements OnInit {

  contents: ContentReponseDto[]= [];

  constructor(private contentClient: ContentClient) {
  }

  ngOnInit(): void {
    this.contentClient.getContents()
      .subscribe(x =>this.contents = x);
  }
}
