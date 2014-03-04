using AutoMapper;
using DaveScriven.CodeSample.Domain.Commands;

namespace DaveScriven.CodeSample.Site
{
    public static class MappingConfig
    {
        public static void InitialiseMappings()
        {
            Mapper.CreateMap<DaveScriven.CodeSample.Data.Catch, CatchLoggedCommand>();
        }
    }
}