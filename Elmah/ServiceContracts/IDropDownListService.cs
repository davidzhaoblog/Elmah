using Elmah.Models;
using Framework.Models;
namespace Elmah.ServiceContracts
{
    public interface IDropDownListService
    {
        /// <summary>
        /// This method will be used to get top level dropdownlists from database for Search and Editing, to minimize roundtrip.
        /// the Key comes from {SolutionName}.Models.Definitions.TopLevelDropDownLists
        /// </summary>
        /// <returns></returns>
        Task<Dictionary<string, List<Framework.Models.NameValuePair>>> GetTopLevelDropDownListsFromDatabase(Elmah.Models.Definitions.TopLevelDropDownLists[] options);
    }
}

