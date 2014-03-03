using AutoMapper;
using DaveScriven.CodeSample.Site.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DaveScriven.CodeSample.Site
{
    public static class MappingConfig
    {
        public static void InitialiseMappings()
        {
            Mapper.CreateMap<DaveScriven.CodeSample.Site.ReadModel.Catch, CatchLoggedCommand>();
        }
    }
}