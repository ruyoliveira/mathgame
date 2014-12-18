<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01//EN"
"http://www.w3.org/TR/html4/strict.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" lang="en">
	<head>
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
		<title>login</title>
		<meta name="author" content="Rodrigo" />
		<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
  		<script type="text/javascript" src="http://www.parsecdn.com/js/parse-1.3.0.min.js"></script>
		<link rel="stylesheet" type="text/css" href="<?php echo base_url() . 'css/bootstrap.css' ?>"/>
		<style type="text/css">
      body {
        padding-top: 40px;
        padding-bottom: 40px;
        background-color: #f5f5f5;
      }

      .form-signin {
        max-width: 300px;
        padding: 19px 29px 29px;
        margin: 0 auto 20px;
        background-color: #fff;
        border: 1px solid #e5e5e5;
        -webkit-border-radius: 5px;
           -moz-border-radius: 5px;
                border-radius: 5px;
        -webkit-box-shadow: 0 1px 2px rgba(0,0,0,.05);
           -moz-box-shadow: 0 1px 2px rgba(0,0,0,.05);
                box-shadow: 0 1px 2px rgba(0,0,0,.05);
      }
      .form-signin .form-signin-heading,
      .form-signin .checkbox {
        margin-bottom: 10px;
      }
      .form-signin input[type="text"],
      .form-signin input[type="password"] {
        font-size: 16px;
        height: auto;
        margin-bottom: 15px;
        padding: 7px 9px;
      }

    </style>
		<!-- Date: 2014-09-24 -->
		
		<script>
			function Login()
			{
				var username = document.getElementById("username").value;
				var password = document.getElementById("password").value;
				if(username == "" || password=="")
					alert('Confira seu UserName e Password');
				else
				{
					Parse.initialize("xawZDj8MECGTAKohaj6O59UWQbXdQ2BXj0LZK2cj", "F7kC68bXvIdEenn6qSjTj21dFn0QVVUyUU7gj8Cv");
					Parse.User.logIn(username, password, {
					  success: function(user) {
					    // Do stuff after successful login.
					    if(user.get('ehProfessor'))
					    	window.location = '<?php echo base_url() . 'index.php/'  ?>';
					    else
					    	{
					    		Parse.User.logOut();
					    		alert('Você não tem permição para entrar!');
					    		window.location = '<?php echo base_url() . 'index.php/welcome/login'  ?>';
					    	}
					  },
					  error: function(user, error) {
					    // The login failed. Check error to see why.
					    alert("Error: " + error.code + " " + error.message);
					  }
					});
				}
			}
			
		</script>
		
	</head>
	<body>
		
	<div class="container">
    <!--  <form style="font-size: 16px;height: auto;margin-bottom: 15px;padding: 7px 9px;">-->
        <h2 class="form-signin-heading">Please Log In</h2> 
        <input id="username" type="text" class="input-block-level" placeholder="Username">
        <input id="password" type="password" class="input-block-level" placeholder="Password">
        <!--<label class="checkbox">
          <input type="checkbox" value="remember-me"> Remember me
        </label>
       	-->
        <button class="btn btn-large btn-primary" onclick="Login()">Log In</button>
      <!--</form>-->
 </div> <!-- /container -->	
	
	</body>
</html>

