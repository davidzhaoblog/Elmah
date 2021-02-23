using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;

using Elmah.MvcCore.Models;
using Elmah.MvcCore.Models.AccountViewModels;
using Elmah.MvcCore.Services;

namespace Elmah.MvcCore.Controllers
{
    [Route("/api/[controller]/[action]")]
    public class AuthenticationApiController : Controller
    {
        private readonly UserManager<Elmah.MvcCore.Models.ApplicationUser> _userManager;
        private readonly SignInManager<Elmah.MvcCore.Models.ApplicationUser> _signInManager;
        private readonly IServiceProvider _serviceProvider;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        private readonly AppSettings _appSettings;
        private readonly Elmah.MvcCore.Services.GoogleAuthService _googleAuthService;
        //private readonly Elmah.MvcCore.Services.FacebookAuthService _facebookAuthService;

        public AuthenticationApiController(
            UserManager<Elmah.MvcCore.Models.ApplicationUser> userManager,
            SignInManager<Elmah.MvcCore.Models.ApplicationUser> signInManager,
            IServiceProvider serviceProvider,
            IEmailSender emailSender,
            ILogger<AuthenticationApiController> logger,
            IOptions<AppSettings> appSettings,
            Elmah.MvcCore.Services.GoogleAuthService googleAuthService
            //,
            //Elmah.MvcCore.Services.FacebookAuthService facebookAuthService
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _serviceProvider = serviceProvider;
            _emailSender = emailSender;
            _logger = logger;
            _appSettings = appSettings.Value;
            _googleAuthService = googleAuthService;
            //_facebookAuthService = facebookAuthService;
        }

        [HttpPost]
        public async Task<Framework.WebApi.AuthenticationResponse> Login([FromBody] Elmah.MvcCore.Models.AccountViewModels.LoginViewModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Email);
            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            return await GetAuthenticationResponse(user, result);
        }

        private async Task<Framework.WebApi.AuthenticationResponse> GetAuthenticationResponse(
            Elmah.MvcCore.Models.ApplicationUser user
            , Microsoft.AspNetCore.Identity.SignInResult result)
        {
            var loginResponse = new Framework.WebApi.AuthenticationResponse
            {
                Succeeded = result.Succeeded
                ,
                IsLockedOut = result.IsLockedOut
                ,
                IsNotAllowed = result.IsNotAllowed
                ,
                RequiresTwoFactor = result.RequiresTwoFactor
                ,
                EntityID = user != null ? user.EntityID : null
            };

            // authentication successful, then generate jwt token
            string tokenInString = Elmah.MvcCore.Security.CustomizedClaimTypes.GetJwtSecurityTokenInString(user.Id.ToLower(), _appSettings.Secret);
            loginResponse.Token = tokenInString;

            // Load LogIn User related data
            if (loginResponse.Succeeded)
            {
                loginResponse.Roles = await _userManager.GetRolesAsync(user);

                #region TODO: Load more data to LoginResponse

                //// TODO: Load more data to LoginResponse
                //using (var scope = _serviceProvider.CreateScope())
                //{
                //    var criteria = new NTierOnTime.CommonBLLEntities.EntityChainedQueryCriteriaIdentifier();
                //    criteria.Identifier.EntityID.NullableValueToCompare = user.EntityID;

                //    var entityResponse = new NTierOnTime.AspNetMvcCoreViewModel.Entity.DashboardVM(); // TODO: how to IoC
                //    entityResponse.CriteriaOfMasterEntity = criteria;
                //    entityResponse.SetServiceProvider(this._serviceProvider);
                //    await entityResponse.LoadData(
                //        isToLoadFK_CourseCategory_Entity_ParentEntityID_List: false
                //        , isToLoadFK_Album_Entity_Owner_List: false
                //        , isToLoadFK_Comment_Entity_PostedByID_List: false
                //        , isToLoadFK_EntityAddress_Entity_EntityID_List: false
                //        , isToLoadFK_EntityAlbum_Entity_EntityID_List: false
                //        , isToLoadFK_EntityCalendarItem_Entity_EntityID_List: false
                //        , isToLoadFK_EntityCommentThread_Entity_EntityID_List: false
                //        , isToLoadFK_EntityContact_Entity_EntityID_List: false
                //        , isToLoadFK_EntityEmail_Entity_EntityID_List: false
                //        , isToLoadFK_EntityScheduleGroup_Entity_EntityID_List: false
                //        , isToLoadFK_EntityVirtualAddress_Entity_EntityID_List: false
                //        , isToLoadFK_Liking_Entity_EntityID_List: false
                //        , isToLoadFK_Liking_Entity_TheOtherSideEntityID_List: false
                //        , isToLoadFK_MemberProgram_Entity_ProgramEntityID_List: false
                //        , isToLoadFK_Membership_Entity_MasterEntityID_List: false
                //        , isToLoadFK_Membership_Entity_SlaveEntityID_List: true
                //        , isToLoadFK_ProgramScheduleCalendarItem_Entity_ProgramEntityID_List: false
                //        , isToLoadFK_BusinessEntity_Entity_EntityID_FormView: false
                //        , isToLoadFK_Class_Entity_EntityID_FormView: false
                //        , isToLoadFK_Course_Entity_EntityID_FormView: false
                //        , isToLoadFK_ActivitySummary_Entity_EntityID_FormView: false
                //        , isToLoadFK_Membership_Entity_MembershipID_FormView: true
                //        , isToLoadFK_Person_Entity_EntityID_FormView: true);

                //    // 1. Entity
                //    if (entityResponse.StatusOfMasterEntity == Framework.Services.BusinessLogicLayerResponseStatus.MessageOK || entityResponse.StatusOfMasterEntity == Framework.Services.BusinessLogicLayerResponseStatus.UIProcessReady)
                //    {
                //        loginResponse.Entity = entityResponse.MasterEntity;
                //    }

                //    // 2. Person
                //    if (entityResponse.StatusOfFK_Person_Entity_EntityID_FormView == Framework.Services.BusinessLogicLayerResponseStatus.MessageOK || entityResponse.StatusOfFK_Person_Entity_EntityID_FormView == Framework.Services.BusinessLogicLayerResponseStatus.UIProcessReady)
                //    {
                //        loginResponse.HasPerson = entityResponse.StatusOfFK_Person_Entity_EntityID_FormView == Framework.Services.BusinessLogicLayerResponseStatus.MessageOK || entityResponse.StatusOfFK_Person_Entity_EntityID_FormView == Framework.Services.BusinessLogicLayerResponseStatus.UIProcessReady;
                //        loginResponse.Person = entityResponse.FK_Person_Entity_EntityID_FormView;
                //    }

                //    // 3. Joined Memberships
                //    if (entityResponse.StatusOfFK_Membership_Entity_SlaveEntityID_List == Framework.Services.BusinessLogicLayerResponseStatus.MessageOK || entityResponse.StatusOfFK_Membership_Entity_SlaveEntityID_List == Framework.Services.BusinessLogicLayerResponseStatus.UIProcessReady)
                //    {
                //        loginResponse.HasJoinedMemberShip = entityResponse.StatusOfFK_Membership_Entity_SlaveEntityID_List == Framework.Services.BusinessLogicLayerResponseStatus.MessageOK || entityResponse.StatusOfFK_Membership_Entity_SlaveEntityID_List == Framework.Services.BusinessLogicLayerResponseStatus.UIProcessReady;
                //        loginResponse.JoinedMemberships = entityResponse.FK_Membership_Entity_SlaveEntityID_List;
                //    }

                //}

                #endregion TODO: Load more data to LoginResponse
            }
            return loginResponse;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public async Task<Framework.WebApi.AuthenticationResponse> Logout([FromBody] Elmah.MvcCore.Models.AccountViewModels.LoginViewModel model)
        {

            // TODO: set a flag? last log out time
            return new Framework.WebApi.AuthenticationResponse { Succeeded = true };
        }

        // POST api/Account/Register
        [AllowAnonymous]
        [HttpPost]
        public async Task<Framework.WebApi.AuthenticationResponse> Register([FromBody] Elmah.MvcCore.Models.AccountViewModels.RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return new Framework.WebApi.AuthenticationResponse { Succeeded = false };
            }

            var user = new Elmah.MvcCore.Models.ApplicationUser() { UserName = model.Email, Email = model.Email };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return new Framework.WebApi.AuthenticationResponse { Succeeded = false };
            }
            else
            {
                // This is a copy from Register method in AccountController.
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);;
                await _emailSender.SendEmailConfirmationAsync(model.Email, callbackUrl);

                /*
                var service1 = _serviceProvider.GetRequiredService<NTierOnTime.WcfContracts.IEntityService>();

                var response = await NTierOnTime.CoreCommonBLL.Helpers.EntityHelper.CreateNewEntity(service1, model.Email, _logger);

                if (response.BusinessLogicLayerResponseStatus == Framework.Services.BusinessLogicLayerResponseStatus.MessageOK || response.BusinessLogicLayerResponseStatus == Framework.Services.BusinessLogicLayerResponseStatus.UIProcessReady)
                {
                    var applicationUser = await _userManager.FindByEmailAsync(model.Email);
                    if (applicationUser != null)
                    {
                        applicationUser.EntityID = response.Message[0].EntityID;
                        await _userManager.UpdateAsync(applicationUser);
                    }
                }
                */
            }

            var loginViewModel = new Elmah.MvcCore.Models.AccountViewModels.LoginViewModel { Email = model.Email, Password = model.Password };

            return await Login(loginViewModel);
        }

        [HttpPost]
        public async Task<Framework.WebApi.AuthenticationResponse> Google([FromBody] Framework.WebApi.UserTokenIdModel model)
        {
            try
            {
                var user = await _googleAuthService.Authenticate(model);

                if (user == null)
                {
                    Debug.WriteLine("ApplicationUser not created: {0}", model.Email);
                    return new Framework.WebApi.AuthenticationResponse { Succeeded = false };
                }
                /*
                if(!user.EntityID.HasValue)
                {
                    var service1 = _serviceProvider.GetRequiredService<NTierOnTime.WcfContracts.IEntityService>();

                    var response = await NTierOnTime.CoreCommonBLL.Helpers.EntityHelper.CreateNewEntity(service1, model.Email, _logger);

                    if (response.BusinessLogicLayerResponseStatus == Framework.Services.BusinessLogicLayerResponseStatus.MessageOK || response.BusinessLogicLayerResponseStatus == Framework.Services.BusinessLogicLayerResponseStatus.UIProcessReady)
                    {
                        var applicationUser = await _userManager.FindByEmailAsync(model.Email);
                        if (applicationUser != null)
                        {
                            applicationUser.EntityID = response.Message[0].EntityID;
                            await _userManager.UpdateAsync(applicationUser);
                        }
                    }
                }
                */
                await _signInManager.SignInAsync(user, true);
                return await GetAuthenticationResponse(user);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return new Framework.WebApi.AuthenticationResponse { Succeeded = false };
            }
        }

        //[HttpPost]
        //public async Task<IActionResult> Facebook([FromBody] UserTokenIdModel token)
        //{
        //    try
        //    {
        //        var user = await _facebookAuthService.Authenticate(token);
        //        await _signInManager.SignInAsync(user, true);
        //        //var jwtToken = GenerateJwtToken(user.Email, user);
        //        // authentication successful, then generate jwt token
        //        string tokenInString = Elmah.MvcCore.Security.CustomizedClaimTypes.GetJwtSecurityTokenInString(user.Id.ToLower(), _appSettings.Secret);
        //        return Ok(tokenInString);
        //    }
        //    catch (Exception e)
        //    {
        //        Debug.WriteLine(e.Message);
        //        return BadRequest(e.Message);
        //    }
        //}

        private async Task<Framework.WebApi.AuthenticationResponse> GetAuthenticationResponse(
            Elmah.MvcCore.Models.ApplicationUser user)
        {
            var loginResponse = new Framework.WebApi.AuthenticationResponse
            {
                Succeeded = true
                ,
                IsLockedOut = false
                ,
                IsNotAllowed = false
                ,
                RequiresTwoFactor = false
                ,
                EntityID = user != null ? user.EntityID : null
            };

            // authentication successful, then generate jwt token
            string tokenInString = Elmah.MvcCore.Security.CustomizedClaimTypes.GetJwtSecurityTokenInString(user.Id.ToLower(), _appSettings.Secret);
            loginResponse.Token = tokenInString;

            // Load LogIn User related data
            if (loginResponse.Succeeded)
            {
                loginResponse.Roles = await _userManager.GetRolesAsync(user);
            }

            return loginResponse;
        }

        private IActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return StatusCode(500);
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }
    }
}

