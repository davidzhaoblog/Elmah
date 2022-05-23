using Elmah.Models;
using Elmah.RepositoryContracts;
using Elmah.EFCoreContext;
using Framework.Models;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Elmah.EFCoreRepositories
{
    public class ElmahErrorRepository
        : IElmahErrorRepository
    {
        private readonly ILogger<ElmahErrorRepository> _logger;
        private readonly EFDbContext _dbcontext;

        public ElmahErrorRepository(EFDbContext dbcontext, ILogger<ElmahErrorRepository> logger)
        {
            _dbcontext = dbcontext;
            _logger = logger;
        }

        public async Task<Response<HttpStatusCode, ElmahErrorModel>> Delete(ElmahErrorIdModel id)
        {
            if (id == null)
                return await Task<Response<HttpStatusCode, ElmahErrorModel>>.FromResult(new Response<HttpStatusCode, ElmahErrorModel> { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing = _dbcontext.ElmahErrors.SingleOrDefault(t => t.ErrorId == id.ErrorId);

                if (existing == null)
                    return await Task<Response<HttpStatusCode, ElmahErrorModel>>.FromResult(new Response<HttpStatusCode, ElmahErrorModel> { Status = HttpStatusCode.NotFound });

                _dbcontext.ElmahErrors.Remove(existing);
                await _dbcontext.SaveChangesAsync();

                return await Task<Response<HttpStatusCode, ElmahErrorModel>>.FromResult(
                    new Response<HttpStatusCode, ElmahErrorModel>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = new ElmahErrorModel
                        {
                            ErrorId = existing.ErrorId,
                            Application = existing.Application,
                            Host = existing.Host,
                            Type = existing.Type,
                            Source = existing.Source,
                            Message = existing.Message,
                            User = existing.User,
                            StatusCode = existing.StatusCode,
                            TimeUtc = existing.TimeUtc,
                            Sequence = existing.Sequence,
                            AllXml = existing.AllXml,
                        }
                    });
            }
            catch (Exception ex)
            {
                return await Task<Response<HttpStatusCode, ElmahErrorModel>>.FromResult(new Response<HttpStatusCode, ElmahErrorModel> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        public async Task<Response<HttpStatusCode, ElmahErrorModel>> Get(ElmahErrorIdModel id)
        {
            if (id == null)
                return await Task<Response<HttpStatusCode, ElmahErrorModel>>.FromResult(new Response<HttpStatusCode, ElmahErrorModel> { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing = _dbcontext.ElmahErrors.SingleOrDefault(t => t.ErrorId == id.ErrorId);

                // TODO: can create a new record here.
                if (existing == null)
                    return await Task<Response<HttpStatusCode, ElmahErrorModel>>.FromResult(new Response<HttpStatusCode, ElmahErrorModel> { Status = HttpStatusCode.BadRequest });

                return await Task<Response<HttpStatusCode, ElmahErrorModel>>.FromResult(
                    new Response<HttpStatusCode, ElmahErrorModel>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = new ElmahErrorModel
                        {
                            ErrorId = existing.ErrorId,
                            Application = existing.Application,
                            Host = existing.Host,
                            Type = existing.Type,
                            Source = existing.Source,
                            Message = existing.Message,
                            User = existing.User,
                            StatusCode = existing.StatusCode,
                            TimeUtc = existing.TimeUtc,
                            Sequence = existing.Sequence,
                            AllXml = existing.AllXml,
                        }
                    });
            }
            catch (Exception ex)
            {
                return await Task<Response<HttpStatusCode, ElmahErrorModel>>.FromResult(new Response<HttpStatusCode, ElmahErrorModel> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        public async Task<Response<HttpStatusCode, ElmahErrorModel>> Create(ElmahErrorModel input)
        {
            if (input == null)
                return await Task<Response<HttpStatusCode, ElmahErrorModel>>.FromResult(new Response<HttpStatusCode, ElmahErrorModel> { Status = HttpStatusCode.BadRequest });
            try
            {
                var toInsert = new ElmahError
                {
                            ErrorId = input.ErrorId,
                            Application = input.Application,
                            Host = input.Host,
                            Type = input.Type,
                            Source = input.Source,
                            Message = input.Message,
                            User = input.User,
                            StatusCode = input.StatusCode,
                            TimeUtc = input.TimeUtc,
                            Sequence = input.Sequence,
                            AllXml = input.AllXml,
                };
                await _dbcontext.ElmahErrors.AddAsync(toInsert);
                await _dbcontext.SaveChangesAsync();
                return await Task<Response<HttpStatusCode, ElmahErrorModel>>.FromResult(
                    new Response<HttpStatusCode, ElmahErrorModel>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = new ElmahErrorModel
                        {
                            ErrorId = toInsert.ErrorId,
                            Application = toInsert.Application,
                            Host = toInsert.Host,
                            Type = toInsert.Type,
                            Source = toInsert.Source,
                            Message = toInsert.Message,
                            User = toInsert.User,
                            StatusCode = toInsert.StatusCode,
                            TimeUtc = toInsert.TimeUtc,
                            Sequence = toInsert.Sequence,
                            AllXml = toInsert.AllXml,
                        }
                    });
            }
            catch (Exception ex)
            {
                return await Task<Response<HttpStatusCode, ElmahErrorModel>>.FromResult(new Response<HttpStatusCode, ElmahErrorModel> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        public async Task<Response<HttpStatusCode, ElmahErrorModel>> Update(ElmahErrorModel input)
        {
            if (input == null)
                return await Task<Response<HttpStatusCode, ElmahErrorModel>>.FromResult(new Response<HttpStatusCode, ElmahErrorModel> { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing = _dbcontext.ElmahErrors.SingleOrDefault(t => t.ErrorId == input.ErrorId);

                // TODO: can create a new record here.
                if (existing == null)
                    return await Task<Response<HttpStatusCode, ElmahErrorModel>>.FromResult(new Response<HttpStatusCode, ElmahErrorModel> { Status = HttpStatusCode.NotFound });

                // TODO: the .CopyTo<> method may modified because some properties may should not be copied.
                existing.ErrorId = input.ErrorId;
                existing.Application = input.Application;
                existing.Host = input.Host;
                existing.Type = input.Type;
                existing.Source = input.Source;
                existing.Message = input.Message;
                existing.User = input.User;
                existing.StatusCode = input.StatusCode;
                existing.TimeUtc = input.TimeUtc;
                existing.Sequence = input.Sequence;
                existing.AllXml = input.AllXml;
                await _dbcontext.SaveChangesAsync();
                return await Task<Response<HttpStatusCode, ElmahErrorModel>>.FromResult(
                    new Response<HttpStatusCode, ElmahErrorModel>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = new ElmahErrorModel
                        {
                            ErrorId = existing.ErrorId,
                            Application = existing.Application,
                            Host = existing.Host,
                            Type = existing.Type,
                            Source = existing.Source,
                            Message = existing.Message,
                            User = existing.User,
                            StatusCode = existing.StatusCode,
                            TimeUtc = existing.TimeUtc,
                            Sequence = existing.Sequence,
                            AllXml = existing.AllXml,
                        }
                    });
            }
            catch (Exception ex)
            {
                return await Task<Response<HttpStatusCode, ElmahErrorModel>>.FromResult(new Response<HttpStatusCode, ElmahErrorModel> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }
    }
}

