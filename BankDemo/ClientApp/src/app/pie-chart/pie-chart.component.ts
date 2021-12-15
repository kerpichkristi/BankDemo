import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { ChartType, ChartDataSets } from 'chart.js';
import { Label } from 'ng2-charts';

@Component({
  selector: 'app-my-pie-chart',
  templateUrl: './pie-chart.component.html'
})
export class TransactionsPieChartComponent implements OnInit {

  private NumberOfShownUsers = 7;
  public pieChartType: ChartType = 'pie';
  public pieChartLabels: Label[];
  public pieChartData: ChartDataSets[];
  

  constructor(private http: HttpClient) { }
 
  ngOnInit() {

    this.http.get('https://localhost:44353/api/Transactions/PieChart1').subscribe(
      data => {

       var ChartData: any[] = [];
       var ChartLabels: any[] = [];

        for (var i = 0; i < this.NumberOfShownUsers; i++) {
          ChartLabels.push(data[i].sender);
          ChartData.push(data[i].sumdebit);
        }
        this.pieChartData = [{ data: ChartData }];
        this.pieChartLabels = ChartLabels;
        this.pieChartData = ChartData;
       
      },
      (err: HttpErrorResponse) => {
        console.log(err.message);
      });
  }
}
