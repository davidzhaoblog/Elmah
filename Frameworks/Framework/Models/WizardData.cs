using System.Collections.Generic;

namespace Framework.Models
{
    public struct WizardData
    {
        /// <summary>
        /// e.g. Welcome wizard
        /// </summary>
        //public string Name;

        public bool Completed;
        public string CurrentItemName;
        public List<WizardDataItem> Items;
    }

    public struct WizardDataItem
    {
        public string Name;
        public bool Completed;
    }
}

