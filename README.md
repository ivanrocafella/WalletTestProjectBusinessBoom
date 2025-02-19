To run a project using the PostgreSQL database in the Visual Studio environment, follow these steps:
  1. Cloning a repository from GitHub:
       - Open Visual Studio.
       - In the Git menu, select Clone Repository.
       - In the dialog box, enter the URL of the repository on GitHub and select a local folder to save the project.
     After cloning the repository, Visual Studio will automatically open the project.
  2. Restore Dependencies:
     Visual Studio typically restores NuGet packages automatically upon opening the project. If this doesn't occur:
       - In the Tools menu, select NuGet Package Manager > Package Manager Console.
       - In the console, run: Update-Package -reinstall
  3. Install and Configure PostgreSQL:
       - Download and install PostgreSQL from the official website.
       - During installation, create a new user named postgres with the password qwerty12345.
  4. Install and Configure PostgreSQL:
       - Open the appsettings.json file in your project.
       - Add or update the connection string as follows:
         "ConnectionStrings": {
         "DefaultConnection": "Host=localhost;Port=5432;Database=walletTestProjectBB;Username=postgres;Password=qwerty12345"
         }
  5. Install Necessary NuGet Packages (if necessary):
       - In the Tools menu, select NuGet Package Manager > Package Manager Console.
       - In the console, run:
       Install-Package Npgsql.EntityFrameworkCore.PostgreSQL
       Install-Package Microsoft.EntityFrameworkCore.Tools
  6. Apply Migrations:
       - In the Package Manager Console, run: Update-Database
  7. Run the Project:
       - Select the type of project to run WalletTestProjectBusinessBoom.API
       - Press F5 or select Debug > Start Debugging to run the project in debug mode.
       - To run without debugging, press Ctrl + F5 or select Debug > Start Without Debugging.
