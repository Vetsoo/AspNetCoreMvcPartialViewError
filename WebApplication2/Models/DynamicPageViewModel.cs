using System.Collections.Generic;

namespace PartialViewsErrorTestApp.Models
{
	public class DynamicPageViewModel
	{
		/// <summary>
		/// Gets or sets the site map.
		/// </summary>
		public List<string> SiteMap { get; set; }

		/// <summary>
		/// Gets or sets the content.
		/// </summary>
		public string Content { get; set; }

		/// <summary>
		/// Gets or sets the static content.
		/// </summary>
		public string StaticContent { get; set; }

		/// <summary>
		/// Gets or sets the page data layer.
		/// </summary>
		public IList<KeyValuePair<string, string>> PageDataLayer { get; set; }

		public Footer Footer { get; set; }

		public PortalHeader Header { get; set; }

		public Foot Foot { get; set; }

		public string Language { get; set; }

		public string Country { get; set; }

		public string Airport { get; set; }

		public string Config { get; set; }

		public string[] Locales { get; set; }

		public Head Head { get; set; }
	}

	public class PortalFooter
	{
		public FooterSection[] Links { get; set; }

		public string CompanyInfo { get; set; }

		public string BottomLinks { get; set; }

		public string BookingFlowFooter { get; set; }

		public string CookieDisclaimerText { get; set; }

		public string AnchorLinkText { get; set; }
	}

	public class FooterSection
	{
		public string Title { get; set; }

		public Link[] Links { get; set; }
	}
	public class Link
	{
		public string Title { get; set; }

		public string InternalLink { get; set; }

		public string ExternalLink { get; set; }

		public bool NewWindow { get; set; }
	}

	public class Footer : PortalFooter
	{
		public Footer(PortalFooter portalFooter)
		{
			this.BookingFlowFooter = portalFooter.BookingFlowFooter;
			this.BottomLinks = portalFooter.BottomLinks;
			this.CompanyInfo = portalFooter.CompanyInfo;
			this.CookieDisclaimerText = portalFooter.CookieDisclaimerText;
			this.CompanyInfo = portalFooter.CompanyInfo;
			this.Links = portalFooter.Links;
			this.AnchorLinkText = portalFooter.AnchorLinkText;
		}

		public string[] Locales { get; set; }

		public string StaticContent { get; set; }
	}

	public class PortalHeader
	{
		public HeaderSection[] Lists { get; set; }

		public ExtendedLink[] QuickLinks { get; set; }
	}

	public class HeaderSection
	{
		public string Title { get; set; }

		public string Icon { get; set; }

		public Link[] List { get; set; }
	}

	public class ExtendedLink : Link
	{
		public string Icon { get; set; }
	}

	public class Foot
	{
		public string[] JavascriptImportTags { get; set; }

		public string Language { get; set; }

		public string[] Locales { get; set; }

		public string Config { get; set; }
	}

	public class Head
	{
		public List<string> AlternateUrls { get; set; }

		public List<string> MetaDataTags { get; set; }

		public string[] CSSImportTags { get; set; }

		public string[] JavascriptPreloadImportTags { get; set; }

		public List<string> PageHeadTags { get; set; }
	}
}
