// Importa as bibliotecas React e hooks useEffect e useState.
import React, { useEffect, useState } from 'react';
// Importa a configuração do axios.
import axios from '../axiosConfig';
// Importa o componente Pie do react-chartjs-2.
import { Pie } from 'react-chartjs-2';
// Importa componentes específicos do Chart.js.
import { Chart as ChartJS, ArcElement, Tooltip, Legend } from 'chart.js';
// Importa o arquivo CSS para estilização.
import '../ExibicaoDados.css';

// Registra componentes do Chart.js.
ChartJS.register(ArcElement, Tooltip, Legend);

// Define o componente funcional ExibicaoDados.
const ExibicaoDados = () => {
    // Define os estados para os pedidos, vendas por região e vendas por produto.
    const [pedidos, setPedidos] = useState([]);
    const [vendasPorRegiao, setVendasPorRegiao] = useState({});
    const [vendasPorProduto, setVendasPorProduto] = useState({});

    // Hook useEffect para buscar dados de pedidos quando o componente é montado.
    useEffect(() => {
        const fetchPedidos = async () => {
            try {
                // Faz uma solicitação GET para buscar todos os pedidos.
                const response = await axios.get('/api/Pedidos/getAll');
                // Atualiza o estado com os dados dos pedidos.
                setPedidos(response.data);

                // Inicializa objetos para armazenar vendas por região e por produto.
                const regiaoData = {};
                const produtoData = {};
                // Itera sobre os pedidos e acumula contagens por região e por produto.
                response.data.forEach(pedido => {
                    regiaoData[pedido.regiao] = (regiaoData[pedido.regiao] || 0) + 1;
                    produtoData[pedido.produto] = (produtoData[pedido.produto] || 0) + 1;
                });

                // Atualiza os estados com os dados acumulados.
                setVendasPorRegiao(regiaoData);
                setVendasPorProduto(produtoData);
            } catch (error) {
                // Loga um erro no console se a solicitação falhar.
                console.error('Erro ao buscar pedidos', error);
            }
        };

        // Chama a função para buscar os pedidos.
        fetchPedidos();
    }, []);

    return (
        <div>
            {/* Título principal da página */}
            <h1>Dados de Vendas</h1>
            {/* Contêiner para os gráficos */}
            <div className="chart-container">
                {/* Item de gráfico para vendas por região */}
                <div className="chart-item">
                    <h2>Vendas por Região</h2>
                    <Pie data={{
                        labels: Object.keys(vendasPorRegiao),
                        datasets: [{
                            data: Object.values(vendasPorRegiao),
                            backgroundColor: ['#FF6384', '#36A2EB', '#FFCE56', '#708090'],
                        }]
                    }} />
                </div>
                {/* Item de gráfico para vendas por produto */}
                <div className="chart-item">
                    <h2>Vendas por Produto</h2>
                    <Pie data={{
                        labels: Object.keys(vendasPorProduto),
                        datasets: [{
                            data: Object.values(vendasPorProduto),
                            backgroundColor: ['#FF6384', '#36A2EB', '#FFCE56'],
                        }]
                    }} />
                </div>
            </div>
            {/* Contêiner para a lista de pedidos */}
            <div>
                <h2>Lista de Pedidos</h2>
                <table>
                    <thead>
                        <tr>
                            <th>Cliente</th>
                            <th>Produto</th>
                            <th>Valor Final</th>
                            <th>Data de Entrega</th>
                            <th>CEP</th>
                            <th>Região</th>
                        </tr>
                    </thead>
                    <tbody>
                        {pedidos.map(pedido => (
                            <tr key={pedido.id}>
                                <td>{pedido.nomeRazaoSocial}</td> {/* Usando nomeRazaoSocial */}
                                <td>{pedido.produto}</td>
                                <td>{pedido.valorFinal}</td>
                                <td>{new Date(pedido.dataEntrega).toLocaleDateString()}</td>
                                <td>{pedido.cep}</td>
                                <td>{pedido.regiao}</td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>
        </div>
    );
};

// Exporta o componente ExibicaoDados como padrão para ser usado em outros arquivos.
export default ExibicaoDados;
