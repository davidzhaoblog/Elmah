using Elmah.Models;
using Elmah.RepositoryContracts;
using Elmah.EFCoreContext;
using Framework.Models;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Elmah.EFCoreRepositories
{
    public class ElmahSourceRepository
        : IElmahSourceRepository
    {
        private readonly ILogger<ElmahSourceRepository> _logger;
        private readonly EFDbContext _dbcontext;

        public ElmahSourceRepository(EFDbContext dbcontext, ILogger<ElmahSourceRepository> logger)
        {
            _dbcontext = dbcontext;
            _logger = logger;
        }

        public async Task<Response<HttpStatusCode, ElmahSourceModel>> Delete(ElmahSourceIdModel id)
        {
            if (id == null)
                return await Task<Response<HttpStatusCode, ElmahSourceModel>>.FromResult(new Response<HttpStatusCode, ElmahSourceModel> { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing = _dbcontext.ElmahSources.SingleOrDefault(t => t.Source == id.Source);

                if (existing == null)
                    return await Task<Response<HttpStatusCode, ElmahSourceModel>>.FromResult(new Response<HttpStatusCode, ElmahSourceModel> { Status = HttpStatusCode.NotFound });

                _dbcontext.ElmahSources.Remove(existing);
                await _dbcontext.SaveChangesAsync();

                return await Task<Response<HttpStatusCode, ElmahSourceModel>>.FromResult(
                    new Response<HttpStatusCode, ElmahSourceModel>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = new ElmahSourceModel
                        {
                            Source = existing.Source,
                        }
                    });
            }
            catch (Exception ex)
            {
                return await Task<Response<HttpStatusCode, ElmahSourceModel>>.FromResult(new Response<HttpStatusCode, ElmahSourceModel> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        public async Task<Response<HttpStatusCode, ElmahSourceModel>> Get(ElmahSourceIdModel id)
        {
            if (id == null)
                return await Task<Response<HttpStatusCode, ElmahSourceModel>>.FromResult(new Response<HttpStatusCode, ElmahSourceModel> { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing = _dbcontext.ElmahSources.SingleOrDefault(t => t.Source == id.Source);

                // TODO: can create a new record here.
                if (existing == null)
                    return await Task<Response<HttpStatusCode, ElmahSourceModel>>.FromResult(new Response<HttpStatusCode, ElmahSourceModel> { Status = HttpStatusCode.BadRequest });

                return await Task<Response<HttpStatusCode, ElmahSourceModel>>.FromResult(
                    new Response<HttpStatusCode, ElmahSourceModel>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = new ElmahSourceModel
                        {
                            Source = existing.Source,
                        }
                    });
            }
            catch (Exception ex)
            {
                return await Task<Response<HttpStatusCode, ElmahSourceModel>>.FromResult(new Response<HttpStatusCode, ElmahSourceModel> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        public async Task<Response<HttpStatusCode, ElmahSourceModel>> Create(ElmahSourceModel input)
        {
            if (input == null)
                return await Task<Response<HttpStatusCode, ElmahSourceModel>>.FromResult(new Response<HttpStatusCode, ElmahSourceModel> { Status = HttpStatusCode.BadRequest });
            try
            {
                var toInsert = new ElmahSource
                {
                            Source = input.Source,
                };
                await _dbcontext.ElmahSources.AddAsync(toInsert);
                await _dbcontext.SaveChangesAsync();
                return await Task<Response<HttpStatusCode, ElmahSourceModel>>.FromResult(
                    new Response<HttpStatusCode, ElmahSourceModel>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = new ElmahSourceModel
                        {
                            Source = toInsert.Source,
                        }
                    });
            }
            catch (Exception ex)
            {
                return await Task<Response<HttpStatusCode, ElmahSourceModel>>.FromResult(new Response<HttpStatusCode, ElmahSourceModel> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        public async Task<Response<HttpStatusCode, ElmahSourceModel>> Update(ElmahSourceModel input)
        {
            if (input == null)
                return await Task<Response<HttpStatusCode, ElmahSourceModel>>.FromResult(new Response<HttpStatusCode, ElmahSourceModel> { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing = _dbcontext.ElmahSources.SingleOrDefault(t => t.Source == input.Source);

                // TODO: can create a new record here.
                if (existing == null)
                    return await Task<Response<HttpStatusCode, ElmahSourceModel>>.FromResult(new Response<HttpStatusCode, ElmahSourceModel> { Status = HttpStatusCode.NotFound });

                // TODO: the .CopyTo<> method may modified because some properties may should not be copied.
                existing.Source = input.Source;
                await _dbcontext.SaveChangesAsync();
                return await Task<Response<HttpStatusCode, ElmahSourceModel>>.FromResult(
                    new Response<HttpStatusCode, ElmahSourceModel>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = new ElmahSourceModel
                        {
                            Source = existing.Source,
                        }
                    });
            }
            catch (Exception ex)
            {
                return await Task<Response<HttpStatusCode, ElmahSourceModel>>.FromResult(new Response<HttpStatusCode, ElmahSourceModel> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }
    }
}

