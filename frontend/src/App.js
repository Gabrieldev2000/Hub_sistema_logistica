// Importa a biblioteca React, que é usada para construir interfaces de usuário.
import React from 'react';
// Importa componentes de roteamento do React Router DOM.
import { Routes, Route, Link } from 'react-router-dom';
// Importa o componente ExibicaoDados.
import ExibicaoDados from './components/ExibicaoDados';
// Importa o componente ImportacaoPedidos.
import ImportacaoPedidos from './components/ImportacaoPedidos';
// Importa o arquivo CSS para estilização da aplicação.
import './App.css';

// Define o componente funcional App, que é o componente principal da aplicação.
function App() {
  return (
    // Elemento raiz da aplicação.
    <div>
      {/* Define a barra de navegação. */}
      <nav>
        <ul>
          {/* Define um item de lista com um link para a página de importação de pedidos. */}
          <li>
            <Link to="/importacao">Importação de Pedidos</Link>
          </li>
          {/* Define um item de lista com um link para a página de exibição de dados. */}
          <li>
            <Link to="/exibicao">Exibição de Dados</Link>
          </li>
        </ul>
      </nav>

      {/* Define um contêiner para os componentes de rota. */}
      <div className="container">
        {/* Define as rotas para a navegação da aplicação. */}
        <Routes>
          {/* Define a rota para o componente ImportacaoPedidos. */}
          <Route path="/importacao" element={<ImportacaoPedidos />} />
          {/* Define a rota para o componente ExibicaoDados. */}
          <Route path="/exibicao" element={<ExibicaoDados />} />
          {/* Define a rota para a página inicial com uma imagem, título e descrição. */}
          <Route 
            path="/" 
            element={
              <div className="pagina-inicial">
                {/* Exibe uma imagem inicial com uma classe de estilização. */}
                <img src="/imagem-inicial.png" alt="Imagem Inicial" className="imagem-inicial" />
                {/* Exibe um título de boas-vindas. */}
                <h1>Seja bem vindo!</h1>
                {/* Exibe uma descrição do sistema com uma classe de estilização. */}
                <p className="descricao-sistema">
                  Este sistema permite importar pedidos através de arquivos CSV e exibir os dados dos pedidos de forma clara e organizada.<br/>
                  Clique em Importação de Pedidos para começar.
                </p>
              </div>
            } 
          />
        </Routes>
      </div>
    </div>
  );
}

// Exporta o componente App como padrão para ser usado em outros arquivos.
export default App;
