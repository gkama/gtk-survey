# gtk-survey
gtk-survey is a survey generator that supports loads of functionalities with simplified CRUD operations and API access

## Overview
`gtk-survey` is designed around `QuestionType` object which defines what a survey question is.
The idea behind this is that these question types can be re-used and shared across the platform.
The backend database will 

## Data Types
Client.cs
Workspace.cs
Survey.cs
Question.cs
QuestionType.cs
QuestionTypeAnswer.cs
Response.cs

Workspace
```csharp
public class Workspace : IWorkspace, IPublicKeyId
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }
    public DateTime Created { get; set; }
    public DateTime LastUpdated { get; set; }
    public int? ClientId { get; set; }
    public Client Client { get; set; }
    public Guid PublicKey { get; set; }
    public ICollection<Survey> Surveys { get; } = new List<Survey>();
}
```

Question
```csharp
public class Question
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Text { get; set; }
    public DateTime Created { get; set; }
    public DateTime LastUpdated { get; set; }
    public int TypeId { get; set; }
    public QuestionType Type { get; set; }
    public Guid PublicKey { get; set; }
}
```