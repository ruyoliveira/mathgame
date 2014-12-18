/**
 * @author Rodrigo
 */
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
function Desafios()
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
		  	 // alert("Successfully retrieved " + results.length + " scores.");
		   	  var html = '<table class="table"><tbody>';
		   	  html+='<tr><th>Desafio</th><th>Tema</th><th>Nível</th></tr>';
		      for (var i = 0; i < results.length; i++) { 
		          var object = results[i];
		          html+= '<tr onMouseOver="this.bgColor =' + "'violet'" + ';"'
		          		+ ' onMouseOut="this.bgColor =' + "'#FFFFFF'" + ';"'
		           + 'onclick="abrirPaginaEstatistica(' + "'" +object.id + "'" + ')"><td>' + object.get('nome') + '</td><td>' + object.get('tema') + '</td><td>' + object.get('nivel') + '</td></tr>';        //<td id="tableRow' + i + '">' + 'Loading...' + '</td></tr>';
		       }
		       
		       html+='</tbody></table>';
		     //  alert(html);
		       document.getElementById('desafiosTable').innerHTML = html;
		       
				//ContaAlunos(results, 0);
		      
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
function abrirPaginaEstatistica(idDesafio)
{
	window.open('<?php echo base_url() . 'index.php/welcome/desafios/' ?>' + idDesafio);
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
					   	  var data = new google.visualization.DataTable();
					   	  data.addColumn('string', 'Aluno');
					   	  data.addColumn('number', 'Pontuação');
					   	  data.addColumn('number','Tempo');
					   	  data.addColumn('number','TimeStamp');
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
					          			 data.addRows([[nome, pontuacao, tempo, timeStamp]]);		 
					         		}
					      			var table = new google.visualization.Table(document.getElementById('EstatisticaTable'));
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
