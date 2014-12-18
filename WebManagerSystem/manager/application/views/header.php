<!DOCTYPE html>
<html lang="en">
	<head>
		<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
  		<script type="text/javascript" src="http://www.parsecdn.com/js/parse-1.3.0.min.js"></script>
		<script type="text/javascript" src="https://www.google.com/jsapi?autoload={'modules':[{'name':'visualization','version':'1','packages':['table','corechart']}]}"></script>
		<script src="//code.jquery.com/jquery-1.10.2.js"></script>
  		<script src="//code.jquery.com/ui/1.11.2/jquery-ui.js"></script>
		<meta charset="utf-8">

		<!-- Always force latest IE rendering engine (even in intranet) & Chrome Frame
		Remove this if you use the .htaccess -->
		<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">

		<title>header</title>
		<meta name="description" content="">
		<meta name="author" content="Rodrigo">

		<meta name="viewport" content="width=device-width, initial-scale=1.0">
		
		<link rel="stylesheet" type="text/css" href="<?php echo base_url() . 'css/bootstrap.css' ?>"/>
		<!-- Replace favicon.ico & apple-touch-icon.png in the root of your domain and delete these references -->
		<link rel="shortcut icon" href="/favicon.ico">
		<link rel="apple-touch-icon" href="/apple-touch-icon.png">
		<script>
			//google.load("visualization", "1", {packages:["corechart"]});
			var table;
			var data;
			var DesafiosInfo = {};
			var EstatisticasInfo = {};
			var AlunosInfo = {};
			function LogOut()
			{
				Parse.User.logOut();
				window.location = '<?php echo base_url() . 'index.php/welcome/login' ?>';
			}
			function Verificar()
			{	
				Parse.initialize("xawZDj8MECGTAKohaj6O59UWQbXdQ2BXj0LZK2cj", "F7kC68bXvIdEenn6qSjTj21dFn0QVVUyUU7gj8Cv");
				var user = Parse.User.current();
				if(user)
				{
					//alert(user.get("nome"));
					return (true);
					//Parse.User.logOut();	
				}
				else
				{
					
					window.location = '<?php echo base_url() . 'index.php/welcome/login' ?>';	
					return (false);
				}
			}
			function Desafios(plotar)
			{
				if(Verificar())
				{
					var user = Parse.User.current();
					
					var Desafio = Parse.Object.extend("Desafio");
					var query = new Parse.Query(Desafio);
					query.equalTo("responsavel", user);
					query.limit(1000);
					query.find({
					  success: function(results) 
					  {
					  	 // alert("Successfully retrieved " + results.length + " scores.")
					   	  data = new google.visualization.DataTable();
					   	  data.addColumn('string','Desafio');
					   	  data.addColumn('string','Tema');
					   	  data.addColumn('number','Nível');
					      for (var i = 0; i < results.length; i++) { 
					          var object = results[i];
					          var id = object.id;
					          var nome = object.get('nome');
					          var tema = object.get('tema');
					          var nivel = object.get('nivel');
					          var _tempoMaximo = object.get('tempoMaximo');
					          console.log(tema);
					          DesafiosInfo[id] = {Nome: nome,Tema: tema,pontuacaoTotal: 0,tempoTotal: 0, quantidade:0, tempoMaximo: _tempoMaximo};
					          data.addRows([[{v:id, f: nome},tema,nivel]]);
					       }
					       if(plotar)
					       {
					       table = new google.visualization.Table(document.getElementById('desafiosTable'));
					       google.visualization.events.addListener(table, 'select', abrirPaginaEstatistica);
					       table.draw(data, {showRowNumbers: true});
					       CarregaGraficosDesafios(true);
					      }
					       else
					       {
					      		CarregaGraficosDesafios(false); 	
					       }
					  },
					  error: function(error) {
					    alert("Error: " + error.code + " " + error.message);
					  }
					});
				}
			}
/* ---------------------------------------------------------------------------------------------
			function ContaAlunos(results, j)
			{
				//alert("j:" +j + "   l: " + results.length);
				if(j < results.length)
				{
			       	var inQuery = results[j].relation('alunos').query();
			          //----------------------------------------
			          inQuery.find({
								  success: function(resultsO) {
										//alert(resultsO.length);
										var _id = "tableRow" + j;
										//alert(_id);
										document.getElementById(_id).innerHTML = resultsO.length;
										
										ContaAlunos(results, j+1);
								  },
								  error: function(error) {
								    alert("Error: " + error.code + " " + error.message);
								    var _id = "tableRow" + j;
									document.getElementById(_id).innerHTML = "ERRO";
									
									ContaAlunos(results, j+1);
								  }
								});
				}
			}
---------------------------------------------------------------------------------------------*/
			function CadastrarDesafio()
			{
				var nomeDesafio = document.getElementById('NomeDesafioCadastro').value;
				var temaDesafio = document.getElementById('TemaDesafioCadastro').value;
				var nivelDesafio = document.getElementById('NivelDesafioCadastro').value;
				if(nomeDesafio != "" && temaDesafio != "")
				{
					if(Verificar())
					{
						var user = Parse.User.current();
						var Desafio = Parse.Object.extend("Desafio");
						var desafio = new Desafio();
						desafio.set('nome', nomeDesafio);
						desafio.set('tema', temaDesafio);
						desafio.set('nivel', parseInt(nivelDesafio));
						desafio.set('responsavel', user);
						desafio.save(null, {
							  success: function(result) {
							    // Execute any logic that should take place after the object is saved.
							    Desafios();
							  },
							  error: function(result, error) {
							    // Execute any logic that should take place if the save fails.
							    // error is a Parse.Error with an error code and message.
							    alert('Erro ao cadastrar desafio: ' + error.message);
							  }
							});
					}
				}
				else
					alert('Complete os campos corrretamente.');
			}
			function abrirPaginaEstatistica()
			{
				var selection = table.getSelection();
				if(selection.length > 0)
				{
					var idDesafio = data.getValue(selection[0].row ,0);
					table.setSelection();
					window.open('<?php echo base_url() . 'index.php/welcome/desafios/' ?>' + idDesafio);
				}
				
			}
			function CarregarEstatistica(idDesafio)
			{
				if(Verificar())
				{
					var user = Parse.User.current();
					var Desafio = Parse.Object.extend("Desafio");
					var Estatistica = Parse.Object.extend("Estatistica");
					var queryDesafio= new Parse.Query(Desafio);
					queryDesafio.equalTo('objectId', idDesafio);					
					queryDesafio.first({
						  success: function(obj) {
						    	var query = new Parse.Query(Estatistica);
								query.equalTo('desafio', obj);
								query.limit(100);
								query.find({
								  success: function(results) 
								  {
								  	 // alert("Successfully retrieved " + results.length + " scores.");
								   	  data = new google.visualization.DataTable();
								   	  data.addColumn('string', 'Aluno');
								   	  data.addColumn('number', 'Pontuação');
								   	  data.addColumn('number','Tempo');
								   	  data.addColumn('datetime','Data');
								   	  var queryAlunos = new Parse.Query(Parse.User);
								   	  queryAlunos.equalTo('ehProfessor', false);
								   	  queryAlunos.find({
								   	  		success: function(alunos)
								   	  		{
								   	  			var arrayDeAlunos = {};
								   	  			//alert(alunos.length);
								   	  			for(var i = 0; i < alunos.length; i++)
								   	  			{
								   	  				arrayDeAlunos[alunos[i].id] = alunos[i].get('nome');
								   	  			}
								   	  			for (var i = 0; i < results.length; i++) { 
								      		   		 var object = results[i];
								      		   		 var nome = arrayDeAlunos[object.get('aluno').id];
								      		   		 var pontuacao = object.get('pontuacao');
								      		   		 var tempo = object.get('tempo');
								      		   		 var _date = object.createdAt;
								          			 var timeStamp = parseInt(((_date.getTime() - Date.parse("January 1, 2014"))/1000));
								          			 data.addRows([[nome, pontuacao, tempo, _date]]);		 
								         		}
								      			table = new google.visualization.Table(document.getElementById('EstatisticaTable'));
								       			table.draw(data,  {showRowNumber: true});
								       
													//ContaAlunos(results, 0);
								      
								   	  		},
								   	  		error: function(err)
								   	  		{
								   	  			 alert("Error: " + err.code + " " + err.message);
								   	  		}
								   	  });
								      
								  },
								  error: function(error) {
								    alert("Error: " + error.code + " " + error.message);
								  }
								});
						  },
						  error: function(error) {
						    alert("Error: " + error.code + " " + error.message);
						  }
						});
				}
			}
			function CarregaGraficosDesafios(plotarGraficos){
				if(Verificar()){
					var Estatistica = Parse.Object.extend("Estatistica");
					var query = new Parse.Query(Estatistica);
					var dataEstatisticas = new google.visualization.DataTable();
					dataEstatisticas.addColumn('string','Desafio');
					dataEstatisticas.addColumn('number','Pontuação Média');
					dataEstatisticas.addColumn('number','Tempo Médio');
					query.limit(1000);
					query.find({
						success: function(estatisticas){
							var length = estatisticas.length;
							for(var i = 0;i < length; i++){
								var estatistica = estatisticas[i];
								var id = estatistica.get('desafio').id;
								////console.log(id);
								var tempo = estatistica.get('tempo');
								if(DesafiosInfo[id] != undefined){
									var pontuacao = estatistica.get('pontuacao');
									//console.log(pontuacao);
									DesafiosInfo[id].pontuacaoTotal = DesafiosInfo[id].pontuacaoTotal + pontuacao;
									DesafiosInfo[id].tempoTotal += tempo;
									DesafiosInfo[id].quantidade++;
								}
								else{
									//console.log("erro");
								}
							}
							if(plotarGraficos)
							{
								var formatter = new google.visualization.NumberFormat({fractionDigits:2});
								for(var obj in DesafiosInfo){
									var mPontuacao = DesafiosInfo[obj].pontuacaoTotal*100.0/(3.0*DesafiosInfo[obj].quantidade);
									var mTempo = DesafiosInfo[obj].tempoTotal*100.0/(DesafiosInfo[obj].quantidade*DesafiosInfo[obj].tempoMaximo);
									//var _temp = "";
									//_temp += DesafiosInfo[obj].Nome + " - " + mPontuacao + " - " + mTempo;
									//console.log(_temp);
									dataEstatisticas.addRows([[DesafiosInfo[obj].Nome, {v: mPontuacao,f: formatter.formatValue(mPontuacao) + " %"}, {v: mTempo, f: formatter.formatValue(mTempo) + " %"}]]);
								}
								var options = {
		        				   title: 'Desafios',
		       					   vAxis: {title: 'Desafio',  titleTextStyle: {color: 'red'}},
						           hAxis: {ticks: [0,20,40,60,80,100]}
						        };
		
								var chart = new google.visualization.BarChart(document.getElementById('Div_GraficoDeBarras'));
								chart.draw(dataEstatisticas, options);	
							}
							else{
								carregarEstatisticasInfo();
							}
							//console.log("CarregarGraficosDesafios OK  ... Plot: " + plotarGraficos);	
						},
						error: function(error) {
						    alert("Error: " + error.code + " " + error.message);
						}
					});
				}
			}
			function carregarEstatisticasInfo(){
				var Estatistica = new Parse.Object.extend("Estatistica");
				var query = new Parse.Query(Estatistica);
				query.limit(1000);
				query.find({
					success: function(estatisticas){
						var length = estatisticas.length;
						for(var i = 0; i<length; i++)
						{
							var obj = estatisticas[i];
							var _alunoId = obj.get('aluno').id;
							var _desafioId = obj.get('desafio').id;
							var _pontuacao = obj.get('pontuacao');
							var _tempo = obj.get('tempo')*100.0/DesafiosInfo[obj.get('desafio').id].tempoMaximo;
							var _timeStemp = obj.createdAt.getTime();
							
							var _tema = DesafiosInfo[obj.get('desafio').id].Tema;
							console.log(_tema);
							EstatisticasInfo[obj.id] = {alunoId: _alunoId, desafioId: _desafioId, pontuacao: _pontuacao, tempo: _tempo, timeStemp: _timeStemp};
							if(AlunosInfo[_alunoId] == undefined)
							{
								//console.log("aluno add: " + _alunoId);
								AlunosInfo[_alunoId] = {Nome: "---", 
														star0: 0, 
														star1: 0, 
														star2: 0, 
														star3: 0,
														pontuacaoTotal:0,
														nJogadas: 0,
														tempoTotal:0,
														temasInfo: {}};
							}
							AlunosInfo[_alunoId]['star' + _pontuacao]++;
							AlunosInfo[_alunoId].nJogadas++;
							AlunosInfo[_alunoId].pontuacaoTotal += _pontuacao;
							AlunosInfo[_alunoId].tempoTotal += _tempo;
							if(AlunosInfo[_alunoId].temasInfo[_tema] == undefined)
							{
								console.log(_tema);
								AlunosInfo[_alunoId].temasInfo[_tema] = {pontuacaoTotal:0,
																		nJogadas: 0,
																		tempoTotal:0,}	
							}
							AlunosInfo[_alunoId].temasInfo[_tema].pontuacaoTotal += _pontuacao;
							AlunosInfo[_alunoId].temasInfo[_tema].tempoTotal += _tempo;
							AlunosInfo[_alunoId].temasInfo[_tema].nJogadas++;
						}
						carregarAlunosInfo();
					},
					error: function(erro){
						
					}
				});
			}
			function carregarAlunosInfo()
			{
				//console.log('carregando alunosInfo');
				data = new google.visualization.DataTable();
				data.addColumn('string','Nome');
				data.addColumn('number','Pontuação Média');
				data.addColumn('number','Tempo Médio');
				//data.addColumn('number','Desafios Concluídos');
				data.addColumn('number','Total de Jogadas');
				
				//-------------------------------------------
				dataTemas = new google.visualization.DataTable();
				dataTemas.addColumn('number','Pontuação Média');
				dataTemas.addColumn('number','Tempo Médio');
				//-------------------------------------------
				var arrayOfQueries = new Array();
				var i = 0;
				var mainQuery = new Parse.Query(Parse.User);
				for(var _id in AlunosInfo){
					//console.log(_id);
					if(i==0)
					{
						mainQuery = new Parse.Query(Parse.User);
						mainQuery.equalTo('objectId', _id);
					}
					else
					{
						var query = new Parse.Query(Parse.User);
						query.equalTo('objectId', _id);
						mainQuery = Parse.Query.or(mainQuery, query);
					}
					i++;	
				}
				mainQuery.limit(1000);
				mainQuery.find({
					success: function(alunos){
						var formatter = new google.visualization.NumberFormat({fractionDigits:2});
						var length = alunos.length;
						for(var j = 0; j<length; j++)
						{
							var obj = alunos[j];
							AlunosInfo[obj.id].Nome = obj.get('nome');
							//console.log(AlunosInfo[obj.id].Nome + " - " + AlunosInfo[obj.id].star0 + " - " + AlunosInfo[obj.id].star1 + " - " + AlunosInfo[obj.id].star2 + " - " + AlunosInfo[obj.id].star3);
							var _nome = AlunosInfo[obj.id].Nome;
							var _pontuacaoMedia = AlunosInfo[obj.id].pontuacaoTotal*100.0/(3.0*AlunosInfo[obj.id].nJogadas);
							var _tempoMedio = AlunosInfo[obj.id].tempoTotal/AlunosInfo[obj.id].nJogadas;
							var _desafiosConcluidos = 0;
							var _nJogadas = AlunosInfo[obj.id].nJogadas;
							data.addRows([[{v: obj.id, f: _nome}, {v: _pontuacaoMedia, f: formatter.formatValue(_pontuacaoMedia) + " %"}, {v:_tempoMedio, f: formatter.formatValue(_tempoMedio) + " %"}, _nJogadas]]);
						}
						table = new google.visualization.Table(document.getElementById('tabelaAlunos'));
						google.visualization.events.addListener(table, 'select',carregaGraficoAluno);
						table.draw(data,  {showRowNumber: true});
						
						var dataAluno = google.visualization.arrayToDataTable([
			         	    ['Pontuação', 'Valor'],
			          		['0 Estrela',1],
			                ['1 Estrela',0],
			                ['2 Estrelas',0],
			                ['3 Estrelas',0],
			                ]);
			
				        var options = {
				          title: 'Aluno:  - - - - - - - - '
				        };
				        var chart = new google.visualization.PieChart(document.getElementById('graficoAluno'));
				        chart.draw(dataAluno, options);
				    	//----------------------
				    	var barChart = new google.visualization.ColumnChart(document.getElementById('graficoPorTemasAluno'));
				    	var dataBarChart = google.visualization.arrayToDataTable([
				    		['Tema', 'Pontuação Média', 'Tempo Médio'],
				    		['- - - -', 0, 0]
				    	]);
				    	
				    	options = {
						    //title: 'Company Performance',
						    hAxis: {title: 'Tema', titleTextStyle: {color: 'red'}},
						    vAxis: {ticks: [0,20,40,60,80,100]}
						  };
						barChart.draw(dataBarChart, options);
						  
				    	//----------------------    
					        
						
					},
					error: function(erro){
						
					}
				});
			}
			function carregaGraficoAluno()
			{
				var selection = table.getSelection();
				if(selection.length > 0)
				{
					//$('#tabelaAlunos').effect("size", {to: {width: 200}}, 1000);
					var idAluno = data.getValue(selection[0].row ,0);
					table.setSelection();
					//alert(idAluno);
					var dataAluno = google.visualization.arrayToDataTable([
			          ['Pontuação', 'Valor'],
			          ['0 Estrela',AlunosInfo[idAluno].star0],
			          ['1 Estrela',AlunosInfo[idAluno].star1],
			          ['2 Estrelas',AlunosInfo[idAluno].star2],
			          ['3 Estrelas',AlunosInfo[idAluno].star3],
			        ]);
			
			        var options = {
			          title: 'Aluno: ' + AlunosInfo[idAluno].Nome
			        };
			
			        var chart = new google.visualization.PieChart(document.getElementById('graficoAluno'));
			
			        chart.draw(dataAluno, options);
			        var dataTemas = new google.visualization.DataTable();
			        dataTemas.addColumn('string', 'Tema');
			        dataTemas.addColumn('number', 'Pontuação Média');
			        dataTemas.addColumn('number', 'Tempo Médio');
			        var formatter = new google.visualization.NumberFormat({fractionDigits:2});
			        for(var tema in AlunosInfo[idAluno].temasInfo)
			        {
			        	console.log(tema);
			        	var _temaPontuacaoMedia = AlunosInfo[idAluno].temasInfo[tema].pontuacaoTotal*100.0/(3.0* AlunosInfo[idAluno].temasInfo[tema].nJogadas);
			        	var _temaTempoMedio = AlunosInfo[idAluno].temasInfo[tema].tempoTotal/ AlunosInfo[idAluno].temasInfo[tema].nJogadas;
			        	
			        	dataTemas.addRows([[tema,{v: _temaPontuacaoMedia, f: formatter.formatValue(_temaPontuacaoMedia) + "%"}, {v:_temaTempoMedio, f: formatter.formatValue(_temaTempoMedio) + "%"}]]);	
			        }
			        var barChart = new google.visualization.ColumnChart(document.getElementById('graficoPorTemasAluno'));
			        options = {
						    //title: 'Company Performance',
						    hAxis: {title: 'Tema', titleTextStyle: {color: 'red'}},
						    vAxis: {ticks: [0,20,40,60,80,100]}
						  };
					barChart.draw(dataTemas, options);
			        
				}
			}
			
		</script>
		<nav class="navbar navbar-default navbar-fixed-top" role="navigation">
 			 <div class="container">
    			<button onclick="location.href='<?php echo base_url() . 'index.php/' ?>'" type="button" class="btn btn-default navbar-btn">Home</button>
  				<button onclick="location.href='<?php echo base_url() . 'index.php/welcome/desafios' ?>'" type="button" class="btn btn-default navbar-btn">Desafios</button>
  				<button onclick="location.href='<?php echo base_url() . 'index.php/welcome/alunos' ?>'" type="button" class="btn btn-default navbar-btn">Alunos</button>
  				<button type="button" class="btn btn-default navbar-btn" onclick="LogOut()">LogOut</button>
  		</div>
		</nav>
		
		
	</head>
			
