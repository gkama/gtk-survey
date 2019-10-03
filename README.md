# gtk-survey
gtk-survey is a survey generator that supports loads of functionalities with simplified CRUD operations and API access

## Overview
`gtk-survey` is designed around `QuestionType` object which defines what a survey question is.
The idea behind this is that these question types can be re-used and shared across the platform.
The backend database will 

## Data Types
```csharp
public class Client
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }
    public DateTime Created { get; set; }
    public DateTime LastUpdated { get; set; }
    public int? BillingId { get; set; }
    public Guid PublicKey { get; set; }
    public ICollection<Workspace> Workspaces { get; } = new List<Workspace>();
}
```
```csharp
public class Workspace
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
```csharp
public class Survey
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime Created { get; set; }
    public string CreatedBy { get; set; }
    public DateTime LastUpdated { get; set; }
    public string LastUpdatedBy { get; set; }
    public int? WorkspaceId { get; set; }
    public Workspace Workspace { get; set; }
    public Guid PublicKey { get; set; }
    public ICollection<SurveyQuestion> SurveyQuestions { get; } = new List<SurveyQuestion>();
}
```
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
```csharp
public class QuestionType
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Guid PublicKey { get; set; }
    public ICollection<QuestionTypeAnswer> Answers { get; } = new List<QuestionTypeAnswer>();
}
```
```csharp
public class QuestionTypeAnswer
{
    public int Id { get; set; }
    public int TypeId { get; set; }
    public string Answer { get; set; }
    public Guid PublicKey { get; set; }
}
```
```csharp
public class Response
{
    public int Id { get; set; }
    public int Count { get; set; }
    public int SurveyQuestionId { get; set; }
    public SurveyQuestion SurveyQuestion { get; set; }
    public int QuestionTypeAnswerId { get; set; }
    public QuestionTypeAnswer QuestionTypeAnswer { get; set; }
    public Guid PublicKey { get; set; }
}
```