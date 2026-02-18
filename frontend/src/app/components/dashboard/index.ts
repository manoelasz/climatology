import { WeatherService } from "../../services";
import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgChartsModule } from 'ng2-charts';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, NgChartsModule],
  templateUrl: './index.html'
})

export class DashboardComponent {

  cidadeSelecionada = '';
  dataInicial!: Date;
  dataFinal!: Date;

  dados: any[] = [];

  constructor(private weatherService: WeatherService) {}

  buscar() {
    this.weatherService
      .buscarPorPeriodo(
        this.cidadeSelecionada,
        this.dataInicial.toISOString(),
        this.dataFinal.toISOString()
      )
      .subscribe(response => {
        this.dados = response;
        this.atualizarGraficos();
      });
  }

  atualizarGraficos() {
    // monta datasets aqui
  }

  public barChartData = {
  labels: ['Jan', 'Fev', 'Mar'],
  datasets: [
    {
      data: [10, 20, 30],
      label: 'Temperatura'
    }
  ]
};

public barChartOptions = {
  responsive: true
};

}
