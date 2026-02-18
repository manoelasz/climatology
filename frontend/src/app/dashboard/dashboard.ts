import { Component, OnInit } from '@angular/core';
import { WeatherService } from '../services/index';
import { Chart } from 'chart.js/auto';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css',
})

export class DashboardComponent implements OnInit {

  cidade = 'São Paulo';
  inicio!: string;
  fim!: string;

  estatisticas: any;
  dados: any[] = [];

  chartTemperatura: any;
  chartUmidade: any;

  constructor(private service: WeatherService) {}

  ngOnInit(): void {
    this.service.consultarCidade(this.cidade).subscribe(() => {
    this.carregarDadosHoje();
  });
  }

  carregarDadosHoje() {
    const hoje = new Date().toISOString().split('T')[0];
    this.inicio = hoje;
    this.fim = hoje;

    this.buscarPeriodo();
    this.buscarEstatisticas();
  }

  buscarPeriodo() {
    this.service.buscarPorPeriodo(this.cidade, this.inicio, this.fim)
      .subscribe(data => {
        this.dados = data;
        this.criarGraficos();
      });
  }

  buscarEstatisticas() {
    this.service.obterEstatisticasHoje(this.cidade)
      .subscribe(data => {
        this.estatisticas = data;
      });
  }

  criarGraficos() {

    const labels = this.dados.map(x =>
      new Date(x.dataConsulta).toLocaleTimeString()
    );

    const temperaturas = this.dados.map(x => x.temperatura);
    const umidades = this.dados.map(x => x.umidade);

    if (this.chartTemperatura) this.chartTemperatura.destroy();
    if (this.chartUmidade) this.chartUmidade.destroy();

    this.chartTemperatura = new Chart('graficoTemp', {
      type: 'line',
      data: {
        labels: labels,
        datasets: [{
          label: 'Temperatura (°C)',
          data: temperaturas
        }]
      }
    });

    this.chartUmidade = new Chart('graficoUmidade', {
      type: 'bar',
      data: {
        labels: labels,
        datasets: [{
          label: 'Umidade (%)',
          data: umidades
        }]
      }
    });
    
  }
  buscar() {
  console.log("Buscando:", this.cidade, this.inicio, this.fim);

  this.buscarPeriodo();
  this.buscarEstatisticas();
  }
}
