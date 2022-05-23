using Elmah.Models;
using Elmah.RepositoryContracts;
using Elmah.EFCoreContext;
using Framework.Models;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Elmah.EFCoreRepositories
{
    public class ElmahTypeRepository
        : IElmahTypeRepository
    {
        private readonly ILogger<ElmahTypeRepository> _logger;
        private readonly EFDbContext _dbcontext;

        public ElmahTypeRepository(EFDbContext dbcontext, ILogger<ElmahTypeRepository> logger)
        {
            _dbcontext = dbcontext;
            _logger = logger;
        }

        public async Task<Response<HttpStatusCode, ElmahTypeModel>> Delete(ElmahTypeIdModel id)
        {
            if (id == null)
                return await Task<Response<HttpStatusCode, ElmahTypeModel>>.FromResult(new Response<HttpStatusCode, ElmahTypeModel> { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing = _dbcontext.ElmahTypes.SingleOrDefault(t => t.Type == id.Type);

                if (existing == null)
                    return await Task<Response<HttpStatusCode, ElmahTypeModel>>.FromResult(new Response<HttpStatusCode, ElmahTypeModel> { Status = HttpStatusCode.NotFound });

                _dbcontext.ElmahTypes.Remove(existing);
                await _dbcontext.SaveChangesAsync();

                return await Task<Response<HttpStatusCode, ElmahTypeModel>>.FromResult(
                    new Response<HttpStatusCode, ElmahTypeModel>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = new ElmahTypeModel
                        {
                            Type = existing.Type,
                        }
                    });
            }
            catch (Exception ex)
            {
                return await Task<Response<HttpStatusCode, ElmahTypeModel>>.FromResult(new Response<HttpStatusCode, ElmahTypeModel> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        public async Task<Response<HttpStatusCode, ElmahTypeModel>> Get(ElmahTypeIdModel id)
        {
            if (id == null)
                return await Task<Response<HttpStatusCode, ElmahTypeModel>>.FromResult(new Response<HttpStatusCode, ElmahTypeModel> { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing = _dbcontext.ElmahTypes.SingleOrDefault(t => t.Type == id.Type);

                // TODO: can create a new record here.
                if (existing == null)
                    return await Task<Response<HttpStatusCode, ElmahTypeModel>>.FromResult(new Response<HttpStatusCode, ElmahTypeModel> { Status = HttpStatusCode.BadRequest });

                return await Task<Response<HttpStatusCode, ElmahTypeModel>>.FromResult(
                    new Response<HttpStatusCode, ElmahTypeModel>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = new ElmahTypeModel
                        {
                            Type = existing.Type,
                        }
                    });
            }
            catch (Exception ex)
            {
                return await Task<Response<HttpStatusCode, ElmahTypeModel>>.FromResult(new Response<HttpStatusCode, ElmahTypeModel> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        public async Task<Response<HttpStatusCode, ElmahTypeModel>> Create(ElmahTypeModel input)
        {
            if (input == null)
                return await Task<Response<HttpStatusCode, ElmahTypeModel>>.FromResult(new Response<HttpStatusCode, ElmahTypeModel> { Status = HttpStatusCode.BadRequest });
            try
            {
                var toInsert = new ElmahType
                {
                            Type = input.Type,
                };
                await _dbcontext.ElmahTypes.AddAsync(toInsert);
                await _dbcontext.SaveChangesAsync();
                return await Task<Response<HttpStatusCode, ElmahTypeModel>>.FromResult(
                    new Response<HttpStatusCode, ElmahTypeModel>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = new ElmahTypeModel
                        {
                            Type = toInsert.Type,
                        }
                    });
            }
            catch (Exception ex)
            {
                return await Task<Response<HttpStatusCode, ElmahTypeModel>>.FromResult(new Response<HttpStatusCode, ElmahTypeModel> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        public async Task<Response<HttpStatusCode, ElmahTypeModel>> Update(ElmahTypeModel input)
        {
            if (input == null)
                return await Task<Response<HttpStatusCode, ElmahTypeModel>>.FromResult(new Response<HttpStatusCode, ElmahTypeModel> { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing = _dbcontext.ElmahTypes.SingleOrDefault(t => t.Type == input.Type);

                // TODO: can create a new record here.
                if (existing == null)
                    return await Task<Response<HttpStatusCode, ElmahTypeModel>>.FromResult(new Response<HttpStatusCode, ElmahTypeModel> { Status = HttpStatusCode.NotFound });

                // TODO: the .CopyTo<> method may modified because some properties may should not be copied.
                existing.Type = input.Type;
                await _dbcontext.SaveChangesAsync();
                return await Task<Response<HttpStatusCode, ElmahTypeModel>>.FromResult(
                    new Response<HttpStatusCode, ElmahTypeModel>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = new ElmahTypeModel
                        {
                            Type = existing.Type,
                        }
                    });
            }
            catch (Exception ex)
            {
                return await Task<Response<HttpStatusCode, ElmahTypeModel>>.FromResult(new Response<HttpStatusCode, ElmahTypeModel> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }
    }
}

