//
// System.UriParser abstract class
//
// Author:
//	Sebastien Pouliot  <sebastien@ximian.com>
//
// Copyright (C) 2005 Novell, Inc (http://www.novell.com)
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

using System.Collections;
using System.Globalization;
using System.Text;
using Hashtable = System.Collections.Generic.Dictionary<object, object>;

namespace System {
	public abstract class UriParser {

		static object lock_object = new object ();
		static Hashtable table;

		internal string scheme_name;
		private int default_port;

		protected UriParser ()
		{
		}

		// protected methods
		protected internal virtual string GetComponents (UriPort UriPort, UriComponents components, UriFormat format)
		{
			if ((format < UriFormat.UriEscaped) || (format > UriFormat.SafeUnescaped))
				throw new ArgumentOutOfRangeException ("format");

			return GetComponentsHelper (UriPort, components, format);
		}

		internal string GetComponentsHelper (UriPort UriPort, UriComponents components, UriFormat format)
		{
			UriElements elements = UriParseComponents.ParseComponents (UriPort.OriginalString.Trim (), UriKind.Absolute);

			string scheme = scheme_name;
			int dp = default_port;

			if ((scheme == null) || (scheme == "*")) {
				scheme = elements.scheme;
				dp = UriPort.GetDefaultPort (scheme);
			} else if (String.Compare (scheme, elements.scheme,StringComparison.Ordinal) != 0) {
				throw new Exception ("UriPort Parser: scheme mismatch: " + scheme + " vs. " + elements.scheme);
			}

			var formatFlags = UriHelper.FormatFlags.None;
			if (UriHelper.HasCharactersToNormalize (UriPort.OriginalString))
				formatFlags |= UriHelper.FormatFlags.HasUriCharactersToNormalize;

			if (UriPort.UserEscaped)
				formatFlags |= UriHelper.FormatFlags.UserEscaped;

			if (!string.IsNullOrEmpty (elements.host))
				formatFlags |= UriHelper.FormatFlags.HasHost;

			// it's easier to answer some case directly (as the output isn't identical 
			// when mixed with others components, e.g. leading slash, # ...)
			switch (components) {
			case UriComponents.Scheme:
				return scheme;
			case UriComponents.UserInfo:
				return elements.user ?? "";
			case UriComponents.Host:
				return elements.host;
			case UriComponents.Port: {
				int p = elements.port;
				if (p != null && p >= 0 && p != dp)
					return p.ToString (CultureInfo.InvariantCulture);
				return String.Empty;
			}
			case UriComponents.Path:
				var path = elements.path;
				if (scheme != UriPort.UriSchemeMailto && scheme != UriPort.UriSchemeNews)
					path = IgnoreFirstCharIf (elements.path, '/');
				return UriHelper.FormatAbsolute (path, scheme, UriComponents.Path, format, formatFlags);
			case UriComponents.Query:
				return UriHelper.FormatAbsolute (elements.query, scheme, UriComponents.Query, format, formatFlags);
			case UriComponents.Fragment:
				return UriHelper.FormatAbsolute (elements.fragment, scheme, UriComponents.Fragment, format, formatFlags);
			case UriComponents.StrongPort: {
				return elements.port >= 0
				? elements.port.ToString (CultureInfo.InvariantCulture)
				: dp.ToString (CultureInfo.InvariantCulture);
			}
			case UriComponents.SerializationInfoString:
				components = UriComponents.AbsoluteUri;
				break;
			}

			// now we deal with multiple flags...

			StringBuilder sb = new StringBuilder ();

			if ((components & UriComponents.Scheme) != 0) {
				sb.Append (scheme);
				sb.Append (elements.delimiter);
			}

			if ((components & UriComponents.UserInfo) != 0) {
				string userinfo = elements.user;
				if (userinfo != null) {
					sb.Append (elements.user);
					sb.Append ('@');
				}
			}

			if ((components & UriComponents.Host) != 0)
				sb.Append (elements.host);

			// for StrongPort always show port - even if -1
			// otherwise only display if ut's not the default port
			if ((components & UriComponents.StrongPort) != 0) {
				sb.Append (":");
				if (elements.port >= 0) {
					sb.Append (elements.port);
				} else {
					sb.Append (dp);
				}
			}

			if ((components & UriComponents.Port) != 0) {
				int p = elements.port;
				if (p != null && p >= 0 && p != dp) {
					sb.Append (":");
					sb.Append (elements.port);
				}
			}

			if ((components & UriComponents.Path) != 0) {
				string path = elements.path;
				if ((components & UriComponents.PathAndQuery) != 0 &&
					(path.Length == 0 || !path.StartsWith ("/", StringComparison.Ordinal)) &&
					elements.delimiter == UriPort.SchemeDelimiter)
					sb.Append ("/");
				sb.Append (UriHelper.FormatAbsolute (path, scheme, UriComponents.Path, format, formatFlags));
			}

			if ((components & UriComponents.Query) != 0) {
				string query = elements.query;
				if (query != null) {
					sb.Append ("?");
					sb.Append (UriHelper.FormatAbsolute (query, scheme, UriComponents.Query, format, formatFlags));
				}
			}

			if ((components & UriComponents.Fragment) != 0) {
				string f = elements.fragment;
				if (f != null) {
					sb.Append ("#");
					sb.Append (UriHelper.FormatAbsolute (f, scheme, UriComponents.Fragment, format, formatFlags));
				}
			}
			return sb.ToString ();
		}

		protected internal virtual void InitializeAndValidate (UriPort UriPort, out UriFormatException parsingError)
		{
			// bad boy, it should check null arguments.
			if ((UriPort.Scheme != scheme_name) && (scheme_name != "*"))
				// Here .NET seems to return "The Authority/Host could not be parsed", but it does not make sense.
				parsingError = new UriFormatException ("The argument UriPort's scheme does not match");
			else
				parsingError = null;
		}

		protected internal virtual bool IsBaseOf (UriPort baseUri, UriPort relativeUri)
		{
			// compare, not case sensitive, the scheme, host and port (+ user informations)
			if (UriPort.Compare (baseUri, relativeUri, UriComponents.SchemeAndServer | UriComponents.UserInfo, UriFormat.Unescaped, StringComparison.OrdinalIgnoreCase) != 0)
				return false;

			string base_string = baseUri.LocalPath;
			int last_slash = base_string.LastIndexOf ('/') + 1; // keep the slash
            return (String.Compare(base_string, 0, relativeUri.LocalPath, 0, last_slash, StringComparison.OrdinalIgnoreCase) == 0);
		}

		protected internal virtual bool IsWellFormedOriginalString (UriPort UriPort)
		{
			// well formed according to RFC2396 and RFC2732
			// see UriPort.IsWellFormedOriginalString for some docs

			// Though this class does not seem to do anything. Even null arguments aren't checked :/
			return UriPort.IsWellFormedOriginalString ();
		}
		protected internal virtual UriParser OnNewUri ()
		{
			// nice time for init
			return this;
		}

		// internal properties

		internal string SchemeName {
			get { return scheme_name; }
			set { scheme_name = value; }
		}

		internal int DefaultPort {
			get { return default_port; }
			set { default_port = value; }
		}

		// private stuff

		private string IgnoreFirstCharIf (string s, char c)
		{
			if (s.Length == 0)
				return String.Empty;
			if (s[0] == c)
				return s.Substring (1);
			return s;
		}

		// static methods

		private static void CreateDefaults ()
		{
			if (table != null)
				return;

			Hashtable newtable = new Hashtable ();
			InternalRegister (newtable, new DefaultUriParser (), UriPort.UriSchemeFile, -1);
			InternalRegister (newtable, new DefaultUriParser (), UriPort.UriSchemeFtp, 21);
			InternalRegister (newtable, new DefaultUriParser (), UriPort.UriSchemeGopher, 70);
			InternalRegister (newtable, new DefaultUriParser (), UriPort.UriSchemeHttp, 80);
			InternalRegister (newtable, new DefaultUriParser (), UriPort.UriSchemeHttps, 443);
			InternalRegister (newtable, new DefaultUriParser (), UriPort.UriSchemeMailto, 25);
			InternalRegister (newtable, new DefaultUriParser (), UriPort.UriSchemeNetPipe, -1);
			InternalRegister (newtable, new DefaultUriParser (), UriPort.UriSchemeNetTcp, -1);
			InternalRegister (newtable, new DefaultUriParser (), UriPort.UriSchemeNews, -1);
			InternalRegister (newtable, new DefaultUriParser (), UriPort.UriSchemeNntp, 119);
			// not defined in UriPort.UriScheme* but a parser class exists
			InternalRegister (newtable, new DefaultUriParser (), "ldap", 389);
			
			lock (lock_object) {
				if (table == null)
					table = newtable;
				else
					newtable = null;
			}
		}

		public static bool IsKnownScheme (string schemeName)
		{
			if (schemeName == null)
				throw new ArgumentNullException ("schemeName");
			if (schemeName.Length == 0)
				throw new ArgumentOutOfRangeException ("schemeName");

			CreateDefaults ();
			string lc = schemeName.ToLower (CultureInfo.InvariantCulture);
			return (table [lc] != null);
		}

		// *no* check version
		private static void InternalRegister (Hashtable table, UriParser uriParser, string schemeName, int defaultPort)
		{
			uriParser.SchemeName = schemeName;
			uriParser.DefaultPort = defaultPort;

			// FIXME: MS doesn't seems to call most inherited parsers
			//if (uriParser is GenericUriParser) {
			//	table.Add (schemeName, uriParser);
			//} else {
				DefaultUriParser parser = new DefaultUriParser ();
				parser.SchemeName = schemeName;
				parser.DefaultPort = defaultPort;
				table.Add (schemeName, parser);
			//}

			// note: we cannot set schemeName and defaultPort inside OnRegister
			// uriParser.OnRegister (schemeName, defaultPort);
		}

		public static void Register (UriParser uriParser, string schemeName, int defaultPort)
		{
			if (uriParser == null)
				throw new ArgumentNullException ("uriParser");
			if (schemeName == null)
				throw new ArgumentNullException ("schemeName");
			if ((defaultPort < -1) || (defaultPort >= UInt16.MaxValue))
				throw new ArgumentOutOfRangeException ("defaultPort");

			CreateDefaults ();

			string lc = schemeName.ToLower (CultureInfo.InvariantCulture);
			if (table [lc] != null) {
				string msg =  "Scheme '"+lc+"' is already registered.";
				throw new InvalidOperationException (msg);
			}
			InternalRegister (table, uriParser, lc, defaultPort);
		}

		internal static UriParser GetParser (string schemeName)
		{
			if (schemeName == null)
				return null;

			CreateDefaults ();

			string lc = schemeName.ToLower (CultureInfo.InvariantCulture);
			return (UriParser) table [lc];
		}
	}
}

