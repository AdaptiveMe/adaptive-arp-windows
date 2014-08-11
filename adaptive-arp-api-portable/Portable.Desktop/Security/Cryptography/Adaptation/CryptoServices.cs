// -----------------------------------------------------------------------
// Copyright (c) David Kean. All rights reserved.
// -----------------------------------------------------------------------
extern alias pcl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PclCryptographicException = pcl::System.Security.Cryptography.CryptographicException;

namespace System.Security.Cryptography.Adaptation
{
    internal static class CryptoServices
    {
        public static void TranslateExceptions(Action action)
        {
            TranslateExceptions<object>(() => { action(); return null; });
        }

        public static T TranslateExceptions<T>(Func<T> action)
        {
            try
            {
                return action();
            }
            catch (CryptographicException ex)
            {
                throw new PclCryptographicException(ex.Message, ex.InnerException);
            }
        }
    }
}
