using AutoMapper;

namespace Todo.AutoMapper
{
    public class AutoMapperBuilder
    {
        private MapperConfiguration _configuration;

        public AutoMapperBuilder WithConfiguration(MapperConfiguration configuration)
        {
            _configuration = configuration;
            return this;
        }

        public IMapper Build()
        {
            return new Mapper(_configuration);
        }
    }
}
