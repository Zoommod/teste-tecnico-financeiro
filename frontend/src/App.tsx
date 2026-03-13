import { useEffect } from 'react';
import { pessoaService } from './services/pessoaService';

function App() {
  useEffect(() => {
    pessoaService.obterTodos()
      .then((data) => {
        console.log('✅ Conexão bem-sucedida! Dados recebidos da API:', data);
      })
      .catch((error) => {
        console.error('❌ Falha na conexão com a API:', error);
      });
  }, []);

  return (
    <div style={{ padding: '20px', fontFamily: 'sans-serif' }}>
      <h1>Teste de Integração: Front-end - Back-end</h1>
    </div>
  );
}

export default App;