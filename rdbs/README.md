# Example databases

## MySQL
[MySQL 5.7 Reference Manual](https://dev.mysql.com/doc/refman/5.7/en/)

1. Connect to MySQL server
* connect: mysql -u username -p password
* disconnect: exit

2. Create/Delete/Use databases
* create: create database database_name;
* display: show databases;
* delete: drop database database_name;
* use: use database_name;

3. Create/Delete tables
* create: create table table_name(column_name column_type constraints);
* display: 
		show tables;
		describe [database_name.]table_name;
		show create table [database_name.]table_name;
* delete:
		drop table table_name;
		
4. Populate data into tables
* insert: insert into table_name(field1, field2, ..., fieldn) 		
			values (value1, value2, ..., valuen);
* source:
* load: 
		load data [local] infile 'c:/file_path' into table table_name;	
		load data [local] infile 'c:\\file_path' into table table_name;
* disable foreign key constraints temporary:
		SET [GLOBAL] FOREIGN_KEY_CHECKS=0; or DISABLE KEYS;
		load data ...
		SET [GLOBAL] FOREIGN_KEY_CHECKS=1; or ENABLE KEYS;
		
5. MySQL change database collation
* server: set name utf8
* database: ALTER DATABASE <database_name> CHARACTER SET utf8 COLLATE utf8_unicode_ci;
* table: ALTER TABLE <table_name> CONVERT TO CHARACTER SET utf8 COLLATE utf8_unicode_ci;
* collumn: ALTER TABLE <table_name> MODIFY <column_name> VARCHAR(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci;
		
