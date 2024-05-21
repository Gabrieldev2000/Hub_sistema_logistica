import React from 'react';
import { createRoot } from 'react-dom/client'; // Importar a nova API de renderização do React 18
import { BrowserRouter } from 'react-router-dom';
import App from './App';

// Selecionar o elemento raiz do DOM
const container = document.getElementById('root');
const root = createRoot(container);

// Renderizar o aplicativo usando a nova API createRoot
root.render(
  <BrowserRouter>
    <App />
  </BrowserRouter>
);
