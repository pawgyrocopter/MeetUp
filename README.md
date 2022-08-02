## To run this application:

1. configure user and password to access PostgreSql database  in *appsettings.Development.json*
2. run ```dotnet ef database drop``` 
3. ```dotnet ef migrations add migrationName```
4. ```dotnet ef database update```


### In order to combine FirstName and LastName:
1. Create migration ```dotnet ef migration add CombineMigration```
2. Edit migration and add for *Up* method following code:
      ```
      migrationBuilder.Sql(
          @"UPDATE ""Users""
          SET ""FirstName"" = REPLACE(""FirstName"",""FirstName"", CONCAT(""FirstName"",' ', ""LastName"")); 
          ALTER TABLE ""Users""
          RENAME COLUMN ""FirstName"" To ""FullName"";
          ALTER TABLE ""Users""
          DROP COLUMN ""LastName"";
      ");
      ```
3. run ```dotnet ef database update```
