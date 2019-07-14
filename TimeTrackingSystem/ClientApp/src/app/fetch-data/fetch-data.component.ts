import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { SampleDataService, WeatherForecast } from '../core/api.generated';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public forecasts: WeatherForecast[] = [];

  constructor(sampleDataService: SampleDataService) {
    sampleDataService.weatherForecasts().subscribe(res => this.forecasts = res)
  }
}
