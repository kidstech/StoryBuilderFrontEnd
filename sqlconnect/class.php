<?php

	//Connect to server.
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

	//Get username & teacher name
	$username = $_POST["name"];
	$teacher = $_POST["teacher"];
	//echo $username . $teacher;

	// Create a prepared query, which prevents SQL Injections from happening.
	// This should be researched to actually see if this can be used to dump information. It *shouldn't* because it is still being prepared.
	//Worth looking at PDO's DSN charset parameter
	$raw_sql = "SELECT * FROM :teacher WHERE username = :username";
	
	$stmt = $db -> prepare($raw_sql);
	$stmt -> bindParam(":teacher", $teacher);
	$stmt -> bindParam(":username", $username);
	$stmt -> execute();
	
	//See if username exists in the classroom
	if($stmt -> rowCount() <= 0)
	{
		echo "2"; //Student not in class
		exit();
	}

	//Get that result
	$result = $stmt -> fetch(PDO::FETCH_ASSOC);

	//TODO
	
?>﻿