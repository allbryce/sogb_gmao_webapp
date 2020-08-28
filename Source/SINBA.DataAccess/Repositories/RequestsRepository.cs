using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dev.Tools.EntityFramework;
using Dev.Tools.Exceptions;
using Dev.Tools.Logger;
using Sinba.BusinessModel.Dto;
using Sinba.BusinessModel.Entity;
using Sinba.BusinessModel.Enums;
using Sinba.BusinessModel.RepositoryInterface;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq.Expressions;


namespace Sinba.DataAccess.Repositories
{

    [DataLog]
    [DataExceptionHandler]
    public class RequestsRepository : GenericRepository<Site>, IRequestsRepository
    {
        public RequestsRepository(SinbaContext dbContext) : base(dbContext) { }



    }
}