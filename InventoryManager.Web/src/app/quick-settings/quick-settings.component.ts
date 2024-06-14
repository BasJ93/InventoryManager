import { Component, OnInit } from '@angular/core';
import { AsyncPipe } from '@angular/common';
import { BehaviorSubject, Observable } from "rxjs";
import { MatSlideToggleModule, MatSlideToggleChange } from '@angular/material/slide-toggle';
import { MatButtonModule } from '@angular/material/button'
import { RouterLink } from '@angular/router'
import { ConfigurationClient } from "../generated/api.generated.clients";
import { QuickSettingsService } from "../services/quick-settings/quick-settings.service";

@Component({
  selector: 'app-quick-settings',
  standalone: true,
  imports: [
    AsyncPipe,
    MatSlideToggleModule,
    RouterLink,
    MatButtonModule
  ],
  templateUrl: './quick-settings.component.html',
  styleUrl: './quick-settings.component.scss'
})
export class QuickSettingsComponent implements OnInit {
  constructor(private quickSettingsService: QuickSettingsService) {
  }
  labelPrinterEnabled!: BehaviorSubject<boolean>;

  private _labelPrinterEnabled: boolean = false;

  ngOnInit(): void {
    this.labelPrinterEnabled = this.quickSettingsService.getLabelPrinterEnabled();

    this.labelPrinterEnabled.subscribe(x => this._labelPrinterEnabled = x);
  }

  labelPrinterEnabledChanged(event: MatSlideToggleChange): void {
    if (event.checked != this._labelPrinterEnabled) {
      this.quickSettingsService.setLabelPrinterEnabled(event.checked);
    }
  }
}
