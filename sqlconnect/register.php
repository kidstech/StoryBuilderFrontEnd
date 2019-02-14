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
		echo "1: connection to database failed! Is the server running?"; // Connection failed to db
		exit();
	}

	//Get username and password from the form we submitted.
	$username = $_POST["name"];
	$password = $_POST["password"];

	//Create a prepared query, which prevents SQL Injections to happen.
	$stmt = $db -> prepare("SELECT username FROM accounts WHERE username = :username");
	$stmt -> bindParam(':username', $username);
	$stmt -> execute();

	//See if that name already exists.
	if($stmt -> rowCount() > 0)
	{
		echo "2: username already exists!"; //Name already exists.
		exit();
	}

	//If the name doesn't exist already, then we can insert it in.
	$stmt = $db -> prepare("INSERT INTO accounts (username, password) VALUES (:username, :password)");
	$stmt -> bindValue(':username', $username);
	$stmt -> bindValue(':password', $password);
	$stmt -> execute();

	//Nothing went wrong, yay!
	echo "0";
	
?>﻿