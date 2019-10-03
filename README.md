# gtk-survey
gtk-survey is a survey generator that supports loads of functionalities with simplified CRUD operations and API access

## Overview
`gtk-survey` is designed around `QuestionType` object which defines what a survey question is.
The idea behind this is that these question types can be re-used and shared across the platform.
The backend database will 

## Data Types
``` csharp
Client.cs
```
- A top level data type that stores information about a client

``` csharp
Workspace.cs
```
- 

``` csharp
Survey.cs
Question.cs
QuestionType.cs
QuestionTypeAnswer.cs

Response.cs
	- Keeps track of how many answers for a specific question type answer there are. This is essentially the object to track the number of answers to survey questions
```