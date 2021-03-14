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
  
  goldUsdJson = [];
  silverUsdJson = [];

  datesAll: any[];
  goldUsdPricesAll: any[];
  silverUsdPricesAll: any[];

  constructor(private httpClient: HttpClient) {}

  ngOnInit(): void {

    this.getGoldUsd().subscribe((dataGold: any[])=>{
      //console.log(dataGold);
      this.goldUsdJson = dataGold;

      this.getSilverUsd().subscribe((dataSilver: any[])=>{
        //console.log(dataSilver);
        this.silverUsdJson = dataSilver;

        this.datesAll = this.goldUsdJson['prices'].map(function(a) {return a.date;} );
        this.goldUsdPricesAll = this.goldUsdJson['prices'].map(function(a) {return a.value;} );
        this.silverUsdPricesAll = this.silverUsdJson['prices'].map(function(a) {return a.value;} );

        const takeEveryDay = 30;
        const datesEvery30Day = [];
        const goldUsdPricesEvery30Day = [];
        const silverUsdPricesEvery30Day = [];

        for (let i = 0; i < 100; i++) {
          datesEvery30Day.push(this.datesAll[i * takeEveryDay]);
          goldUsdPricesEvery30Day.push(this.goldUsdPricesAll[i * takeEveryDay]);
          silverUsdPricesEvery30Day.push(this.silverUsdPricesAll[i * takeEveryDay]);
        }

        this.options = {
          legend: {
            data: ['gold usd', 'silver usd'],
            align: 'left',
          },
          tooltip: {},
          xAxis: {
            data: datesEvery30Day,
            silent: false,
            splitLine: {
              show: false,
            },
          },
          yAxis: {},
          series: [
            {
              name: 'gold usd',
              type: 'bar',
              data: goldUsdPricesEvery30Day,
              animationDelay: (idx) => idx * 10,
            },
            {
              name: 'silver usd',
              type: 'bar',
              data: silverUsdPricesEvery30Day,
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
    // return this.httpClient.get("http://goldchartsapi.azurewebsites.net/api/GoldCharts/USD/Gold/2000-1-1/2009-3-31")
    return this.httpClient.get("https://localhost:44314/api/GoldCharts/USD/Gold/2000-1-1/2009-3-31") 
  }

  public getSilverUsd() {
    // return this.httpClient.get("http://goldchartsapi.azurewebsites.net/api/GoldCharts/USD/Gold/2000-1-1/2009-3-31")
    return this.httpClient.get("https://localhost:44314/api/GoldCharts/USD/Silver/2000-1-1/2009-3-31") 
  }
}
