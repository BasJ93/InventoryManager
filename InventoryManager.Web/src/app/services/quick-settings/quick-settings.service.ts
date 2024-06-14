import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from "rxjs";
import { ConfigurationClient } from "../../generated/api.generated.clients";

@Injectable({
  providedIn: 'root'
})
export class QuickSettingsService {

  constructor(private configurationClient: ConfigurationClient) {
    this.configurationClient.getQuickLabelPrinterConfiguration()
      .subscribe(x => {
        this.setLabelPrinterEnabled(x.labelPrinterEnabled);
      });
  }

  private labelPrinterEnabled: BehaviorSubject<boolean> = new BehaviorSubject(false);

  getLabelPrinterEnabledAsObservable(): Observable<boolean> {
    return this.labelPrinterEnabled.asObservable();
  }

  getLabelPrinterEnabled(): BehaviorSubject<boolean> {
    return this.labelPrinterEnabled;
  }

  setLabelPrinterEnabled(enabled: boolean): void {
    this.labelPrinterEnabled.next(enabled);
  }
}
