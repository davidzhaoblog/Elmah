using Elmah.Models;
using Elmah.RepositoryContracts;
using Elmah.EFCoreContext;
using Framework.Models;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Elmah.EFCoreRepositories
{
    public class ElmahUserRepository
        : IElmahUserRepository
    {
        private readonly ILogger<ElmahUserRepository> _logger;
        private readonly EFDbContext _dbcontext;

        public ElmahUserRepository(EFDbContext dbcontext, ILogger<ElmahUserRepository> logger)
        {
            _dbcontext = dbcontext;
            _logger = logger;
        }

        public async Task<Response<HttpStatusCode, ElmahUserModel>> Delete(ElmahUserIdModel id)
        {
            if (id == null)
                return await Task<Response<HttpStatusCode, ElmahUserModel>>.FromResult(new Response<HttpStatusCode, ElmahUserModel> { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing = _dbcontext.ElmahUsers.SingleOrDefault(t => t.User == id.User);

                if (existing == null)
                    return await Task<Response<HttpStatusCode, ElmahUserModel>>.FromResult(new Response<HttpStatusCode, ElmahUserModel> { Status = HttpStatusCode.NotFound });

                _dbcontext.ElmahUsers.Remove(existing);
                await _dbcontext.SaveChangesAsync();

                return await Task<Response<HttpStatusCode, ElmahUserModel>>.FromResult(
                    new Response<HttpStatusCode, ElmahUserModel>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = new ElmahUserModel
                        {
                            User = existing.User,
                        }
                    });
            }
            catch (Exception ex)
            {
                return await Task<Response<HttpStatusCode, ElmahUserModel>>.FromResult(new Response<HttpStatusCode, ElmahUserModel> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        public async Task<Response<HttpStatusCode, ElmahUserModel>> Get(ElmahUserIdModel id)
        {
            if (id == null)
                return await Task<Response<HttpStatusCode, ElmahUserModel>>.FromResult(new Response<HttpStatusCode, ElmahUserModel> { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing = _dbcontext.ElmahUsers.SingleOrDefault(t => t.User == id.User);

                // TODO: can create a new record here.
                if (existing == null)
                    return await Task<Response<HttpStatusCode, ElmahUserModel>>.FromResult(new Response<HttpStatusCode, ElmahUserModel> { Status = HttpStatusCode.BadRequest });

                return await Task<Response<HttpStatusCode, ElmahUserModel>>.FromResult(
                    new Response<HttpStatusCode, ElmahUserModel>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = new ElmahUserModel
                        {
                            User = existing.User,
                        }
                    });
            }
            catch (Exception ex)
            {
                return await Task<Response<HttpStatusCode, ElmahUserModel>>.FromResult(new Response<HttpStatusCode, ElmahUserModel> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        public async Task<Response<HttpStatusCode, ElmahUserModel>> Create(ElmahUserModel input)
        {
            if (input == null)
                return await Task<Response<HttpStatusCode, ElmahUserModel>>.FromResult(new Response<HttpStatusCode, ElmahUserModel> { Status = HttpStatusCode.BadRequest });
            try
            {
                var toInsert = new ElmahUser
                {
                            User = input.User,
                };
                await _dbcontext.ElmahUsers.AddAsync(toInsert);
                await _dbcontext.SaveChangesAsync();
                return await Task<Response<HttpStatusCode, ElmahUserModel>>.FromResult(
                    new Response<HttpStatusCode, ElmahUserModel>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = new ElmahUserModel
                        {
                            User = toInsert.User,
                        }
                    });
            }
            catch (Exception ex)
            {
                return await Task<Response<HttpStatusCode, ElmahUserModel>>.FromResult(new Response<HttpStatusCode, ElmahUserModel> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        public async Task<Response<HttpStatusCode, ElmahUserModel>> Update(ElmahUserModel input)
        {
            if (input == null)
                return await Task<Response<HttpStatusCode, ElmahUserModel>>.FromResult(new Response<HttpStatusCode, ElmahUserModel> { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing = _dbcontext.ElmahUsers.SingleOrDefault(t => t.User == input.User);

                // TODO: can create a new record here.
                if (existing == null)
                    return await Task<Response<HttpStatusCode, ElmahUserModel>>.FromResult(new Response<HttpStatusCode, ElmahUserModel> { Status = HttpStatusCode.NotFound });

                // TODO: the .CopyTo<> method may modified because some properties may should not be copied.
                existing.User = input.User;
                await _dbcontext.SaveChangesAsync();
                return await Task<Response<HttpStatusCode, ElmahUserModel>>.FromResult(
                    new Response<HttpStatusCode, ElmahUserModel>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = new ElmahUserModel
                        {
                            User = existing.User,
                        }
                    });
            }
            catch (Exception ex)
            {
                return await Task<Response<HttpStatusCode, ElmahUserModel>>.FromResult(new Response<HttpStatusCode, ElmahUserModel> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }
    }
}

