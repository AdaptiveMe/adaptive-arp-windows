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
 *     * 
 *
 * =====================================================================================================================
 */
using Adaptive.Arp.Api;
using System;
using System.Collections.Generic;
using Windows.Storage;

namespace Adaptive.Arp.Impl
{
    public class SecurityImpl : ISecurity
    {
        public void DeleteSecureKeyValuePairs(string[] keys, string publicAccessName, Api.ISecureKVResultCallback callback)
        {
            try
            {
                var keychain = (ApplicationData.Current.LocalSettings.Containers.ContainsKey(publicAccessName)) ? ApplicationData.Current.LocalSettings.Containers[publicAccessName] : null;
                if (keychain != null)
                {
                    List<SecureKeyPair> successfullKeyPairs = new List<SecureKeyPair>();
                    List<SecureKeyPair> failedKeyPairs = new List<SecureKeyPair>();
                    foreach (string key in keys)
                    {
                        if (keychain.Values.ContainsKey(key))
                        {
                            keychain.Values.Remove(key);
                            SecureKeyPair entry = new SecureKeyPair();
                            entry.SetKey(key);
                            entry.SetValue(keychain.Values[key].ToString());
                            successfullKeyPairs.Add(entry);
                        }
                        //else
                        //failedKeyPairs.Add(key);
                    }
                    if (successfullKeyPairs.Count > 0) callback.OnResult(successfullKeyPairs.ToArray());
                }
                else
                {
                    callback.OnError(ISecureKVResultCallback.Error.NoPermission);
                }
            }
            catch (Exception)
            {
                callback.OnError(ISecureKVResultCallback.Error.NoPermission);
            }
        }

        public void GetSecureKeyValuePairs(string[] keys, string publicAccessName, Api.ISecureKVResultCallback callback)
        {
            try
            {
                var keychain = (ApplicationData.Current.LocalSettings.Containers.ContainsKey(publicAccessName)) ? ApplicationData.Current.LocalSettings.Containers[publicAccessName] : null;
                if (keychain != null)
                {
                    List<SecureKeyPair> foundKeyPairs = new List<SecureKeyPair>();
                    foreach (string key in keys)
                    {
                        if (keychain.Values.ContainsKey(key))
                        {
                            SecureKeyPair foundPair = new SecureKeyPair();
                            foundPair.SetKey(key);
                            foundPair.SetValue(keychain.Values[key].ToString());
                            foundKeyPairs.Add(foundPair);
                        }
                    }
                    if (foundKeyPairs.Count > 0) callback.OnResult(foundKeyPairs.ToArray());
                    else callback.OnError(ISecureKVResultCallback.Error.NoMatchesFound);
                }
                else
                {
                    callback.OnError(ISecureKVResultCallback.Error.NoPermission);
                }
            }
            catch (Exception ex)
            {
                callback.OnError(ISecureKVResultCallback.Error.NoPermission);
            }
        }

        public bool IsDeviceModified()
        {
            throw new NotImplementedException();
        }

        public void SetSecureKeyValuePairs(Api.SecureKeyPair[] keyValues, string publicAccessName, Api.ISecureKVResultCallback callback)
        {
            try
            {
                var keychain = (ApplicationData.Current.LocalSettings.Containers.ContainsKey(publicAccessName)) ? ApplicationData.Current.LocalSettings.Containers[publicAccessName] : ApplicationData.Current.LocalSettings.CreateContainer(publicAccessName, ApplicationDataCreateDisposition.Always);
                if (keychain != null)
                {
                    List<SecureKeyPair> successfullKeyPairs = new List<SecureKeyPair>();
                    List<SecureKeyPair> overridenKeyPairs = new List<SecureKeyPair>();
                    List<SecureKeyPair> failedKeyPairs = new List<SecureKeyPair>();
                    foreach (SecureKeyPair entry in keyValues)
                    {
                        try
                        {
                            if (keychain.Values.ContainsKey(entry.GetKey()))
                            {
                                keychain.Values[entry.GetKey()] = entry.GetValue();
                                overridenKeyPairs.Add(entry);
                            }
                            else
                            {
                                keychain.Values.Add(entry.GetKey(), entry.GetValue());
                                successfullKeyPairs.Add(entry);
                            }
                        }
                        catch (Exception ex)
                        {
                            failedKeyPairs.Add(entry);
                        }
                    }
                    if (successfullKeyPairs.Count > 0) callback.OnResult(successfullKeyPairs.ToArray());
                    if (overridenKeyPairs.Count > 0) callback.OnWarning(overridenKeyPairs.ToArray(), ISecureKVResultCallback.Warning.EntryOverride);
                    if (successfullKeyPairs.Count == 0 && overridenKeyPairs.Count == 0) callback.OnError(ISecureKVResultCallback.Error.NoMatchesFound);
                }
                else
                {
                    callback.OnError(ISecureKVResultCallback.Error.NoMatchesFound);
                }
            }
            catch (Exception ex)
            {
                callback.OnError(ISecureKVResultCallback.Error.NoPermission);
            }
        }
    }
}