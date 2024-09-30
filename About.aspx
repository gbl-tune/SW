<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="SW.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        body{
            background-color:#f8f9fc;
        }
        .card-header {
            font-weight: 600;
            font-size: .8rem;
            border:none;
            background-color:#13328212;

        }
        .chart-container .card-header {
            font-weight: 600;
            font-size: 1rem;
            color:#4e73df!important;
        }

        .card {
            margin: 10px;
            box-shadow: 0 .5rem 1rem rgba(0, 0, 0, .15) !important;
            border:none;
        }

        .dashboard {
            display: grid;
            grid-template-columns: repeat(4, 1fr);
            gap: 15px;
        }

        .chart-container {
            margin-top: 20px;
        }

        @media (max-width: 768px) {
            .dashboard {
                grid-template-columns: 1fr 1fr;
            }
        }

        @media (max-width: 576px) {
            .dashboard {
                grid-template-columns: 1fr;
            }
        }
    </style>

    <div class="container-fluid p-3">

        <!-- Dashboard Cards -->
        <div class="dashboard row text-center">
            <!-- PAPs Registados -->
            <div class="">
                <div class="card shadow-sm">
                    <div class="card-header bg-primary text-white">PAPs REGISTADOS</div>
                    <div class="card-body">
                        <h4>
                            <asp:Label ID="lblPAPs" runat="server" Text="0"></asp:Label></h4>
                    </div>
                </div>
            </div>

            <!-- Estruturas Afectadas -->
            <div class="">
                <div class="card shadow-sm">
                    <div class="card-header bg-success text-white">ESTRUTURAS AFECTADAS</div>
                    <div class="card-body">
                        <h4>
                            <asp:Label ID="lblEstruturas" runat="server" Text="0"></asp:Label></h4>
                    </div>
                </div>
            </div>

            <!-- Lugares Espirituais -->
            <div class="">
                <div class="card shadow-sm">
                    <div class="card-header bg-info text-white">LUGARES ESPIRITUAIS</div>
                    <div class="card-body">
                        <h4>
                            <asp:Label ID="lblLugares" runat="server" Text="0"></asp:Label></h4>
                    </div>
                </div>
            </div>

            <!-- CAF Mulher Viúva -->
            <div class="">
                <div class="card shadow-sm">
                    <div class="card-header bg-warning text-white">CAF MULHER VIÚVA</div>
                    <div class="card-body">
                        <h4>
                            <asp:Label ID="lblCAF" runat="server" Text="0"></asp:Label></h4>
                    </div>
                </div>
            </div>
        </div>

        <!-- Gráficos -->
        <div class="row">
            <!-- Gráfico de Barras: PAPs por Dia -->
            <div class="col-md-6 chart-container">
                <div class="card shadow-sm">
                    <div class="card-header">PAPs registados por dia</div>
                    <div class="card-body">
                        <canvas id="barChart"></canvas>
                    </div>
                </div>
            </div>

            <!-- Gráfico de Pizza: CAFs por Sexo -->
            <div class="col-md-6 chart-container">
                <div class="card shadow-sm">
                    <div class="card-header">CAFs por sexo</div>
                    <div class="card-body">
                        <canvas id="pieChart"></canvas>
                    </div>
                </div>
            </div>
        </div>

        <!-- Mais Gráficos -->
        <div class="row">
            <!-- Mapa ou outro gráfico, conforme necessidade -->
            <div class="col-md-6 chart-container">
                <div class="card shadow-sm">
                    <div class="card-header">PAPs registados por local</div>
                    <div class="card-body">
                        <!-- Mapa ou gráfico -->
                    </div>
                </div>
            </div>

            <!-- Gráfico adicional -->
            <div class="col-md-6 chart-container">
                <div class="card shadow-sm">
                    <div class="card-header">PAPs registados por inquiridor</div>
                    <div class="card-body">
                        <canvas id="lineChart"></canvas>
                    </div>
                </div>
            </div>
        </div>
    </div>
    

   

    <!-- Scripts para Charts.js -->
    <script>
        // Dados para o gráfico de barras
        var ctxBar = document.getElementById('barChart').getContext('2d');
        var barChart = new Chart(ctxBar, {
            type: 'bar',
            data: {
                labels: [], // Serão preenchidas dinamicamente
                datasets: [{
                    label: 'PAPs por Dia',
                    data: [], // Dados do banco de dados
                    backgroundColor: 'rgba(54, 162, 235, 0.5)',
                    borderColor: 'rgba(54, 162, 235, 1)',
                    borderWidth: 1
                }]
            },
            options: {
                scales: {
                    y: {
                        beginAtZero: true
                    }
                }
            }
        });

        // Dados para o gráfico de pizza
        var ctxPie = document.getElementById('pieChart').getContext('2d');
        var pieChart = new Chart(ctxPie, {
            type: 'pie',
            data: {
                labels: ['Masculino', 'Feminino'],
                datasets: [{
                    label: 'CAFs por Sexo',
                    data: [], // Dados do banco de dados
                    backgroundColor: ['rgba(255, 99, 132, 0.5)', 'rgba(54, 162, 235, 0.5)'],
                    borderColor: ['rgba(255, 99, 132, 1)', 'rgba(54, 162, 235, 1)'],
                    borderWidth: 1
                }]
            }
        });
    </script>


</asp:Content>
