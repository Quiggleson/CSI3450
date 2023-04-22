# CSI3450
Timecard project for CSI3450 at Oakland University

<h2>Download <a href="https://dotnet.microsoft.com/en-us/download/dotnet/7.0">.NET SDK 7.0</a></h2>
Follow the instructions on the website <br>

<h2> Add user secrets </h2>
cd to the file with .csproj <br>

run `dotnet user-secrets init`

If you do not know the localhost connection string, you can reference 
<a href="https://www.connectionstrings.com/">ConnectionStrings.com</a>

It should be along the lines of this, replacing `<username>` and `<password>` with your username and password, respectively<br>
`Server=localhost;Database=timecard;Uid=<username>;Pwd=<password>;`

Once you have your connection string, run <br>
``dotnet user-secrets set 'ConnectionStrings:Default' 'Server=localhost;Database=timecard;Uid=<username>;Pwd=<password>;'`` <br>
I don't know how finnicky macs are, but if that doesn't work, try double quotes <br>


<h2>Running the goods </h2>
Run from Visual Studio or <br>

Run ``dotnet run`` in the command line (make sure you're in the directory with .csproj)<br>

<h3>Login info:</h3>
Manager account: <br>
Employee ID: 987654321 <br>
Password: password2 <br><br>

Employee account: <br>
Employee ID: 212821421 <br>
Password: password6 <br>
