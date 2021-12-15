import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { ChartType, ChartDataSets } from 'chart.js';
import { Color, Label } from 'ng2-charts';


@Component({
  selector: 'app-my-pie-chart2',
  templateUrl: './pie-chart2.component.html'
})
export class TransactionsPieChart2Component implements OnInit {


  private NumberOfShownUsers = 7;
  public pieChartType: ChartType = 'pie';
  public pieChartLabels: Label[];
  public pieChartData: ChartDataSets[];
  public pieChartColor: Color[] = [{
  
    backgroundColor: [
      'rgba(255, 150, 132, 0.5)',
      'rgba(169, 50, 38, 0.5)',
      'rgba(118, 68, 138, 0.5)',
      'rgba(31, 97, 141 , 0.5)',
      'rgba(17, 122, 101 , 0.5)',
      'rgba(35, 155, 86 , 0.5)',
      'rgba(247, 220, 111, 0.5)',
      'rgb(230, 126, 34, 0.5 )]']
   }
   
  ]
  

  constructor(private http: HttpClient) { }


  ngOnInit(){

  this.http.get('https://localhost:44353/api/Transactions/PieChart2').subscribe(
    data => {
     
      var ChartData: any[] = [];
      var ChartLabels: any[] = [];

      for (var i = 0; i < this.NumberOfShownUsers; i++) {
        ChartLabels.push(data[i].sender);
        ChartData.push(data[i].sumcredit);
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






