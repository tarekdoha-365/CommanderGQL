using HotChocolate.Types;
using CommanderGQL.Models;
using HotChocolate;
using CommanderGQL.Data;
using System.Linq;

namespace CommanderGQL.GraphQL.Platforms
{
    public class PlatformType: ObjectType<Platform>
    {
        protected override void Configure(IObjectTypeDescriptor<Platform> descriptor)
        {

            descriptor.Description("Represents any Software or service that has command line interface");
            descriptor.Field(p=>p.LicenseKey).Ignore();
            descriptor
            .Field(p=>p.Commands)
            .ResolveWith<Resolvers>(p=>p.GetCommands(default!,default!))
            .UseDbContext<AppDbContext>()
            .Description("This is the first of available commands for this platform");
        }
        private class Resolvers{
            public IQueryable<Command> GetCommands(Platform platform, [ScopedService] AppDbContext context)
            {
                return context.Commands.Where(p=>p.PlatformId==platform.Id);
            }
        }
    }
}