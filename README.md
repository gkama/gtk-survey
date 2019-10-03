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

Question
```csharp
public class Question : IQuestion<IQuestionType>, IPublicKeyId
{
	[JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("questionText")]
    public string Text { get; set; }

    [JsonProperty("created")]
    public DateTime Created { get; set; }

    [JsonProperty("lastUpdated")]
    public DateTime LastUpdated { get; set; }

    [JsonProperty("typeId")]
    public int TypeId { get; set; }

    [JsonProperty("type")]
    public QuestionType Type { get; set; }

    [JsonProperty("publicKey")]
    public Guid PublicKey { get; set; }
}
```