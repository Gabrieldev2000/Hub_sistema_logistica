import axios from 'axios';

const instance = axios.create({
    baseURL: 'http://localhost:5000' // Definindo a URL base com a porta padrão
});

export default instance;
