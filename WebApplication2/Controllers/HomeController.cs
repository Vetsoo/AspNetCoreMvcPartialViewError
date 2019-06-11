using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PartialViewsErrorTestApp.Models;

namespace PartialViewsErrorTestApp.Controllers
{
	public class HomeController : Controller
	{
		public async Task<ActionResult> Index()
		{
			var context = this.HttpContext;
			var path = context.Request.Path.Value;

			this.ViewBag.NewUser = context.Request.Cookies.ContainsKey("CookieDisclaimer") || this
				                       .HttpContext.Request.Cookies["CookieDisclaimer"] == "false";

			var viewModel = new DynamicPageViewModel();

			var alternativePaths = new string[] {"/en/path", "/fr/path"};

			var pathLanguage = "EN";
			if (path.Length > 3)
				pathLanguage = path.Substring(1, 2);
			viewModel.Language = this.SetLanguageAsThePath(pathLanguage);

			this.PopulateDynamicPageViewModel(viewModel, alternativePaths);

			viewModel.PageDataLayer = new List<KeyValuePair<string, string>>();

			viewModel.Head = new Head
			{
				AlternateUrls = alternativePaths.ToList(),
				MetaDataTags = new List<string>(),
				PageHeadTags = new List<string>()
			};

			viewModel.Content = viewModel.Language == "EN" ? "<h1>English CONTENT</h1>" : "<h1>FRENCH content</h1>";

			return View(viewModel);
		}

		protected string SetLanguageAsThePath(string path = null)
		{
			if (string.IsNullOrEmpty(path) || !this.IsPathContainingJustTheLanguage(path.ToUpperInvariant()))
				return (string)null;
			path = path.ToUpperInvariant();
			if (path != this.UserLanguage)
				this.HttpContext.Response.Cookies.Append("Language", path, this.GetCookieOptions(this.HttpContext));
			return path;
		}

		protected bool IsPathContainingJustTheLanguage(string path)
		{
			var languages = new string[] {"EN", "FR"};
			return languages.Contains(path);
		}

		public string UserLanguage
		{
			get
			{
				if (this.Request.Cookies.ContainsKey("Language"))
					return this.Request.Cookies["Language"];
				return (string)null;
			}
			set
			{
				this.UserLanguage = value;
			}
		}

		public string UserCountry
		{
			get
			{
				if (this.Request.Cookies.ContainsKey("Country"))
					return JsonConvert.DeserializeObject<LocationCookie>(this.Request.Cookies["Country"]).Value;
				return (string)null;
			}
			set
			{
				this.UserCountry = value;
			}
		}

		public string UserNearestAirport
		{
			get
			{
				if (this.Request.Cookies.ContainsKey("Airport"))
					return JsonConvert.DeserializeObject<LocationCookie>(this.Request.Cookies["Airport"]).Value;
				return (string)null;
			}
			set
			{
				this.UserNearestAirport = value;
			}
		}

		public class LocationCookie
		{
			public LocationCookie(string secundary, string primary = null)
			{
				this.Primary = primary;
				this.Secundary = secundary;
			}

			public string Primary { get; set; }

			public string Secundary { get; set; }

			[JsonIgnore]
			public string Value
			{
				get
				{
					if (string.IsNullOrEmpty(this.Primary))
						return this.Secundary;
					return this.Primary;
				}
			}
		}

		public CookieOptions GetCookieOptions(HttpContext httpContext)
		{
			string str1 = "localhost";
			return new CookieOptions()
			{
				Domain = str1,
				Path = "/",
				Expires = DateTime.Now.AddDays(30.0)
			};
		}

		private void PopulateDynamicPageViewModel(DynamicPageViewModel viewModel, string[] alternativePaths)
		{
			this.SetDefaultConfig(viewModel, alternativePaths);
			viewModel.Header = new PortalHeader() {Lists = new HeaderSection[0], QuickLinks = new ExtendedLink[0]};
			viewModel.Footer = new Footer(new PortalFooter { Links = new FooterSection[0] }){ Locales = viewModel.Locales };
			viewModel.StaticContent = "static content";
			viewModel.Footer.StaticContent = viewModel.StaticContent;
		}

		protected void SetDefaultConfig(DynamicPageViewModel viewModel, string[] alternativePaths = null, string language = null)
		{
			viewModel.Language = viewModel.Language ?? language ?? this.UserLanguage ?? "EN";
			viewModel.Country = this.UserCountry ?? "BE";
			viewModel.Airport = this.UserNearestAirport ?? "BRU";
			viewModel.Config = "this is the config";
			viewModel.Locales = new string[] { "EN", "FR" };
			viewModel.Foot = new Foot()
			{
				Config = viewModel.Config,
				Locales = viewModel.Locales,
				Language = viewModel.Language
			};
		}
	}
}
