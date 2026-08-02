REM cd E:\Software

java  -Dwebdriver.ie.driver=IEDriverServer.exe -Dwebdriver.chrome.driver=chromedriver.exe -Dwebdriver.gecko.driver=geckodriver.exe -jar selenium-server-standalone-3.13.0.jar -role node -nodeConfig grid.json -hub http://localhost:4444/grid/register

pause>null