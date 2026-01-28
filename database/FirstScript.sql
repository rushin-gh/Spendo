--create database SpendoDB;
use SpendoDB;

--select * from sys.tables
--select * from INFORMATION_SCHEMA.TABLES

--create login spendo_app_user with password = 'spendo_app_password', check_policy = on;
--create user spendo_app_user for login spendo_app_user;

--ALTER ROLE db_datareader ADD MEMBER spendo_app_user;
--ALTER ROLE db_datawriter ADD MEMBER spendo_app_user;

-- Source - https://stackoverflow.com/a
-- Posted by Brijesh Kumar Tripathi
-- Retrieved 2026-01-14, License - CC BY-SA 4.0

--select
--    'data source=' + @@servername +
--    ';initial catalog=' + db_name() +
--    case type_desc
--        when 'WINDOWS_LOGIN' 
--            then ';trusted_connection=true'
--        else
--            ';user id=' + suser_name() + ';password=<<YourPassword>>'
--    end
--    as ConnectionString
--from sys.server_principals
--where name = suser_name()
--data source=Rushi\SQLEXPRESS;initial catalog=SpendoDB;trusted_connection=true


-- Source - https://stackoverflow.com/a
-- Posted by MarioDS, modified by community. See post 'Timeline' for change history
-- Retrieved 2026-01-14, License - CC BY-SA 4.0
--Data Source=Rushi\SQLEXPRESS;Initial Catalog=SpendoDB;User Id=spendo_app_user;Password=spendo_app_password


---- Created user for migrations
--create login spendo_db_owner with password = 'spendo_db_owner_pass', check_policy = on;
--create user spendo_db_owner for login spendo_db_owner;
--ALTER ROLE db_owner ADD MEMBER spendo_db_owner;

 --select * from Expenses

 --Insert into expenses values
 --('Tea', NULL, 10, GETDATE())

 --INSERT INTO Expenses
 --SELECT Title, Description, Amount, Date FROM Expenses