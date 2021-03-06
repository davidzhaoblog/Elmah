using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Framework.Xamariner
{
    /// <summary>
    /// Implementations of this interface MUST convert iOS and Android
    /// platform-specific locales to a value supported in .NET because
    /// ONLY valid .NET cultures can have their RESX resources loaded and used.
    /// <remarks>
    /// Lists of valid .NET cultures can be found here:
    ///   https://github.com/xamarin/xamarin-forms-samples/tree/master/UsingResxLocalization
    ///   http://www.localeplanet.com/dotnet/
    ///   http://www.csharp-examples.net/culture-names/
    /// You should always test all the locales implemented in your application.
    /// Sample apps built using the Xamarin.Forms framework.
    /// You exclude the 'Extension' suffix when using in Xaml markup
    ///
    /// License
    /// The Apache License 2.0 applies to all samples in this repository.
    /// Copyright 2011 Xamarin Inc
    /// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with the License. You may obtain a copy of the License at
    /// http://www.apache.org/licenses/LICENSE-2.0
    /// Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the License for the specific language governing permissions and limitations under the License.
    /// </remarks>
    /// </summary>
    /// <seealso cref="Xamarin.Forms.Xaml.IMarkupExtension" />
    [ContentProperty("Text")]
    public class TranslateExtension : IMarkupExtension
    {
        private static Dictionary<string, ResourceManager> ResourceIdToResourceManagerMapping = new Dictionary<string, ResourceManager>();
        readonly CultureInfo ci = null;

        public TranslateExtension() {
            ci = DependencyService.Get<Framework.Xamariner.ILocalize>().GetCurrentCultureInfo();
        }

        public string ResourceId { get; set; }
        public string Text { get; set; }

        public object ProvideValue (IServiceProvider serviceProvider)
        {
            if (Text == null)
                return "";

            ResourceManager temp = GetResourceManager(ResourceId);

            if (temp != null)
            {
                string translation;
                translation = temp.GetString(Text, ci);

                //if (ci == null)
                //{
                //    translation = temp.GetString(Text);
                //}
                //else
                //{
                //    translation = temp.GetString(Text, ci);
                //}

                if (translation == null)
                {
#if DEBUG
                    throw new ArgumentException(
                        String.Format("Key '{0}' was not found in resources '{1}' for culture '{2}'.", Text, ResourceId, ci.Name),
                        "Text");
#else
                translation = Text; // HACK: returns the key, which GETS DISPLAYED TO THE USER
#endif
                }
                return translation;
            }
            else
            {
                return this.Text;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="resourceId">the C# namespace of the .Resx</param>
        /// <returns></returns>
        public ResourceManager GetResourceManager(string resourceId)
        {
            if (ResourceIdToResourceManagerMapping.ContainsKey(resourceId))
                return ResourceIdToResourceManagerMapping[resourceId];
            return null;
        }

        /// <summary>
        /// The type will be used to get type fullname and Assembly, then tranform to ResourceManager
        /// type fullname is the resourceId.
        /// </summary>
        /// <param name="type"></param>
        public static void RegisterResourceManager(Type type)
        {
            if(!ResourceIdToResourceManagerMapping.ContainsKey(type.FullName))
                ResourceIdToResourceManagerMapping.Add(
                    type.FullName
                    , new ResourceManager(type.FullName, type.GetTypeInfo().Assembly));
        }
    }
}

