using Elmah.Models;
using Elmah.RepositoryContracts;
using Elmah.EFCoreContext;
using Framework.Models;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Elmah.EFCoreRepositories
{
    public class ElmahApplicationRepository
        : IElmahApplicationRepository
    {
        private readonly ILogger<ElmahApplicationRepository> _logger;
        private readonly EFDbContext _dbcontext;

        public ElmahApplicationRepository(EFDbContext dbcontext, ILogger<ElmahApplicationRepository> logger)
        {
            _dbcontext = dbcontext;
            _logger = logger;
        }

        public async Task<Response<HttpStatusCode, ElmahApplicationModel>> Delete(ElmahApplicationIdModel id)
        {
            if (id == null)
                return await Task<Response<HttpStatusCode, ElmahApplicationModel>>.FromResult(new Response<HttpStatusCode, ElmahApplicationModel> { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing = _dbcontext.ElmahApplications.SingleOrDefault(t => t.Application == id.Application);

                if (existing == null)
                    return await Task<Response<HttpStatusCode, ElmahApplicationModel>>.FromResult(new Response<HttpStatusCode, ElmahApplicationModel> { Status = HttpStatusCode.NotFound });

                _dbcontext.ElmahApplications.Remove(existing);
                await _dbcontext.SaveChangesAsync();

                return await Task<Response<HttpStatusCode, ElmahApplicationModel>>.FromResult(
                    new Response<HttpStatusCode, ElmahApplicationModel>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = new ElmahApplicationModel
                        {
                            Application = existing.Application,
                        }
                    });
            }
            catch (Exception ex)
            {
                return await Task<Response<HttpStatusCode, ElmahApplicationModel>>.FromResult(new Response<HttpStatusCode, ElmahApplicationModel> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        public async Task<Response<HttpStatusCode, ElmahApplicationModel>> Get(ElmahApplicationIdModel id)
        {
            if (id == null)
                return await Task<Response<HttpStatusCode, ElmahApplicationModel>>.FromResult(new Response<HttpStatusCode, ElmahApplicationModel> { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing = _dbcontext.ElmahApplications.SingleOrDefault(t => t.Application == id.Application);

                // TODO: can create a new record here.
                if (existing == null)
                    return await Task<Response<HttpStatusCode, ElmahApplicationModel>>.FromResult(new Response<HttpStatusCode, ElmahApplicationModel> { Status = HttpStatusCode.BadRequest });

                return await Task<Response<HttpStatusCode, ElmahApplicationModel>>.FromResult(
                    new Response<HttpStatusCode, ElmahApplicationModel>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = new ElmahApplicationModel
                        {
                            Application = existing.Application,
                        }
                    });
            }
            catch (Exception ex)
            {
                return await Task<Response<HttpStatusCode, ElmahApplicationModel>>.FromResult(new Response<HttpStatusCode, ElmahApplicationModel> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        public async Task<Response<HttpStatusCode, ElmahApplicationModel>> Create(ElmahApplicationModel input)
        {
            if (input == null)
                return await Task<Response<HttpStatusCode, ElmahApplicationModel>>.FromResult(new Response<HttpStatusCode, ElmahApplicationModel> { Status = HttpStatusCode.BadRequest });
            try
            {
                var toInsert = new ElmahApplication
                {
                            Application = input.Application,
                };
                await _dbcontext.ElmahApplications.AddAsync(toInsert);
                await _dbcontext.SaveChangesAsync();
                return await Task<Response<HttpStatusCode, ElmahApplicationModel>>.FromResult(
                    new Response<HttpStatusCode, ElmahApplicationModel>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = new ElmahApplicationModel
                        {
                            Application = toInsert.Application,
                        }
                    });
            }
            catch (Exception ex)
            {
                return await Task<Response<HttpStatusCode, ElmahApplicationModel>>.FromResult(new Response<HttpStatusCode, ElmahApplicationModel> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        public async Task<Response<HttpStatusCode, ElmahApplicationModel>> Update(ElmahApplicationModel input)
        {
            if (input == null)
                return await Task<Response<HttpStatusCode, ElmahApplicationModel>>.FromResult(new Response<HttpStatusCode, ElmahApplicationModel> { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing = _dbcontext.ElmahApplications.SingleOrDefault(t => t.Application == input.Application);

                // TODO: can create a new record here.
                if (existing == null)
                    return await Task<Response<HttpStatusCode, ElmahApplicationModel>>.FromResult(new Response<HttpStatusCode, ElmahApplicationModel> { Status = HttpStatusCode.NotFound });

                // TODO: the .CopyTo<> method may modified because some properties may should not be copied.
                existing.Application = input.Application;
                await _dbcontext.SaveChangesAsync();
                return await Task<Response<HttpStatusCode, ElmahApplicationModel>>.FromResult(
                    new Response<HttpStatusCode, ElmahApplicationModel>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = new ElmahApplicationModel
                        {
                            Application = existing.Application,
                        }
                    });
            }
            catch (Exception ex)
            {
                return await Task<Response<HttpStatusCode, ElmahApplicationModel>>.FromResult(new Response<HttpStatusCode, ElmahApplicationModel> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }
    }
}

