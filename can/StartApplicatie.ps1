$splash = @"          

/  __ \               | |    | (_)               
| /  \/ __ _ _ __   __| | ___| |___   _____ _ __ 
| |    / _` | '_ \ / _` |/ _ \ | \ \ / / _ \ '__|
| \__/\ (_| | | | | (_| |  __/ | |\ V /  __/ |   
 \____/\__,_|_| |_|\__,_|\___|_|_| \_/ \___|_|   
 
"@
write-host $splash -foreground "yellow"

echo 'Belangrijk! Heb je al een keer Candeliver gestart?';
echo 'Bij een nieuwe versie is het aanbevolen eerst de oude containers te verwijderen en de databases.';
echo 'En de snapshot opnieuw in te laden.';
echo ' ';
PAUSE Enter to continue
echo ' ';

$removeContainers = Read-Host 'Wil je ALLE docker containers stoppen en verwijderen? (y/n)'
echo ' ';
$loadSnapshot = Read-Host 'Wil je de snapshot inladen (demo events inladen)? (y/n)'
echo ' ';

if ($removeContainers -eq 'y')  {
 docker stop $(docker ps -a -q)
 docker rm $(docker ps -a -q)
 docker rmi $(docker images -q) -f
 echo ' ';
} 

echo '-------------- Start Eventbus --------------'
echo ' ';
start powershell {docker-compose -f eventbus-docker-compose.yml up}

if ($removeContainers -eq 'y') {
	timeout /t 200 /nobreak
} else {
	timeout /t 15 /nobreak
}

if ($loadSnapshot -eq 'y')  {
	echo '-------------- Create/load Snapshot --------------'
	echo ' ';
	start powershell {docker-compose -f snapshot-docker-compose.yml up}
	timeout /t 60 /nobreak
}

echo ' ';
echo '-------------- Services worden gestart! --------------'

cd ../
cd CAN.BackOffice\src\CAN.BackOffice\bin\Release
start  docker-compose up -d

cd ../../../../../
cd CAN.Bestellingbeheer\CAN.Bestellingbeheer\src\CAN.Bestellingbeheer.Facade\bin\Release
start  docker-compose up -d

cd ../../../../../../
cd CAN.Klantbeheer\CAN.Klantbeheer\src\CAN.Klantbeheer.Facade\bin\Release
start  docker-compose up -d

cd ../../../../../../
cd CAN.Webwinkel\src\CAN.Webwinkel\bin\Release
start  docker-compose up -d

cd ../../../../../
cd CAN.AuthenticatieService\src\CAN.Candeliver.BackOfficeAuthenticatie\bin\Release
start  docker-compose up -d

cd ../../../../../
cd CAN.WinkelmandjeBeheer\CAN.WinkelmandjeBeheer\src\CAN.WinkelmandjeBeheer.Facade\bin\Release
start  docker-compose up -d

if ($removeContainers -eq 'y') {
	timeout /t 300 /nobreak
} else {
	timeout /t 15 /nobreak
}

echo ' ';
echo 'Webwinkel beschikbaar op http://localhost:11500'
echo 'Backoffice beschikbaar op http://localhost:11200'
echo ' ';

start microsoft-edge:http://localhost:11500
start microsoft-edge:http://localhost:11200

$end = @"
 -------- __@      __@       __@       __@      __~@
 ----- _`\<,_    _`\<,_    _`\<,_     _`\<,_    _`\<,_
 ---- (*)/ (*)  (*)/ (*)  (*)/ (*)  (*)/ (*)  (*)/ (*)
 ~~~~~~~~~~~~~~~~~~~~ Candeliver ~~~~~~~~~~~~~~~~~~~~
"@

write-host $end -foreground "yellow"
PAUSE Enter to exit