<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01//EN"
"http://www.w3.org/TR/html4/strict.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" lang="en">
	<head>
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
		<title>temp_gerador</title>
		<meta name="author" content="Rodrigo" />
		<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
  		<script type="text/javascript" src="http://www.parsecdn.com/js/parse-1.3.0.min.js"></script>
		<script>
			var ArrayDesafios;
			var ArrayAlunos;
			function getDesafios()
			{
				Parse.initialize("xawZDj8MECGTAKohaj6O59UWQbXdQ2BXj0LZK2cj", "F7kC68bXvIdEenn6qSjTj21dFn0QVVUyUU7gj8Cv");
				var user = Parse.User.current();
				var Desafio = Parse.Object.extend("Desafio");
					var query = new Parse.Query(Desafio);
					query.equalTo("responsavel", user);
					query.limit(1000);
					query.find({
					  success: function(results) 
					  {
					  	  ArrayDesafios = results;
					  	  console.log(results.length + 'Desafios Carregados');
					  	  getAlunos();
					  },
					  error: function(erro)
					  {
					  		console.log('Falha ao carregar desafios');
					  }
					});
			}
			function getAlunos()
			{

				var userQuery = new Parse.Query(Parse.User);
				userQuery.equalTo('ehProfessor', false);
				userQuery.limit(1000);
				userQuery.find({
					success: function(results)
					{
						ArrayAlunos = results;
						console.log(results.length + 'alunos carregados');
						gerarEstatisticas();
					},
					error: function(results)
					{
						console.log('falha ao carregar alunos');
					}
				});
			}
			function gerarEstatisticas()
			{
				var Estatistica = Parse.Object.extend("Estatistica");
				lengthAlunos = ArrayAlunos.length;
				lengthDesafios = ArrayDesafios.length;
				for(var i = 0; i < lengthAlunos; i++)
				{
					var alunoAtual = ArrayAlunos[i];
					for(var j = 0; j < lengthDesafios; j++)
					{
						var desafioAtual = ArrayDesafios[j];
						var novaEstatistica = new Estatistica();
						
						var tentativas = Math.floor(Math.random() * 5 + 1);
						console.log('aluno: ' + alunoAtual.get('nome') + '  -   Desafios: ' + desafioAtual.get('nome') + '   -   tentativas: ' + tentativas);
						for(var k = 0; k < tentativas; k++)
						{
							var pontuacao = Math.floor(Math.random() * 4);
							var tempo = Math.floor(Math.random()*15 + 15);
							novaEstatistica.set('aluno',alunoAtual);
							novaEstatistica.set('desafio',desafioAtual);
							novaEstatistica.set('pontuacao',pontuacao);
							novaEstatistica.set('tempo',tempo);
							novaEstatistica.save(null,{
								success: function(est)
								{
										
								},
								error: function(err)
								{
									console.log('erro');
								}
								
							});
						}
					}
				}
			}
			
		</script>
	</head>
	<body onload="getDesafios()">

	</body>
</html>

