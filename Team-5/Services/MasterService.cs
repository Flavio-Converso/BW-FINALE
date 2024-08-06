using Team_5.Context;
using Team_5.Services.Interfaces;

namespace Team_5.Services
{
    public class MasterService : IMasterService
    {
        private readonly DataContext _ctx;
        public MasterService(DataContext dataContext)
        {
            _ctx = dataContext;
        }

    }
}
