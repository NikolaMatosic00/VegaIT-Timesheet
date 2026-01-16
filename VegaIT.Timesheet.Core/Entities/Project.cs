using CSharpFunctionalExtensions;
using VegaIT.Timesheet.Core.Entities.Enumerations;
using VegaIT.Timesheet.Core.Entities.Validations.ProjectValidators;

namespace VegaIT.Timesheet.Core.Entities;


public class Project
{

    public Guid ProjectId { get; }

    public Client Client { get; }

    public Employee Leader { get; }

    public String ProjectName { get; }

    public String Description { get; }

    public EStatusOfProject Status { get; }


    public static Result<Project> Create(Guid id, Client client, Employee leader, string projectName,
        string projectDescription, EStatusOfProject eStatusOfProject)
    {
        var projectNameResult = ProjectNameV.Create(projectName);
        var projectDescriptionResult = ProjectDescription.Create(projectDescription);

        var result = Result.Combine(projectNameResult, projectDescriptionResult);

        return result.IsSuccess
            ? Result.Success(new Project(id, client, leader, projectName, projectDescription, eStatusOfProject))
            : Result.Failure<Project>(result.Error);
    }

    private Project(Guid id, Client client, Employee leader, string projectName, string projectDescription,
        EStatusOfProject eStatusOfProject)
    {
        ProjectId = id;
        Client = client;
        Leader = leader;
        ProjectName = projectName;
        Description = projectDescription;
        Status = eStatusOfProject;
    }

}
