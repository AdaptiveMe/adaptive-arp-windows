/*
 * =| ADAPTIVE RUNTIME PLATFORM |=======================================================================================
 *
 * (C) Copyright 2013-2014 Carlos Lozano Diez t/a Adaptive.me <http://adaptive.me>.
 *
 * Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with
 * the License. You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on
 * an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the License for the
 * specific language governing permissions and limitations under the License.
 *
 * Original author:
 *
 *     * Carlos Lozano Diez
 *                 <http://github.com/carloslozano>
 *                 <http://twitter.com/adaptivecoder>
 *                 <mailto:carlos@adaptive.me>
 *
 * Contributors:
 *
 *     * David Barranco Bonilla
 *             <https://github.com/aryslan>
 *             <mailto:ddbc@gft.com>
 *
 * =====================================================================================================================
 */

using Adaptive.Arp.Api;
using Adaptive.Arp.Impl.Globalization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Windows.Storage;

namespace Adaptive.Arp.Impl.WinPhone
{
    public class GlobalizationImpl : IGlobalization
    {
        protected const String APP_CONFIG_PATH = @"Html/app/config";
        protected const String DICT_TAG = "dict";
        protected const String I18N_CONFIG_FILE = @"Html/app/config/i18n-config.xml";
        protected const String KEY_TAG = "key";
        protected const String PLIST_EXTENSION = ".plist";
        protected GlobalizationConfig _I18NConfiguration = null;
        protected Dictionary<string, SortedDictionary<String, string>> languageFilesDictionary = new Dictionary<string, SortedDictionary<string, string>>();

        public GlobalizationImpl()
        {
            //ManualResetEvent AllFinished = new ManualResetEvent(false);
            TaskFactory taskFactory = new TaskFactory();
            var myTask = taskFactory.StartNew<Task<bool>>(async () =>
            {
                try
                {
                    StorageFile I18NConfigFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri(@"ms-appx:///" + I18N_CONFIG_FILE));
                    XmlSerializer serializer = new XmlSerializer(typeof(GlobalizationConfig));
                    XmlReader reader = XmlReader.Create(await I18NConfigFile.OpenStreamForReadAsync());
                    _I18NConfiguration = (GlobalizationConfig)serializer.Deserialize(reader);
                    FillLanguagesDictionary();
                    //AllFinished.Set();
                    return true;
                }
                catch (Exception)
                {
                    //AllFinished.Set();
                    return false;
                }
            });
            /*AllFinished.WaitOne();
            if (myTask.Result.Result)
            {
                //ALL OK
            }
            else
            {
                //ERROR HAPPENED
            }*/
        }

        public string[] GetLocaleSupportedDescriptors()
        {
            return (languageFilesDictionary != null && languageFilesDictionary.Count > 0) ? languageFilesDictionary.Keys.ToArray() : null;
        }

        public string GetResourceLiteral(string key, Locale locale)
        {
            if (!String.IsNullOrWhiteSpace(key))
            {
                key = key.ToUpper();
                String sLocaleName = (locale == null) ? _I18NConfiguration.DefaultLanguage.Language.ToLower() : locale.ToString().ToLower();
                sLocaleName = (sLocaleName.StartsWith("-")) ? sLocaleName.Remove(0, 1) : sLocaleName;
                sLocaleName = (sLocaleName.EndsWith("-")) ? sLocaleName.Remove(sLocaleName.Length - 1, 1) : sLocaleName;
                if (languageFilesDictionary.ContainsKey(sLocaleName) && languageFilesDictionary[sLocaleName].ContainsKey(key))
                {
                    return languageFilesDictionary[sLocaleName][key].ToString();
                }
                else
                {
                    string sNewLocalName = (!String.IsNullOrWhiteSpace(locale.GetLanguage())) ? locale.GetLanguage().ToLower() : String.Empty;
                    if (!sNewLocalName.Equals(sLocaleName) && !String.IsNullOrWhiteSpace(sNewLocalName))
                    {
                        if (languageFilesDictionary.ContainsKey(sLocaleName) && languageFilesDictionary[sLocaleName].ContainsKey(key))
                        {
                            return languageFilesDictionary[sLocaleName][key].ToString();
                        }
                    }
                    sNewLocalName = (!String.IsNullOrWhiteSpace(locale.GetCountry())) ? locale.GetCountry().ToLower() : String.Empty;
                    if (!sNewLocalName.Equals(sLocaleName) && !String.IsNullOrWhiteSpace(sNewLocalName))
                    {
                        if (languageFilesDictionary.ContainsKey(sLocaleName) && languageFilesDictionary[sLocaleName].ContainsKey(key))
                        {
                            return languageFilesDictionary[sLocaleName][key].ToString();
                        }
                    }
                }
            }
            return String.Empty;
        }

        public IDictionary<string, string> GetResourceLiterals(Locale locale)
        {
            String sLocaleName = (locale == null) ? _I18NConfiguration.DefaultLanguage.Language.ToLower() : locale.ToString().ToLower();
            sLocaleName = (sLocaleName.StartsWith("-")) ? sLocaleName.Remove(0, 1) : sLocaleName;
            sLocaleName = (sLocaleName.EndsWith("-")) ? sLocaleName.Remove(sLocaleName.Length - 1, 1) : sLocaleName;
            if (languageFilesDictionary.ContainsKey(sLocaleName))
                return languageFilesDictionary[sLocaleName];
            else
            {
                string sNewLocalName = (!String.IsNullOrWhiteSpace(locale.GetLanguage())) ? locale.GetLanguage().ToLower() : String.Empty;
                if (!sNewLocalName.Equals(sLocaleName) && !String.IsNullOrWhiteSpace(sNewLocalName))
                {
                    if (languageFilesDictionary.ContainsKey(sLocaleName))
                    {
                        return languageFilesDictionary[sLocaleName];
                    }
                }
                sNewLocalName = (!String.IsNullOrWhiteSpace(locale.GetCountry())) ? locale.GetCountry().ToLower() : String.Empty;
                if (!sNewLocalName.Equals(sLocaleName) && !String.IsNullOrWhiteSpace(sNewLocalName))
                {
                    if (languageFilesDictionary.ContainsKey(sLocaleName))
                    {
                        return languageFilesDictionary[sLocaleName];
                    }
                }
            }
            return null;
        }

        #region PRIVATE_METHODS

        private async void FillLanguagesDictionary()
        {
            StorageFolder configFolder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(APP_CONFIG_PATH);
            var files = (await configFolder.GetFilesAsync()).Where(x => x.FileType.Equals(PLIST_EXTENSION)).ToList();

            Parallel.ForEach(files, async (languageFile) =>
            {
                SortedDictionary<string, string> languageFileContentDictionary = new SortedDictionary<string, string>();
                using (var fileStream = await languageFile.OpenStreamForReadAsync())
                {
                    XDocument xDoc = XDocument.Load(fileStream);
                    if (xDoc.Root != null && xDoc.Root.HasElements)
                    {
                        var dict = xDoc.Root.Element(XName.Get(DICT_TAG));
                        if (dict != null && dict.HasElements)
                        {
                            var elementList = dict.Descendants().ToList();
                            if (elementList.Count % 2 == 0)
                            {
                                for (int i = 0; i < elementList.Count; i += 2)
                                {
                                    var keyElement = elementList[i];
                                    var valueElement = elementList[i + 1];

                                    if (keyElement.Name.LocalName.Equals(KEY_TAG, StringComparison.CurrentCultureIgnoreCase))
                                    {
                                        if (!languageFileContentDictionary.ContainsKey(keyElement.Value.ToUpper()))
                                        {
                                            languageFileContentDictionary.Add(keyElement.Value.ToUpper(), valueElement.Value);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                languageFilesDictionary.Add(languageFile.DisplayName.ToLower(), languageFileContentDictionary);
            });
        }

        #endregion PRIVATE_METHODS
    }
}