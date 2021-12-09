import { Component, OnInit } from '@angular/core';
@Component({
  selector: 'app-my-pie-chart2',
  templateUrl: './pie-chart2.component.html'
})
export class MyPieChart2Component implements OnInit {




  public pieChartLabels = ['Sales Q1', 'Sales Q2', 'Sales Q3', 'Sales Q4'];
  public pieChartData = [120, 150, 180, 90];





  public pieChartType = 'pie';
  constructor() { }
  ngOnInit() {
  }
}
