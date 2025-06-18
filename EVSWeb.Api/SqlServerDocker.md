


docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=sql@Master" -e "MSSQL_PID=Express" -p 1433:1433 --name sqlserver-evs -d mcr.microsoft.com/mssql/server:2022-latest