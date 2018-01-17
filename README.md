# Bodlollio #
This is a school-project. It's not meant to be used anywhere else...

Have fun

## Getting started ##
Modify your connection string according to your machine

Add your personal NEXMO-API DATA into NexmoData.cs 
Than execute following command at project root:
``` shell
    git update-index --assume-unchanged /Bodlollio/Bodlollio/App_Start/NexmoData.cs
```
This will tell git to ignore further changes on the file. 

you may have to do some own changes on the file to get it work with
the code. (make it static, public members, etc.)

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
