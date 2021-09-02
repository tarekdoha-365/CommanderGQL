using System.Linq;
using CommanderGQL.Data;
using CommanderGQL.Models;
using HotChocolate;
using HotChocolate.Types;

namespace CommanderGQL.GraphQL.Commands{
    public class CommandType : ObjectType<Command>
    {
        protected override void Configure(IObjectTypeDescriptor<Command> descriptor)
        {
            descriptor.Description("");
            descriptor.Field<Command>(p=>p.Platform)
            .ResolveWith<Resolvers>(c=>c.GetPlatform(default!,default!))
            .UseDbContext<AppDbContext>()
            .Description("This is a platform to which command belong");
        }
        private class Resolvers{
            public Platform GetPlatform(Command command, [ScopedService] AppDbContext context){
                return context.Platforms.FirstOrDefault(p=>p.Id==command.PlatformId);
            }
        }
    }
}