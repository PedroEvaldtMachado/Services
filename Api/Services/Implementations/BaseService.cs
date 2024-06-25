using Api.Domain;

namespace Api.Services.Implementations
{
    public abstract class BaseService
    {
        protected readonly Lazy<DatabaseContext> Context;

        protected BaseService(BaseServiceParams baseServiceParams)
        {
            Context = baseServiceParams.DatabaseContext;
        }
    }

    public class BaseServiceParams
    {
        public Lazy<DatabaseContext> DatabaseContext { get; init; }

        public BaseServiceParams(Lazy<DatabaseContext> databaseContext)
        {
            DatabaseContext = databaseContext;
        }
    }
}
