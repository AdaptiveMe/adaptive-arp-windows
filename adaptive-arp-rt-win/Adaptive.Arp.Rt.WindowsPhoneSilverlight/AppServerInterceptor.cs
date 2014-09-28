using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adaptive.Arp.Rt.WindowsPhoneSilverlight
{
    public class AppServerInterceptor : UriParser
    {
        // Summary:
        //     Gets the components from a URI.
        //
        // Parameters:
        //   uri:
        //     The System.Uri to parse.
        //
        //   components:
        //     The System.UriComponents to retrieve from uri.
        //
        //   format:
        //     One of the System.UriFormat values that controls how special characters are
        //     escaped.
        //
        // Returns:
        //     A string that contains the components.
        //
        // Exceptions:
        //   System.ArgumentOutOfRangeException:
        //     format is invalid- or -components is not a combination of valid System.UriComponents
        //     values.
        //
        //   System.InvalidOperationException:
        //     uri requires user-driven parsing- or -uri is not an absolute URI. Relative
        //     URIs cannot be used with this method.

        protected override string GetComponents(Uri uri, UriComponents components, UriFormat format)
        {
            Debug.WriteLine("GetComponents {0} {1}", uri, components);
            return base.GetComponents(uri, components, format);
        }

        //
        // Summary:
        //     Initialize the state of the parser and validate the URI.
        //
        // Parameters:
        //   uri:
        //     The System.Uri to validate.
        //
        //   parsingError:
        //     Validation errors, if any.
        protected override void InitializeAndValidate(Uri uri, out UriFormatException parsingError)
        {
            Debug.WriteLine("InitializeAndValidate {0}", uri);
            parsingError = null;
        }
        //
        // Summary:
        //     Determines whether baseUri is a base URI for relativeUri.
        //
        // Parameters:
        //   baseUri:
        //     The base URI.
        //
        //   relativeUri:
        //     The URI to test.
        //
        // Returns:
        //     true if baseUri is a base URI for relativeUri; otherwise, false.
        protected override bool IsBaseOf(Uri baseUri, Uri relativeUri)
        {
            Debug.WriteLine("IsBaseOf {0} {1}",baseUri, relativeUri);
            return false;
        }
        //
        // Summary:
        //     Indicates whether a URI is well-formed.
        //
        // Parameters:
        //   uri:
        //     The URI to check.
        //
        // Returns:
        //     true if uri is well-formed; otherwise, false.
        protected override bool IsWellFormedOriginalString(Uri uri)
        {
            Debug.WriteLine("IsWebFormed {0}", uri);
            return true;
        }
        //
        // Summary:
        //     Called by System.Uri constructors and Overload:System.Uri.TryCreate to resolve
        //     a relative URI.
        //
        // Parameters:
        //   baseUri:
        //     A base URI.
        //
        //   relativeUri:
        //     A relative URI.
        //
        //   parsingError:
        //     Errors during the resolve process, if any.
        //
        // Returns:
        //     The string of the resolved relative System.Uri.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     baseUri parameter is not an absolute System.Uri- or -baseUri parameter requires
        //     user-driven parsing.
        protected override string Resolve(Uri baseUri, Uri relativeUri, out UriFormatException parsingError)
        {
            parsingError = null;
            Debug.WriteLine("Resolve baseUri: {0}  relativeUri: {1}", baseUri, relativeUri);
            return "";
        }
    }
}
