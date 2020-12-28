import { Component } from '@angular/core';
import { SignalRService } from 'src/service/signalr.service';
import { HttpClient } from '@angular/common/http';
import { PersonService } from 'src/service/person.service';
import { ColorType } from 'src/enum/colortype.enum';
import { ChartModel } from 'src/model/chart.model';
import { ChartType } from 'chart.js';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'Web';
  
  constructor(public signalRService: SignalRService, public personService: PersonService, private http: HttpClient) { }

  public chartOptions: any = {
    scaleShowVerticalLines: true,
    responsive: true,
    scales: {
      yAxes: [{
        ticks: {
          beginAtZero: true
        }
      }]
    }
  };
  public chartLabels: string[] = ['Gráfico de quantidade de pessoas por cor'];
  public chartType: ChartType = 'bar';
  public chartData: ChartModel[] = new Array<ChartModel>();
  public chartLegend: boolean = true;

  ngOnInit() {
    this.signalRService.startConnection();
    this.signalRService.addPersonCommandListener(this.getData);   
    this.getData();
  }
  public getData = () => {
    this.chartData = new Array<ChartModel>();
    
    this.personService.getPersonByColor(ColorType.Amarelo).subscribe( q => { console.log(this.chartData);this.chartData.push({label: "Amarelo", data: [q.quantity], backgroundColor: '#FFFF00' }); } );
    this.personService.getPersonByColor(ColorType.Branco).subscribe( q => { this.chartData.push({label: "Branco", data: [q.quantity], backgroundColor: '#0066FF' }); } );
    this.personService.getPersonByColor(ColorType.Indigena).subscribe( q => { this.chartData.push({label: "Indigena", data: [q.quantity], backgroundColor: '#009900' }); } );
    this.personService.getPersonByColor(ColorType.Preto).subscribe( q => { this.chartData.push({label: "Preto", data: [q.quantity], backgroundColor: '#000' }); } );
    this.personService.getPersonByColor(ColorType.Pardo).subscribe( q => { this.chartData.push({label: "Pardo", data: [q.quantity], backgroundColor: '#663300' });} );
  }
}
