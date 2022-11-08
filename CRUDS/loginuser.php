<?php


//EXOUME PROBLIMA ME TA PASSWORDS SUGKRINEI TA KANONIKA KAI OXI TO HASH MALLON


$con = mysqli_connect("localhost","root","root","playersinfo");
if(mysqli_connect_errno())
{
    echo "1";
    exit();
}

//we ansure that we are communication with our app and no from an aunothoried system
$appkey = $_POST['appassword'];

if($appkey != "thisisfromtheapp!")
{
    exit();
}

$username = $_POST['username'];
$usernameClean = filter_var($username, FILTER_SANITIZE_EMAIL);
$password = $_POST['password'];
//$passhash = password_hash($password, PASSWORD_DEFAULT); //security with hash

$usernamecheckquery = "SELECT * FROM players WHERE username =  '" .$usernameClean. "'; ";
$usernamecheckresult = mysqli_query($con, $usernamecheckquery) or die("2"); //error code #2 - username check query failed

if($usernamecheckresult->num_rows !=1)
{
    echo ("3"); //error code #3 - name exists
    exit();
}else {
    $fetchedpassword = mysqli_fetch_assoc($usernamecheckresult)["password"];     
    if(password_verify($password,$fetchedpassword))
    {
        echo("0" . " " ."Login successful");
        $playerinfo = "SELECT * FROM players WHERE username =  '" .$usernameClean. "'; ";
        $playerinfoResult = mysqli_query($con, $playerinfo) or die("5"); //error code #5 - email check query failed
       
        $existingPlayerInfo = mysqli_fetch_assoc($playerinfoResult);
        $playerUsername = $existingPlayerInfo["username"];
        $playerScore = $existingPlayerInfo["score"];
        echo($playerUsername . ":" . $playerScore);   
    }else {        
        echo("4");           
    }  
}

$con -> close();
                
//Error codes
    //1 -Databse connection failed
    //2 -Username check query failed
    //3 -Username not existing or is more than 1 in the table
    //4 -Password is incorrect
    //5 -Player info query failed
?>