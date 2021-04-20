using AutoMapper;
using Lms.Core.Dto;
using Lms.Core.Entities;
using Lms.Data.Data;
using NUnit.Framework;

namespace Lms.Test
{
    public class MapperTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CourseMapTest()
        {
            var config = new MapperConfiguration(cfg =>
                cfg.CreateMap<Course, CourseDto>());
            config.AssertConfigurationIsValid();
        }

        [Test]
        public void ModuleMapTest()
        {
            var config = new MapperConfiguration(cfg =>
                cfg.CreateMap<Module, ModuleDto>());
            config.AssertConfigurationIsValid();
        }
    }
}