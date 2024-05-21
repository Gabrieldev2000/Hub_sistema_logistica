// Importa as bibliotecas React e hooks useState.
import React, { useState } from 'react';
// Importa a configuração do axios.
import axios from '../axiosConfig';
// Importa o hook useNavigate do react-router-dom para navegação.
import { useNavigate } from 'react-router-dom';
// Importa o arquivo CSS para estilização.
import '../ImportacaoPedidos.css';

const ImportacaoPedidos = () => {
    // Define os estados para o arquivo, nome do arquivo e estado de carregamento.
    const [file, setFile] = useState(null);
    const [fileName, setFileName] = useState("Nenhum arquivo escolhido");
    const [isLoading, setIsLoading] = useState(false);
    // Inicializa o hook de navegação.
    const navigate = useNavigate();

    // Função para lidar com a mudança de arquivo.
    const handleFileChange = (e) => {
        // Atualiza o estado com o arquivo selecionado.
        setFile(e.target.files[0]);
        // Atualiza o nome do arquivo ou define um texto padrão se nenhum arquivo for escolhido.
        setFileName(e.target.files[0] ? e.target.files[0].name : "Nenhum arquivo escolhido");
    };

    // Função para lidar com o upload do arquivo.
    const handleUpload = async () => {
        if (!file) {
            // Alerta o usuário se nenhum arquivo for selecionado.
            alert('Por favor, selecione um arquivo antes de fazer o upload.');
            return;
        }

        // Cria um objeto FormData e adiciona o arquivo.
        const formData = new FormData();
        formData.append('file', file);
        setIsLoading(true); // Define o estado de carregamento como verdadeiro.

        try {
            // Faz a solicitação POST para enviar o arquivo para a API.
            await axios.post('/api/Pedidos/import', formData, {
                headers: {
                    'Content-Type': 'multipart/form-data'
                }
            });
            // Alerta o usuário sobre o sucesso da importação.
            alert('Pedidos importados com sucesso!');
            // Redireciona para a página de exibição de dados.
            navigate('/exibicao');
        } catch (error) {
            // Loga um erro no console e alerta o usuário se a importação falhar.
            console.error('Erro ao importar pedidos', error);
            alert('Erro ao importar pedidos');
        } finally {
            // Define o estado de carregamento como falso.
            setIsLoading(false);
        }
    };

    return (
        <div className="import-container">
            <h1>Importar Pedidos</h1>
            <p>
                Use esta página para importar pedidos através de arquivos excel CSV. 
                Certifique-se de que o arquivo está no formato correto antes de fazer o upload.
            </p>
            <div className="description">
                <p>
                    Bem-vindo ao sistema de importação de pedidos da HubCount! Aqui você pode carregar seus pedidos 
                    através de arquivos CSV. Verifique se o arquivo está no formato correto antes de fazer o upload.
                    Após a importação, você poderá visualizar os dados importados na página de Exibição de Dados.
                </p>
                <p>
                    Para começar, clique em "Escolher arquivo" para selecionar o arquivo desejado e em seguida clique em "Carregar" para importar os pedidos.
                </p>
            </div>
            <div className="input-group">
                <label htmlFor="file-upload" className="file-input-label">
                    {/* Input para seleção do arquivo */}
                    <input 
                        id="file-upload"
                        type="file" 
                        onChange={handleFileChange} 
                        accept=".csv, application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, application/vnd.ms-excel" 
                        className="file-input"
                    />
                    {/* Exibe o nome do arquivo selecionado */}
                    {fileName}
                </label>
                {/* Botão para carregar o arquivo */}
                <button onClick={handleUpload} className="upload-button" disabled={isLoading}>
                    {isLoading ? 'Carregando...' : 'Carregar'}
                </button>
            </div>
            <div className="download-link">
                {/* Link para baixar a planilha de exemplo */}
                <a href="../Planilha_exemplo.csv" download>Baixar Planilha de Exemplo</a>
            </div>
        </div>
    );
};

// Exporta o componente ImportacaoPedidos como padrão para ser usado em outros arquivos.
export default ImportacaoPedidos;
