set HERE=%CD%
set JAVA_HOME=%JAVA_HOME%
set PATH=%JAVA_HOME%\jre\bin;%JAVA_HOME%\bin;%PATH%
set SELENIUM_VERSION=3.8.1
set PLATFORM=WINDOWS

set CHROME_VERSION=62
set CHROME_DRIVER_LOC=%HERE%/chromedriver.exe
set CHROME_BINARY_LOC=C:\Program Files (x86)\Google\Chrome\Application

set FIREFOX_VERSION=57
set FIREFOX_DRIVER_LOC=%HERE%/geckodriver.exe
set FIREFOX_BINARY_LOC=C:\Program Files\Mozilla Firefox

set IE_VERSION=11
set IE_DRIVER_LOC=%HERE%/IEDriverServer.exe

set HUB_HOST=localhost
set HUB_PORT=5555
set HUB_URL=http://%HUB_HOST%:%HUB_PORT%/grid/register

set NODE_HOST=localhost
set NODE_PORT=5556

start java -jar selenium-server-standalone-%SELENIUM_VERSION%.jar -role hub -port %HUB_PORT%
start java -jar selenium-server-standalone-3.8.1.jar -role node -hub %HUB_URL% -port %NODE_PORT% -browser "browserName=firefox,version=%FIREFOX_VERSION%,firefox_bin=%FIREFOX_BINARY_LOC%,maxInstances=5,platform=%PLATFORM%" -browser "browserName=chrome,version=%CHROME_VERSION%,chrome_binary=%CHROME_BINARY_LOC%,maxInstances=5,platform=%PLATFORM%" -browser "browserName=internet explorer,version=%IE_VERSION%,maxInstances=5,platform=%PLATFORM%"
phantomjs --webdriver=5557 --webdriver-selenium-grid-hub=http://%HUB_HOST%:%HUB_PORT%