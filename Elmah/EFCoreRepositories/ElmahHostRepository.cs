using Elmah.Models;
using Elmah.RepositoryContracts;
using Elmah.EFCoreContext;
using Framework.Models;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Elmah.EFCoreRepositories
{
    public class ElmahHostRepository
        : IElmahHostRepository
    {
        private readonly ILogger<ElmahHostRepository> _logger;
        private readonly EFDbContext _dbcontext;

        public ElmahHostRepository(EFDbContext dbcontext, ILogger<ElmahHostRepository> logger)
        {
            _dbcontext = dbcontext;
            _logger = logger;
        }

        public async Task<Response<HttpStatusCode, ElmahHostModel>> Delete(ElmahHostIdModel id)
        {
            if (id == null)
                return await Task<Response<HttpStatusCode, ElmahHostModel>>.FromResult(new Response<HttpStatusCode, ElmahHostModel> { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing = _dbcontext.ElmahHosts.SingleOrDefault(t => t.Host == id.Host);

                if (existing == null)
                    return await Task<Response<HttpStatusCode, ElmahHostModel>>.FromResult(new Response<HttpStatusCode, ElmahHostModel> { Status = HttpStatusCode.NotFound });

                _dbcontext.ElmahHosts.Remove(existing);
                await _dbcontext.SaveChangesAsync();

                return await Task<Response<HttpStatusCode, ElmahHostModel>>.FromResult(
                    new Response<HttpStatusCode, ElmahHostModel>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = new ElmahHostModel
                        {
                            Host = existing.Host,
                        }
                    });
            }
            catch (Exception ex)
            {
                return await Task<Response<HttpStatusCode, ElmahHostModel>>.FromResult(new Response<HttpStatusCode, ElmahHostModel> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        public async Task<Response<HttpStatusCode, ElmahHostModel>> Get(ElmahHostIdModel id)
        {
            if (id == null)
                return await Task<Response<HttpStatusCode, ElmahHostModel>>.FromResult(new Response<HttpStatusCode, ElmahHostModel> { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing = _dbcontext.ElmahHosts.SingleOrDefault(t => t.Host == id.Host);

                // TODO: can create a new record here.
                if (existing == null)
                    return await Task<Response<HttpStatusCode, ElmahHostModel>>.FromResult(new Response<HttpStatusCode, ElmahHostModel> { Status = HttpStatusCode.BadRequest });

                return await Task<Response<HttpStatusCode, ElmahHostModel>>.FromResult(
                    new Response<HttpStatusCode, ElmahHostModel>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = new ElmahHostModel
                        {
                            Host = existing.Host,
                        }
                    });
            }
            catch (Exception ex)
            {
                return await Task<Response<HttpStatusCode, ElmahHostModel>>.FromResult(new Response<HttpStatusCode, ElmahHostModel> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        public async Task<Response<HttpStatusCode, ElmahHostModel>> Create(ElmahHostModel input)
        {
            if (input == null)
                return await Task<Response<HttpStatusCode, ElmahHostModel>>.FromResult(new Response<HttpStatusCode, ElmahHostModel> { Status = HttpStatusCode.BadRequest });
            try
            {
                var toInsert = new ElmahHost
                {
                            Host = input.Host,
                };
                await _dbcontext.ElmahHosts.AddAsync(toInsert);
                await _dbcontext.SaveChangesAsync();
                return await Task<Response<HttpStatusCode, ElmahHostModel>>.FromResult(
                    new Response<HttpStatusCode, ElmahHostModel>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = new ElmahHostModel
                        {
                            Host = toInsert.Host,
                        }
                    });
            }
            catch (Exception ex)
            {
                return await Task<Response<HttpStatusCode, ElmahHostModel>>.FromResult(new Response<HttpStatusCode, ElmahHostModel> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        public async Task<Response<HttpStatusCode, ElmahHostModel>> Update(ElmahHostModel input)
        {
            if (input == null)
                return await Task<Response<HttpStatusCode, ElmahHostModel>>.FromResult(new Response<HttpStatusCode, ElmahHostModel> { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing = _dbcontext.ElmahHosts.SingleOrDefault(t => t.Host == input.Host);

                // TODO: can create a new record here.
                if (existing == null)
                    return await Task<Response<HttpStatusCode, ElmahHostModel>>.FromResult(new Response<HttpStatusCode, ElmahHostModel> { Status = HttpStatusCode.NotFound });

                // TODO: the .CopyTo<> method may modified because some properties may should not be copied.
                existing.Host = input.Host;
                await _dbcontext.SaveChangesAsync();
                return await Task<Response<HttpStatusCode, ElmahHostModel>>.FromResult(
                    new Response<HttpStatusCode, ElmahHostModel>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = new ElmahHostModel
                        {
                            Host = existing.Host,
                        }
                    });
            }
            catch (Exception ex)
            {
                return await Task<Response<HttpStatusCode, ElmahHostModel>>.FromResult(new Response<HttpStatusCode, ElmahHostModel> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }
    }
}

