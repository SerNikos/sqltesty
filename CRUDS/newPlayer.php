<?php

    $con = mysqli_connect("localhost","root","root","playersinfo");

    if(mysqli_connect_errno())
    {
        echo "1";
        exit();
    }

    //wwwfrom Unity
    $username = $_POST['username'];
    $usernameClean = filter_var($username, FILTER_SANITIZE_EMAIL);

    $email = $_POST['email'];
    $emailClean = filter_var($email, FILTER_SANITIZE_EMAIL);

    $password = $_POST['password'];
    $passhash = password_hash($password, PASSWORD_DEFAULT); //security with hash

    
    //echo($passhash); //<- this is the prouf thatr is working

    //we ansure that we are communication with our app and no from an aunothoried system
    $appkey = $_POST['appassword'];

    if($appkey != "thisisfromtheapp!")
    {
        exit();
    }

    $usernamecheckquesry = "SELECT username FROM players WHERE username =  '" .$usernameClean. "'; ";
    $usernamecheck = mysqli_query($con, $usernamecheckquesry) or die("2"); //error code #2 - username check query failed


    if(mysqli_num_rows($usernamecheck) > 0)
    {
        echo "3"; //error code #3 - name exists
        exit();
    }

    $emailcheckquesry = "SELECT email FROM players WHERE email =  '" .$emailClean. "'; ";
    $emailcheck = mysqli_query($con, $emailcheckquesry) or die("4"); //error code #4 - email check query failed

    if(mysqli_num_rows($emailcheck) > 0)
    {
        echo "5"; 
        exit();
    }

    //Insert the data to phpmyadmin database
    $insertuserquesry = "INSERT INTO players (username, email, password) VALUES ('".$usernameClean."', '".$emailClean."', '".$passhash."');";
    mysqli_query($con, $insertuserquesry) or die("6"); //error code #6 - insert query failed
    echo("0" . " " ."New player added");

    $con -> close();

    //Error codes
    //1 -Databse connection failed
    //2 -Username check query failed
    //3 -Username already exists
    //4 -Email check query failed
    //5 -Email already exists
    //6 -Insert query failed
?>