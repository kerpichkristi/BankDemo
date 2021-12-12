import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { ChartType, ChartDataSets } from 'chart.js';
import { Label } from 'ng2-charts';


@Component({
  selector: 'app-my-pie-chart2',
  templateUrl: './pie-chart2.component.html'
})
export class TransactionsPieChart2Component implements OnInit {


  public pieChartType: ChartType = 'pie';
  public pieChartLabels: Label[];
  public pieChartData: ChartDataSets[];
  
  //public chartDataReady = true;

  constructor(private http: HttpClient) { }


  ngOnInit(){

  this.http.get('https://localhost:44353/api/Transactions/PieChart2').subscribe(
    data1 => {
     
      var ChartData: any[] = [];
      var ChartLabels: any[] = [];

      for (var i = 0; i <= 7; i++) {
        ChartLabels.push(data1[i].sender);
      }

      for (var i = 0; i <= 7; i++) {
        ChartData.push(data1[i].sumcredit);
      }
      
        this.pieChartData = [{ data: ChartData }];
        this.pieChartLabels = ChartLabels;
        this.pieChartData = ChartData;
      console.log(this.pieChartLabels);
      console.log(this.pieChartData);

      
      
     
    },
      (err: HttpErrorResponse) => {
      console.log(err.message);
    });


  

  }
}






