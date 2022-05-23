using Elmah.Models;
using Elmah.RepositoryContracts;
using Elmah.EFCoreContext;
using Framework.Models;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Elmah.EFCoreRepositories
{
    public class ElmahStatusCodeRepository
        : IElmahStatusCodeRepository
    {
        private readonly ILogger<ElmahStatusCodeRepository> _logger;
        private readonly EFDbContext _dbcontext;

        public ElmahStatusCodeRepository(EFDbContext dbcontext, ILogger<ElmahStatusCodeRepository> logger)
        {
            _dbcontext = dbcontext;
            _logger = logger;
        }

        public async Task<Response<HttpStatusCode, ElmahStatusCodeModel>> Delete(ElmahStatusCodeIdModel id)
        {
            if (id == null)
                return await Task<Response<HttpStatusCode, ElmahStatusCodeModel>>.FromResult(new Response<HttpStatusCode, ElmahStatusCodeModel> { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing = _dbcontext.ElmahStatusCodes.SingleOrDefault(t => t.StatusCode == id.StatusCode);

                if (existing == null)
                    return await Task<Response<HttpStatusCode, ElmahStatusCodeModel>>.FromResult(new Response<HttpStatusCode, ElmahStatusCodeModel> { Status = HttpStatusCode.NotFound });

                _dbcontext.ElmahStatusCodes.Remove(existing);
                await _dbcontext.SaveChangesAsync();

                return await Task<Response<HttpStatusCode, ElmahStatusCodeModel>>.FromResult(
                    new Response<HttpStatusCode, ElmahStatusCodeModel>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = new ElmahStatusCodeModel
                        {
                            StatusCode = existing.StatusCode,
                            Name = existing.Name,
                        }
                    });
            }
            catch (Exception ex)
            {
                return await Task<Response<HttpStatusCode, ElmahStatusCodeModel>>.FromResult(new Response<HttpStatusCode, ElmahStatusCodeModel> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        public async Task<Response<HttpStatusCode, ElmahStatusCodeModel>> Get(ElmahStatusCodeIdModel id)
        {
            if (id == null)
                return await Task<Response<HttpStatusCode, ElmahStatusCodeModel>>.FromResult(new Response<HttpStatusCode, ElmahStatusCodeModel> { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing = _dbcontext.ElmahStatusCodes.SingleOrDefault(t => t.StatusCode == id.StatusCode);

                // TODO: can create a new record here.
                if (existing == null)
                    return await Task<Response<HttpStatusCode, ElmahStatusCodeModel>>.FromResult(new Response<HttpStatusCode, ElmahStatusCodeModel> { Status = HttpStatusCode.BadRequest });

                return await Task<Response<HttpStatusCode, ElmahStatusCodeModel>>.FromResult(
                    new Response<HttpStatusCode, ElmahStatusCodeModel>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = new ElmahStatusCodeModel
                        {
                            StatusCode = existing.StatusCode,
                            Name = existing.Name,
                        }
                    });
            }
            catch (Exception ex)
            {
                return await Task<Response<HttpStatusCode, ElmahStatusCodeModel>>.FromResult(new Response<HttpStatusCode, ElmahStatusCodeModel> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        public async Task<Response<HttpStatusCode, ElmahStatusCodeModel>> Create(ElmahStatusCodeModel input)
        {
            if (input == null)
                return await Task<Response<HttpStatusCode, ElmahStatusCodeModel>>.FromResult(new Response<HttpStatusCode, ElmahStatusCodeModel> { Status = HttpStatusCode.BadRequest });
            try
            {
                var toInsert = new ElmahStatusCode
                {
                            StatusCode = input.StatusCode,
                            Name = input.Name,
                };
                await _dbcontext.ElmahStatusCodes.AddAsync(toInsert);
                await _dbcontext.SaveChangesAsync();
                return await Task<Response<HttpStatusCode, ElmahStatusCodeModel>>.FromResult(
                    new Response<HttpStatusCode, ElmahStatusCodeModel>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = new ElmahStatusCodeModel
                        {
                            StatusCode = toInsert.StatusCode,
                            Name = toInsert.Name,
                        }
                    });
            }
            catch (Exception ex)
            {
                return await Task<Response<HttpStatusCode, ElmahStatusCodeModel>>.FromResult(new Response<HttpStatusCode, ElmahStatusCodeModel> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        public async Task<Response<HttpStatusCode, ElmahStatusCodeModel>> Update(ElmahStatusCodeModel input)
        {
            if (input == null)
                return await Task<Response<HttpStatusCode, ElmahStatusCodeModel>>.FromResult(new Response<HttpStatusCode, ElmahStatusCodeModel> { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing = _dbcontext.ElmahStatusCodes.SingleOrDefault(t => t.StatusCode == input.StatusCode);

                // TODO: can create a new record here.
                if (existing == null)
                    return await Task<Response<HttpStatusCode, ElmahStatusCodeModel>>.FromResult(new Response<HttpStatusCode, ElmahStatusCodeModel> { Status = HttpStatusCode.NotFound });

                // TODO: the .CopyTo<> method may modified because some properties may should not be copied.
                existing.StatusCode = input.StatusCode;
                existing.Name = input.Name;
                await _dbcontext.SaveChangesAsync();
                return await Task<Response<HttpStatusCode, ElmahStatusCodeModel>>.FromResult(
                    new Response<HttpStatusCode, ElmahStatusCodeModel>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = new ElmahStatusCodeModel
                        {
                            StatusCode = existing.StatusCode,
                            Name = existing.Name,
                        }
                    });
            }
            catch (Exception ex)
            {
                return await Task<Response<HttpStatusCode, ElmahStatusCodeModel>>.FromResult(new Response<HttpStatusCode, ElmahStatusCodeModel> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }
    }
}

