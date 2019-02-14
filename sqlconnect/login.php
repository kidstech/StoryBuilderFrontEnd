<?php

	//Connect to server
	try
	{
		//mysql:host=url;dbname=databasename, user, pass
		$db = new PDO('mysql:host=localhost;dbname=storybuilder', 'root', 'root');
	}
	catch(Exeception $e)
	{
		//echo $e->getMessage();
		echo "1"; // Connection failed to db
		exit();
	}
	
	//Get username and password from the form we submitted.
	$username = $_POST["name"];
	$password = $_POST["password"];
	
	//Attempt to retrieve information on the account
	$stmt = $db -> prepare("SELECT * FROM accounts WHERE username = :username");
	$stmt -> bindValue(":username", $username);
	$stmt -> execute();
	
	//See if username exists
	if($stmt -> rowCount() <= 0)
	{
		echo "2"; //Name doesn't exist
		exit();
	}
	
	//Get that result
	$result = $stmt -> fetch(PDO::FETCH_ASSOC);
	
	//See if the password matches
	if($result['password'] != $password)
	{
		echo "2"; //Incorrect password
		exit();
	}
	
	//If we got this far we are logged in successfully!
	
	//Now get the list of teachers from the students
	$stmt = $db -> prepare("SELECT teacher FROM enrollment WHERE username = :username");
	$stmt -> bindValue(":username", $username);
	$stmt -> execute();
 	
	//Get that result
	$result = $stmt -> fetchAll();
	
	//Create a variable to store the finished array in
	$msg = "";
	
	//Loop through each row we found and add to message
	foreach($result as $row)
	{
		$msg .= ($row['teacher'] . ",");
	}
	
	//Return a list of all the teachers after removing the last comma
	echo rtrim($msg, ", ");
		
?>