<h1>Desafio Manipulaê</h1>
<p>Para realizar a autenticação no header use: <code>Authorization: Bearer <token></code></p>
<h3>Primeiros passos para executar o projeto</h3>
<hr/>
<ul>
    <li>
        Clone o repositório <code>git clone https://github.com/viniciusoliverrs/DesafioManipulae</code>
    </li>
    <li>
        Abra um console dentro da pasta DesafioManipulae/DasafioManipulae.API
    </li>
    <li>
        Execute o comando <code>dotnet watch run</code>
    </li>
    <li>
        No navegador entre no endereço <code>http://localhost:5000/swagger</code>
    </li>
</ul>
<h3>Rotas:</h3>
<hr/>
/Auth/Login - POST - Parâmetros de entrada: String userName, String password - Retorno: String token
<br/>
/Auth/Register - POST - String titulo, String autor, String descricao, int duracao, DateTime publicadoEm
<br/>
/Video - GET - Retorna todos os videos.
<br/>
/Video/ - GET Parâmetros de entrada: int IdVideo - Retorno: Retorna o video com o IdVideo indicado.
<br/>