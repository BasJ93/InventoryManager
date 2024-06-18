import { Component, OnInit } from '@angular/core';
import { FormsModule, FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatButton } from '@angular/material/button';
import {
  ConfigurationClient,
  LabelPrinterConfigurationDto
} from "../../../generated/api.generated.clients";
import { QuickSettingsService } from "../../../services/quick-settings/quick-settings.service";

@Component({
  selector: 'app-label-printer-settings',
  standalone: true,
  imports: [
    MatFormFieldModule,
    FormsModule,
    ReactiveFormsModule,
    MatInputModule,
    MatSlideToggleModule,
    MatButton
  ],
  templateUrl: './label-printer-settings.component.html',
  styleUrl: './label-printer-settings.component.scss'
})
export class LabelPrinterSettingsComponent implements OnInit {

  constructor(private configurationClient: ConfigurationClient, private quickSettingsService: QuickSettingsService) {
  }

  /* Printer settings are:
   * - enabled
   * - has cutter
   * - delayed cut enabled
   * - delayed cut command
   * - label printer address
   * - is network printer
   */
  public formGroup = new FormGroup({
    labelPrinterEnabled: new FormControl(false),
    isNetworkPrinter: new FormControl(false),
    printerAddress: new FormControl(''),
    hasCutter: new FormControl(false),
    usesDelayedCut: new FormControl(false),
    delayedCutCommand: new FormControl('')
  });

  ngOnInit(): void {
    this.configurationClient.getLabelPrinterConfiguration()
      .subscribe(x => {
        console.log(x);
        this.formGroup.reset({
          labelPrinterEnabled: x.labelPrinterEnabled,
          isNetworkPrinter: x.networkLabelPrinter,
          printerAddress: x.labelPrinterAddress,
          hasCutter: x.hasCutter,
          usesDelayedCut: x.usesDelayedCut,
          delayedCutCommand: x.delayedCutterCommand
        });
      });
  }

  saveSettings(): void {
    if (this.formGroup.valid) {
      let request: LabelPrinterConfigurationDto = new LabelPrinterConfigurationDto();
      if (this.formGroup.controls["labelPrinterEnabled"].value != null) {
        request.labelPrinterEnabled = this.formGroup.controls["labelPrinterEnabled"].value;
      }
      if (this.formGroup.controls["isNetworkPrinter"].value != null) {
        request.networkLabelPrinter = this.formGroup.controls["isNetworkPrinter"].value;
      }
      if (this.formGroup.controls["printerAddress"].value != null) {
        request.labelPrinterAddress = this.formGroup.controls["printerAddress"].value;
      }
      if (this.formGroup.controls["hasCutter"].value != null) {
        request.hasCutter = this.formGroup.controls["hasCutter"].value;
      }
      if (this.formGroup.controls["usesDelayedCut"].value != null) {
        request.usesDelayedCut = this.formGroup.controls["usesDelayedCut"].value;
      }
      if (this.formGroup.controls["delayedCutCommand"].value != null) {
        request.delayedCutterCommand = this.formGroup.controls["delayedCutCommand"].value;
      }
      console.log(request);
      this.configurationClient.setLabelPrinterConfiguration(request)
        .subscribe(x => {
          console.log(x);
          this.quickSettingsService.setLabelPrinterEnabled(x.labelPrinterEnabled);
        });
    }
  }
}
