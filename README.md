# Bodlollio #
This is a school-project. It's not meant to be used anywhere else...

Have fun

## Getting started ##
Download Dev-Edition of MS SQL. For having a local ms-database running
https://www.microsoft.com/de-ch/sql-server/sql-server-downloads

Execute bodlollio_db.sql

if it's says bodlollio_db dosn't exist. Execute first line first than
the rest.

Modify your connection string according to your machine

Add your personal NEXMO-API DATA into Nexmo Data. 
Than execute following command at project root:
``` shell
    git update-index --assume-unchanged /Bodlollio/Bodlollio/App_Start/NexmoData.cs
```
This will tell git to ignore further changes


## User Groups ##
Our User groups are:
* User
* Admin

## User statuses ##
Our user statuses are:
  * blocked
  * loggeIn
  * loggedOut
  * unspecified
