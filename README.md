1. After cloning the project, and opening this file, follow the below steps
    i.  To build the docker image, navigate to the project root founder and run from the terminal, "docker build -t blogpost_api_test ."
    ii. blogpost_api_test is the project image as seen in the docker compose file.
    iii. use docker compose up to run the docker compose file
    iv. To access the table in the database, install and database client that support PostgreSQL and use the credentials in the app settings.json to login.

NB:

You can change the port if the port 5050 is already in use in your PC. Also, docker desktop must be running before point i above
The database engine for this task is PostgresSQL and the .net SDK is 6.
Migration file is part of the project and run Update-Database from the Package manager console of Visual studio.
