import { Component, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";

@Component({
    templateUrl:'./dashboard.component.html',
     styleUrls: ['dashboard.component.scss']
})

export class DashboardComponent implements OnInit {

returnUrl:string = '';   

    constructor(private route: ActivatedRoute,private router: Router){}

    ngOnInit(){
        this.chartPieData = this.initCharts('categoria',5);
        this.chartPieData2 = this.initCharts('tipo',3);
        this.chartPieData3 = this.initCharts('unidade',7);
    }

    public colors: string[] = ['#0d6efd','#ad92e0','#6f42c1','#d63384','#dc3545','#fd7e14','#ffc107','#198754','#20c997','#0dcaf0','#e55353','#39f','#4f5d73','#FF6384','#36A2EB','#FFCE56'];

    chartPieData = {
      labels: [''],
      datasets: [
        {
          data: [1],
          backgroundColor: [''],
          hoverBackgroundColor: ['']
        }
      ]
    };

    chartPieData2 = {
        labels: [],
        datasets: [
          {
            data: [],
            backgroundColor: [''],
            hoverBackgroundColor: ['']
          }
        ]
      };    

      chartPieData3 = {
        labels: [],
        datasets: [
          {
            data: [],
            backgroundColor: [''],
            hoverBackgroundColor: ['']
          }
        ]
      };  

      initCharts(_data: string, _count: number): any{

        var _chartPie = {
          labels: [''],
          datasets: [
            {
              data: [1],
              backgroundColor: [''],
              hoverBackgroundColor: ['']
            }
          ]
        };
    
        var _chartLabel: string[] = [];
        var _chartData: number[] = [];
        var _color: string[] = [];
        var _houverColor: string[] = [];
    
    
        for (let index = 0; index < _count; index++) {
    
          var randomData = Math.floor(Math.random()*100);
    
          if (randomData ==0)
             randomData = Math.floor(Math.random()*100);
    
          var randomColor = Math.floor(Math.random()*this.colors.length);      
    
          _chartLabel.push(_data+' '+randomData.toString());
          _chartData.push(randomData);
    
    
          _color.push(this.colors[randomColor])
          _houverColor.push(this.colors[randomColor])
        }
    
        _chartPie = {
          labels: _chartLabel,
          datasets: [
            {
              data: _chartData,
              backgroundColor: _color,
              hoverBackgroundColor: _houverColor
            }
          ]
        };
    
        return _chartPie;
      }
    
}