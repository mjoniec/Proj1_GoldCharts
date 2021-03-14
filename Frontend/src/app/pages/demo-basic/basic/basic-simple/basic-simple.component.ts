import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
//import { Observable } from 'rxjs';
declare const require: any; // DEMO IGNORE

@Component({
  selector: 'app-basic-simple',
  templateUrl: './basic-simple.component.html',
  styleUrls: ['./basic-simple.component.scss'],
})
export class BasicSimpleComponent implements OnInit {
  html = require('!!html-loader?-minimize!./basic-simple.component.html'); // DEMO IGNORE
  component = require('!!raw-loader!./basic-simple.component.ts').default; // DEMO IGNORE
  options: any;
  goldUsd = [];
  silverUsd = [];
  xAxisData: any[];
  data1: any[];
  data2: any[];

  constructor(private httpClient: HttpClient) {}

  ngOnInit(): void {

    this.getGoldUsd().subscribe((dataGold: any[])=>{
      console.log(dataGold);
      this.goldUsd = dataGold;

      this.getSilverUsd().subscribe((dataSilver: any[])=>{
        console.log(dataSilver);
        this.silverUsd = dataSilver;

        // const xAxisData = [];
        //const data1 = [];
        //const data2 = [];

        this.xAxisData = this.goldUsd['prices'].map(function(a) {return a.date;});
        this.data1 = this.goldUsd['prices'].map(function(a) {return a.value;});
        this.data2 = this.silverUsd['prices'].map(function(a) {return a.value;});

        //console.log(this.goldUsd['prices']);
        //console.log(this.data1);

        for (let i = 0; i < 100; i++) {
          //xAxisData.push('category' + i);
          //data1.push((Math.sin(i / 5) * (i / 5 - 10) + i / 9) * 5);
          //data2.push((Math.cos(i / 5) * (i / 5 - 10) + i / 6) * 5);
        }

        this.options = {
          legend: {
            data: ['bar', 'bar2'],
            align: 'left',
          },
          tooltip: {},
          xAxis: {
            // data: xAxisData,
            data: this.xAxisData,
            silent: false,
            splitLine: {
              show: false,
            },
          },
          yAxis: {},
          series: [
            {
              name: 'bar',
              type: 'bar',
              // data: data1,
              data: this.data1,
              animationDelay: (idx) => idx * 10,
            },
            {
              name: 'bar2',
              type: 'bar',
              // data: data2,
              data: this.data2,
              animationDelay: (idx) => idx * 10 + 100,
            },
          ],
          animationEasing: 'elasticOut',
          animationDelayUpdate: (idx) => idx * 5,
        }; // this.options

      }) //getSilverUsd
    }); //getGoldUsd


  }

  public getGoldUsd() {
    // return this.httpClient.get("http://goldchartsapi.azurewebsites.net/api/GoldCharts/USD/Gold/2000-1-1/2000-3-31")
    return this.httpClient.get("https://localhost:44314/api/GoldCharts/USD/Gold/2000-1-1/2000-3-31") 
  }

  public getSilverUsd() {
    // return this.httpClient.get("http://goldchartsapi.azurewebsites.net/api/GoldCharts/USD/Gold/2000-1-1/2000-3-31")
    return this.httpClient.get("https://localhost:44314/api/GoldCharts/USD/Silver/2000-1-1/2000-3-31") 
  }
}
