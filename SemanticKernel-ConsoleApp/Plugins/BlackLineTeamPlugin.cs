using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using Microsoft.SemanticKernel;
using SemanticKernel_ConsoleApp.Models;

namespace SemanticKernel_ConsoleApp.Plugins;

public class BlackLineTeamPlugin
{
    private readonly List<BlackLineMember> teams = new Faker<BlackLineMember>()
        .RuleFor(x => x.Id, _ => Guid.NewGuid())
        .RuleFor(x => x.Name, f => f.Person.FullName)
        .RuleFor(x => x.Email, f => f.Person.Email)
        .RuleFor(x => x.Role, f => f.PickRandom(new[] { "Admin", "Member", "Viewer", "Software Engineer" }))
        .Generate(100);

    [KernelFunction("get_all_software_engineers")]
    [Description("Gets a list of Black line's software engineer")]
    public List<BlackLineMember> GetAllSoftwareEngineers()
    {
        return teams.Where(x => x.Role == "Software Engineer").ToList();
    }


    [KernelFunction("Update_Software_Engineer_Role")]
    [Description("Update role for Black line's software engineer")]
    public async Task<BlackLineMember> UpdateRoleForSoftwareEngineer(Guid engineerID, string role)
    {
        var engineer = teams.FirstOrDefault(x => x.Id == engineerID);
        if (engineer != null)
        {
            engineer.Role = role;
        }
        else
        {
            throw new ArgumentException("Engineer not found");
        }
        engineer.Role = role;
        return engineer;
    }

}