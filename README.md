# JsonDeserialization
Repository which shows how to protect against json deserialization attacks in dot net based web applications by whitelisting the types of models that will be binded.
It has two parts:   
Part 1 - Demonstration of JSON Deserialization attack by running Calculator and Command Prompt   
Part 2 - Demonstration of securing against JSON Deserialization attacks by whitelisting the models that are allowed to bind.  

## Getting Started
These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. 

## Prerequisites
Dot Net Framework,  IDE like Visual Studio , NewtonSoft.Json Nuget Package

## How to run this project?
Just clone this project on your local machine and publish this project locally.

The project is a Dot Net based MVC web application which has a Home Controller.The Home Controller has two Action methods Secure Deserialization and
Insecure Deserialization.

## Insecure Deserialization (localhost/port_number/Home/InsecureDeserialization):-  
In the deseriliazation settings, TypeNameHandling is set to all.The object that is deserialized is of the type System.Windows.Data.ObjectDataProvider which is used to spawn calculator and command prompt  
to show the successful attack launch.

## Secure Deserialization ((localhost/port_number/Home/SecureDeserialization))
A whitelist of models is created that will be deserialized.In the deseriliazation settings, it is specified that only known types should be binded.  
The web application will throw an exception when a type of object which is not in the whitelist is deserialized.

## Payload used for attack

{'$type':'System.Windows.Data.ObjectDataProvider, PresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35','MethodName':'Start','MethodParameters':{'$type':'System.Collections.ArrayList, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089','$values':['cmd','/ccalc']},'ObjectInstance':{'$type':'System.Diagnostics.Process, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'}}






